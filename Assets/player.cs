using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject gameController;
    private GameController _gameController;
    
    private Rigidbody2D _rigidBody2d;
    
    private float _initialX;
        

    private bool _isGrounded;
    private bool _isFalling;

    private bool _isJumping;
    
    private bool _holdJump;
    private float _jumpHoldTimer;
    

    private bool _isDead;
    //Brian's code starts here
    public GameObject powerManager;
    // private PowerUpManger _powerManager;
    
    void Start()
    {
        _gameController = gameController.GetComponent<GameController>();
        // _powerManager = powerManager.GetComponent<PowerUpManger>();
        _rigidBody2d = GetComponent<Rigidbody2D>();

        _initialX = transform.position.x;
    }
    public void OnCollisionEnter2D(Collision2D other)
    {

        if (other.gameObject.CompareTag("Enemy"))
        {
            _isDead = true;
        }
    }

    public bool GetGrounded()
    {
        return _isGrounded;
    }
    // Ends here
    public bool GetJumping()
    {
        return _isJumping;
    }

    public bool GetFalling()
    {
        return _isFalling;
    }

    public bool CheckIfDead()
    {
        return _isDead;
    }
    
    // Update is called once per frame
    void Update()
    {
        //Check if the player is in the abyss
        if (this.transform.position.y <= -10)
        {
            _isDead = true;
        }
        //If so then stop the game by setting speed to 0
        if (_isDead)
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
            if (this._isGrounded)
            {
                _holdJump = true;
                _jumpHoldTimer = 0;
            }
        }

        if (Input.GetKey(KeyCode.Space) && _holdJump && this._isGrounded)
        {
            _jumpHoldTimer += Time.deltaTime;
        }
        
        if (Input.GetKeyUp(KeyCode.Space) && this._isGrounded)
        {
            _holdJump = false;
            if (_jumpHoldTimer > 3)
            {
                _jumpHoldTimer = 3;
            }

            _rigidBody2d.AddForce(transform.up * (420 + 10f * _jumpHoldTimer), ForceMode2D.Impulse);
            _isJumping = true;
        }

        if (_rigidBody2d.velocity.y < 0)
        {
            this._isFalling = true;
        }
    }

    private void FixedUpdate()
    {
        // Since box collider was stupid and didn't work as intended, ray cast was my only option
        Vector3 updatePos = new Vector3(0, -0.05f, 0);
        RaycastHit2D hit =  Physics2D.Raycast(transform.position + updatePos, -Vector2.up);

        if (hit.collider != null && hit.collider.CompareTag("Platform"))
        {
            float distance = MathF.Abs(hit.point.y - transform.position.y);
            if (distance - 0.075 <= 0)
            {
                _isGrounded = true;
                _isJumping = false;
            }
            else
            {
                _isGrounded = false;
            }
        }
        else
        {
            _isGrounded = false;
        }
    }
}