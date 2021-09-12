using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Gun : MonoBehaviour
{
    private Transform firePoint;
    private Transform weaponPosition;
    private SpriteRenderer weaponSprite;
    private LineRenderer bulletLine;

    [SerializeField] private AudioSource gunSound;
    [SerializeField] private int MAX_AMMO = 30;

    public int ammo;
    public float damage = 15f;
    public float reloadTime = 1.25f;
    public float firingRate = 0.2f;

    public static TextMeshProUGUI reloadText;
    public static TextMeshProUGUI ammoInfoText;

    bool hasAmmo = true;
    bool needReload = false;
    bool canShoot = true;

    void Start()
    {
        ammo = MAX_AMMO;
        weaponPosition = GetComponent<Transform>();
        weaponSprite = GetComponentInChildren<SpriteRenderer>();
        firePoint = GameObject.Find(gameObject.name + "FirePoint").transform;

        reloadText = GameObject.Find("ReloadText").GetComponent<TextMeshProUGUI>();
        ammoInfoText = GameObject.Find("AmmoText").GetComponent<TextMeshProUGUI>();
        bulletLine = GameObject.Find("BulletLine").GetComponent<LineRenderer>();

        Object.DontDestroyOnLoad(bulletLine);
    }

    void Update()
    {
        if (GameController.paused) {
            return;
        }

        Aim();
        UpdateWeapon();

        if (canShoot && hasAmmo && Input.GetMouseButton(0)) {
            gunSound.Play();
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
            reloadText.text = "";
            ammo = MAX_AMMO;
            hasAmmo = true;
            canShoot = true;
        }
    }

    void Aim()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 aimDirection = (mousePos - transform.position).normalized;
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;

        weaponSprite.flipX = weaponSprite.flipY = Mathf.Abs(angle) > 90;

        Vector3 scale = weaponSprite.transform.localScale;
        scale.x = Mathf.Abs(angle) > 90 ? -Mathf.Abs(scale.x) : Mathf.Abs(scale.x);
        weaponSprite.transform.localScale = scale;

        weaponPosition.eulerAngles = new Vector3(0, 0, angle);
    }

    IEnumerator Shoot()
    {
        if (!canShoot) {
            yield break;
        }

        canShoot = false;

        RaycastHit2D[] hitInfo = Physics2D.RaycastAll(firePoint.position, firePoint.right);

        LineRenderer bulletLinePrefab = Instantiate(bulletLine);
        bulletLinePrefab.SetPosition(0, firePoint.position);

        Vector2 bulletDestination = (firePoint.position + (firePoint.right * 100));
        foreach (RaycastHit2D hitObject in hitInfo) {
            if (hitObject.collider.name.Equals("Obstacle") || hitObject.collider.CompareTag("Obstacle")) {
                bulletDestination = hitObject.point;
                break;
            }
            else if (hitObject.collider.name.Equals("EnemyMedic") || hitObject.collider.CompareTag("Enemy")) {
                bulletDestination = hitObject.collider.transform.position;
                Soldier soldier = hitObject.collider.gameObject.GetComponent<Soldier>();
                if (soldier) {
                    soldier.Damage(damage);
                }
                break;
            }
        }

        bulletLinePrefab.SetPosition(1, bulletDestination);

        bulletLinePrefab.enabled = true;
        yield return new WaitForSeconds(0.02f);
        bulletLinePrefab.enabled = false;

        Destroy(bulletLinePrefab.gameObject);
        --ammo;

        StartCoroutine(Recoil());
    }

    IEnumerator Recoil()
    {
        yield return new WaitForSeconds(firingRate);
        canShoot = true;
    }

    void UpdateWeapon()
    {
        if (ammo <= 0) {
            needReload = true;
            hasAmmo = false;
            canShoot = false;
        }

        ammoInfoText.text = ammo + " / " + MAX_AMMO;
    }
}

