using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialExit : MonoBehaviour
{
    [SerializeField] int levelIndex;

    void OnDestroy()
    {
        SceneManager.LoadScene(levelIndex);
    }
}
