using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Solider : MonoBehaviour
{
    private Animator myAnimator;

    private bool isFlying = true;
    private bool isRunning = false;

    private Rigidbody2D myRigidbody2D;

    [SerializeField]
    private GameObject target;

    [SerializeField]
    private float speedFly = 0.5f;

    [SerializeField]
    private float speedRun = 0.01f;


    private SpriteRenderer mySpriteRenderer;

    private float lastCollider;
    private bool isGround;
    private bool isCollider;
    private Vector3 lastVelocity;

    private float thrust = 5.0f;

    void Start()
    {
        myAnimator = GetComponent<Animator>();
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        myRigidbody2D = GetComponent<Rigidbody2D>();

        isGround = true;
        isCollider = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(!GameManager.Instance.IsGameOver)
        {
            lastVelocity = myRigidbody2D.velocity;

            if(!isCollider)
            {
                if (isFlying)
                {
                    Flying();
                }
                else
                {
                    myAnimator.SetBool("Flying", false);
                }
                if (isRunning)
                {
                    Running();
                }
            }
            else
            {
                StartCoroutine(DestroySolider());
            }
        }
    }

    private void Flying()
    {
        myAnimator.SetBool("Flying", isFlying);
        transform.Translate(new Vector3(0, -Time.deltaTime * speedFly, 0));
    }

    private void Running()
    {
        myAnimator.SetBool("Running", isRunning);

        Vector3 relativePoint = transform.InverseTransformPoint(target.transform.position);
        float move = relativePoint.x * speedRun;
        float movement = Mathf.Clamp(move, -0.01f, 0.01f);
        if (relativePoint.x < 0.0)
        {
            mySpriteRenderer.flipX = true;
        }
        transform.Translate(new Vector3(movement, 0, 0));
    }

    private IEnumerator DestroySolider()
    {
        yield return new WaitForSeconds(2.0f);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "ground")
        {
            isFlying = false;
            isRunning = true;
        }

        if (collision.gameObject.tag == "player")
        {
            FindObjectOfType<Player>().Die();
        }

        if(collision.gameObject.tag == "bullet")
        {
            isCollider = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "ground")
        {
            isFlying = false;
            isRunning = true;
        }

        if(collision.gameObject.tag == "player")
        {
            FindObjectOfType<Player>().Die();
        }

        if (collision.gameObject.tag == "bullet")
        {
            myRigidbody2D.AddForce(collision.transform.up * thrust, ForceMode2D.Impulse);
            Destroy(collision.gameObject);
            isCollider = true;

            GameManager.Instance.AddScore();
        }
    }
}
