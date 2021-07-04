using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class CombatSystem : MonoBehaviour
{
    #region Fields and Properties

    protected CharacterStats target;

    protected AbilitySystem abilitySystem;
    protected NavMeshAgent navMeshAgent;
    protected CharacterMovement characterMovement;

    protected int damageDealt;

    protected StateMachine combatStateMachine;

    public CharacterStats Target => target;
    public NavMeshAgent NavMeshAgent => navMeshAgent;

    public float AttackRange { get; protected set; } = 2f;
    public bool Selecting { get; protected set; } = false;
    public bool Attacking { get; protected set; } = false;
    public bool FinishedAttack { get; protected set; } = false;
    public bool InCombat { get; protected set; } = false;
    public bool IsActiveTurn { get; protected set; } = false;

    #endregion

    #region Unity Functions

    protected virtual void Awake()
    {
        abilitySystem = GetComponent<AbilitySystem>();
        navMeshAgent = GetComponentInParent<NavMeshAgent>();
        characterMovement = GetComponentInParent<CharacterMovement>();
    }

    protected virtual void Start() => CombatStateMachine();

    protected virtual void Update() => combatStateMachine.Tick();

    #endregion

    #region Entity Combat Accessibility
 
    public virtual void Attack() => StartCoroutine(nameof(CastRandomAbility));

    protected virtual IEnumerator CastRandomAbility()
    {
        var randomAbility = abilitySystem.Abilities[Random.Range(0, abilitySystem.Abilities.Count - 1)];

        yield return StartCoroutine(nameof(randomAbility.CastAttack));

        target.ModifyHealth(-damageDealt);
        Attacking = true;

        yield return new WaitForSeconds(1f);

        Attacking = false;
    }

    protected abstract void CombatStateMachine();

    public void CombatStatus(bool state) => InCombat = state;
    public void SelectionStatus(bool state) => Selecting = state;
    public void AttackStatus(bool state) => Attacking = state;
    public void AttackHasEnded(bool state) => FinishedAttack = state;
    public void TurnStatus(bool state) => IsActiveTurn = state;
    public Coroutine RotateToTarget(Vector3 position) => characterMovement.RotateToTarget(position);

    #endregion
}
