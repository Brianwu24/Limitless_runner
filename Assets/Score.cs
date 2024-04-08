using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public GameObject gameController;
    private GameController _gameController;

    public GameObject player;
    private Player _player;

    private Text _scoreText;
    // Start is called before the first frame update
    void Start()
    {
        _gameController = gameController.GetComponent<GameController>();
        _player = player.GetComponent<Player>();
        _scoreText = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!_player.CheckIfDead())
        {
            _scoreText.text = $"Score: {Mathf.RoundToInt(_gameController.GetScore())}";
        }
        
        
    }
}
