using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] int sceneIndex;
    [SerializeField] int tutorialIndex;

    public void StartGame()
    {
        SceneManager.LoadScene(sceneIndex);
    }

    public void StartTutorial()
    {
        SceneManager.LoadScene(tutorialIndex);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
