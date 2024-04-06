using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private static readonly float removalXPosition = -10f;

    
    private void Update()
    {
        // Move the enemy downwards
        transform.Translate(Vector3.down * Time.deltaTime);
        transform.Translate(Vector3.left * (3f * Time.deltaTime));

        // Check if the enemy is past the left border, remove it if true
        if (transform.position.x < removalXPosition)
        {
            Destroy(gameObject);
        }
    }
    

}