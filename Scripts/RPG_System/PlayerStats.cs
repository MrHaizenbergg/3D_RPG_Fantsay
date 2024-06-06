
public class PlayerStats : CharacterStats
{
    public PlayerLevel playerLevel {  get; set; }

    private void Start()
    {
        playerLevel=GetComponent<PlayerLevel>();
        EquipmentManager.Instance.onEquipmentChanged += OnEquipmentChanged;
    }

    private void OnEquipmentChanged(Equipment newItem, Equipment oldItem)
    {
        if (oldItem != null)
        {
            armor.RemoveModifier(oldItem.armorModifier);
            damage.RemoveModifier(oldItem.damageModifier);
        }

        if (newItem != null)
        {
            armor.AddModifier(newItem.armorModifier);
            damage.AddModifier(newItem.damageModifier);
        }
    }

    public override void Die()
    {
        base.Die();
        //Kill the Player
        PlayerManager.instance.KillPlayer();
    }
}
