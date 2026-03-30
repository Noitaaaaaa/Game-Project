using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private BoxCollider2D boxCollider;
    [SerializeField] private CircleCollider2D circleCollider;

    public CharacterController2D controller;

    public float runSpeed = 40f;

    float horizontalMove = 0f;
    bool jump = false;
    bool crouch = false;

    private bool canDash = true;
    private bool isDashing;
    private float dashTime = 0.2f;
    private float dashPower = 24f;
    private float dashCooldown = 1f;

    void Update()
    {
        if (isDashing) return;

        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
        }

        crouch = Input.GetButton("Crouch");

        boxCollider.enabled = !crouch;

        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
        {
            StartCoroutine(Dash());
        }
    }

    void FixedUpdate()
    {
        if (isDashing) return;

        controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
        jump = false;
    }

    private IEnumerator Dash()
{
    canDash = false;
    isDashing = true;

    // Determine dash direction
    float dashDirection = transform.localScale.x; // 1 = right, -1 = left
    float dashDuration = dashTime;
    
    // Save original velocity so we can restore after
    Vector2 originalVelocity = controller.GetComponent<Rigidbody2D>().linearVelocity;

    while (dashDuration > 0)
    {
        controller.GetComponent<Rigidbody2D>().linearVelocity = new Vector2(dashDirection * dashPower, 0f);
        dashDuration -= Time.deltaTime;
        yield return null;
    }

    // Restore original velocity
    controller.GetComponent<Rigidbody2D>().linearVelocity = originalVelocity;

    isDashing = false;
    yield return new WaitForSeconds(dashCooldown);
    canDash = true;
}
}