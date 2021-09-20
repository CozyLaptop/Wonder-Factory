using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float baseSpeed = 1.0f;
    public static Vector3 movementDirection;
    public static float movementSpeed;
    private float horizontalMovement;
    private float verticalMovement;

    public static Rigidbody rb;
    private Animator anim;
    private bool playerMoving;
    private Vector3 lastMove;
    // Start is called before the first frame update
    void Start(){
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();

    }
    void Update(){
        horizontalMovement = Input.GetAxisRaw("Horizontal");
        verticalMovement = Input.GetAxisRaw("Vertical");
        anim.SetFloat("MoveX", Input.GetAxisRaw("Horizontal"));
        anim.SetFloat("MoveY", Input.GetAxisRaw("Vertical"));
        anim.SetBool("PlayerMoving", playerMoving);
        anim.SetFloat("LastMoveX", lastMove.x);
        anim.SetFloat("LastMoveY", lastMove.z);
        movementDirection = new Vector3(horizontalMovement, 0, verticalMovement);
        movementSpeed = Mathf.Clamp(movementDirection.magnitude, 0.0f, 1.0f);
        movementDirection.Normalize();

        if (anim.GetBool("PlayerAttacking") == false){
            if (horizontalMovement > 0.5f || horizontalMovement < -0.5f){
                playerMoving = true;
                lastMove = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f);
                rb.velocity = movementDirection * movementSpeed * baseSpeed;
            }
            if (verticalMovement > 0.5f || verticalMovement < -0.5f){
                playerMoving = true;
                lastMove = new Vector3(0f, 0f, Input.GetAxisRaw("Vertical"));
                rb.velocity = movementDirection * movementSpeed * baseSpeed;
            }
            if (verticalMovement == 0 && horizontalMovement == 0){
                playerMoving = false;
            }
        }
        if (playerMoving == false || anim.GetBool("PlayerAttacking")){
            rb.velocity = new Vector3(0, 0, 0); 
        };

    
    }
}