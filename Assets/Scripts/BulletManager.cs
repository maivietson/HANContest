using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    [SerializeField]
    private GameObject bulletPrefab;

    [SerializeField]
    private float timeDelay = 2.0f;

    private bool isStartInit = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!GameManager.Instance.IsGameOver)
        {
            if (isStartInit)
            {
                StartCoroutine("StartInstance");
            }
        }
    }

    IEnumerator StartInstance()
    {
        isStartInit = false;
        yield return new WaitForSeconds(timeDelay);
        ReinitBullet();
    }

    private void ReinitBullet()
    {
        bulletPrefab.name = "bullet";
        Instantiate(bulletPrefab, new Vector3(0, -3.34f, 0), Quaternion.identity);
    }

    public void setStartInit(bool value)
    {
        isStartInit = value;
    }

}
