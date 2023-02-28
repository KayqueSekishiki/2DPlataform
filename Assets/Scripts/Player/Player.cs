using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using DG.Tweening;


public class Player : MonoBehaviour
{
    public Rigidbody2D myRigidBody;
    public HealthBase healthBase;
    public TextMeshProUGUI uiTextPlayerName;

    [Header("Setup")]
    public SOPlayerSetup soPlayerSetup;
    private Animator _currentPlayer;

    [Header("Jump Collision Check")]
    public Collider2D myCollider2D;
    public float distToGround;
    public float spaceToGround = .1f;

    public ParticleSystem jumpVFX;

    private float _currentSpeed;

    private void Awake()
    {
        if (healthBase != null)
        {
            healthBase.OnKill += OnPlayerKill;
        }

        _currentPlayer = Instantiate(soPlayerSetup.player, transform);

        if (myCollider2D != null)
        {
            distToGround = myCollider2D.bounds.extents.y;
        }
    }

    private void OnPlayerKill()
    {
        healthBase.OnKill -= OnPlayerKill;
        _currentPlayer.SetTrigger(soPlayerSetup.triggerDeath);
    }

    private void Start()
    {
        uiTextPlayerName.text = soPlayerSetup.playeName;
    }

    private void Update()
    {
        IsGrounded();
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
            uiTextPlayerName.rectTransform.localScale = new Vector3(-1, 1, 1);

            if (myRigidBody.transform.localScale.x != -1)
            {
                myRigidBody.transform.DOScaleX(-1, soPlayerSetup.playerSwipeDuration);

            }

            if (IsGrounded())
            {
                _currentPlayer.SetBool(soPlayerSetup.boolRun, true);
            }


        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            myRigidBody.velocity = new Vector2(_currentSpeed, myRigidBody.velocity.y);
            uiTextPlayerName.rectTransform.localScale = new Vector3(1, 1, 1);

            if (myRigidBody.transform.localScale.x != 1)
            {
                myRigidBody.transform.DOScaleX(1, soPlayerSetup.playerSwipeDuration);
            }
            myRigidBody.transform.localScale = new Vector3(1, 1, 1);

            if (IsGrounded())
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

    private bool IsGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, distToGround + spaceToGround);
        if (hit.collider != null && hit.transform.CompareTag("Jumpable"))
        {
            return hit;
        }
        else
        {
            return false;
        }
    }

    private void HandleJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded() || Input.GetKeyDown(KeyCode.UpArrow) && IsGrounded())
        {

            myRigidBody.velocity = soPlayerSetup.jumpForce * Vector2.up;
            myRigidBody.transform.localScale = new Vector2(myRigidBody.transform.localScale.x, 1);
            _currentPlayer.SetTrigger(soPlayerSetup.triggerJump);
            PlayJumpVFX();
        }

    }

    private void PlayJumpVFX()
    {
        if (jumpVFX != null)
        {
            jumpVFX.Play();
        }
    }


    public void DestroyMe()
    {
        Destroy(gameObject);
        SceneManager.LoadScene(2);
    }
}
