using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MovementController
{
    [Header("Attack Action")]
    [SerializeField] protected GameObject laevateinn;
    [SerializeField] protected int attackState = 0;
    [SerializeField] protected float timeSpendHold = 0;
    [SerializeField][Range(0, 1f)] protected float dashAttackThreshold = .3f;
    [SerializeField] protected bool hasDashAttacked = false;
    [SerializeField][Range(0, 40f)] protected float dashAmount = 20f;
    [SerializeField] protected float dashTime = .7f;
    // Start is called before the first frame update
    
    void Update()
    {
        Move();
        UpdateFaceDirection();
    }

    public override void Attack(){
        attackState++;

        playerAnimator.SetInteger("attackState", attackState);

        if (attackState >= 3) {
            attackState = 0;
        }

        hasDashAttacked = false;
    }
    
    public void DashAttack(){
        if (hasDashAttacked) return;

        timeSpendHold += Time.deltaTime; //todo not set into 0

        if(timeSpendHold >= dashAttackThreshold){
            timeSpendHold = 0;
            hasDashAttacked = true; //cooldown

            //execute dash attack
            // todo: not in the first slash
            StartCoroutine(executeDashAttack());
        }
    }

    IEnumerator executeDashAttack(){
        canMove = false;

        // rb.AddForce(dashAmount * Vector2.right, ForceMode2D.Impulse);
        rb.velocity = new Vector2(dashAmount * faceDirection, rb.velocity.y);
        playerAnimator.SetTrigger("dashAttack");

        yield return new WaitForSeconds(dashTime);

        rb.velocity = new Vector2(0, rb.velocity.y);
        
        
        playerAnimator.SetTrigger("endDashAttack");
        canMove = true;
    }
}
