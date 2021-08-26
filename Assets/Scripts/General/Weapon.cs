using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Weapon : MonoBehaviour
{
    GameObject rifle;
    GameObject pistol;

    void Awake()
    {
        rifle = GameObject.Find("Rifle");
        pistol = GameObject.Find("Pistol");
        pistol.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F)) {
            SwitchGun();
        }
    }

    void SwitchGun()
    {
        // negate current rifle active value
        bool rifleActive = !rifle.activeInHierarchy;

        rifle.SetActive(rifleActive);
        pistol.SetActive(!rifleActive);
    }
}
