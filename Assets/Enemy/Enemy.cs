using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // public GameObject gameController;
    private GameController _gameController;

    private float _gravity;
    private float _velocityY;
    

    private bool _grounded;

    public void Start()
    {
        _gameController = GameObject.Find("GameController").GetComponent<GameController>();
        _gravity = _gameController.gravity;
    }
    
    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Platform"))
        {
            _grounded = true;
            
        }
    }

    public void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Platform"))
        {
            _grounded = true;
            
        }
    }

    public void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Platform"))
        {
            _grounded = false;
        }
    }


    private void FixedUpdate()
    {
        transform.Translate(Vector3.left * (1.25f * _gameController.GetSpeed() * Time.deltaTime));
        // Move the enemy downwards
        if (_grounded)
        {

            _velocityY = 5;
            transform.Translate(Vector3.up * 0.05f);
            _grounded = false;
        
        }
        
        if (!_grounded)
        {
            transform.Translate(Vector3.up *
                                ((Time.deltaTime * _velocityY) + (0.75f * _gravity * MathF.Pow(Time.deltaTime, 2))));
        }

        // Check if the enemy is past the left border, remove it if true
        if (transform.position.x < -10)
        {
            Destroy(gameObject);
        }
    }
    

}