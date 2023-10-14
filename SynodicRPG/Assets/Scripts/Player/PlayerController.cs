using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public enum PlayerStates { IDLE, RUN, ATTACK }
public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    public Animator anim;
    public Rigidbody2D theRB;
    public SpriteRenderer spr;
    public Vector2 moveDirection;
    public float speed = 50;
    public bool canAttack = true;
    public PlayerStates playerState = PlayerStates.IDLE;

    public bool canMove = true;

    public DialogueTrigger dig;

    public int weaponDamage;

    public int health, maxhealth;
    public float flash = 0;

    private void Awake()
    {
        if(instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);
        health = maxhealth;
    }

    private void FixedUpdate()
    {
        if(playerState != PlayerStates.ATTACK && canMove)
        {
            theRB.velocity = moveDirection * speed;
            
        }
        else
        {
            theRB.velocity = Vector2.zero;
        }


        HandleAnimation();

    }

    private void Update()
    {
        spr.material.SetFloat("_BlendOpacity", flash);
    }

    public void HandleAnimation()
    {
        anim.SetFloat("moveX", moveDirection.x);
        anim.SetFloat("moveY", moveDirection.y);

        if(moveDirection != Vector2.zero && playerState != PlayerStates.ATTACK)
        {
            anim.SetFloat("LastMoveX", moveDirection.x);
            anim.SetFloat("LastMoveY", moveDirection.y);
        }
    }

    public void OnMove(InputValue movementValue)
    {
        if (canMove)
        {
            moveDirection = movementValue.Get<Vector2>();
        }
        else
        {
            moveDirection = Vector2.zero;
        }
        

    }

    public void OnFire(InputValue fireValue)
    {
        if(playerState != PlayerStates.ATTACK && canMove)
        {
            if (canAttack)
            {
                anim.SetTrigger("Sword");
                playerState = PlayerStates.ATTACK;
            }
        }
        
    }

    public void ReturnToIdle()
    {
        playerState = PlayerStates.IDLE;
    }

    public void OnDialogue(InputValue fireValue)
    {
        if(dig != null)
        {
            dig.TriggerDialogue();
            moveDirection = Vector2.zero;
        }
    }

    public void TakeDamage(int dam)
    {
        health -= dam;
        anim.SetTrigger("Flash");
        UIManager.instance.UpdateHealthSlider();
    }
}
