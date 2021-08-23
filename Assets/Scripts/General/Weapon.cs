using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Weapon : MonoBehaviour
{
    public int ammo = 30;
    public float reloadTime = 2f;

    public Transform firePoint;
    public LineRenderer bulletLine;
    public TextMeshProUGUI reloadText;
    public TextMeshProUGUI ammoText;

    private Transform weaponPosition;
    private SpriteRenderer weaponSprite;

    const int MAX_AMMO = 30;
    bool hasAmmo = true;
    bool needReload = false;

    void Awake()
    {
        weaponPosition = GameObject.Find("Rifle").GetComponent<Transform>();
        weaponSprite = GameObject.Find("Rifle").GetComponentInChildren<SpriteRenderer>();
    }

    void Update()
    {
        Aim();

        UpdateWeapon();

        if (hasAmmo && Input.GetMouseButtonDown(0)) {
            StartCoroutine(Shoot());
        }

        if (needReload || Input.GetKeyDown(KeyCode.R)) {
            StartCoroutine(Reload());
        }
    }

    IEnumerator Reload()
    {
        if (ammo < MAX_AMMO) {
            needReload = false;
            reloadText.text = "Reloading...";
            yield return new WaitForSeconds(reloadTime);
            ammo = MAX_AMMO;
            hasAmmo = true;
            reloadText.text = "";
        }
    }

    void Aim()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 aimDirection = (mousePos - transform.position).normalized;
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;

        weaponSprite.flipY = Mathf.Abs(angle) > 90;
        weaponPosition.eulerAngles = new Vector3(0, 0, angle);
    }

    IEnumerator Shoot()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(firePoint.position, firePoint.right);

        bulletLine.SetPosition(0, firePoint.position);

        if (hitInfo) {
            bulletLine.SetPosition(1, hitInfo.point);
        }
        else {
            bulletLine.SetPosition(1, firePoint.position + firePoint.right * 100);
        }

        bulletLine.enabled = true;
        yield return new WaitForSeconds(0.02f);
        bulletLine.enabled = false;

        --ammo;
    }

    void UpdateWeapon()
    {
        if (ammo <= 0) {
            needReload = true;
            hasAmmo = false;
        }

        ammoText.text = ammo + " / " + MAX_AMMO;
    }
}
