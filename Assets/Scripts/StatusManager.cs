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

    [SerializeField] private MovementController movementController;

    [Header("Impact Effect")]
    [SerializeField] float damageImpactAmount = 2f;

    void Start(){
        if(col == null){
            col = GetComponent<CapsuleCollider2D>();
        }

        if(movementController == null){
            movementController = GetComponent<MovementController>();
        }

        currentHealth = maxHealth;
    }

    void Update(){
        if(currentHealth <= 0){
            Debug.LogWarning(transform.name + " dies");
            Destroy(transform.gameObject);
        }
    }


    // be careful, this componenet is also applied on player
    void OnTriggerEnter2D(Collider2D otherCol){
        if(otherCol.CompareTag("Laevateinn")){
            StartCoroutine(executeImpactEffect(otherCol));
        }
    }

    IEnumerator executeImpactEffect(Collider2D otherCol){
        currentHealth -= 50;
        movementController.canMove = false;
        movementController.rb.AddForce(Vector2.right * Mathf.Sign(transform.position.x - otherCol.transform.position.x) * damageImpactAmount, ForceMode2D.Impulse);
        print(otherCol.name);
        yield return new WaitForSeconds(1f); //stun amount
        movementController.canMove = true;
    }
}
