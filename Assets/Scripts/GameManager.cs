using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject player;
    PlayerWeapon weaponInfo;

    [SerializeField] TextMeshProUGUI ammoText;

    void Awake()
    {
        weaponInfo = player.GetComponent<PlayerWeapon>();

        ammoText.text = $"Ammo: {weaponInfo.GetMaxAmmo()}";
    }

    void Update()
    {
        ammoText.text = $"Ammo: {weaponInfo.GetCurrentAmmo()}";
    }
}
