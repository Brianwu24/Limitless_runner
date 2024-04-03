using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = Unity.Mathematics.Random;

public class Sky
{
    private GameController _gameController;
    private Random _rng;
    
    private List<GameObject> _backgroundsMain;
    private List<GameObject> _backgroundsOver;
    private List<GameObject> _clouds;

    private Vector3 _updateIncBack;
    private Vector3 _updateIncCloud;
    public Sky(GameObject gameController, List<GameObject> backgroundsMain, List<GameObject> backgroundsOver, List<GameObject> clouds)
    {
        this._rng = new Random(1234);
        this._gameController = gameController.GetComponent<GameController>();
        _updateIncBack = new Vector3(x: this._gameController.GetSpeed(), y: 0, z:0);
        _updateIncCloud = new Vector3(x: this._gameController.GetSpeed(), y: 0, z:0);

        _backgroundsMain = backgroundsMain;
        _backgroundsOver = backgroundsOver;
        _clouds = clouds;
    }

    private void UpdateInc()
    {
        this._updateIncBack.x = 0.5f * this._gameController.GetSpeed() * Time.deltaTime;
        
        this._updateIncCloud.x = this._gameController.GetSpeed() * Time.deltaTime;
        this._updateIncCloud.y = 0.25f * MathF.Sin(Time.time) * Time.deltaTime;
    }
    

    public void UpdateSky()
    {
        this.UpdateInc();
        for (int i = 0; i < 3; i++)
        {
            this._backgroundsMain[i].transform.position -= this._updateIncBack;
            
            if (this._backgroundsMain[i].transform.position.x <= -2 * 8.85)
            {
                Vector3 resetPos = new Vector3(33.2f - this._rng.NextFloat(0, 2.4f), 0, this._backgroundsMain[i].transform.position.z);
                this._backgroundsMain[i].transform.position = resetPos;
            }
            
            this._backgroundsOver[i].transform.position -= this._updateIncBack;
            if (this._backgroundsOver[i].transform.position.x <= -2 * 8.9f)
            {
                Vector3 resetPos = new Vector3(4 * 8.9f, 0, this._backgroundsOver[i].transform.position.z);
                this._backgroundsOver[i].transform.position = resetPos;
            }
            
            this._clouds[i].transform.position -= this._updateIncCloud; 
            if (this._clouds[i].transform.position.x <= -2 * 8.85)
            {
                Vector3 resetPos = new Vector3(33.2f - this._rng.NextFloat(0, 2.4f), 0, this._clouds[i].transform.position.z);
                this._clouds[i].transform.position = resetPos;
            }
        }
        
    }
    
}


public class Buildings : MonoBehaviour
{
    private GameController _gameController;
    private Random _rng;
    
    private List<GameObject> _refBuildings;

    private List<GameObject> _gameBuildings;
    // private List<GameObject> _backgroundsOver;
    // private List<GameObject> _clouds;
    //
    private Vector3 _updateInc;
    public Buildings(GameObject gameController, List<GameObject> buildings)
    {
        this._rng = new Random(1234);
        this._refBuildings = buildings;

        bool enoughBuildings = false;
        int maxRandomNum = this._refBuildings.Count;
        while (!enoughBuildings)
        {
            // _gameBuildings.Add(GameObject.Instantiate()));
        }
        //     this._gameController = gameController.GetComponent<GameController>();
        //     _updateIncBack = new Vector3(x: this._gameController.GetSpeed(), y: 0, z:0);
        //     _updateIncCloud = new Vector3(x: this._gameController.GetSpeed(), y: 0, z:0);
        //
        //     _backgroundsMain = backgroundsMain;
        //     _backgroundsOver = backgroundsOver;
        //     _clouds = clouds;
    }

    private void UpdateInc()
    {
        this._updateInc.x = 0.5f * this._gameController.GetSpeed() * Time.deltaTime;
    }
    

    // public void UpdateSky()
    // {
    //     this.UpdateInc();
    //     for (int i = 0; i < 3; i++)
    //     {
    //         this._backgroundsMain[i].transform.position -= this._updateIncBack;
    //         
    //         if (this._backgroundsMain[i].transform.position.x <= -2 * 8.85)
    //         {
    //             Vector3 resetPos = new Vector3(33.2f - this._rng.NextFloat(0, 2.4f), 0, this._backgroundsMain[i].transform.position.z);
    //             this._backgroundsMain[i].transform.position = resetPos;
    //         }
    //         
    //         this._backgroundsOver[i].transform.position -= this._updateIncBack;
    //         if (this._backgroundsOver[i].transform.position.x <= -2 * 8.9f)
    //         {
    //             Vector3 resetPos = new Vector3(4 * 8.9f, 0, this._backgroundsOver[i].transform.position.z);
    //             this._backgroundsOver[i].transform.position = resetPos;
    //         }
    //         
    //         this._clouds[i].transform.position -= this._updateIncCloud; 
    //         if (this._clouds[i].transform.position.x <= -2 * 8.85)
    //         {
    //             Vector3 resetPos = new Vector3(33.2f - this._rng.NextFloat(0, 2.4f), 0, this._clouds[i].transform.position.z);
    //             this._clouds[i].transform.position = resetPos;
    //         }
    //     }
    //     
    // }
    //
}


public class BackgroundController : MonoBehaviour
{
    public GameObject gameController;

    public GameObject backgroundOpac;

    public GameObject backgroundMain;

    public GameObject cloud;

    public GameObject building1;

    public float mul;


    private Random _rng;

    private Sky _sky;
    void Start()
    {
        // this._rng = new Random(1234);
        //
        // List <GameObject> backgroundsMain = new List<GameObject>();
        // List <GameObject> backgroundsOpac = new List<GameObject>();
        // List <GameObject> clouds = new List<GameObject>();
        //
        // for (int i = 0; i < 3; i++)
        // {
        //     float randomStart = _rng.NextFloat(0, 2.4f);
        //     if (i == 0)
        //     {
        //         randomStart = 0;
        //     }
        //     
        //     backgroundsMain.Add(Instantiate(backgroundMain, new Vector3(2 * i * 8.9f - randomStart, 0, 10), Quaternion.identity, this.transform));
        //     backgroundsOpac.Add(Instantiate(backgroundOpac, new Vector3(2 * i * 8.9f, 0, 8), Quaternion.identity, this.transform));
        //     clouds.Add(Instantiate(cloud,new Vector3(2 * i * 8.9f - randomStart, 0, 9), Quaternion.identity, this.transform));
        // }
        //
        // _sky = new Sky(gameController: gameController,
        //     backgroundsMain: backgroundsMain,
        //     backgroundsOver: backgroundsOpac,
        //     clouds: clouds);
        
        List < GameObject > instantiatedBuildings1 = new List<GameObject>();
        Debug.Log(building1.transform);
        
        int i = 0;
        float prevPosition = -8.9f;
        foreach (Transform child in building1.transform)
        {
            float xBounds = child.gameObject.GetComponent<Renderer>().bounds.size.x / 2f;
            prevPosition += xBounds;
            GameObject building = Instantiate(child.gameObject, new Vector3(prevPosition, 0, 0), Quaternion.identity,
                this.transform);
            building.GetComponent<Renderer>().enabled = false;
            instantiatedBuildings1.Add(building);
            
            prevPosition += xBounds;
            i++;
        }
    }

    // Update is called once per frame
    void Update()
    {
     // _sky.UpdateSky();   
     
    }
}
