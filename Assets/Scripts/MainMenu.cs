using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Animator transitionAnim;
    public string sceneName;

    public void SwitchScene()
    {
        Debug.Log("switching");
        StartCoroutine(LoadLevel());
    }

    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();        
    }

    IEnumerator LoadLevel()
    {
        transitionAnim.SetTrigger("end");
        yield return new WaitForSeconds(2.5f);
        SceneManager.LoadScene(sceneName);
    }
}
