using UnityEngine;
using UnityEngine.AI;

public class CharacterAnimator : MonoBehaviour
{
    public AnimationClip replacableAttackAnim;
    public AnimationClip[] defaultAttackAnimSet;
    protected AnimationClip[] currentAttackAnimSet;

    NavMeshAgent agent;
    protected Animator animator;
    protected CharacterCombat combat;
    public AnimatorOverrideController overrideController;
    protected PlayerController playerController;

    protected virtual void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        playerController = GetComponent<PlayerController>();
        animator = GetComponentInChildren<Animator>();
        combat = GetComponent<CharacterCombat>();
        GetComponent<CharacterStats>().OnGetHit += OnGetHit;

        if (overrideController == null)
        {
            overrideController = new AnimatorOverrideController(animator.runtimeAnimatorController);
        }


        animator.runtimeAnimatorController = overrideController;
        currentAttackAnimSet = defaultAttackAnimSet;
        combat.OnAttack += OnAttack;
        combat.OnBlock += OnShieldBlock;
    }

    protected virtual void Update()
    {
        float speedPercent = agent.velocity.magnitude / agent.speed/1.5f;     
        animator.SetFloat("speedPercent", speedPercent, 0.1f, Time.deltaTime);

        animator.SetBool("InCombat", combat.inCombat);
    }

    public virtual void OnAttack()
    {
        animator.SetTrigger("attack");
        int attackIndex = Random.Range(0, currentAttackAnimSet.Length);
        overrideController[replacableAttackAnim.name] = currentAttackAnimSet[attackIndex];
    }

    public virtual void OnGetHit()
    {
        animator.SetTrigger("hitReaction");
    }

    protected virtual void OnShieldBlock()
    {
        //Debug.Log("ShieldBlockEvent");
    }
}
