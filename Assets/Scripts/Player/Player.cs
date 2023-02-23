using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Player : MonoBehaviour
{
    public Rigidbody2D myRigidBody;


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
    public string TriggerJump = "Jump";
    public float playerSwipeDuration = .1f;


    private float _currentSpeed;

    private void Update()
    {
        HandleMovement();
        HandleJump();
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
            animator.SetBool(boolRun, true);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            myRigidBody.velocity = new Vector2(_currentSpeed, myRigidBody.velocity.y);
            if (myRigidBody.transform.localScale.x != 1)
            {
                myRigidBody.transform.DOScaleX(1, playerSwipeDuration);
            }
            myRigidBody.transform.localScale = new Vector3(1, 1, 1);
            animator.SetBool(boolRun, true);
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
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            myRigidBody.velocity = jumpForce * Vector2.up;
            myRigidBody.transform.localScale = new Vector2(myRigidBody.transform.localScale.x, 1);
            animator.SetTrigger(TriggerJump);
            DOTween.Kill(myRigidBody.transform);
          //  HandleScaleJump();
        }
    }

    private void HandleScaleJump()
    {
        myRigidBody.transform.DOScaleY(jumpScaleY, animationDuration).SetLoops(2, LoopType.Yoyo).SetEase(ease);
        myRigidBody.transform.DOScaleX(myRigidBody.transform.localScale.x > 0 ? jumpScaleX : -jumpScaleX, animationDuration).SetLoops(2, LoopType.Yoyo).SetEase(ease);
    }
}
