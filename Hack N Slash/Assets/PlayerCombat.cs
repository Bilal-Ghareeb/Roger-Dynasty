using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCombat : MonoBehaviour
{

    public Animator animator;
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;
    public int attackDamage = 1;
    public float attackRate = 2f;
    private float nextAttackTime = 0;

    private Demon demon;

   
    void Start()
    {
        animator = GetComponent<Animator>();
       
    }


    public void Fire(InputAction.CallbackContext context)
    {
        CameraShake.Instance.ShakeCamera(0.3f, 0.1f);

        if (Time.time >= nextAttackTime)
        {
            if (gameObject.tag == "Player" && animator.GetCurrentAnimatorStateInfo(0).IsName("Player_Idle"))
            {
                animator.SetInteger("Attack_Index", Random.Range(0, 2));
                animator.SetTrigger("Sword_Attack");
                

                nextAttackTime = Time.time + 1f / attackRate;

                Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

                foreach (Collider2D enemy in hitEnemies)
                {
                    enemy.GetComponent<Demon>().TakeDamge(attackDamage);
                }
                
            }

            if (gameObject.tag == "Player" && animator.GetCurrentAnimatorStateInfo(0).IsName("Player_Run"))
            {
                animator.SetInteger("Attack_Index", Random.Range(0, 2));
                animator.SetTrigger("Sword_Attack");
                

                nextAttackTime = Time.time + 1f / attackRate;

                Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

                foreach (Collider2D enemy in hitEnemies)
                {
                    enemy.GetComponent<Demon>().TakeDamge(attackDamage);
                    
                }
               
            }
        }
        
    }

    void OnDrawGizmosSelected()
    {
        if(attackPoint == null)
        {
            return;
        }

        Gizmos.DrawWireSphere(attackPoint.position , attackRange);
    }

}
