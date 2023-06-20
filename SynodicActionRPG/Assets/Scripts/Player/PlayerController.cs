using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public enum facingDirection { Up, upRight, Right, downRight, Down, downLeft, Left, upLeft}
public class PlayerController : MonoBehaviour
{
    // Public variables to be set in the Unity editor.
    public facingDirection fd = facingDirection.Right;
    public float moveSpeed = 1f; // The speed at which the player moves.
    public float collisionOffset = 0.5f; // Additional distance to check for collisions beyond the movement distance.
    public ContactFilter2D movementFilter; // Filter to check which objects should be considered for movement collision.
    public float spriteDirThreshold = 0.9f; // Threshold for determining when to update the last move direction.
    public bool canMove = true;
    public Transform bulletSpawn;
    public GameObject bullet;
    public float AttackCooldown = 1;
    private float acTimer;

    // Private variables.
    private Vector2 movementInput; // The current movement input (x and y directions).
    private Rigidbody2D rb; // The Rigidbody2D component attached to the player.
    private Animator animator; // The Animator component attached to the player.
    private List<RaycastHit2D> castCollisions = new List<RaycastHit2D>(); // List to store raycast hit results for collision detection.

    // Called once at the start of the game.
    void Start()
    {
        // Get the Rigidbody2D and Animator components attached to the player.
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        //acTimer = AttackCooldown;
    }

    // Called at a fixed interval (ideal for physics calculations).
    private void FixedUpdate()
    {
        if(canMove)
            HandleMovement(); // Handle player movement.
        HandleAnimation(); // Handle player animation based on movement.
    }

    private void Update()
    {
        if(acTimer > 0)
        {
            acTimer -= Time.deltaTime;
        }
    }

    // Handle the player's movement.
    private void HandleMovement()
    {
        // If the player has input for movement.
        if (movementInput != Vector2.zero)
        {
            // Try to move in the direction of input.
            bool success = TryMove(movementInput);

            // If movement was not successful, try moving only horizontally.
            if (!success)
            {
                success = TryMove(new Vector2(movementInput.x, 0));

                // If horizontal movement was not successful, try moving only vertically.
                if (!success)
                {
                    TryMove(new Vector2(0, movementInput.y));
                }
            }
        }
    }

    // Handle the animation of the player based on movement direction.
    private void HandleAnimation()
    {
        // Set the moveX and moveY parameters in the Animator to the current movement input's x and y values.
        animator.SetFloat("moveX", movementInput.x);
        animator.SetFloat("moveY", movementInput.y);

        // Only update lastMoveX and lastMoveY if there is some movement input.
        // This way, when the player stops, lastMoveX and lastMoveY will retain the values of the last movement direction.
        if (movementInput != Vector2.zero)
        {
            animator.SetFloat("lastMoveX", movementInput.x);
            animator.SetFloat("lastMoveY", movementInput.y);
        }
    }

    // Try to move the player in the given direction.
    // Returns true if the movement was successful, false if there was an obstacle.
    private bool TryMove(Vector2 direction)
    {
        // Cast a ray in the direction of movement to check for collisions.
        int count = rb.Cast(
                direction,
                movementFilter,
                castCollisions,
                moveSpeed * Time.fixedDeltaTime + collisionOffset);

        // If there were no collisions, move in the given direction.
        if (count == 0)
        {
            rb.MovePosition(rb.position + direction * moveSpeed * Time.fixedDeltaTime);
            return true;
        }

        // If there was a collision, return false.
        return false;
    }

    // Called when movement input is detected.
    public void OnMove(InputValue movementValue)
    {
        // Get the Vector2 movement input from the InputValue.
        movementInput = movementValue.Get<Vector2>();
    }

    public void OnFire(InputValue fireValue)
    {
        if(acTimer <= 0)
        {
            animator.SetTrigger("Shoot");
            GameObject bull = Instantiate(bullet, bulletSpawn.position, bulletSpawn.rotation);
            if (fd == facingDirection.Down)
            {
                bull.GetComponent<SpriteRenderer>().sortingOrder = 1;
            }
            acTimer = AttackCooldown;

        }
        

    }

    public void FireBullet()
    {
        if(acTimer <= 0)
        {
            
        }
        
        
    }
    public void ChangeLookDir(facingDirection dir)
    {
        fd = dir;
    }

    public void SetCanMove(int val)
    {
        if (val == 1)
        {
            canMove = true;
        }
        else
        {
            canMove = false;
        }
    }
}
