using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject player;
    PlayerHealth healthInfo;
    PlayerWeapon weaponInfo;
    PlayerScore scoreInfo;

    [SerializeField] TextMeshProUGUI healthText;
    [SerializeField] TextMeshProUGUI ammoText;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI waveCounter;
    int currentWave;

    void Awake()
    {
        healthInfo = player.GetComponent<PlayerHealth>();
        weaponInfo = player.GetComponent<PlayerWeapon>();
        scoreInfo = player.GetComponent<PlayerScore>();

        healthText.text = $"Health: {healthInfo.GetMaxHealth()}%";
        ammoText.text = $"Ammo: {weaponInfo.GetMaxAmmo()}/{weaponInfo.GetMaxAmmo()}";
        scoreText.text = "Score: 0";
        waveCounter.text = "Wave 1";
        currentWave = 1;
    }

    void Update()
    {
        healthText.text = $"Health: {healthInfo.GetCurrentHealth()}%";
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
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void UpdateWave()
    {
        currentWave++;
    }
}
