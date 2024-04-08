using System;
using PowerUps;
using UnityEngine;


public class InfiniteVoidPowerUp : BasePowerUp
{
    private float _slowDownMul;
    private float _originalSpeed;
    public InfiniteVoidPowerUp(GameController gameController, float scoreInc, float powerUpTimeLimit, float slowDownMul) :
        base(gameController, "InfiniteVoid", scoreInc, powerUpTimeLimit)
    {
        this._slowDownMul = slowDownMul;
        this._originalSpeed = this.gameController.GetSpeed();
    }

    public void AffectSpeed()
    {
        _originalSpeed = gameController.GetSpeed();
        gameController.SetSpeed(_originalSpeed * _slowDownMul);
    }

    public void ResetSpeed()
    {
        gameController.SetSpeed(_originalSpeed);
    }

    public void IncreaseScore()
    {
        gameController.AddToScore(scoreIncrease);
    }

}



class InfiniteVoid : MonoBehaviour
{
    private GameController _gameController;
    private SpriteRenderer _imageRendered;
    private InfiniteVoidPowerUp _powerUp;
    private Collider2D _powerUpCollider;
    
    private bool _used;
    private float _timePassed;
    private float _powerUpTimeLimit;
    public void Start()
    {
        _gameController = GameObject.Find("GameController").GetComponent<GameController>();
        _imageRendered = GetComponent<SpriteRenderer>();
        _powerUp = new InfiniteVoidPowerUp(_gameController, 5f, 10f, 0.75f);
        _powerUpCollider = this.GetComponent<Collider2D>();
        _powerUpTimeLimit = _powerUp.GetTimeLimit();
    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player") && !_used)
        {
            _powerUpCollider.isTrigger = false;
            _used = true;
            _imageRendered.enabled = false;
            _powerUp.AffectSpeed(); 
            _powerUp.IncreaseScore();
        }
    
    }

    public void Update()
    {
        transform.Translate(Vector3.left * (_gameController.GetSpeed() * Time.deltaTime));
        if (_used)
        {
            _timePassed += Time.deltaTime;
            if (_timePassed >= _powerUpTimeLimit)
            {
                _powerUp.ResetSpeed();
                Destroy(gameObject);
            }
        }
    }
}  


