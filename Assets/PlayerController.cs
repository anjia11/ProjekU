using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [Header("Atribut Player")]
    [Header("Move")]
    [SerializeField] float speedPlayer;
    private float moveX;
    [SerializeField] Rigidbody2D RB2D;

    [Header("Flip")]
    [SerializeField] bool isFacingLeft = true;

    [Header("Grounded")]
    [SerializeField] Transform groundCheck;
    [SerializeField] float checkRadius;
    [SerializeField] LayerMask groundLayer;

    [Header("Jump")]
    [SerializeField] float jumpForce;
    [SerializeField] int dobelJumpValue;
    private int dobelJump;

    [Header("Dash")]
    [SerializeField] float dashSpeed;
    [SerializeField] bool canDash;
    [SerializeField] bool isDashing;
    [SerializeField] float dashingTime;
    [SerializeField] float dashingCD;

    [Header("Sliding")]
    [SerializeField] Transform wallCheck;
    [SerializeField] Vector2 wallCheckBoxSize;
    [SerializeField] LayerMask wallLayer;
    [SerializeField] float slideSpeed;
    bool isSliding = false;

    private float posisiScale;

    [SerializeField] Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        //anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(isDashing){
            anim.SetTrigger("Dashing");
            return;
        }

        moveX = Input.GetAxisRaw("Horizontal");
        Flip();
        Jump();
        if (Input.GetKeyDown(KeyCode.H) && canDash){
            StartCoroutine(Dash());
            
        }

        if (moveX != 0){
            anim.SetBool("IsRuning", true);
        }else{
            anim.SetBool("IsRuning", false);
        }

        Sliding();
    }

    private void FixedUpdate() {
        if(isDashing){
            return;
        }
        RB2D.velocity = new Vector2(moveX * speedPlayer, RB2D.velocity.y);
    }

    void Flip(){
        if(isFacingLeft && moveX > 0){
            transform.localEulerAngles = new Vector3(0, 180, 0);
            isFacingLeft = false;
        }
        else if (!isFacingLeft && moveX < 0){
            transform.localEulerAngles = new Vector3(0, 0, 0);
            isFacingLeft = true;
        }
    }

    bool IsGrounded(){
        return Physics2D.OverlapCircle(groundCheck.position, checkRadius, groundLayer);
    }

    void Jump(){
        if (IsGrounded()){
            dobelJump = dobelJumpValue;
            anim.SetBool("Jump", false);
        }
        else{
            anim.SetBool("Jump", true);
        }

        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded()){
            RB2D.velocity = Vector2.up * jumpForce;
        }

        else if (Input.GetKeyDown(KeyCode.Space) && !IsGrounded() && dobelJump > 0){
            RB2D.velocity = Vector2.up * jumpForce;
            anim.SetBool("Jump", true);
            dobelJump--;
        }
    }

    float Posisi(){
        if (isFacingLeft){
            posisiScale = -1;
        }else{
            posisiScale = 1;
        }

        return posisiScale;
    }

    IEnumerator Dash(){
        canDash = false;
        isDashing = true;
        float oGraviti = RB2D.gravityScale;
        RB2D.gravityScale = 0;
        RB2D.velocity = new Vector2(Posisi() * dashSpeed, 0f);
        
        yield return new WaitForSeconds(dashingTime);

        RB2D.gravityScale = oGraviti;
        isDashing = false;
        yield return new WaitForSeconds(dashingCD);
        canDash = true;
    }

    bool IsWallTouched(){
        return Physics2D.OverlapBox(wallCheck.position, wallCheckBoxSize, 0, wallLayer);
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(wallCheck.position, wallCheckBoxSize);
    }

    void Sliding(){
        if (IsWallTouched() && !IsGrounded())
        {
            isSliding = true;
            anim.SetTrigger("Sliding");
            anim.SetBool("Jump", false);
        }
        else
        {
            isSliding = false;
        }

        if (isSliding){
            RB2D.velocity = new Vector2(RB2D.velocity.x, Mathf.Clamp(RB2D.velocity.y, -slideSpeed, float.MaxValue));
        }
    }
}
