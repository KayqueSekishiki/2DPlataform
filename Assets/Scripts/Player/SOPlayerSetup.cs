using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[CreateAssetMenu]
public class SOPlayerSetup : ScriptableObject
{

    public Animator player;
    public SOString soStringName;

    [Header("Speed Setup")]
    public Vector2 friction;
    public float speed;
    public float speedRun;
    public float jumpForce;

    [Header("Animation Player")]
    public string boolRun = "Run";
    public string triggerJump = "Jump";
    public bool jumping = false;
    public string triggerDeath = "Death";
    public float playerSwipeDuration = .1f;
}
