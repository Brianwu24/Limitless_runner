using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject gameController;
    private GameController _gameController;
    
    private Rigidbody2D _rigidBody2d;
    
    private float _initialX;
        

    public bool _grounded;
    public bool _isFalling;

    public bool holdJump;
    public float jumpHoldTimer;
    

    private bool isDead;
    //Brian's code starts here
    public GameObject powerManager;
    private PowerUpManger _powerManager;
    
    void Start()
    {
        _gameController = gameController.GetComponent<GameController>();
        _powerManager = powerManager.GetComponent<PowerUpManger>();
        _rigidBody2d = GetComponent<Rigidbody2D>();

        _initialX = transform.position.x;
    }
    public void OnCollisionEnter2D(Collision2D other)
    {

        if (other.gameObject.CompareTag("Enemy"))
        {
            isDead = true;
        }
    }

    // Ends here
    public bool GetJumping()
    {
        return !this._grounded;
    }

    public bool GetFalling()
    {
        return _isFalling;
    }
    
    // Update is called once per frame
    void Update()
    {
        //Check if the player is in the abyss
        if (this.transform.position.y <= -10)
        {
            isDead = true;
        }
        //If so then stop the game by setting speed to 0
        if (isDead)
        {
            _gameController.SetSpeed(0);
        }

        if (transform.position.x < _initialX)
        {
            transform.Translate(Vector3.right * (Time.deltaTime * _gameController.GetSpeed()));
        }
        
        // Activate Jump
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (this._grounded)
            {
                holdJump = true;
                jumpHoldTimer = 0;
            }
        }

        if (Input.GetKey(KeyCode.Space) && holdJump && this._grounded)
        {
            jumpHoldTimer += Time.deltaTime;
        }
        
        if (Input.GetKeyUp(KeyCode.Space) && this._grounded)
        {
            holdJump = false;
            if (jumpHoldTimer > 3)
            {
                jumpHoldTimer = 3;
            }

            _rigidBody2d.AddForce(transform.up * (7 + 0.25f * jumpHoldTimer), ForceMode2D.Impulse);
        }

        if (_rigidBody2d.velocity.y < 0)
        {
            this._isFalling = true;
        }
    }

    private void FixedUpdate()
    {
        Vector3 updatePos = new Vector3(0, -0.05f, 0);
        RaycastHit2D hit =  Physics2D.Raycast(transform.position + updatePos, -Vector2.up);

        if (hit.collider != null && hit.collider.CompareTag("Platform"))
        {
            float distance = MathF.Abs(hit.point.y - transform.position.y);
            if (distance - 0.075 <= 0)
            {
                _grounded = true;
            }
            else
            {
                _grounded = false;
            }
        }
        else
        {
            _grounded = false;
        }
    }





}