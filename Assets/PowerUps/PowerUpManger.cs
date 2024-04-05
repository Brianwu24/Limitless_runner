using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpManger : MonoBehaviour
{
    public GameObject circle;

    private Queue<GameObject> _powerUps;
    // Start is called before the first frame update
    void Start()
    {
        _powerUps = new Queue<GameObject>();
        GameObject powerup = Instantiate(circle, new Vector3(-2, 1, 0), Quaternion.identity, this.transform);
        _powerUps.Enqueue(powerup);
    }

    public GameObject GetClosestPowerUp()
    {
        return _powerUps.Peek();
    }

    public void DestroyClosestPowerUp()
    {
        Destroy(_powerUps.Dequeue());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
