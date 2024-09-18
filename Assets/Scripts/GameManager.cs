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

    [SerializeField] TextMeshProUGUI healthText;
    [SerializeField] TextMeshProUGUI ammoText;

    void Awake()
    {
        healthInfo = player.GetComponent<PlayerHealth>();
        weaponInfo = player.GetComponent<PlayerWeapon>();

        healthText.text = $"Health: {healthInfo.GetMaxHealth()}";
        ammoText.text = $"Ammo: {weaponInfo.GetMaxAmmo()}";
    }

    void Update()
    {
        healthText.text = $"Health: {healthInfo.GetCurrentHealth()}";
        ammoText.text = $"Ammo: {weaponInfo.GetCurrentAmmo()}";

        if (healthInfo.GetCurrentHealth() <= 0)
        {
            Death();
        }
    }

    void Death()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
