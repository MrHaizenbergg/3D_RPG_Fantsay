using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap2 : MonoBehaviour
{
    public AudioSource audioSource;
    public EnemyCall caller;

    private void Awake()
    {
        caller = GetComponent<EnemyCall>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            ShakeRings();
            caller.CallEnemy();
            Debug.Log("TrapRings");
        }
    }

    public void ShakeRings()
    {
        audioSource.Play();
    }
}
