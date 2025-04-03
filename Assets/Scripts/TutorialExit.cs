using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialExit : MonoBehaviour
{
    [SerializeField] int levelIndex;
    bool nearBarrier;

    void OnDestroy()
    {
        if (nearBarrier && !UIManager.gamePaused)
        {
            SceneManager.LoadSceneAsync(levelIndex);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            nearBarrier = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            nearBarrier = false;
        }
    }
}
