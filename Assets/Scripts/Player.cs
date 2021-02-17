using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    private Animator myAnimator;

    // Start is called before the first frame update
    void Start()
    {
        myAnimator = GetComponent<Animator>();
        myAnimator.enabled = false;
    }

    public void Die()
    {
        myAnimator.enabled = true;
        GameOver();
        StartCoroutine("WaitDying");
    }

    private static void GameOver()
    {
        GameManager.Instance.IsGameOver = true;
        GameObject bullet = GameObject.FindGameObjectWithTag("bullet");
        Destroy(bullet);
    }

    IEnumerator WaitDying()
    {
        yield return new WaitForSeconds(2.0f);
        SceneManager.LoadScene("GameOver");
    }
}
