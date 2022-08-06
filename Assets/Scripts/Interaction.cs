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

    private void Update()
    {
      //  MagicCar();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Portal") && firstAppleIsSpawned == true)
        {
            Instantiate(magicApple1, spawnApplePoint.position, Quaternion.identity);
            firstAppleIsSpawned = false;
        }

        if (collision.gameObject.CompareTag("MagicApple"))
        {
            Destroy(collision.gameObject);
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
            Destroy(collision.gameObject);
        }
    }

    void MagicCar()
    {
        if(firstAppleIsSpawned == false)
        {
          
        }
    }
}
