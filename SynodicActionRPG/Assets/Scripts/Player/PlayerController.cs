using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 1f;
    public float collisionOffset = 0.5f;
    public ContactFilter2D movementFilter;
    public float spriteDirThreshhold = 0.9f;

    public Vector2 movementInput;
    Rigidbody2D theRB;
    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();

    public Animator anim;
    void Start()
    {
        theRB = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (movementInput != Vector2.zero)
        {
            bool success = TryMove(movementInput);

            if (!success)
            {
                success = TryMove(new Vector2(movementInput.x, 0));

                if (!success)
                {
                    success = TryMove(new Vector2(0, movementInput.y));
                }
            }
        }

        anim.SetFloat("moveX", movementInput.x);
        anim.SetFloat("moveY", movementInput.y);

        if(movementInput.x >= spriteDirThreshhold || movementInput.x <= -spriteDirThreshhold || movementInput.y >= spriteDirThreshhold || movementInput.y <= -spriteDirThreshhold)
        {
            anim.SetFloat("lastMoveX", movementInput.x);
            anim.SetFloat("lastMoveY", movementInput.y);
        }
    }

    private bool TryMove(Vector2 direction)
    {
        int count = theRB.Cast(
                direction,
                movementFilter,
                castCollisions,
                moveSpeed * Time.deltaTime + collisionOffset);
        if (count == 0)
        {
            theRB.MovePosition(theRB.position + direction * moveSpeed * Time.deltaTime);
            return true;
        }
        else
        {
            return false;
        }

        
    }

    void OnMove(InputValue movementValue)
    {
        movementInput = movementValue.Get<Vector2>();
    }
}
