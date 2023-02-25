using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;


public class Player : MonoBehaviour
{
    public Rigidbody2D myRigidBody;
    public HealthBase healthBase;

    [Header("Setup")]
    public SOPlayerSetup soPlayerSetup;

    private float _currentSpeed;
    public Animator _currentPlayer;

    private void Awake()
    {
        if (healthBase != null)
        {
            healthBase.OnKill += OnPlayerKill;
        }

       // _currentPlayer = Instantiate(soPlayerSetup.player, tr);
    }

    private void OnPlayerKill()
    {
        healthBase.OnKill -= OnPlayerKill;
        _currentPlayer.SetTrigger(soPlayerSetup.triggerDeath);
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
            _currentSpeed = soPlayerSetup.speedRun;
            _currentPlayer.speed = 2f;
        }
        else
        {
            _currentSpeed = soPlayerSetup.speed;
            _currentPlayer.speed = 1f;
        }


        if (Input.GetKey(KeyCode.LeftArrow))
        {
            myRigidBody.velocity = new Vector2(-_currentSpeed, myRigidBody.velocity.y);


            if (myRigidBody.transform.localScale.x != -1)
            {
                myRigidBody.transform.DOScaleX(-1, soPlayerSetup.playerSwipeDuration);
            }

            if (!soPlayerSetup.jumping)
            {
                _currentPlayer.SetBool(soPlayerSetup.boolRun, true);
            }


        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            myRigidBody.velocity = new Vector2(_currentSpeed, myRigidBody.velocity.y);
            if (myRigidBody.transform.localScale.x != 1)
            {
                myRigidBody.transform.DOScaleX(1, soPlayerSetup.playerSwipeDuration);
            }
            myRigidBody.transform.localScale = new Vector3(1, 1, 1);

            if (!soPlayerSetup.jumping)
            {
                _currentPlayer.SetBool(soPlayerSetup.boolRun, true);
            }
        }
        else
        {
            myRigidBody.velocity = new Vector2(0, myRigidBody.velocity.y);
            _currentPlayer.SetBool(soPlayerSetup.boolRun, false);
        }


        if (myRigidBody.velocity.x > 0)
        {
            myRigidBody.velocity -= soPlayerSetup.friction;
        }
        else if (myRigidBody.velocity.x < 0)
        {
            myRigidBody.velocity += soPlayerSetup.friction;

        }

    }

    private void HandleJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !soPlayerSetup.jumping || Input.GetKeyDown(KeyCode.UpArrow) && !soPlayerSetup.jumping)
        {
            myRigidBody.velocity = soPlayerSetup.jumpForce * Vector2.up;
            myRigidBody.transform.localScale = new Vector2(myRigidBody.transform.localScale.x, 1);
            _currentPlayer.SetTrigger(soPlayerSetup.triggerJump);
            soPlayerSetup.jumping = true;
            DOTween.Kill(myRigidBody.transform);
        }
    }


    public void DestroyMe()
    {
        Destroy(gameObject);
        SceneManager.LoadScene(2);

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 8)
        {
            soPlayerSetup.jumping = false;
        }
    }
}
