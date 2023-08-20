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
    public float rollForce = 5f;
    float moveSpeed;
    Vector2 movementInput;
    Vector2 prevMotion = new(0,-1);
    public bool isSprinting = false;
    [Header("Other")]
    public GameObject dashEcho;
    private SwordAttack swordAttack;
    private Rigidbody2D body;
    private Animator animator;
    private ParticleSystem dustParticles;
    private TrailRenderer trail;
    private SpriteRenderer spriteRenderer;

    void Start(){
        swordAttack = GetComponentInChildren<SwordAttack>();
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        dustParticles = GetComponentInChildren<ParticleSystem>();
        trail = GetComponentInChildren<TrailRenderer>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        moveSpeed = walkSpeed;
    }
    private void FixedUpdate(){
        body.velocity = Vector2.MoveTowards(body.velocity, movementInput*moveSpeed, Time.deltaTime*moveSpeed*moveSmoothness);
        Animate();
        if(body.velocity.magnitude <= 1.5f || isSprinting){
            trail.emitting = false;
        }
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
        isSprinting = true;
    }
    void OnSprintDisabled(){
        moveSpeed = walkSpeed;
        dustParticles.Stop();
        isSprinting = false;
    }
    void OnFire(){
        animator.SetTrigger("Attack");
    }

    void OnDash(){
        //
        Vector2 direction = movementInput;
        List<RaycastHit2D> results = new List<RaycastHit2D>();
        ContactFilter2D contactFilter2D = new ContactFilter2D();
        contactFilter2D.SetLayerMask(1);
        
        for (int i = 0; i < 5; i++)
        {
            int count = body.Cast(
            direction,
            contactFilter2D,
            results,
            dashForce/5
            );
            if (count == 0){
                body.position += direction*dashForce/5;
                GameObject echo = Instantiate(dashEcho, body.position, body.transform.rotation);
                echo.GetComponent<SpriteRenderer>().sprite = spriteRenderer.sprite;
                echo.GetComponent<SpriteRenderer>().color = Color.Lerp(Color.Lerp(Color.cyan, Color.magenta, .2f*i), Color.gray, .5f);
            }
        }
    }

    void OnRoll(){
        trail.emitting = true;
        body.velocity += movementInput*rollForce;
    }

}
