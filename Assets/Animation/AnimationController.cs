using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    private Player _player;
    private Animator _anim;

    private static readonly int IsJumping = Animator.StringToHash("isJumping");

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
    }
}
