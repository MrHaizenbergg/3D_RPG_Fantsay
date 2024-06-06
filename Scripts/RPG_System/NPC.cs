using UnityEngine;

public class NPC : Interactable
{
    //DialogSystem

    public override void Interact()
    {
        base.Interact();
        Debug.Log("Trade");
    }
}
