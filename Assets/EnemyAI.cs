using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    private float xMoveInput = 0, yMoveInput = 0;
    MovementController movementController;

    [SerializeField] GameObject player;

    [SerializeField] float targetDistanceToPlayer = 3;
    [SerializeField] float currentDistanceToPlayer;
    [SerializeField] float followThreshold = 1.2f;

    void Start(){
        if(movementController == null){
            movementController = GetComponent<MovementController>();
        }

        if (player == null){
            player = GameObject.FindGameObjectWithTag("Player");
        }
    }

    // Update is called once per frame
    void Update()
    {
        currentDistanceToPlayer = transform.position.x - player.transform.position.x;
        // move
        if (Mathf.Abs(currentDistanceToPlayer) >= followThreshold * targetDistanceToPlayer){ // change this static value
            xMoveInput = -(currentDistanceToPlayer);
        } else if (Mathf.Abs(currentDistanceToPlayer) < targetDistanceToPlayer) {
            xMoveInput = (currentDistanceToPlayer);
        } else {
            xMoveInput = 0;
        }
        
        movementController.SetMovement(xMoveInput, yMoveInput);


        // attack
        // if (Input.GetKeyDown(KeyCode.Space)){
        //     movementController.Attack();
        // }

        // // dash attack
        // if (Input.GetKey(KeyCode.Space)){
        //     movementController.DashAttack();
        // }
    }
}
