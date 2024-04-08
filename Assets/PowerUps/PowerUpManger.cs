using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = Unity.Mathematics.Random;


//Spawns Infinite voids once in a white
public class PowerUpManger : MonoBehaviour
{

    public GameObject infiniteVoid;

    private float _timePassed;
    private Random _rng;

    private float _timeTillNextPowerUp;
    // Start is called before the first frame update
    
    void Start()
    {
        _rng = new Random((uint)UnityEngine.Random.Range(1, 100000));
        _timeTillNextPowerUp = _rng.NextFloat(10, 30);
        Instantiate(infiniteVoid, new Vector3(7f, 6, 0), Quaternion.identity, this.transform);
        _timePassed = 0;
    }
    
    // Update is called once per frame
    void Update()
    {
        if (_timePassed >= _timeTillNextPowerUp)
        {
            Instantiate(infiniteVoid, new Vector3(5f + _rng.NextFloat(-2f, 2f), 6, 0), Quaternion.identity, this.transform);
            _timePassed = 0;
            _timeTillNextPowerUp = _rng.NextFloat(10, 30);
        }

        _timePassed += Time.deltaTime;
    }
}
