using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    private float horizontal;
    private float speed = 8f;
    private float jumpingPower = 16f;
    private bool isFacingRight = true;

    [Header("Wall Mechanics")]
    private bool isWallSliding;
    private float wallSlidingSpeed = 2f;

    private bool isWallJumping;
    private float wallJumpingDirection;
    private float wallJumpingTime = 0.2f;
    private float wallJumpingCounter;
    private float wallJumpingDuration = 0.4f;
    private Vector2 wallJumpingPower = new Vector2(8f, 16f);

    private float wallJumpCooldown = 0.3f;
    private float lastWallJumpTime;

    private float wallJumpControlLockTime = 0.2f;
    private float wallJumpLockCounter;

    [Header("Dash")]
    private bool canDash = true;
    private bool isDashing;
    private float dashPower = 24f;
    private float dashTime = 0.2f;
    private float dashCooldown = 1f;

    [Header("Crouch")]
    private bool crouch;
    [SerializeField] private BoxCollider2D boxCollider;
    [SerializeField] private CircleCollider2D circleCollider;

    [Header("References")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;

    void Update()
    {
        if (isDashing) return;

        horizontal = Input.GetAxisRaw("Horizontal");

        // Jump
        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpingPower);
        }

        if (Input.GetButtonUp("Jump") && rb.linearVelocity.y > 0f)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, rb.linearVelocity.y * 0.5f);
        }

        // Crouch
        crouch = Input.GetKey(KeyCode.LeftControl);
        boxCollider.enabled = !crouch;

        // Dash
        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
        {
            StartCoroutine(Dash());
        }

        WallSlide();
        WallJump();

        if (!isWallJumping)
        {
            Flip();
        }
    }

    void FixedUpdate()
    {
        if (isDashing) return;

        float moveSpeed = crouch ? speed * 0.5f : speed;

        if (wallJumpLockCounter > 0)
        {
            wallJumpLockCounter -= Time.fixedDeltaTime;

            // Force movement away from wall
            rb.linearVelocity = new Vector2(
                wallJumpingDirection * wallJumpingPower.x,
                rb.linearVelocity.y
            );
        }
        else if (!isWallJumping)
        {
            rb.linearVelocity = new Vector2(horizontal * moveSpeed, rb.linearVelocity.y);
        }
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private bool IsWalled()
    {
        return Physics2D.OverlapCircle(wallCheck.position, 0.2f, wallLayer);
    }

    private void WallSlide()
    {
        if (IsWalled() && !IsGrounded() && horizontal != 0f)
        {
            isWallSliding = true;
            rb.linearVelocity = new Vector2(
                rb.linearVelocity.x,
                Mathf.Clamp(rb.linearVelocity.y, -wallSlidingSpeed, float.MaxValue)
            );
        }
        else
        {
            isWallSliding = false;
        }
    }

    private void WallJump()
    {
        if (isWallSliding)
        {
            isWallJumping = false;
            wallJumpingDirection = -transform.localScale.x;
            wallJumpingCounter = wallJumpingTime;

            CancelInvoke(nameof(StopWallJumping));
        }
        else
        {
            wallJumpingCounter -= Time.deltaTime;
        }

        if (Input.GetButtonDown("Jump") &&
            wallJumpingCounter > 0f &&
            Time.time >= lastWallJumpTime + wallJumpCooldown)
        {
            isWallJumping = true;

            rb.linearVelocity = new Vector2(
                wallJumpingDirection * wallJumpingPower.x,
                wallJumpingPower.y
            );

            wallJumpingCounter = 0f;
            lastWallJumpTime = Time.time;

            // Lock movement so player is forced away
            wallJumpLockCounter = wallJumpControlLockTime;

            if (transform.localScale.x != wallJumpingDirection)
            {
                Flip();
            }

            Invoke(nameof(StopWallJumping), wallJumpingDuration);
        }
    }

    private void StopWallJumping()
    {
        isWallJumping = false;
    }

    private void Flip()
    {
        if ((isFacingRight && horizontal < 0f) ||
            (!isFacingRight && horizontal > 0f))
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;

        float direction = transform.localScale.x;

        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;

        rb.linearVelocity = new Vector2(direction * dashPower, 0f);

        yield return new WaitForSeconds(dashTime);

        rb.gravityScale = originalGravity;
        isDashing = false;

        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }
}