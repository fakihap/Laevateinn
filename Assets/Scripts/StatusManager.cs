using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusManager : MonoBehaviour
{
    [Header("Status")]
    [SerializeField] int currentHealth = 0;
    [SerializeField] int maxHealth = 100;

    [Header("Battle Properties")]
    [SerializeField] private CapsuleCollider2D col;

    void Start(){
        if(col == null){
            col = GetComponent<CapsuleCollider2D>();
        }

        currentHealth = maxHealth;
    }

    void Update(){
        if(currentHealth <= 0){
            Debug.LogWarning(transform.name + " dies");
        }
    }


    // be careful, this componenet is also applied on player
    void OnTriggerEnter2D(Collider2D otherCol){
        if(otherCol.CompareTag("Laevateinn")){
            currentHealth -= 50;
        }
    }
}
