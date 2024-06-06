using UnityEngine;

[CreateAssetMenu(fileName ="New Item",menuName ="Inventory/Item")]
public class ItemRPG : ScriptableObject
{
    new public string name = "New Item";
    public Sprite icon = null;
    public bool isDefaultItem=false;

    public virtual void Use()
    {
        Debug.Log("Use: " + name);
    }

    public void RemoveFromInventory()
    {
        InventoryRPG.instance.Remove(this);
    }
}
