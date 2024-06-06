using UnityEngine;

public class ItemPickUp : Interactable
{
    public ItemRPG item;

    public override void Interact()
    {
        base.Interact();
        PickUp();
    }

    private void PickUp()
    {
        Debug.Log("PickingUp item: " + item.name);
        bool wasPickedUp = InventoryRPG.instance.Add(item);

        if (wasPickedUp)
            Destroy(gameObject);
    }
}
