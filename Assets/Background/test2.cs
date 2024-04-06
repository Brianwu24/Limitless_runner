using System;
using System.Linq;
using System.Collections.Generic;


using UnityEngine;
using Random = Unity.Mathematics.Random;


public class Platforms
{
    private GameController _gameController;
    private Transform _parentTransform;
    private Transform _managerTransform; 
    private List<GameObject> _refPlatforms;
    private List<GameObject> _platforms;

    private Random _rng;
    private Vector3 _updateInc;
    

    public Platforms(GameController gameController, Transform parentTransform, Transform managerTransform)
    {

        _parentTransform = parentTransform;
        _gameController = gameController;
        _rng = new Random((uint)UnityEngine.Random.Range(1, 100000));

        _refPlatforms = new List<GameObject>();
        _platforms = new List<GameObject>();
        //Getting ref platforms

        Transform _managerTansform = managerTransform;
        
        
        foreach (Transform child in _parentTransform)
        {
            _refPlatforms.Add(child.gameObject);
        }

        //Generating initial plats
        float prevPosition = -8.9f;
        
        for (int i = 0; i < 20; i++)
        {
            int randomNum = this._rng.NextInt(0, this._refPlatforms.Count);
            
            GameObject chosenPlatform = _refPlatforms[randomNum];
            float xSize = chosenPlatform.GetComponent<Renderer>().bounds.size.x / 2f;
            prevPosition += xSize; // 
            GameObject newPlatform = GameObject.Instantiate(chosenPlatform, new Vector3(prevPosition + 0.1f, 0, 0), Quaternion.identity, _managerTansform);
            _platforms.Add(newPlatform);
            prevPosition += xSize;
        }
        
        
    }
    private void UpdateInc()
    {
        this._updateInc.x = 1.1f * this._gameController.GetSpeed() * Time.deltaTime;
    }
    
    public void UpdatePlatforms()
    {
        UpdateInc();
        bool isDestroyPlatform = false;
        foreach (GameObject platform in this._platforms)
        {
            platform.transform.position -= _updateInc;

            if (platform.transform.position.x <= -8.9)
            {
                isDestroyPlatform = true;
            }                
        }

        if (isDestroyPlatform)
        {
            GameObject badPlatform = this._platforms[0];
            _platforms.RemoveAt(0);
            GameObject.Destroy(badPlatform);
                
            int randomChoice = _rng.NextInt(0, this._refPlatforms.Count);
            GameObject chosenPlatform = _platforms[randomChoice];
            float xSize = chosenPlatform.GetComponent<Renderer>().bounds.size.x;
            
            // Get the last building for the new update position 
            GameObject firstBuilding = _platforms.Last();
            Vector3 newPos = new Vector3(firstBuilding.transform.position.x + (firstBuilding.GetComponent<Renderer>().bounds.size.x + xSize) / 2,
                0- _rng.NextFloat(0f, 0.15f), 0); 
            GameObject newBuilding = GameObject.Instantiate(chosenPlatform, newPos, Quaternion.identity, this._managerTransform);
            _platforms.Add(newBuilding);
        }
        
        
        
    }
    

}


public class test2 : MonoBehaviour
{
    public GameObject gameController;
    private GameController _gameController;

    public GameObject parent;

    private Platforms platfrom1;
    
    
    // Start is called before the first frame update
    void Start()
    {
        _gameController = gameController.GetComponent<GameController>();   
        platfrom1 = new Platforms(_gameController, parent.transform, this.transform);
    }

    // Update is called once per frame
    void Update()
    {
        platfrom1.UpdatePlatforms();
    }
}
