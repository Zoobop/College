using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    #region Fields and Properties

    private NavMeshAgent navMeshAgent;
    private PlayerMovement playerMovement;
    private PlayerCombatSystem playerCombatSystem;
    private EntityCamera entityCamera;

    private Animator animator;

    private StateMachine stateMachine;

    #endregion

    #region Player State Machine

    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        playerMovement = GetComponent<PlayerMovement>();
        playerCombatSystem = GetComponentInChildren<PlayerCombatSystem>();
        entityCamera = GetComponentInChildren<EntityCamera>();

        animator = GetComponentInChildren<Animator>();

        stateMachine = new StateMachine();

        /** STATES **/

        var startUp = new StartUp();
        var followLeader = new FollowLeader(navMeshAgent, playerMovement, entityCamera);
        var partyLeader = new PartyLeader(navMeshAgent, playerMovement, playerCombatSystem, entityCamera);
        var inactiveMember = new InactiveMember(gameObject);

        /** TRANSITIONS **/

        void Transition(IState from, IState to, Func<bool> condition) => stateMachine.AddTransition(from, to, condition);
        //void Any(IState to, Func<bool> condition) => stateMachine.AddAnyTransition(to, condition);

        Transition(startUp, inactiveMember, PartyIsFinalizedAndIsInactive());
        Transition(startUp, followLeader, PartyIsFinalizedAndIsFollower());
        Transition(startUp, partyLeader, PartyIsFinalizedAndIsPartyLeader());
        Transition(inactiveMember, followLeader, IsCurrentMember());
        Transition(followLeader, inactiveMember, IsInactiveMember());
        Transition(followLeader, partyLeader, IsPartyLeader());
        Transition(partyLeader, inactiveMember, IsInactiveMember());
        Transition(partyLeader, followLeader, IsFollower());

        /** CONDITIONS **/

        Func<bool> PartyIsFinalizedAndIsInactive() => () => PlayerParty.Active != null && !PlayerParty.Party.Contains(gameObject);
        Func<bool> PartyIsFinalizedAndIsFollower() => () => PlayerParty.Active != null && PlayerParty.Active != gameObject;
        Func<bool> PartyIsFinalizedAndIsPartyLeader() => () => PlayerParty.Active != null && PlayerParty.Active == gameObject;
        Func<bool> IsCurrentMember() => () => PlayerParty.Party.Contains(gameObject);
        Func<bool> IsInactiveMember() => () => !PlayerParty.Party.Contains(gameObject);
        Func<bool> IsFollower() => () => PlayerParty.Active != gameObject;
        Func<bool> IsPartyLeader() => () => PlayerParty.Active == gameObject;

        /** STATE MACHINE STARTUP **/

        stateMachine.SetState(startUp);
    }

    private void Update() => stateMachine.Tick();

    #endregion
}