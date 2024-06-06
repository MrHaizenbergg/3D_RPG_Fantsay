using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyArcherCombat : CharacterCombat
{
    //variables visible in the inspector
    public GameObject arrow;
    public Transform arrowSpawner;
    public GameObject animationArrow;

    public Transform target;
    //not visible in the inspector
    public bool shooting;
    private bool addArrowForce;
    private GameObject newArrow;
    private float shootingForce;
    public string attackTag;
    public bool spread;

    private Rigidbody rb;

    //public event System.Action OnAttackArcher;

    private int maxAlliesPerEnemy;

    protected override void Start()
    {
        base.Start();

        target = PlayerManager.instance.player.transform;

        if (GetComponent<Archer>() || gameObject.tag == "Enemy")
            spread = false;

        maxAlliesPerEnemy = 1;
    }

    protected override void Update()
    {
        base.Update();
    }

    void LateUpdate()
    {
        //check if the archer shoots an arrow
        if (addArrowForce && this.gameObject != null && target != null && newArrow != null && arrowSpawner != null)
        {
            //create a shootingforce
            shootingForce = Vector3.Distance(transform.position, target.transform.position);
            //add shooting force to the arrow
            rb = newArrow.GetComponent<Rigidbody>();

            rb.AddForce(transform.TransformDirection(new Vector3(0, shootingForce * 12 +
            ((target.transform.position.y - transform.position.y) * 40), shootingForce * 35)));

            addArrowForce = false;
        }
    }

    public IEnumerator shoot()
    {
        //archer is currently shooting
        shooting = true;

        //add a new arrow
        newArrow = Instantiate(arrow, arrowSpawner.position, arrowSpawner.rotation) as GameObject;
        newArrow.GetComponent<Arrow>().arrowOwner = this.gameObject;
        //shoot it using rigidbody addforce
        addArrowForce = true;

        //wait and set shooting back to false
        yield return new WaitForSeconds(0.5f);
        shooting = false;
    }
}
