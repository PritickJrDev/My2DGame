using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    public GameObject magicApple1;
    public Transform spawnApplePoint;
    public GameObject magicDoll;
    public Transform spawnMagicDollPoint;

    bool firstAppleIsSpawned = true;
    bool firstAppleIsDestroyed;

    public GameObject dollHitEffect;

    private Transform targetMagicChest, magicAppleTransform;
    private Rigidbody2D magicAppleRb;
    bool canMoveApple, canSpawnApple, isAppleActive;

    private void Awake()
    {
        //magicAppleRb = GameObject.FindGameObjectWithTag("MagicApple").GetComponent<Rigidbody2D>();
        //magicAppleTransform = GameObject.FindGameObjectWithTag("MagicApple").GetComponent<Transform>();
        targetMagicChest = GameObject.FindGameObjectWithTag("MagicChest").GetComponent<Transform>();
    }

    private void FixedUpdate()
    {
        if (canMoveApple)
        {
            AddMagicApple();
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && canSpawnApple == true)
        {
            isAppleActive = true;
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("MagicApple"))
        {
            canMoveApple = true;
            collision.gameObject.GetComponent<CircleCollider2D>().isTrigger = true;
            firstAppleIsDestroyed = true;
        }

        if(collision.gameObject.CompareTag("Portal") && firstAppleIsDestroyed == true)
        {
            GameObject myCar = Instantiate(magicDoll, spawnMagicDollPoint.position, Quaternion.identity);
            myCar.GetComponent<Rigidbody2D>().velocity = Vector2.right * 5;
            firstAppleIsDestroyed = false;
        }

        if (collision.gameObject.CompareTag("MagicDoll"))
        {
            Instantiate(dollHitEffect, collision.gameObject.transform.position, collision.gameObject.transform.rotation);
            Destroy(collision.gameObject);
        }

        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Portal"))
        {
            canSpawnApple = true;
            Debug.Log("Entered");
        }

        if (collision.gameObject.CompareTag("Portal") && firstAppleIsSpawned == true && isAppleActive)
        {
                Instantiate(magicApple1, spawnApplePoint.position, Quaternion.identity);
                magicAppleRb = GameObject.FindGameObjectWithTag("MagicApple").GetComponent<Rigidbody2D>();
                magicAppleTransform = GameObject.FindGameObjectWithTag("MagicApple").GetComponent<Transform>();
                firstAppleIsSpawned = false;
            
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Portal"))
        {
            canSpawnApple = false;
            Debug.Log("Exited");
        }
    }

    void AddMagicApple()
    {
        Vector2 direction = (Vector2)targetMagicChest.position - magicAppleRb.position;
        direction.Normalize();
        float rotateAmount = Vector3.Cross(direction, magicAppleTransform.transform.up).z;
        magicAppleRb.angularVelocity = -rotateAmount * 800f;
        magicAppleRb.velocity = magicAppleTransform.transform.up * 10f;

    }
}
