using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private Animator anim;
    public float attackTime;
    public float startTimeAttack;

    public Transform attackLocation;
    public float attackRange;
    public LayerMask enemies;
    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    void FixedUpdate()
    {
        if (Input.GetKeyDown("space")) {
            attackTime = startTimeAttack;
            anim.SetBool("PlayerAttacking", true);
            Collider2D[] damage = Physics2D.OverlapCircleAll( attackLocation.position, attackRange, enemies );
        } 
        if (attackTime > 0) {
        attackTime -= Time.deltaTime;
        }
        if (anim.GetBool("PlayerAttacking") == true){
            PlayerController.stopMovement();
        }
        if (attackTime <= 0){
            attackTime = 0;
            anim.SetBool("PlayerAttacking", false);
            PlayerController.startMovement();
        
            // for (int i = 0; i < damage.Length; i++){
            //         Destroy( damage[i].gameObject );
            //     }
            // }
        }
    }
        private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackLocation.position, attackRange);
    }
}