using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    [SerializeField] GameObject facingRef;
    [SerializeField] Camera m_Camera;
    Vector2 moveDirection;
    Vector2 mouseMove;
    Rigidbody rb;
    [SerializeField] int acceleration = 2;
    [SerializeField] int deceleration = 6;
    [SerializeField] int maxWalkSpeed = 10;
    [SerializeField] int maxSprintSpeed = 12;
    [SerializeField] float sensitivity = 0.5f;
    [SerializeField] float jumpForce;
    [SerializeField] LayerMask groundMask;
    [SerializeField] float groundCheckDistance = 0.2f;
    [SerializeField] Transform groundCheckPoint;

    bool sprinting = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMove();

        m_Camera.transform.Rotate(mouseMove.y * -1 * (0.5f + sensitivity) * 8 * Time.deltaTime, 0, 0);
        transform.Rotate(0, mouseMove.x * (0.5f + sensitivity) * 8 * Time.deltaTime, 0);
    }

    void OnMove(InputValue value)
    {
        moveDirection = value.Get<Vector2>();
        
    }

    void OnMouse(InputValue value)
    {
        mouseMove = value.Get<Vector2>();

        
    }

    void OnSprint(InputValue value)
    {
        sprinting = (value.Get<float>() > 0.5f) ? true : false;
    }

    void OnJump(InputValue value)
    {
        if (IsGrounded() && value.Get<float>() > 0.5f)
        {
            Vector3 vel = rb.linearVelocity;
            vel.y = 0f;
            rb.linearVelocity = vel;

            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    bool IsGrounded()
    {
        return Physics.Raycast(groundCheckPoint.position, Vector3.down, groundCheckDistance, groundMask);
    }



    void PlayerMove()
    {
        Vector3 moveVal = transform.TransformDirection(new Vector3(moveDirection.x, 0, moveDirection.y));

        int maxSpeed = (!sprinting) ? maxWalkSpeed : maxSprintSpeed; // set max speed depending on if sprinting or not

        Vector3 targetVelocity = moveVal * maxSpeed;
        Vector3 currentVelocity = rb.linearVelocity;

        targetVelocity.y = currentVelocity.y; // carry over y velo to maintain gravity

        float rate = (moveVal.magnitude > 0.1f) ? acceleration : deceleration; // check for if accelerating or decelerating and set rate accordingly

        rate *= (sprinting) ? 1.5f : 1f; // if we are sprinting multiply accel/decel rate

        rb.linearVelocity = Vector3.Lerp(currentVelocity, targetVelocity, rate * Time.deltaTime);
    }

}
