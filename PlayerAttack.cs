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
    public bool playerAttacking;
    private void Start()
    {
        attackTime = 0;
        anim = GetComponent<Animator>();
    }
    void FixedUpdate()
    {
        if (Input.GetKeyDown("space")) {
            attackTime = startTimeAttack;
            anim.SetBool("PlayerAttacking", true);

            // playerAttacking = true;
            // Collider2D[] damage = Physics2D.OverlapCircleAll( attackLocation.position, attackRange, enemies );
        } 
        if (attackTime > 0) {
        attackTime -= Time.deltaTime;
        PlayerController.stopMovement();

        }
        // if (anim.GetBool("PlayerAttacking") == true){
        // if (playerAttacking == true){
        //     PlayerController.stopMovement();
        // }
        if (attackTime < 0){
            attackTime = 0;
            // playerAttacking = false;
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