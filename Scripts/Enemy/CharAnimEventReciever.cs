using System;
using UnityEngine;

public class CharAnimEventReciever : MonoBehaviour
{
    public CharacterCombat combat;
    [SerializeField] private EnemyArcherCombat archerCombat;
    public bool canAttack;

    public event EventHandler CanAttackEvent;
    public event EventHandler NotCanAttackEvent;

    public void AttackHitEventArcher()
    {
        if (archerCombat != null)
        {
            StartCoroutine(archerCombat.shoot());
            Debug.Log("ArcherHitEvent");
        }
    }

    public void ArrowOn()
    {
        archerCombat.animationArrow.SetActive(true);
    }

    public void ArrowOff()
    {
        archerCombat.animationArrow.SetActive(false);
    }

    public void AttackHitEvent()
    {
        combat.AttackHit_AnimationEvent();
    }
    public void CanAttackOn()
    {
        CanAttackEvent?.Invoke(this, EventArgs.Empty);
    }
    public void CanAttackOff()
    {
        NotCanAttackEvent?.Invoke(this, EventArgs.Empty);
    }
}
