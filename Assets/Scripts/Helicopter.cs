using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Helicopter : MonoBehaviour
{
    [SerializeField]
    private float speed = 5.0f;

    [SerializeField]
    private GameObject helicopterFragPrefab;

    [SerializeField]
    private GameObject soliderPrefab;

    private bool isDestroy = false;
    private Animator myAnimator;
    private Rigidbody2D myRigidbody2D;

    private Vector3 lastPosition;

    private bool stopFly = false;

    public float thrust = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        myAnimator = GetComponent<Animator>();
        myRigidbody2D = GetComponent<Rigidbody2D>();

        StartCoroutine("StartSolider");
    }

    // Update is called once per frame
    void Update()
    {
        if(!stopFly)
        {
            if (transform.parent.transform.name == "InstanceHelicopterLeft")
            {
                transform.position = new Vector3(transform.position.x + speed * Time.deltaTime, transform.position.y);
            }
            else
            {
                transform.position = new Vector3(transform.position.x - speed * Time.deltaTime, transform.position.y);
            }

            lastPosition = transform.position;
        }
    }

    IEnumerator StartSolider()
    {
        bool hasSolider = Random.Range(1, 8) < 5 ? false : true;
        if(hasSolider)
        {
            float range = Random.Range(1, 3);
            yield return new WaitForSeconds(range);
            InitSolider();
        }
    }

    private void InitSolider()
    {
        if(transform.position.x <= -2.0f || transform.position.x >= 2.0f )
        {
            Instantiate(soliderPrefab, transform.position, Quaternion.identity);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "limitX")
        {
            Destroy(gameObject);
        }

        if(collision.tag == "bullet")
        {
            Destroy(collision.gameObject);
        }
    }

    public void Die(Vector2 deathKick)
    {
        stopFly = true;
        myAnimator.SetTrigger("Dying");
        StartCoroutine("WaitDying");
        GameObject helicopter = Instantiate(helicopterFragPrefab, lastPosition, Quaternion.identity) as GameObject;
        helicopter.GetComponent<HellicopterFrag>().IsDestroy = true;
        //hf.IsDestroy = true;
        helicopter.gameObject.GetComponent<Rigidbody2D>().AddForce(deathKick * thrust);
    }

    IEnumerator WaitDying()
    {
        yield return new WaitForSeconds(0.5f);
        SwapHelicopter();
    }

    private void SwapHelicopter()
    {
        Destroy(gameObject);
    }
}
