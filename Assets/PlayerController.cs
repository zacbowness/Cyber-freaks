using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 1f;
    Vector2 movementInput;
    Vector2 prevMotion = new Vector2(0,-1);
    bool isMoving;
    public SwordAttack swordAttack;
    public Rigidbody2D body;
    public Animator animator;
    public GameObject DustParticles;

    private void FixedUpdate(){
        body.MovePosition(body.position + movementInput * moveSpeed * Time.fixedDeltaTime);
        Animate();
        
    }

    private void Animate(){
        if (movementInput.magnitude <= .2f ){
            animator.SetFloat("Horizontal", prevMotion.x * .2f);
            animator.SetFloat("Vertical", prevMotion.y* .2f);
        }else {
            animator.SetFloat("Horizontal", movementInput.x);
            animator.SetFloat("Vertical", movementInput.y);
            prevMotion = movementInput;
        }
    }

    public void SwordAttack(){
        swordAttack.Attack();
    }
    public void EndSwordAttack(){
        swordAttack.StopAttack();
    }

    void OnMove(InputValue movementValue){
        movementInput = movementValue.Get<Vector2>();
    }
    void OnFire(){
        animator.SetTrigger("Attack");
    }

    

}
