using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : Enemy
{
    bool movementLocked = true;
    void FixedUpdate(){
        animator.SetFloat("Horizontal", System.Convert.ToInt32(facingRight));
        switch(currentState){
            case State.Idle:
                animator.SetBool("isHostile", false);
                break;
            case State.Hostile:
                animator.SetBool("isHostile", true);
                Vector2 targetPos = target.GetComponent<Rigidbody2D>().position + new Vector2(0, -0.15f);
                Vector2 movementDir = (targetPos - body.position);
                facingRight = movementDir.x >= 0;
                if (!movementLocked && movementDir.magnitude>0.15f){
                    body.MovePosition(body.position + moveSpeed * Time.fixedDeltaTime * movementDir.normalized);
                }
                break;
        }
    }

    public void LockMovement(){
        movementLocked = true;
    }
    public void UnlockMovement(){
        movementLocked = false;
    }
}