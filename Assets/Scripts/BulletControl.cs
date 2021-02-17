using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletControl : MonoBehaviour
{
    Camera mainCam;
    
    private Vector3 moveDirection;
    private bool isFire = false;
    private bool isStartInit = false;
    private Quaternion lastRotation;

    [SerializeField]
    private GameObject bulletPrefab;

    [SerializeField]
    private float speed = 0.5f;

    [SerializeField]
    private float timeDelay = 2.0f;

    void Awake()
    {
        mainCam = Camera.main;
    }

    void Start()
    {
        //StartCoroutine("StartInstance");
    }

    // Update is called once per frame
    void Update()
    {
        if (!isFire) RotationBulletWithMouse();
        if (Input.GetMouseButtonDown(0) && !isFire)
        {
            isFire = true;
            CaculatePositionOfBullet();
            //ReinitBullet();
            //StartCoroutine("StartInstance");
            FindObjectOfType<BulletManager>().setStartInit(true);
        }

        MoveBullet();
    }

    private void MoveBullet()
    {
        transform.position = transform.position + moveDirection * speed * Time.deltaTime;
        transform.rotation = lastRotation;
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    private void CaculatePositionOfBullet()
    {
        moveDirection = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position);
        moveDirection.z = 0;
        moveDirection.Normalize();
    }

    private void RotationBulletWithMouse()
    {
        float objectDepthFromCamera = Vector3.Dot(
                    transform.position - mainCam.transform.position,
                    mainCam.transform.forward);

        Vector3 cursorWorldPosition = mainCam.ScreenToWorldPoint(Input.mousePosition
                + Vector3.forward * objectDepthFromCamera);

        transform.rotation = Quaternion.LookRotation(Vector3.forward,
                cursorWorldPosition - transform.position);

        lastRotation = transform.rotation;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "helicopter")
        {
            Vector2 death = collision.gameObject.transform.position;
            collision.gameObject.GetComponent<Helicopter>().Die(death);
        }
    }
}
