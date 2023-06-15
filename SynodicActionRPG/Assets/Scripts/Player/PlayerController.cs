using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 1f;
    public float collisionOffset = 0.5f;
    public ContactFilter2D movementFilter;
    public float spriteDirThreshold = 0.9f;

    private Vector2 movementInput;
    private Rigidbody2D rb;
    private Animator animator;
    private List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        HandleMovement();
        HandleAnimation();
    }

    private void HandleMovement()
    {
        if (movementInput != Vector2.zero)
        {
            bool success = TryMove(movementInput);

            if (!success)
            {
                success = TryMove(new Vector2(movementInput.x, 0));

                if (!success)
                {
                    TryMove(new Vector2(0, movementInput.y));
                }
            }
        }
    }

    private void HandleAnimation()
    {
        animator.SetFloat("moveX", movementInput.x);
        animator.SetFloat("moveY", movementInput.y);

        if (Mathf.Abs(movementInput.x) >= spriteDirThreshold || Mathf.Abs(movementInput.y) >= spriteDirThreshold)
        {
            animator.SetFloat("lastMoveX", movementInput.x);
            animator.SetFloat("lastMoveY", movementInput.y);
        }
    }

    private bool TryMove(Vector2 direction)
    {
        int count = rb.Cast(
                direction,
                movementFilter,
                castCollisions,
                moveSpeed * Time.fixedDeltaTime + collisionOffset);

        if (count == 0)
        {
            rb.MovePosition(rb.position + direction * moveSpeed * Time.fixedDeltaTime);
            return true;
        }

        return false;
    }

    public void OnMove(InputValue movementValue)
    {
        movementInput = movementValue.Get<Vector2>();
    }
}
