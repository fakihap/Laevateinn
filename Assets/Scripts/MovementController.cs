using System.Collections;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
public class MovementController : MonoBehaviour
{
    #region properties
    [Header("Debug")]
    [SerializeField] bool debugMode = false;

    [Header("Properties")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField][Range(0, 10)] private float xMoveSpeed = 1f , yMoveSpeed = 1f;
    [SerializeField][Range(0.1f, 5f)] private float onGroundRaycastLength = 1f;
    [SerializeField] LayerMask groundMask;
    [SerializeField] Animator playerAnimator;

    [Header("Input")]
    [SerializeField] private float xMoveInput = 0;
    [SerializeField] private float yMoveInput = 0;


    [Header("In-Game Properties")]
    [SerializeField] private bool canMove = true;
    // [SerializeField] private bool canJump;
    [SerializeField] private float currentXMoveSpeed;
    [SerializeField] private float currentYMoveSpeed;
    [SerializeField] private bool onGround = false;

    [Header("Attack Action")]
    [SerializeField] private GameObject laevateinn;
    [SerializeField] private int attackState = 0;
    [SerializeField] private float timeSpendHold = 0;
    [SerializeField][Range(0, 1f)] private float dashAttackThreshold = .3f;
    [SerializeField] private bool hasDashAttacked = false;
    [SerializeField][Range(0, 40f)] private float dashAmount = 20f;
    [SerializeField] private float dashTime = .7f;

    #endregion

    // Start is called before the first frame update
    void Start()
    {
    
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        // LocomoteAttack();
    }


    void Move(){
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


    bool canJump(){
        if ( debugMode ) Debug.DrawLine(transform.position, transform.position + Vector3.down * onGroundRaycastLength, Color.red);
        
        onGround = Physics2D.Raycast(transform.position, Vector2.down, onGroundRaycastLength, groundMask);

        return onGround;
    }

    public void Attack(){
        attackState++;

        playerAnimator.SetInteger("attackState", attackState);

        if (attackState >= 3) {
            attackState = 0;
        }

        hasDashAttacked = false;
    }
    public void DashAttack(){
        if (hasDashAttacked) return;

        timeSpendHold += Time.deltaTime;

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
        rb.velocity = new Vector2(dashAmount, rb.velocity.y);
        playerAnimator.SetTrigger("dashAttack");

        yield return new WaitForSeconds(dashTime);

        rb.velocity = new Vector2(0, rb.velocity.y);
        
        
        playerAnimator.SetTrigger("endDashAttack");
        canMove = true;
    }

    public void SetMovement(float xValue, float yValue){
        xMoveInput = Mathf.Clamp(xValue, -1, 1);
        yMoveInput = Mathf.Clamp(yValue, -1, 1);
    }
}
