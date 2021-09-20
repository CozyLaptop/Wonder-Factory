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
    private void Start(){
        attackTime = 0;
        anim = GetComponent<Animator>();
    }
    void Update(){
        if (Input.GetKeyDown("space")) {
            attackTime = startTimeAttack;
            anim.SetBool("PlayerAttacking", true);
        } 
        if (anim.GetBool("PlayerAttacking") == true){
        attackTime -= Time.deltaTime;
        Debug.Log("Attack time is: " + attackTime);
        }
        if (attackTime < 0){
            attackTime = 0;
            anim.SetBool("PlayerAttacking", false);
        }
    }
        private void OnDrawGizmosSelected(){
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackLocation.position, attackRange);
    }
}