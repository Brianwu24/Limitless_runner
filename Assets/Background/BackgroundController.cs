// using System;

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using MathF = System.MathF;
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
    public Sky(GameController gameController, List<GameObject> backgroundsMain, List<GameObject> backgroundsOver, List<GameObject> clouds)
    {
        _rng = new Random((uint)UnityEngine.Random.Range(1, 100000));
        
        _gameController = gameController;
        _updateIncBack = new Vector3(x: this._gameController.GetSpeed(), y: 0, z:0);
        _updateIncCloud = new Vector3(x: this._gameController.GetSpeed(), y: 0, z:0);

        _backgroundsMain = backgroundsMain;
        _backgroundsOver = backgroundsOver;
        _clouds = clouds;
    }

    private void UpdateInc()
    {
        this._updateIncBack.x = 0.1f * this._gameController.GetSpeed() * Time.deltaTime;
        
        this._updateIncCloud.x = 0.15f * this._gameController.GetSpeed() * Time.deltaTime;
        this._updateIncCloud.y = 0.2f * MathF.Sin(Time.time) * Time.deltaTime;
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
                Vector3 resetPos = new Vector3(4 * 8.925f, 0, this._backgroundsOver[i].transform.position.z);
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


public class Buildings
{
    private GameController _gameController;
    private Transform _parentTransform;
    private Random _rng;
    
    private List<GameObject> _buildings;
    private int _refBuildingCount;

    private float z;
    private float y;
    private float speedMul;
    
    private Vector3 _updateInc;
    public Buildings(float y, float z, float speedMul, GameController gameController, Transform parentTransform, Transform buildingsTransform)
    {
        _buildings = new List<GameObject>();
        foreach (Transform child in buildingsTransform)
        {
            _buildings.Add(child.gameObject);
        }
        this.z = z;
        this.y = y;
        this.speedMul = speedMul;
        
        
        _gameController = gameController;
        _parentTransform = parentTransform;
        
        _rng = new Random((uint)UnityEngine.Random.Range(1, 100000));
        
        _refBuildingCount = this._buildings.Count;
        float prevPosition = -8.9f;

        while (true)
        {
            int randomNum = this._rng.NextInt(0, this._refBuildingCount);
            GameObject chosenBuilding = _buildings[randomNum];
            float xSize = chosenBuilding.GetComponent<Renderer>().bounds.size.x / 2f;
            prevPosition += xSize;
            GameObject copiedBuilding = GameObject.Instantiate(chosenBuilding, new Vector3(prevPosition, this.y - _rng.NextFloat(0f, 0.15f), this.z), Quaternion.identity, this._parentTransform);
            _buildings.Add(copiedBuilding);


            prevPosition += xSize;
            if (prevPosition >= 20)
            {
                break;
            }
        }
    }

    public void UpdateSpeedMul(float newSpeedMul)
    {
        if (speedMul <= 0)
        {
            throw new Exception($"Can make speed multplier less than 0");
        }
        this.speedMul = newSpeedMul;
    }

    private void UpdateInc()
    {
        this._updateInc.x = this.speedMul * this._gameController.GetSpeed() * Time.deltaTime;
    }
    

    public void UpdateBuildings()
    {
        UpdateInc();
        bool destroyLastBuilding = false;
        foreach (GameObject building in this._buildings.Skip(this._refBuildingCount))
        {
            building.transform.position -= this._updateInc;
            
            if (building.transform.position.x <= -13)
            {
                destroyLastBuilding = true;
            }
        }

        if (destroyLastBuilding)
        {
            //Remove the building from the list and destroy it 
            GameObject lastBuilding = this._buildings[this._refBuildingCount];
            this._buildings.Remove(lastBuilding);
            GameObject.Destroy(lastBuilding);
            
            //Make a new building and add it to the end of the list along with making sure that the x coord is at the end
            // Get the basic info for multiple calls
            int randomChoice = _rng.NextInt(0, this._refBuildingCount);
            GameObject chosenNewBuilding = _buildings[randomChoice];
            float xSize = chosenNewBuilding.GetComponent<Renderer>().bounds.size.x;
            
            // Get the last building for the new update position 
            GameObject firstBuilding = _buildings.Last();
            Vector3 newPos = new Vector3(firstBuilding.transform.position.x + (firstBuilding.GetComponent<Renderer>().bounds.size.x + xSize) / 2,
                this.y - _rng.NextFloat(0f, 0.15f), this.z); 
            GameObject newBuilding = GameObject.Instantiate(chosenNewBuilding, newPos, Quaternion.identity, this._parentTransform);
            _buildings.Add(newBuilding);
            destroyLastBuilding = false;
        }
    }
}


public class BackgroundController : MonoBehaviour
{
    public GameObject gameController;
    private GameController _gameController;
    
    private Random _rng;

    // Pertaining to sky
    public GameObject backgroundOpac;

    public GameObject backgroundMain;

    public GameObject cloud;
    private Sky _sky;
    
    // P{ertaining to buildings
    public GameObject buildingPrefab1;
    public GameObject buildingPrefab2;
    public GameObject buildingPrefab3;
    public GameObject buildingPrefab4;
    public GameObject buildingPrefab5;

    private List<Buildings> instantiatedBuildings;
    void Start()
    {
        this._rng = new Random((uint)UnityEngine.Random.Range(1, 100000));
        _gameController = gameController.GetComponent<GameController>();
        
        List <GameObject> backgroundsMain = new List<GameObject>();
        List <GameObject> backgroundsOpac = new List<GameObject>();
        List <GameObject> clouds = new List<GameObject>();
        
        for (int i = 0; i < 3; i++)
        {
            float randomStart = _rng.NextFloat(0, 2.4f);
            if (i == 0)
            {
                randomStart = 0;
            }
            
            backgroundsMain.Add(Instantiate(backgroundMain, new Vector3(2 * i * 8.9f - randomStart, 0, 10), Quaternion.identity, this.transform));
            backgroundsOpac.Add(Instantiate(backgroundOpac, new Vector3(2 * i * 8.925f, 0, 8), Quaternion.identity, this.transform));
            clouds.Add(Instantiate(cloud,new Vector3(2 * i * 8.9f - randomStart, 0, 9), Quaternion.identity, this.transform));
        }
        
        this._sky = new Sky(gameController: _gameController,
            backgroundsMain: backgroundsMain,
            backgroundsOver: backgroundsOpac,
            clouds: clouds);

        instantiatedBuildings = new List<Buildings>();
        Transform parentTransform = this.transform;
        instantiatedBuildings.Add(new Buildings(1f, 7, 0.25f, _gameController, parentTransform, buildingPrefab1.transform));
        instantiatedBuildings.Add(new Buildings(0.8f, 6, 0.35f, _gameController, parentTransform, buildingPrefab2.transform));
        instantiatedBuildings.Add(new Buildings(0.7f, 5, 0.45f, _gameController, parentTransform, buildingPrefab3.transform));
        instantiatedBuildings.Add(new Buildings(0.675f, 4, 0.55f, _gameController, parentTransform, buildingPrefab4.transform));
        instantiatedBuildings.Add(new Buildings(0.1f, 3, 0.75f, _gameController, parentTransform, buildingPrefab5.transform));
        
    }

    // Update is called once per frame
    void Update()
    {
     this._sky.UpdateSky();
     foreach (Buildings buildingClass in this.instantiatedBuildings)
     {
         buildingClass.UpdateBuildings(); 
     }
    }
}
