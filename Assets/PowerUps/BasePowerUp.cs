using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace PowerUps
{
    public class BasePowerUp
    {
        public GameController gameController;
        public float powerUpTimeLimit;
        public string powerUpName;
        public float scoreIncrease;


        public BasePowerUp(GameController gameController, string name, float scoreInc, float powerUpTimeLimit)
        {
            this.gameController = gameController;
            this.powerUpName = name;
            this.scoreIncrease = scoreInc;
            this.powerUpTimeLimit = powerUpTimeLimit;
        }
        // Update is called once per frame

        public string GetName()
        {
            return this.powerUpName;
        }

        public float GetTimeLimit()
        {
            return this.powerUpTimeLimit;
        }
        

    }
}




