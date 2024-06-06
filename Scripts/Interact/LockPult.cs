using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockPult : MonoBehaviour
{
    public ParticleSystem particle;
    public Animator anim;
    
    private void Awake()
    {
        anim = GetComponent<Animator>();       
    }

    public void ShowParticle()
    {
        particle.Play();
        anim.SetTrigger("ChickProvod");
    }
}
