using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Prefs")]
    public float walkSpeed = 1f;
    public float sprintSpeed = 1.5f;
    public float moveSmoothness = 10f;
    public float dashForce = 3f;
    float moveSpeed;
    Vector2 movementInput;
    Vector2 prevMotion = new(0,-1);
    bool isMoving;
    public bool isSprinting = false;
    [Header("Other")]
    public SwordAttack swordAttack;
    public Rigidbody2D body;
    public Animator animator;
    public ParticleSystem dustParticles;

    void Start(){
        moveSpeed = walkSpeed;
    }
    private void FixedUpdate(){
        
        body.velocity = Vector2.MoveTowards(body.velocity, movementInput*moveSpeed, Time.deltaTime*moveSpeed*moveSmoothness);
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
    void OnSprintEnabled(){
        moveSpeed = sprintSpeed;
        dustParticles.Play();

    }
    void OnSprintDisabled(){
        moveSpeed = walkSpeed;
        dustParticles.Stop();

    }
    void OnFire(){
        animator.SetTrigger("Attack");
    }

    void OnDash(){
        body.velocity += movementInput*dashForce;
    }

}
