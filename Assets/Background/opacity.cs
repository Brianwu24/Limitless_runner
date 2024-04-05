using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class opacity : MonoBehaviour
{
    private SpriteRenderer cloudsRenderer; 
    // Start is called before the first frame update
    void Start()
    {
        cloudsRenderer = GetComponent<SpriteRenderer>();
        cloudsRenderer.color = new Color(1f, 1f, 1f, 0.4f);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
