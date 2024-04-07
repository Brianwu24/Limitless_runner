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

    // Start is called before the first frame update
    void Start()
    {
        _player = GetComponent<Player>();
        _anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_player.GetJumping())
        {
            _anim.SetBool(IsJumping, true);
        }
        else
        {
            _anim.SetBool(IsJumping, false);
        }

        if (_player.GetFalling())
        {
            _anim.SetBool(IsFalling, true);
        }
        else
        {
            _anim.SetBool(IsFalling, false);
        }
    }
}
