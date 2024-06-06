using UnityEngine;

public class Interactable : MonoBehaviour
{
    public float radius = 3f;
    public Transform interactionTransform;

    private bool isFocus = false;
    private bool hasInteracted = false;
    private Transform player;

    private float distance;

    public virtual void Interact()
    {
        //Debug.Log("Interacting with: " + transform.name);
    }

    public void OnFocused(Transform playerTransform)
    {
        isFocus = true;
        player = playerTransform;
        hasInteracted = false;
    }
    public void OnDefocused()
    {
        isFocus = false;
        player = null;
        hasInteracted = false;
    }

    protected virtual void Update()
    {
        if (isFocus && !hasInteracted)
        {
            //float distance = Vector3.Distance(player.position, interactionTransform.position);

            distance = ((player.position - interactionTransform.position).sqrMagnitude);

            if (distance <= radius)
            {
                Interact();
                //Debug.Log("INTERACT");
                hasInteracted = true;
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (interactionTransform == null)
            interactionTransform = transform;

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(interactionTransform.position, radius);
    }
}
