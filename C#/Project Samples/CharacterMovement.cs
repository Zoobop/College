using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public abstract class CharacterMovement : MonoBehaviour
{
    #region Fields and Properties

    [SerializeField] protected float defaultMovementSpeed = 4f;
    [SerializeField] protected float turnSpeed = 500f;

    // NavMesh
    protected NavMeshAgent navMeshAgent;
    protected Interactable target;
    protected float targetStopDistance;

    // Coroutines
    protected WaitForSeconds defaultTickTime = new WaitForSeconds(.2f);
    protected Coroutine moveToTarget;
    protected Coroutine moveToPoint;
    protected Coroutine rotateToTarget;

    public Interactable Target { get => target; protected set => target = value; }

    public float DefaultMovementSpeed => defaultMovementSpeed;
    public float TurnSpeed => turnSpeed;

    #endregion

    #region Unity Functions

    protected virtual void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();

        navMeshAgent.speed = defaultMovementSpeed;
        navMeshAgent.angularSpeed = turnSpeed;
    }

    #endregion

    #region Base Virtual/Abstract Functions

    public virtual void SetTarget(Interactable target)
    {
        Target = target;

        MoveToTarget();
    }

    public virtual Coroutine RotateToTarget(Vector3 targetPosition) => rotateToTarget = StartCoroutine(nameof(Rotate), targetPosition);

    public virtual void MoveToPoint(Vector3 point)
    {
        if (rotateToTarget != null)
            StopCoroutine(nameof(Rotate));

        if (moveToPoint != null)
            StopCoroutine(nameof(Move));
        
        if (moveToTarget != null)
            StopCoroutine(nameof(FollowTarget));

        moveToPoint = StartCoroutine(nameof(Move), point);
    }

    public virtual void MoveToTarget()
    {
        if (rotateToTarget != null)
            StopCoroutine(nameof(Rotate));

        if (moveToPoint != null)
            StopCoroutine(nameof(Move));

        if (moveToTarget != null)
            StopCoroutine(nameof(FollowTarget));

        moveToTarget = StartCoroutine(nameof(FollowTarget));
    }

    protected virtual IEnumerator Rotate(Vector3 target)
    {
        yield break;
    }

    protected virtual IEnumerator Move(Vector3 point)
    {
        yield break;
    }

    protected virtual IEnumerator FollowTarget()
    {
        yield break;
    }

    #endregion

    // Custom Editor Tools
    public abstract void SetMemberValues(EntityData.PhysicsInfo physicsInfo);
}
