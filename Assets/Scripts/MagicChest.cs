using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicChest : MonoBehaviour
{

    public GameObject collectedApple1;
    public GameObject collectedEffect;
    public GameObject playerDialogue3, vortexEffect;
    public Transform vortexEffectSpawn;
    public bool isAppleCollected = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("MagicApple"))
        {
            Destroy(collision.gameObject, 2f);
            StartCoroutine(FirstApple());
        }
    }

    IEnumerator FirstApple()
    {
        yield return new WaitForSeconds(2f);
        collectedApple1.SetActive(true);
        Instantiate(collectedEffect, transform.position, transform.rotation);
        playerDialogue3.GetComponent<DialogueTrigger>().TriggerDialogue();
        Instantiate(vortexEffect, vortexEffectSpawn.position, vortexEffectSpawn.rotation);
        FindObjectOfType<AudioManager>().Play("PortalOpen");
        isAppleCollected = true;
    }
}
