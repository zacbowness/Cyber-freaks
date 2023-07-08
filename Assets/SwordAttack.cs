using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAttack : MonoBehaviour
{
   public Collider2D swordCollider;
   public int damage = 30;
   public void Attack(){
    swordCollider.enabled = true;
   }
   public void StopAttack(){
    swordCollider.enabled = false;
   }

   private void OnTriggerEnter2D(Collider2D other){
      if(other.tag == "Enemy"){
         Enemy enemy = other.GetComponent<Enemy>();
         enemy.TakeDamage(damage);
      }
   }

}
