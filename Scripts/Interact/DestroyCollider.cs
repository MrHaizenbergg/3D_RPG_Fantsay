using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyCollider : MonoBehaviour
{
    private BoxCollider boxcollider;
    private Rigidbody rb;

    private void Awake()
    {
        boxcollider = GetComponent<BoxCollider>();
        rb = GetComponent<Rigidbody>();
    }

    //public IEnumerator DeleteColl()
    //{
    //    yield return 
    //}

    public void DeleteColl(Vector3 vector)
    {
        rb.AddForce(vector, ForceMode.Impulse);
        StartCoroutine(OffGameObject());      
    }

    IEnumerator OffGameObject()
    {
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }
}
