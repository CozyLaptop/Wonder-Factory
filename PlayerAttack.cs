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
        } 
        // if (attackTime > 0) {
        if (anim.GetBool("PlayerAttacking") == true){
        attackTime -= Time.deltaTime;
        Debug.Log("Attack time is: " + attackTime);
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