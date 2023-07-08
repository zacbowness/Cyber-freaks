using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health = 100;
    public bool facingRight;
    public Rigidbody2D body;
    public float moveSpeed = 0.5f;
    public int Health{
        set{
            health = value;
            if(health <= 0){
                animator.SetTrigger("Killed");
            }
        }
        get{
            return health;
        }
    }
    public Animator animator;
    public GameObject target;
    public enum State{
        Idle, Hostile
    }
    public State currentState;

    void FixedUpdate(){
    }
    public void TakeDamage(int amount){
        animator.SetTrigger("Hurt");
        Health -= amount;
    }

    void Delete(){
        Destroy(gameObject);
    }
}
