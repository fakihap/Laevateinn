using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    private float xMoveInput = 0, yMoveInput = 0;
    [SerializeField] EnemyMovement movementController;

    [SerializeField] GameObject player;

    [SerializeField] public float targetDistanceToPlayer = 3;
    [SerializeField] float currentDistanceToPlayer;
    [SerializeField] public float followThreshold = 2f;

    void Start(){
        if(movementController == null){
            movementController = GetComponent<EnemyMovement>();
        }

        if (player == null){
            player = GameObject.FindGameObjectWithTag("Player");
        }
    }
    void Update()
    {
        FollowPlayer();
    }

    void FollowPlayer(){
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
    }
}
