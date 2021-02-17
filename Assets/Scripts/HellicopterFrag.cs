using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HellicopterFrag : MonoBehaviour
{
    #region Singleton class : HellicopterFrag
    public static HellicopterFrag Instance;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }
    #endregion

    private bool _isDestroy;

    public bool IsDestroy
    {
        get { return _isDestroy; }
        set { _isDestroy = value; }
    }

    private void Update()
    {
        if(_isDestroy)
        {
            StartCoroutine(WaitToDestroy());
        }
    }

    private IEnumerator WaitToDestroy()
    {
        yield return new WaitForSeconds(2.5f);
        Destroy(gameObject);
    }
}
