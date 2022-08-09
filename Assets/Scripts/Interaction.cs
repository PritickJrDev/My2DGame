using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    public GameObject magicApple1;
    public Transform spawnApplePoint;
    public GameObject magicDoll;
    public Transform spawnMagicDollPoint;
    bool changeApplePos = false;

    bool firstAppleIsSpawned = true;
    bool firstAppleIsDestroyed;

    public GameObject dollHitEffect;

    private Transform targetMagicChest, magicAppleTransform;
    private Rigidbody2D magicAppleRb;
    bool canMoveApple, canSpawnApple, isAppleActive;

    public GameObject inputE;
    bool eActive;

    public GameObject playerDialogue1;
    public GameObject playerDialogue2;
    public GameObject playerDialogue4;

    private void Awake()
    {
        //magicAppleRb = GameObject.FindGameObjectWithTag("MagicApple").GetComponent<Rigidbody2D>();
        //magicAppleTransform = GameObject.FindGameObjectWithTag("MagicApple").GetComponent<Transform>();
        targetMagicChest = GameObject.FindGameObjectWithTag("MagicChest").GetComponent<Transform>();
       
    }

    private void Start()
    {
        playerDialogue1.GetComponent<DialogueTrigger>().TriggerDialogue();
    }

    private void FixedUpdate()
    {
        if (canMoveApple && magicAppleRb != null && !changeApplePos)
        {
            RotateAppleAroundPlayer();
           // AddMagicApple();
            StartCoroutine(AppleToChest());
        }
        
        if(canMoveApple && magicAppleRb != null && changeApplePos)
        {
            AddMagicApple();
        }
    }

    IEnumerator AppleToChest()
    {
        yield return new WaitForSeconds(2f);
        changeApplePos = true;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && canSpawnApple == true)
        {
            isAppleActive = true;
        }

        if (eActive)
        {
            inputE.SetActive(false);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("MagicApple"))
        {
            FindObjectOfType<Movement>().moveSpeed = 0;
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
            gameObject.GetComponent<Movement>().playerCanMove = false;
            canMoveApple = true;
            collision.gameObject.GetComponent<CircleCollider2D>().isTrigger = true;
            firstAppleIsDestroyed = true;
            StartCoroutine(MovePlayerAgain());
        }


        if (collision.gameObject.CompareTag("MagicDoll"))
        {
            Instantiate(dollHitEffect, collision.gameObject.transform.position, collision.gameObject.transform.rotation);
            Destroy(collision.gameObject);
            StartCoroutine(OpenDialogue4());
        }

        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("MagicDollTrigger") && firstAppleIsDestroyed == true)
        {
            GameObject myDoll = Instantiate(magicDoll, spawnMagicDollPoint.position, Quaternion.identity);
            myDoll.GetComponent<Rigidbody2D>().velocity = Vector2.right * 7;
            firstAppleIsDestroyed = false;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Portal"))
        {
            canSpawnApple = true;
            if (!eActive)
            {
                inputE.SetActive(true);
            }
        }

        if (collision.gameObject.CompareTag("Portal") && firstAppleIsSpawned == true && isAppleActive)
        {
            Instantiate(magicApple1, spawnApplePoint.position, Quaternion.identity);
            magicAppleRb = GameObject.FindGameObjectWithTag("MagicApple").GetComponent<Rigidbody2D>();
            magicAppleTransform = GameObject.FindGameObjectWithTag("MagicApple").GetComponent<Transform>();
            firstAppleIsSpawned = false;
            eActive = true;
            StartCoroutine(OpenDiaglogue2());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Portal"))
        {
            canSpawnApple = false;
            canSpawnApple = true;
            if (!eActive)
            {
                inputE.SetActive(false);
            }
        }
    }

    void RotateAppleAroundPlayer()
    {
        Vector2 direction = (Vector2)gameObject.transform.position - magicAppleRb.position;
        direction.Normalize();
        float rotateAmount = Vector3.Cross(direction, magicAppleTransform.transform.up).z;
        magicAppleRb.angularVelocity = -rotateAmount * 800f;
        magicAppleRb.velocity = magicAppleTransform.transform.up * 10f;
    }

    void AddMagicApple()
    {
        Vector2 direction = (Vector2)targetMagicChest.position - magicAppleRb.position;
        direction.Normalize();
        float rotateAmount = Vector3.Cross(direction, magicAppleTransform.transform.up).z;
        magicAppleRb.angularVelocity = -rotateAmount * 800f;
        magicAppleRb.velocity = magicAppleTransform.transform.up * 10f;
    }

    IEnumerator MovePlayerAgain()
    {
        yield return new WaitForSeconds(8f);
        gameObject.GetComponent<SpriteRenderer>().flipX = false;
        gameObject.GetComponent<Movement>().playerCanMove = true;
    }
    

    IEnumerator OpenDiaglogue2()
    {
        yield return new WaitForSeconds(2f);
        FindObjectOfType<Movement>().moveSpeed = 0;
        playerDialogue2.GetComponent<DialogueTrigger>().TriggerDialogue();
    }

    IEnumerator OpenDialogue4()
    {
        yield return new WaitForSeconds(1f);
        FindObjectOfType<Movement>().moveSpeed = 0;
        playerDialogue4.GetComponent<DialogueTrigger>().TriggerDialogue();
    }

}
