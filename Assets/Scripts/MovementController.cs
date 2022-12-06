using System.Collections;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
public abstract class MovementController : MonoBehaviour
{
    [Header("Debug")]
    [SerializeField] protected bool debugMode = false;

    [Header("Properties")]
    [SerializeField] public Rigidbody2D rb;
    [SerializeField][Range(0, 10)] protected float xMoveSpeed = 1f , yMoveSpeed = 1f;
    [SerializeField][Range(0.1f, 5f)] protected float onGroundRaycastLength = 1f;
    [SerializeField] protected LayerMask groundMask;
    [SerializeField] protected Animator playerAnimator;

    [Header("Input")]
    [SerializeField] protected float xMoveInput = 0;
    [SerializeField] protected float yMoveInput = 0;


    [Header("In-Game Properties")]
    [SerializeField] protected int faceDirection = 1;
    [SerializeField] public bool canMove = true;
    // [SerializeField] protected bool canJump;
    [SerializeField] protected float currentXMoveSpeed;
    [SerializeField] protected float currentYMoveSpeed;
    [SerializeField] protected bool onGround = false;


    protected void Move(){
        if(!canMove){
            return;
        }
        
        currentXMoveSpeed = xMoveInput * xMoveSpeed;
        currentYMoveSpeed = yMoveInput * yMoveSpeed;
        
        if ( Mathf.Abs(currentXMoveSpeed) > 0 ) {
            rb.velocity = new Vector2(currentXMoveSpeed, rb.velocity.y); // enforcing a set speed
        }

        if ( canJump() && Mathf.Abs(currentYMoveSpeed) > 0 ) {
            rb.velocity = new Vector2(rb.velocity.x, 0); // set y force to 0, avoiding unwanted behavior
            rb.AddForce(new Vector2(0, yMoveSpeed), ForceMode2D.Impulse);
        }
    }


    public abstract void Attack();

    bool canJump(){
        if ( debugMode ) Debug.DrawLine(transform.position, transform.position + Vector3.down * onGroundRaycastLength, Color.red);
        
        onGround = Physics2D.Raycast(transform.position, Vector2.down, onGroundRaycastLength, groundMask);

        return onGround;
    }

    public void SetMovement(float xValue, float yValue){
        xMoveInput = Mathf.Clamp(xValue, -1, 1);
        yMoveInput = Mathf.Clamp(yValue, -1, 1);

        if (xMoveInput != 0){
            faceDirection = (int)Mathf.Sign(xMoveInput); // care : enemy will also flip
        }
    }

    protected void UpdateFaceDirection(){
        transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x) * faceDirection, transform.localScale.y, transform.localScale.z);
    }
}
