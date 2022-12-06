using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private float xMoveInput = 0, yMoveInput = 0;
    MovementController movementController;

    void Start(){
        if(movementController == null){
            movementController = GetComponent<MovementController>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        // move
        xMoveInput = Input.GetAxisRaw("Horizontal");
        yMoveInput = Input.GetAxisRaw("Vertical");
        movementController.SetMovement(xMoveInput, yMoveInput);


        // attack
        if (Input.GetKeyDown(KeyCode.Space)){
            movementController.Attack();
        }

        // dash attack
        if (Input.GetKey(KeyCode.Space)){
            movementController.DashAttack();
        }
    }
}
