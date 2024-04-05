using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update

    public float gravity;
    public Vector2 velocity;            // Vector2 is the X & Y positions
    public float jumpVelocity = 20;
    public float groundHeight = 10;
    public bool grounded;

    public bool holdJump;
    public float maxHoldJumpTime = 0.4f;
    public float jumpTimer;

    public float jumpGrounddistance = 1; // so people can jump just before hitting the ground so it isn't clunky

    
    //Brian's code starts here
    public GameObject powerManager;
    private PowerUpManger _powerManager;

    private Collider _playerCollider;
    void Start()
    {
        _powerManager = powerManager.GetComponent<PowerUpManger>();
        _playerCollider = GetComponent<Collider>();
    }
    
    private void GetPowerUp()
    {
        //O(1) time complexity check
        GameObject nearestPowerUp = _powerManager.GetClosestPowerUp();
        // if (nearestPowerUp.Item2.tag)
        // Debug.Log(nearestPowerUp.GetComponent<().tag);


    }
    // Ends here
    
    // Update is called once per frame
    void Update()
    {
        Vector2 position = transform.position;
        float groundDistance = Mathf.Abs(position.y - groundHeight);

        if (grounded || groundDistance <= jumpGrounddistance)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                grounded = false;
                velocity.y = jumpVelocity;
                holdJump = true;
                jumpTimer = 0;
            }
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            holdJump = false;
        }
        
        //Check for PowerUpCollision
        GetPowerUp();



    }

    private void FixedUpdate()
    {
        Vector2 position = transform.position;

        if (!grounded)
        {

            position.y += velocity.y * Time.fixedDeltaTime; // changing the player position every frame
            if (!holdJump)
            {
                velocity.y += gravity * Time.fixedDeltaTime; // changing how much the y axis changes per frame
            }

            if (position.y <= groundHeight)
            {
                position.y = groundHeight;
                grounded = true;
            }

            if (holdJump)
            {
                jumpTimer += Time.fixedDeltaTime;
                if (jumpTimer >= maxHoldJumpTime)
                {
                    holdJump = false;
                }
            }
        }


        transform.position = position;
    }

}