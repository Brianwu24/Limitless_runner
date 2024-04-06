using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BasePowerUp
{
    private GameController _gameController;
    private string _name;
    private string _color;
    private float _scoreIncrease;

    public BasePowerUp(GameController gamecontroller, string color, string name, float score)
    {
        _gameController = gamecontroller;
        _name = name;
        _color = color;
        _scoreIncrease = score;
    }
    // Update is called once per frame

}

