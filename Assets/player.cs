using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update

    public float gravity;
    
    public float velocityY;         

    public bool grounded;
    public bool inAir;

    public bool holdJump;
    public float maxHoldJumpTime = 0.4f;
    public float jumpHoldTimer;

    public float jumpGrounddistance = 1; // so people can jump just before hitting the ground so it isn't clunky

    
    //Brian's code starts here
    public GameObject powerManager;
    private PowerUpManger _powerManager;

    // private BoxCollider2D _playerCollider;
    void Start()
    {
        _powerManager = powerManager.GetComponent<PowerUpManger>();
        // _playerCollider = GetComponent<Collider>();
    }
    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Platform"))
        {
            grounded = true;
        }
    }

    public void OnCollisionStay(Collision other)
    {
        if (other.gameObject.CompareTag("Platform"))
        {
            // Debug.Log("Grounded");
            grounded = true;
        }
    }

    public void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag("Platform"))
        {
            // Debug.Log("Not grounded");
            grounded = false;
            inAir = true;
        }
    }

    // private void GetPowerUp()
    // {
    //     //O(1) time complexity check
    //     GameObject nearestPowerUp = _powerManager.GetClosestPowerUp();
    //     // if (nearestPowerUp.Item2.tag)
    //     Debug.Log(this._playerCollider.OnCol);
    //     // Debug.Log(_playerCollider.);
    //
    //
    // }
    // Ends here
    
    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (grounded)
            {
                holdJump = true;
                jumpHoldTimer = 0;
            }
        }

        if (Input.GetKey(KeyCode.Space) && holdJump && grounded)
        {
            jumpHoldTimer += Time.deltaTime;
        }

        if (Input.GetKeyUp(KeyCode.Space) && grounded)
        {
            holdJump = false;
            if (jumpHoldTimer > 3)
            {
                jumpHoldTimer = 3;
            }
            velocityY = 5 + jumpHoldTimer;
            transform.Translate(Vector3.up * 0.1f);
            grounded = false;
            inAir = true;
        }

        if (grounded && inAir)
        {
            inAir = false;
            velocityY = 0;
        }
        
        //Check for PowerUpCollision
        // GetPowerUp();
        
    }

    private void FixedUpdate()
    {
        // Vector2 position = transform.position;
        // if (grounded)
        // {
        //     accelerationY = 0;
        //     velocityY = 0;
        //     
        // }
        // if (velocityY != 0)
        // {
        if (inAir)
        {
            transform.Translate(Vector3.up * ((Time.deltaTime * velocityY) + (0.75f * gravity * MathF.Pow(Time.deltaTime , 2))));
        }
            
        // }
    //
    //     if (!grounded)
    //     {
    //
    //         position.y += velocity.y * Time.fixedDeltaTime; // changing the player position every frame
    //         if (!holdJump)
    //         {
    //             velocity.y += gravity * Time.fixedDeltaTime; // changing how much the y axis changes per frame
    //         }
    //
    //         if (position.y <= groundHeight)
    //         {
    //             position.y = groundHeight;
    //             grounded = true;
    //         }
    //
    //         if (holdJump)
    //         {
    //             jumpTimer += Time.fixedDeltaTime;
    //             if (jumpTimer >= maxHoldJumpTime)
    //             {
    //                 holdJump = false;
    //             }
    //         }
    //     }
    //
    //
        // transform.position = position;
    }

}