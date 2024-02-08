using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private PlayerInput playerInput;
    private PlayerInput.PlayerActions playerActions;

    private Rigidbody2D rb;

    [Header("Input Settings")]
    [SerializeField] private LayerMask groundLayer;

    [SerializeField] private float moveSpeed = 5f;

    private bool isGrounded = true;

    const float RAY_DISTANCE = 0.5f;

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
        if (!isGrounded)
            return;

        Move();
    }
    /// <summary>
    /// Set player movement from direction and speed
    /// </summary>
    void Move()
    {
        //Stop player from sliding
        /*if (!playerActions.Move.IsInProgress() && rb.velocity.magnitude > 0)
            rb.velocity -= rb.velocity;*/

        rb.velocity = moveSpeed * Time.deltaTime * ReadDirection();
    }
    /// <summary>
    /// Gets the direction from key input
    /// </summary>
    /// <returns></returns>
    Vector2 ReadDirection()
    {
        Vector2 direction = playerActions.Move.ReadValue<Vector2>();
        return direction.normalized;
    }

    //Enable/Disable PlayerActions load/unload
    private void OnEnable()
    {
        playerActions.Enable();
    }
    private void OnDisable()
    {
        playerActions.Disable();
    }
}
