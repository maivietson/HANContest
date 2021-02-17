using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelicopterManager : MonoBehaviour
{
    [SerializeField]
    private GameObject helicopterPrefab;

    private float hightPosition = 3.97f;
    private float lowPosition = 2.93f;

    [SerializeField]
    private float timeRepeat = 1.0f;

    [SerializeField]
    private float spawnRate = 1.5f;

    // Start is called before the first frame update
    void Start()
    {
        StartSpawning();
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.Instance.IsGameOver)
        {
            StopSpawning();
        }
    }

    void SpawnHelicopter()
    {
        float randomPos = Random.Range(0, 2) == 0 ? hightPosition : lowPosition;
        Vector3 positionInit = new Vector3(transform.position.x, randomPos, transform.position.z);
        GameObject helicopter = Instantiate(helicopterPrefab, positionInit, Quaternion.identity) as GameObject;
        helicopter.name = "Helicopter";
        if (gameObject.transform.name == "InstanceHelicopterLeft")
        {
            helicopter.transform.localScale = new Vector3(1f, 1f, 1f);
        }
        else
        {
            helicopter.transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        helicopter.transform.parent = gameObject.transform;
    }

    public void StartSpawning()
    {
        InvokeRepeating("SpawnHelicopter", timeRepeat, spawnRate);
    }

    public void StopSpawning()
    {
        CancelInvoke("SpawnHelicopter");
    }
}
