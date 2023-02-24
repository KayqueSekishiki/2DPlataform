using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Player : MonoBehaviour
{
    public Rigidbody2D myRigidBody;
    public HealthBase healthBase;

    [Header("Speed Setup")]
    public Vector2 friction = new(.1f, 0);
    public float speed;
    public float speedRun;
    public float jumpForce;

    [Header("Speed Setup")]
    public float jumpScaleY = 1.5f;
    public float jumpScaleX = 0.7f;
    public float animationDuration = 0.3f;
    public Ease ease = Ease.OutBack;

    [Header("Animation")]
    public Animator animator;
    public string boolRun = "Run";
    public string triggerJump = "Jump";
    public bool jumping = false;
    public string triggerDeath = "Death";
    public float playerSwipeDuration = .1f;


    private float _currentSpeed;


    private void Awake()
    {
        if (healthBase != null)
        {
            healthBase.OnKill += OnPlayerKill;
        }
    }

    private void OnPlayerKill()
    {
        healthBase.OnKill -= OnPlayerKill;
        animator.SetTrigger(triggerDeath);
    }

    private void Update()
    {

        HandleJump();
    }

    private void FixedUpdate()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {

        if (Input.GetKey(KeyCode.LeftControl))
        {
            _currentSpeed = speedRun;
            animator.speed = 2f;
        }
        else
        {
            _currentSpeed = speed;
            animator.speed = 1f;
        }


        if (Input.GetKey(KeyCode.LeftArrow))
        {
            myRigidBody.velocity = new Vector2(-_currentSpeed, myRigidBody.velocity.y);


            if (myRigidBody.transform.localScale.x != -1)
            {
                myRigidBody.transform.DOScaleX(-1, playerSwipeDuration);
            }

            if (!jumping)
            {
                animator.SetBool(boolRun, true);
            }


        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            myRigidBody.velocity = new Vector2(_currentSpeed, myRigidBody.velocity.y);
            if (myRigidBody.transform.localScale.x != 1)
            {
                myRigidBody.transform.DOScaleX(1, playerSwipeDuration);
            }
            myRigidBody.transform.localScale = new Vector3(1, 1, 1);

            if (!jumping)
            {
                animator.SetBool(boolRun, true);
            }
        }
        else
        {
            animator.SetBool(boolRun, false);
        }


        if (myRigidBody.velocity.x > 0)
        {
            myRigidBody.velocity -= friction;
        }
        else if (myRigidBody.velocity.x < 0)
        {
            myRigidBody.velocity += friction;

        }

    }

    private void HandleJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !jumping || Input.GetKeyDown(KeyCode.UpArrow) && !jumping)
        {
            myRigidBody.velocity = jumpForce * Vector2.up;
            myRigidBody.transform.localScale = new Vector2(myRigidBody.transform.localScale.x, 1);
            animator.SetTrigger(triggerJump);
            jumping = true;
            DOTween.Kill(myRigidBody.transform);
        }
    }


    public void DestroyMe()
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 8)
        {
            jumping = false;
        }
    }




}
