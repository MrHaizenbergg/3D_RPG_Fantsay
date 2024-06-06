using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap1 : MonoBehaviour
{
    private PlayerController player;
    private TriggerEvents triggerEvents;
    public Animator animator;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        triggerEvents=GetComponent<TriggerEvents>();                      
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Interact" && !triggerEvents.activated) 
        {
            animator.SetTrigger("TrapAttacked");
            Invoke("DisTrapItem", 1.5f);
            Debug.Log("ItemTrap");
        }
    }

    private void DisTrapItem()
    {
        gameObject.SetActive(false);
    }

    public void DamageLegs()
    {
        player.PlayerLegsBreak();
    }
}
