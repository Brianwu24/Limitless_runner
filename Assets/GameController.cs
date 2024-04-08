using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    // Start is called before the first frame update
    private float _totalTime;
    private float _speed = 3;

    public float gravity;

    public float GetSpeed()
    {
        return this._speed;
    }

    public void SetSpeed(float newSpeed)
    {
        if (newSpeed >= 0)
        {
            this._speed = newSpeed;
        }
    }

    public float GetTotalTime()
    {
        return _totalTime;
    }

    // Update is called once per frame
    void Update()
    {
        _totalTime += Time.deltaTime;
        if (_totalTime < 10 && this._speed != 0)
        {
            _speed += Time.deltaTime / 7.5f;
        }
    }
}
