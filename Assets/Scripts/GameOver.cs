using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    [SerializeField] private Text _text;

    private GameObject _gameManager;

    private void Awake()
    {
        _gameManager = GameObject.Find("GameManager");
    }

    private void Start()
    {
        _text.text = _gameManager.GetComponent<GameManager>().Score.ToString();
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene("MainScreen");
    }
}
