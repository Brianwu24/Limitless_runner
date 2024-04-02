using System;
using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using Unity.VisualScripting;
using UnityEditor.Overlays;
using UnityEngine;

public class Sky
{
    private GameController _gameController;
    private GameObject _backgroundMain;
    private GameObject _backgroundOver;
    private GameObject _clouds;

    private Vector3 _updateIncBack;
    private Vector3 _updateIncCloud;
    public Sky(GameObject gameController,Transform parentTransform, GameObject backgroundMain, GameObject backgroundOver, GameObject clouds)
    {
        this._gameController = gameController.GetComponent<GameController>();
        _updateIncBack = new Vector3(x: this._gameController.GetSpeed(), y: 0, z:0);
        _updateIncCloud = new Vector3(x: this._gameController.GetSpeed(), y: 0, z:0);

        _backgroundMain = backgroundMain;
        // _backgroudMain = Instantiate(backgroundOver,new Vector3(0, 0, 8), Quaternion.identity, parentTransform);
        // _clouds = Instantiate(clouds, new Vector3(0, 0, 9), Quaternion.identity, parentTransform);
    }

    private void UpdateInc()
    {
        this._updateIncBack.x = this._gameController.GetSpeed() * Time.deltaTime;
        
        this._updateIncCloud.x = this._gameController.GetSpeed() * Time.deltaTime;
        this._updateIncCloud.y = 0.5f * MathF.Sin(Time.time) * Time.deltaTime;
    }

    public void UpdateSky()
    {
        this.UpdateInc();
        _backgroundMain.transform.position += this._updateIncBack;
    }
    
}
public class backgroundController : MonoBehaviour
{
    public GameObject gameController;

    public GameObject backgroundOpac;

    public GameObject backgroundMain;

    public GameObject clouds;
    // Start is called before the first frame update

    private Sky sky1;
    void Start()
    {
        
        
        // GameObject back1_ = Instantiate(backgroundOpac,new Vector3(0, 0, 8), Quaternion.identity, this.transform);
        // Instantiate(backgroundMain,new Vector3(0, 0, 10), Quaternion.identity, this.transform);
        // Instantiate(clouds,new Vector3(0, 0, 9), Quaternion.identity, this.transform);
        sky1 = new Sky(gameController: gameController,
            parentTransform: this.transform,
            backgroundMain: Instantiate(backgroundMain, new Vector3(0, 0, 10), Quaternion.identity, this.transform),
            backgroundOver: Instantiate(backgroundOpac, new Vector3(0, 0, 8), Quaternion.identity, this.transform),
            clouds:Instantiate(clouds,new Vector3(0, 0, 9), Quaternion.identity, this.transform));
    }

    // Update is called once per frame
    void Update()
    {
     sky1.UpdateSky();   
    }
}
