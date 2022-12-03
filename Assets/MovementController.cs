using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
public class MovementController : MonoBehaviour
{
    [Header("Debug")]
    [SerializeField] bool debugMode = false;

    [Header("Properties")]
    [SerializeField] private Rigidbody2D rb;
    [Range(0, 5)] private float xMoveSpeed = 1f , yMoveSpeed = 1f;
    [Range(0.1f, 5f)] private float onGroundRaycastLength = 1f;
    [SerializeField] LayerMask groundMask;


    [Header("In-Game Properties")]
    [SerializeField] private float currentXMoveSpeed, currentYMoveSpeed;
    [SerializeField] private bool onGround = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }


    void Move(){
        currentXMoveSpeed = Input.GetAxisRaw("Horizontal") * xMoveSpeed;
        currentYMoveSpeed = Input.GetAxisRaw("Vertical") * yMoveSpeed;

        if ( Mathf.Abs(currentXMoveSpeed) > 0 ) {
            rb.AddForce(new Vector2(currentXMoveSpeed, rb.velocity.y));
        }

        if ( canJump() && Mathf.Abs(currentYMoveSpeed) > 0 ) {
            rb.velocity = new Vector2(rb.velocity.x, 0); // set y force to 0, avoiding unwanted behavior
            rb.AddForce(new Vector2(0, yMoveSpeed), ForceMode2D.Impulse);
        }
    }


    bool canJump(){
        if ( debugMode ) Debug.DrawLine(transform.position, transform.position + Vector3.down * onGroundRaycastLength, Color.red);
        return Physics2D.Raycast(transform.position, Vector2.down, onGroundRaycastLength, groundMask);
    }
}
