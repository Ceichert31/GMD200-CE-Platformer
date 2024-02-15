using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
[RequireComponent(typeof(Rigidbody2D))]
public class InputManager : MonoBehaviour
{
    private PlayerInput playerInput;
    private PlayerInput.PlayerActions playerActions;

    private Rigidbody2D rb;

    [Header("Input Settings")]
    [SerializeField] private LayerMask groundLayer;

    [SerializeField]
    private float
        moveSpeed = 5f,
        jumpForce = 500f,
        dashSpeed = 10f,
        dashCooldown = 1.5f;

    private bool 
        isGrounded = true,
        isSlowed,
        canDash = true;

    const float RAY_DISTANCE = 0.6f;

    void Awake()
    {
        playerInput = new PlayerInput();
        playerActions = playerInput.Player;

        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        //Ground Check
        if (!Physics2D.Raycast(transform.position, Vector2.down, RAY_DISTANCE, groundLayer))
            isGrounded = false;
        else
            isGrounded = true;
    }

    private void FixedUpdate()
    {
        //Constantly apply gravity
        /*if (!isGrounded)
            rb.AddForce(gravityForce * Time.unscaledDeltaTime * Vector2.down);*/

        if (!canDash) return;

        Move();
    }
    /// <summary>
    /// Set player movement from direction and speed
    /// </summary>
    void Move()
    {
        rb.velocity = new Vector2(ReadDirection().x, rb.velocity.y);
    }
    /// <summary>
    /// Gets the direction from key input
    /// </summary>
    /// <returns></returns>
    Vector2 ReadDirection()
    {
        Vector2 direction = playerActions.Move.ReadValue<Vector2>();
        direction.Normalize();
        direction = moveSpeed * Time.unscaledDeltaTime * direction;
        return direction;
    }
    /// <summary>
    /// Add force upwards
    /// </summary>
    /// <param name="ctx"></param>
    void Jump(InputAction.CallbackContext ctx)
    {
        if (!isGrounded) return;
        
        rb.AddForce(jumpForce * Vector2.up);
    }

    public void Bounce(float bounceForce)
    {
        rb.AddForce(bounceForce * Vector2.up, ForceMode2D.Impulse);
    }

    void TimeSlow(InputAction.CallbackContext ctx)
    {
        isSlowed = !isSlowed;
        TimeManager.timeController?.Invoke(isSlowed);
    }

    void Dash(InputAction.CallbackContext ctx)
    {
        /*canDash = false;
        rb.velocity = ReadDirection() * dashSpeed;
        Invoke(nameof(ResetDash), dashCooldown);*/
    }
    void ResetDash() => canDash = true;
    //Enable/Disable PlayerActions load/unload
    private void OnEnable()
    {
        playerActions.Enable();
        playerActions.Jump.performed += Jump;
        playerActions.Time.performed += TimeSlow;
        playerActions.Dash.performed += Dash;
    }
    private void OnDisable()
    {
        playerActions.Disable();
        playerActions.Jump.performed -= Jump;
        playerActions.Time.performed -= TimeSlow;
        playerActions.Dash.performed -= Dash;
    }
}
