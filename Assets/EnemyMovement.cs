using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MovementController
{
    void Start(){
        rb = GetComponent<Rigidbody2D>();
    }

    void Update(){
        Move();
        UpdateFaceDirection();
    }

    public override void Attack(){

    }
}
