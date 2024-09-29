using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static bool gamePaused;
    [SerializeField] GameObject player;
    [SerializeField] GameObject enemy;
    PlayerHealth healthInfo;
    PlayerWeapon weaponInfo;
    PlayerScore scoreInfo;

    [SerializeField] TextMeshProUGUI healthText;
    [SerializeField] Slider healthSlider;
    [SerializeField] TextMeshProUGUI ammoText;
    [SerializeField] GameObject lowAmmoText;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI waveCounter;
    int currentWave;

    float enemyStartHealth = 2f;
    float enemyStartDamage = 25f;
    float enemyStartPoints = 10f;

    [SerializeField] GameObject gameOverScreen;
    [SerializeField] GameObject victoryPanel;
    [SerializeField] GameObject deathPanel;
    [SerializeField] TextMeshProUGUI statsText;
    int mainMenuIndex = 0;

    public int GetCurrentWave()
    {
        return currentWave;
    }

    void Awake()
    {
        gamePaused = false;
        Time.timeScale = 1.0f;

        healthInfo = player.GetComponent<PlayerHealth>();
        weaponInfo = player.GetComponent<PlayerWeapon>();
        scoreInfo = player.GetComponent<PlayerScore>();

        healthText.text = $"{healthInfo.GetMaxHealth()}%";
        healthSlider.value = healthInfo.GetMaxHealth();
        ammoText.text = $"Ammo: {weaponInfo.GetMaxAmmo()}/{weaponInfo.GetMaxAmmo()}";
        lowAmmoText.SetActive(false);
        scoreText.text = "Score: 0";
        waveCounter.text = "Wave 1";
        currentWave = 1;

        enemy.GetComponent<EnemyHealth>().SetMaxHealth(enemyStartHealth);
        enemy.GetComponent<EnemyHealth>().SetDamage(enemyStartDamage);
        enemy.GetComponent<EnemyHealth>().SetPoints(enemyStartPoints);

        gameOverScreen.SetActive(false);
    }

    void Update()
    {
        healthText.text = $"{healthInfo.GetCurrentHealth()}%";
        healthSlider.value = healthInfo.GetCurrentHealth();
        ammoText.text = $"Ammo: {weaponInfo.GetCurrentAmmo()}/{weaponInfo.GetMaxAmmo()}";
        if (weaponInfo.GetCurrentAmmo() <= 3)
        {
            lowAmmoText.SetActive(true);
        }
        else
        {
            lowAmmoText.SetActive(false);
        }

        scoreText.text = $"Score: {scoreInfo.GetScore()}";
        waveCounter.text = $"Wave {currentWave}";

        if (healthInfo.GetCurrentHealth() <= 0)
        {
            deathPanel.SetActive(true);
            victoryPanel.SetActive(false);
            GameOver();
        }
    }

    void GameOver()
    {
        healthInfo.SetCurrentHealth(0);
        gameOverScreen.SetActive(true);
        statsText.text = $"Waves Survived: {currentWave}\nFinal Score: {scoreInfo.GetScore()}";
        gamePaused = true;
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
    }

    public void UpdateWave()
    {
        if (currentWave < 30)
        {
            currentWave++;
        }
        else
        {
            victoryPanel.SetActive(true);
            deathPanel.SetActive(false);
            GameOver();
        }
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
