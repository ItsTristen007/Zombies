using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static bool gamePaused;
    [SerializeField] GameObject player;
    [SerializeField] CinemachineVirtualCamera vCamera;
    [SerializeField] GameObject pauseScreen;
    int mainMenuIndex = 0;

    void Awake()
    {
        gamePaused = false;
        Time.timeScale = 1f;

        vCamera.enabled = true;
        pauseScreen.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !gamePaused)
        {
            Pause();
        }
    }

    void LockCamera()
    {
        vCamera.enabled = false;
    }

    void UnlockCamera()
    {
        vCamera.enabled = true;
    }

    void Pause()
    {
        gamePaused = true;
        Time.timeScale = 0f;
        pauseScreen.SetActive(true);
        LockCamera();
        Cursor.lockState = CursorLockMode.None;
    }

    public void Resume()
    {
        gamePaused = false;
        Time.timeScale = 1f;
        pauseScreen.SetActive(false);
        UnlockCamera();
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(mainMenuIndex);
    }
}
