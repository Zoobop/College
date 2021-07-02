using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class Entity : Interactable
{
    #region Fields and Properties

    [SerializeField] protected EntityGeneralInfo generalInfo;

    public EntityGeneralInfo GeneralInfo { get => generalInfo; protected set => generalInfo = value; }

    #endregion

    #region Unity Functions

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Start()
    {
        name = generalInfo.name;
    }

    #endregion

    // Interactable implementation
    protected override Color ApplyTeamColor() => generalInfo.GetTeamColor();

    // Custom Editor Tools
    public abstract void TransferValues(EntityData.EntityInfo entityInfo);
}

public abstract class EntityLoadData
{
}