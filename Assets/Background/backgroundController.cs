using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class backgroundController : MonoBehaviour
{
    public GameObject backgroundOpac;

    public GameObject backgroundMain;

    public GameObject clouds;
    // Start is called before the first frame update
    void Start()
    {
        Instantiate(backgroundOpac,new Vector3(0, 0, -2), Quaternion.identity, this.transform);
        Instantiate(backgroundMain,new Vector3(0, 0, -2), Quaternion.identity, this.transform);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
