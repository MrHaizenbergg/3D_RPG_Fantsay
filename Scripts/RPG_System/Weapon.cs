using System;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private CharAnimEventReciever animEventReciever;
    [SerializeField] private PlayerStats myStats;
    [SerializeField] private Collider boxCollider;

    private void Start()
    {
        if (animEventReciever == null)
            animEventReciever = PlayerManager.instance.player.GetComponentInChildren<CharAnimEventReciever>();
        if (myStats == null)
            myStats = PlayerManager.instance.player.GetComponent<PlayerStats>();

        animEventReciever.CanAttackEvent += EnableWeaponCollider;
        animEventReciever.NotCanAttackEvent += DisableWeaponCollider;
    }

    private void OnDisable()
    {
        animEventReciever.CanAttackEvent -= EnableWeaponCollider;
        animEventReciever.NotCanAttackEvent -= DisableWeaponCollider;
    }

    private void EnableWeaponCollider(object sender,EventArgs e)
    {
        boxCollider.enabled = true;
    }

    private void DisableWeaponCollider(object sender, EventArgs e)
    {
        boxCollider.enabled = false;
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Enemy")
        {
            CharacterStats enemy = col.GetComponent<CharacterStats>();

            if (enemy != null)
            {
                enemy.GetComponent<CharacterStats>().TakeDamage(myStats.damage.GetValue());
            }
        }
    }
}
