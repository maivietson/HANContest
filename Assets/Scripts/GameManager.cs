using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    #region Singleton class: GameManager
    public static GameManager Instance;

    private void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("game");

        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }

        if (Instance == null)
        {
            Instance = this;
        }

        DontDestroyOnLoad(this.gameObject);
    }
    #endregion

    private bool isGameOver;
    private int _score;

    [SerializeField] private Text _text;

    public bool IsGameOver
    {
        get { return isGameOver; }
        set { isGameOver = value; }
    }

    public int Score
    {
        get { return _score; }
        set { _score = value; }
    }

    void Start()
    {
        isGameOver = false;
        _score = 0;
    }

    public void AddScore()
    {
        _score++;
        _text.text = _score.ToString();
    }
}
