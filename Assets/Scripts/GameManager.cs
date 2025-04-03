using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject enemy;
    PlayerHealth healthInfo;
    PlayerWeapon weaponInfo;
    PlayerScore scoreInfo;

    [SerializeField] TextMeshProUGUI healthText;
    [SerializeField] Slider healthSlider;
    [SerializeField] TextMeshProUGUI ammoText;
    [SerializeField] GameObject lowAmmoText;
    [SerializeField] TextMeshProUGUI currentWeaponText;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI waveCounter;
    int currentWave;
    int maxWave = 20;
    bool newWeapon;

    float enemyStartHealth = 30f;
    float enemyStartDamage = 25f;
    float enemyStartPoints = 10f;

    [SerializeField] GameObject crosshair;
    [SerializeField] GameObject shotgunCrosshair;
    [SerializeField] GameObject gameOverScreen;
    [SerializeField] GameObject victoryPanel;
    [SerializeField] GameObject deathPanel;
    [SerializeField] TextMeshProUGUI statsText;
    [SerializeField] CinemachineVirtualCamera vCamera;

    public int GetCurrentWave()
    {
        return currentWave;
    }

    void Awake()
    {
        healthInfo = player.GetComponent<PlayerHealth>();
        weaponInfo = player.GetComponent<PlayerWeapon>();
        scoreInfo = player.GetComponent<PlayerScore>();

        healthText.text = $"{healthInfo.GetMaxHealth()}%";
        healthSlider.value = healthInfo.GetMaxHealth();
        ammoText.text = $"{weaponInfo.GetMaxAmmo()}/{weaponInfo.GetMaxAmmo()}";
        lowAmmoText.SetActive(false);
        scoreText.text = "Score: 0";
        waveCounter.text = "Page 1";
        currentWave = 1;

        enemy.GetComponent<EnemyHealth>().SetMaxHealth(enemyStartHealth);
        enemy.GetComponent<EnemyController>().SetDamage(enemyStartDamage);
        enemy.GetComponent<EnemyHealth>().SetPoints(enemyStartPoints);

        crosshair.SetActive(true);
        shotgunCrosshair.SetActive(false);
        gameOverScreen.SetActive(false);
    }

    void Update()
    {
        healthText.text = $"{healthInfo.GetCurrentHealth()}%";
        healthSlider.value = healthInfo.GetCurrentHealth();
        ammoText.text = $"{weaponInfo.GetCurrentAmmo()}/{weaponInfo.GetMaxAmmo()}";
        if (weaponInfo.GetCurrentAmmo() <= (weaponInfo.GetMaxAmmo() / 4f))
        {
            lowAmmoText.SetActive(true);
        }
        else
        {
            lowAmmoText.SetActive(false);
        }

        scoreText.text = $"Score: {scoreInfo.GetScore()}";
        waveCounter.text = $"Page {currentWave}";

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
        UIManager.gamePaused = true;
        Time.timeScale = 0f;
        vCamera.enabled = false;
        Cursor.lockState = CursorLockMode.None;
    }

    public void UpdateWave()
    {
        if (currentWave < maxWave)
        {
            currentWave++;
            newWeapon = false;

            while (!newWeapon)
            {
                // Switch statement that randomly chooses a weapon for the player to use each wave
                // Also checks if the player gets the same weapon they already have, and will loop until they get a new weapon
                int randNum = Random.Range(0, 4);
                switch (randNum)
                {
                    case 0:
                        if (currentWeaponText.text != "Pistol")
                        {
                            weaponInfo.SwitchToPistol();
                            currentWeaponText.text = "Pistol";
                            newWeapon = true;
                        }
                        break;
                    case 1:
                        if (currentWeaponText.text != "Shotgun")
                        {
                            weaponInfo.SwitchToShotgun();
                            currentWeaponText.text = "Shotgun";
                            newWeapon = true;
                        }
                        break;
                    case 2:
                        if (currentWeaponText.text != "SMG")
                        {
                            weaponInfo.SwitchToSMG();
                            currentWeaponText.text = "SMG";
                            newWeapon = true;
                        }
                        break;
                    case 3:
                        if (currentWeaponText.text != "Rifle")
                        {
                            weaponInfo.SwitchToRifle();
                            currentWeaponText.text = "Rifle";
                            newWeapon = true;
                        }
                        break;
                    default:
                        Debug.Log("An error has occurred");
                        break;
                }
            }
        }
        else
        {
            victoryPanel.SetActive(true);
            deathPanel.SetActive(false);
            GameOver();
        }

        if (weaponInfo.GetUsingShotgun())
        {
            crosshair.SetActive(false);
            shotgunCrosshair.SetActive(true);
        }
        else
        {
            crosshair.SetActive(true);
            shotgunCrosshair.SetActive(false);
        }
    }

}
