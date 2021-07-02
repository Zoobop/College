using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CapsuleCollider))]
public abstract class Interactable : MonoBehaviour
{
    #region Fields and Properties

    [SerializeField] protected float interactRadius = 3.5f;
    [SerializeField] protected float radiusOffset = 1;

    protected CapsuleCollider capsuleCollider;
    protected SelectionShader selectionShader;

    public float InteractRadius => interactRadius;
    public float RadiusOffset => radiusOffset;
    public bool IsFocus { get; protected set; } = false;

    #endregion

    #region Unity Functions

    protected virtual void Awake()
    {
        capsuleCollider = GetComponent<CapsuleCollider>();
        selectionShader = GetComponentInChildren<SelectionShader>();
    }

    protected virtual void Start()
    {
        capsuleCollider.isTrigger = true;
        capsuleCollider.radius = interactRadius;
        capsuleCollider.height = interactRadius;
        capsuleCollider.center = new Vector3(capsuleCollider.center.x, capsuleCollider.center.y + radiusOffset, capsuleCollider.center.z);
    }

    #endregion

    #region Interact Functions

    public virtual void Highlight()
    {
        selectionShader.SetColor(ApplyTeamColor());
        selectionShader.EnableHighlight();
    }

    public void RemoveHighlight()
    {
        selectionShader.DisableHighlight();
    }

    public void SetIsFocus(bool state)
    {
        IsFocus = state;

        TryInteract(IsFocus);
    }

    public virtual void Interact()
    {
        // Interact
        Debug.Log("Interacting with " + name);
    }

    private void TryInteract(bool focus)
    {
        if (focus)
            Interact();
    }

    #endregion

    // Default
    protected virtual Color ApplyTeamColor() => Color.white;

    #region Override Functions

    private void OnDrawGizmosSelected()
    {
        Vector3 gizmoPos = new Vector3(transform.position.x, transform.position.y + radiusOffset, transform.position.z);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(gizmoPos, interactRadius);
    }

    #endregion
}
