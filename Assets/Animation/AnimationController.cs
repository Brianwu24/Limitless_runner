using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    private Player _player;
    private Animator _anim;

    //Rider specific optimization, not written by Brian
    private static readonly int IsJumping = Animator.StringToHash("isJumping");
    private static readonly int IsFalling = Animator.StringToHash("isFalling");
    private static readonly int IsGrounded = Animator.StringToHash("isGrounded");

    // Start is called before the first frame update
    void Start()
    {
        _player = GetComponent<Player>();
        _anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        _anim.SetBool(IsJumping, _player.GetJumping());
        _anim.SetBool(IsFalling, _player.GetFalling());
        _anim.SetBool(IsGrounded, _player.GetGrounded());
    }
}
