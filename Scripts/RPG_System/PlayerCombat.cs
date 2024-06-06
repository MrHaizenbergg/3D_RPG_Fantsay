using UnityEngine;

public class PlayerCombat : CharacterCombat
{
    EquipmentManager EquipmentManager;

    protected override void Start()
    {
        base.Start();      
        EquipmentManager = EquipmentManager.Instance;
    }

    protected override void Update()
    {
        base.Update();

        if (Input.GetButton("ShieldBlock") && EquipmentManager.shieldEquip)
        {
            ShieldBlock();
        }          
    }

    public override void Attack()
    {
       base.Attack();
       AttackHit_AnimationEvent();
    }

    public override void AttackHit_AnimationEvent()
    {
        base.AttackHit_AnimationEvent();
    }

    public override void ShieldBlock()
    {
        base.ShieldBlock();
    }
}
