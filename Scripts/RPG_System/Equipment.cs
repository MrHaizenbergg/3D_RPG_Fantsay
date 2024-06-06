using UnityEngine;

[CreateAssetMenu(fileName ="New Equipment",menuName ="Inventory/Equipment")]
public class Equipment : ItemRPG
{
    public EquipmentSlot equipSlot;
    public SkinnedMeshRenderer mesh;
    public EquipmentMeshRegion[] coveredMeshRegions;
    public GameObject itemObject;
    public bool twoHand;
    //public Mesh mesh;

    public int armorModifier;
    public int damageModifier;

    public override void Use()
    {
        base.Use();
        EquipmentManager.Instance.Equip(this);
        RemoveFromInventory();
    }
}

public enum EquipmentSlot { Head,Chest,Legs,Weapon,Shield,Feet}
public enum EquipmentMeshRegion {Head, Legs,Arms,Torso} //Correspond body blendshapes