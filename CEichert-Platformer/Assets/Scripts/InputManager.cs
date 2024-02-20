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
        groundStompForce = 500f,
        jumpBoostWindow = 0.5f,
        jumpBoostCooldown = 1.5f;

    private bool
        isGrounded = true,
        isSlowed,
        jumpWindow,
        canJumpBoost = true,
        groundSlamSFX;

    private Animator animator;

    [SerializeField] private Transform groundCheckTransform;

    const float GROUND_CHECK_RADIUS = 0.1f;

    void Awake()
    {
        playerInput = new PlayerInput();
        playerActions = playerInput.Player;

        rb = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        //If movement inputs are detected, set bool to true
        animator.SetBool("Walking", playerActions.Move.IsInProgress());
        animator.SetBool("Grounded", isGrounded);
    }

    private void FixedUpdate()
    {
        //Ground Check
        if (Physics2D.OverlapCircle(groundCheckTransform.position, GROUND_CHECK_RADIUS, groundLayer))
            isGrounded = true;
        else
            isGrounded = false;
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("MovingPlatform"))
            transform.SetParent(collision.gameObject.transform, true);

        if (groundSlamSFX)
        {
            groundSlamSFX = false;
            SoundManager.soundManager?.Invoke(SoundID.GroundSlam);
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("MovingPlatform"))
            transform.SetParent(null, true);
    }
    /// <summary>
    /// Add force upwards
    /// </summary>
    /// <param name="ctx"></param>
    void Jump(InputAction.CallbackContext ctx)
    {
        if (!isGrounded) return;

        if (jumpWindow)
        {
            rb.AddForce(jumpForce * Vector2.up / 2);

            canJumpBoost = false;
            Invoke(nameof(ResetJumpBoostCooldown), jumpBoostCooldown);
        }

        SoundManager.soundManager?.Invoke(SoundID.Jump);
        
        rb.AddForce(jumpForce * Vector2.up);
    }

    public void Bounce(float bounceForce)
    {
        //Cancel any previous momentum
        rb.velocity = Vector2.zero;

        SoundManager.soundManager?.Invoke(SoundID.Bounce);

        rb.AddForce(bounceForce * Vector2.up, ForceMode2D.Impulse);
    }

    void TimeSlow(InputAction.CallbackContext ctx)
    {
        isSlowed = !isSlowed;
        TimeManager.timeController?.Invoke(isSlowed);
    }

    void GroundStomp(InputAction.CallbackContext ctx)
    {
        if (!isGrounded)
        {
            rb.AddForce(groundStompForce * Time.unscaledDeltaTime * Vector2.down, ForceMode2D.Impulse);

            groundSlamSFX = true;

            if (canJumpBoost)
            {
                jumpWindow = true;
                Invoke(nameof(ResetWindow), 0.5f);
            }
        }
    }

    void RestartLevel(InputAction.CallbackContext ctx) => SceneLoader.reloadScene?.Invoke();
    void ResetWindow() => jumpWindow = false;
    void ResetJumpBoostCooldown() => canJumpBoost = true;
    //Enable/Disable PlayerActions load/unload
    private void OnEnable()
    {
        playerActions.Enable();
        playerActions.Jump.performed += Jump;
        playerActions.Time.performed += TimeSlow;
        playerActions.Dash.performed += GroundStomp;
        playerActions.Restart.performed += RestartLevel;
    }
    private void OnDisable()
    {
        playerActions.Disable();
        playerActions.Jump.performed -= Jump;
        playerActions.Time.performed -= TimeSlow;
        playerActions.Dash.performed -= GroundStomp;
        playerActions.Restart.performed -= RestartLevel;
    }
}
