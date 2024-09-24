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
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI waveCounter;
    int currentWave;

    float enemyStartHealth = 2f;
    float enemyStartDamage = 25f;

    [SerializeField] GameObject gameOverScreen;
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
        scoreText.text = "Score: 0";
        waveCounter.text = "Wave 1";
        currentWave = 1;

        enemy.GetComponent<EnemyHealth>().SetMaxHealth(enemyStartHealth);
        enemy.GetComponent<EnemyHealth>().SetDamage(enemyStartDamage);

        gameOverScreen.SetActive(false);
    }

    void Update()
    {
        healthText.text = $"{healthInfo.GetCurrentHealth()}%";
        healthSlider.value = healthInfo.GetCurrentHealth();
        ammoText.text = $"Ammo: {weaponInfo.GetCurrentAmmo()}/{weaponInfo.GetMaxAmmo()}";
        scoreText.text = $"Score: {scoreInfo.GetScore()}";
        waveCounter.text = $"Wave {currentWave}";

        if (healthInfo.GetCurrentHealth() <= 0)
        {
            Death();
        }
    }

    void Death()
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
        currentWave++;
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
