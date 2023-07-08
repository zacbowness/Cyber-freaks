using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetingRange : MonoBehaviour
{
    GameObject target;
    void Start(){
        target = GetComponentInParent<Enemy>().target;
    }
    
    private void OnTriggerEnter2D(Collider2D other){
        GameObject otherObject = other.gameObject;
        if (otherObject.Equals(target)){
            Enemy parent = GetComponentInParent<Enemy>();
            parent.currentState = Enemy.State.Hostile;
        }  
        
    }

    private void OnTriggerExit2D(Collider2D other){
        GameObject otherObject = other.gameObject;
        if (otherObject.Equals(target)){
            Enemy parent = GetComponentInParent<Enemy>();
            parent.currentState = Enemy.State.Idle;
        }  
    }
}
