using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerParty : MonoBehaviour, IDataManagement
{
    #region Singleton

    public static PlayerParty Instance { get; private set; }

    void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    #endregion

    #region Fields, Properties, and Events

    [SerializeField] private List<GameObject> partyMembers;
    private GameObject previousActive;

    public static GameObject Active { get; private set; }
    public static List<GameObject> Party { get; private set; }

    public const int MAX_SIZE = 3;

    public static event Action<PlayerParty> OnPartyChanged = delegate { };
    public static event Action OnPlayerChanged = delegate { };

    #endregion

    #region Unity Functions

    private void OnEnable()
    {
        InitializeParty();
    }

    #endregion

    #region Active Player

    public void NextMember()
    {
        // Returns if there is only one member
        if (partyMembers.Count == 1)
            return;

        // Get next member index -- loops around
        int currentIndex = partyMembers.IndexOf(Active);

        if (currentIndex == partyMembers.Count - 1)
            currentIndex = 0;
        else
            currentIndex++;

        // Set member to new party leader and set camera
        previousActive = Active;
        Active = partyMembers[currentIndex];
        PrioritizeCamera();

        // Update UI through event invocation
        PlayerStats stats = Active.GetComponent<PlayerStats>();

        OnPlayerChanged?.Invoke();
        OnPartyChanged?.Invoke(this);
    }

    public void PreviousMember()
    {
        // Returns if there is only one member
        if (partyMembers.Count == 1)
            return;

        // Get previous member index -- loops around
        int currentIndex = partyMembers.IndexOf(Active);

        if (currentIndex == 0)
            currentIndex = partyMembers.Count - 1;
        else
            currentIndex--;

        // Set member to new party leader and set camera
        previousActive = Active;
        Active = partyMembers[currentIndex];
        PrioritizeCamera();

        // Update UI through event invocation
        PlayerStats stats = partyMembers[currentIndex].GetComponent<PlayerStats>();

        OnPlayerChanged?.Invoke();
        OnPartyChanged?.Invoke(this);
    }

    #endregion

    #region Party Modifiers
    public void JoinParty(GameObject member)
    {
        // Throw warning if member already in party
        if (partyMembers.Contains(member))
        {
            Debug.LogWarning("Member to be added already exists in the party.");
            return;
        }

        // Add member if within max party size constraint
        if (partyMembers.Count < MAX_SIZE)
        {
            partyMembers.Add(member);

            if (member.GetComponent<PartyMember>() == null)
                member.AddComponent<PartyMember>();

            member.GetComponent<PartyMember>().ActivateMember();

            // Enable camera
            member.transform.Find("EntityCam").gameObject.SetActive(true);

            // Debug
            Debug.Log($"{member.name} has joined the party!");
        }

        Party = partyMembers;
        PrioritizeCamera();

        OnPartyChanged?.Invoke(this);
    }

    public void LeaveParty(GameObject member)
    {
        // Returns if there is only one member
        if (partyMembers.Count == 1)
            return;

        member.GetComponent<PartyMember>().DeactivateMember();

        // Disable camera
        member.GetComponentInChildren<EntityCamera>().Deselect();
        member.GetComponentInChildren<EntityCamera>().gameObject.SetActive(false);

        partyMembers.Remove(member);

        Party = partyMembers;
        PrioritizeCamera();

        OnPartyChanged?.Invoke(this);
    }

    /// <summary>
    /// 
    /// Initializes the player party at the start of the game
    /// 
    /// </summary>
    private void InitializeParty()
    {
        // Creates a new Party List
        partyMembers = new List<GameObject>();

        Transform spawnPoint = SpawnPoint.pointToSpawn;

        // Loading data will go here
        GameObject player = Instantiate(Assets.Player);
        //GameObject partyMember2 = Instantiate(Assets.Jun);
        //GameObject partyMember3 = Instantiate(Assets.Togech);

        partyMembers.AddRange(new List<GameObject> { player });

        Active = partyMembers[0];
        previousActive = Active;
        Party = partyMembers;

        PrioritizeCamera();
    }

    private void SpawnParty()
    {
        for (var i = 0; i < partyMembers.Count; i++)
        {
            partyMembers[i] = Instantiate(partyMembers[i], transform);
        }
    }

    private void PrioritizeCamera()
    {
        previousActive.GetComponentInChildren<EntityCamera>().ChangeValues();

        for (var i = 0; i < partyMembers.Count; i++)
        {
            var cam = partyMembers[i].GetComponentInChildren<EntityCamera>();

            cam.Deselect();
            cam.ApplyChanges();
        }

        int activeIndex = partyMembers.IndexOf(Active);
        partyMembers[activeIndex].GetComponentInChildren<EntityCamera>().Select();
    }

    #endregion

    #region IDataManagement
    public void SaveData()
    {
        // Save Player
        FileManager.gameLoader.DATA_player = partyMembers[0].GetComponent<Player>().ToData();
        FileManager.gameLoader.DATA_playerInventory = partyMembers[0].GetComponent<Inventory>().ToData();

        // Save Party Data
        foreach (GameObject member in partyMembers)
        {
            // Save NPC Data
            if (member.TryGetComponent(out NPC npc))
                FileManager.gameLoader.DATA_partyMembers.Add(npc.ToData());

            // Transforms -- position and rotation
            FileManager.gameLoader.DATA_partyPosition.Add(member.transform.position);
            FileManager.gameLoader.DATA_partyRotation.Add(member.transform.rotation.eulerAngles);

            // Party Stats
            FileManager.gameLoader.DATA_playerPartyStats.Add(member.GetComponent<PlayerStats>().ToData());

            // Party Equipment
            FileManager.gameLoader.DATA_memberEquipment.Add(member.GetComponent<Equipment>().ToData());
        }

        Debug.Log("Player Party Successfully Saved!");
    }

    public void LoadData()
    {
        // No load data
        if (FileManager.gameLoader == null)
        {
            Debug.Log("No Player Party Data to load!");
            return;
        }

        // Ckeck if party is null
        if (partyMembers == null)
        {
            Debug.LogError("\'_partyMembers\' member is not instantiated!");
            return;
        }

        // Load Player
        partyMembers[0].GetComponent<Player>().SetMemberValues(FileManager.gameLoader.DATA_player);
        partyMembers[0].GetComponent<Inventory>().SetMemberValues(FileManager.gameLoader.DATA_playerInventory);

        // Load Party
        for (var i = 0; i < partyMembers.Count; i++)
        {
            // Check for NPC Data
            if (FileManager.gameLoader.DATA_partyMembers[i].GetType() == typeof(NPC))
                partyMembers[i].GetComponent<NPC>().SetMemberValues(FileManager.gameLoader.DATA_partyMembers[i]);

            // Transform -- position and rotation
            partyMembers[i].transform.position = FileManager.gameLoader.DATA_partyPosition[i];
            partyMembers[i].transform.rotation.SetLookRotation(FileManager.gameLoader.DATA_partyRotation[i]);

            // Party Stats
            partyMembers[i].GetComponent<PlayerStats>().SetMemberValues(FileManager.gameLoader.DATA_playerPartyStats[i]);

            // Party Equipment
            partyMembers[i].GetComponent<Equipment>().SetMemberValues(FileManager.gameLoader.DATA_memberEquipment[i]);
        }

        OnPartyChanged?.Invoke(this);
        Debug.Log("Player Party Successfully Loaded!");
    }
    #endregion
}
