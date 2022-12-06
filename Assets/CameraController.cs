using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] Transform followTarget;
    [SerializeField] float maxDist = 2, chaseSpeed = 2;

    [SerializeField] float currentXDist;

    void Start(){
        if(followTarget == null){
            followTarget = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }

    void Update(){
        FollowMovement();
    }

    void FollowMovement(){
        currentXDist = Mathf.Abs(transform.position.x - followTarget.position.x);
        if ( currentXDist >= maxDist ){
            transform.position = new Vector3(Mathf.Lerp(transform.position.x, followTarget.position.x, chaseSpeed * Time.deltaTime), transform.position.y, transform.position.z);
        }
    }
}
