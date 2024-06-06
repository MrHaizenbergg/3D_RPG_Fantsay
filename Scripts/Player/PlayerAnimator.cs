using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : CharacterAnimator
{
    public WeaponAnimations[] weaponAnimations;
    Dictionary<Equipment, AnimationClip[]> weaponAnimationsDict;

    public Animation cameraAnimations;
    public EquipmentManager equipmentManager;

    [Tooltip("Camera attack animation name")]
    public string cameraAttackAnimName;

    protected override void Start()
    {
        equipmentManager = EquipmentManager.Instance;
        base.Start();

        equipmentManager.onEquipmentChanged += OnEquipmentChanged;

        weaponAnimationsDict = new Dictionary<Equipment, AnimationClip[]>();
        foreach (WeaponAnimations a in weaponAnimations)
        {
            weaponAnimationsDict.Add(a.weapon, a.clips);
        }
    }

    protected override void Update()
    {
        float speedPercent = playerController.characterController.velocity.magnitude / playerController.walkSpeed / 1.5f;
        animator.SetFloat("speedPercent", speedPercent, 0.1f, Time.deltaTime);

        if (equipmentManager.weaponTwoHand)
        {
            animator.SetBool("twoHand", true);
        }
        else
        {
            animator.SetBool("twoHand", false);
            animator.SetBool("InCombat", combat.inCombat);
            animator.SetBool("shieldUP", combat.inBlock);
        }
    }

    public override void OnAttack()
    {
        cameraAnimations.Play(cameraAttackAnimName);
        base.OnAttack();
        Debug.Log("OnAttackOverride");
    }

    void OnEquipmentChanged(Equipment newItem, Equipment oldItem)
    {
        if (newItem != null && newItem.equipSlot == EquipmentSlot.Weapon)
        {
            animator.SetLayerWeight(1, 1);
            if (weaponAnimationsDict.ContainsKey(newItem))
            {
                currentAttackAnimSet = weaponAnimationsDict[newItem];
            }
        }
        else if (newItem == null && oldItem != null && oldItem.equipSlot == EquipmentSlot.Weapon)
        {
            animator.SetLayerWeight(1, 0);
            currentAttackAnimSet = defaultAttackAnimSet;
        }

        if (newItem != null && newItem.equipSlot == EquipmentSlot.Shield)
        {
            animator.SetLayerWeight(2, 1);
        }
        else if (newItem == null && oldItem != null && oldItem.equipSlot == EquipmentSlot.Shield)
        {
            animator.SetLayerWeight(2, 0);
        }
    }

    [System.Serializable]
    public struct WeaponAnimations
    {
        public Equipment weapon;
        public AnimationClip[] clips;
    }

}
