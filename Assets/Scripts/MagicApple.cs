using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicApple : MonoBehaviour
{

    public GameObject magicTrail;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            FindObjectOfType<AudioManager>().Play("Drop");
        }

        if (collision.gameObject.CompareTag("Player"))
        {
            FindObjectOfType<AudioManager>().Play("OrbFly");
            magicTrail.SetActive(true);
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("MagicChest"))
        {
            FindObjectOfType<AudioManager>().Play("OrbImpact");
        }
    }
}
