using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public Animator anim;
    public Rigidbody2D theRB;
    public Vector2 moveDirection;
    public float speed = 50;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void FixedUpdate()
    {
        theRB.velocity = moveDirection * speed;
        //theRB.MovePosition(theRB.position + moveDirection * speed * Time.deltaTime);
        HandleAnimation();
    }

    public void HandleAnimation()
    {
        anim.SetFloat("moveX", moveDirection.x);
        anim.SetFloat("moveY", moveDirection.y);

        if(moveDirection != Vector2.zero)
        {
            anim.SetFloat("LastMoveX", moveDirection.x);
            anim.SetFloat("LastMoveY", moveDirection.y);
        }
    }

    public void OnMove(InputValue movementValue)
    {
        moveDirection = movementValue.Get<Vector2>();

    }
}
