using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    // Start is called before the first frame update
    private float _totalTime;
    private float _speed = 3;
    private float _score;

    public float gravity;
    

    public float GetSpeed()
    {
        return this._speed;
    }

    public void SetSpeed(float newSpeed)
    {
        if (newSpeed < 0)
        {
            throw new Exception("New Speed cannot be less then 0");
        }
        this._speed = newSpeed;
    }

    public float GetTotalTime()
    {
        return _totalTime;
    }

    public float GetScore()
    {
        return _score;
    }

    public void AddToScore(float scoreInc)
    {
        if (scoreInc < 0)
        {
            throw new Exception($"Can't decrease player score, make sure that the score increase is not negative");
        }

        _score += scoreInc;
    }
    
    

    // Update is called once per frame
    void Update()
    {
        _totalTime += Time.deltaTime;
        if (_totalTime < 10 && this._speed != 0)
        {
            float dt = Time.deltaTime;
            _speed += dt / 7.5f;
            _score += dt;
        }
    }
}
