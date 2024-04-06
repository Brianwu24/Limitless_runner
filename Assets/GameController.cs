using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    // Start is called before the first frame update
    private float _totalTime;
    private float _speed = 1;

    public float GetSpeed()
    {
        return this._speed;
    }
    
    // void Start()
    // {
    // }

    // Update is called once per frame
    void Update()
    {
        _totalTime += Time.deltaTime;
        if (_totalTime < 20)
        {
            _speed += Time.deltaTime / 10;
        }
    }
}
