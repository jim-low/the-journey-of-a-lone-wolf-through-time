using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Gun : MonoBehaviour
{
    private Transform firePoint;
    private Transform weaponPosition;
    private SpriteRenderer weaponSprite;

    [SerializeField]
    private int MAX_AMMO = 30;

    public int ammo;
    public float damage = 15f;
    public float reloadTime = 1.25f;
    public float firingRate = 0.2f;
    public LineRenderer bulletLine;

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
    }

    void Update()
    {
        Aim();
        UpdateWeapon();

        if (canShoot && hasAmmo && Input.GetMouseButton(0)) {
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

        weaponSprite.flipX = Mathf.Abs(angle) > 90;
        weaponSprite.flipY = Mathf.Abs(angle) > 90;

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

        RaycastHit2D[] hitInfo = Physics2D.RaycastAll(firePoint.position, firePoint.right);

        bulletLine.SetPosition(0, firePoint.position);

        Vector2 bulletDestination = (Vector2)(firePoint.position + firePoint.right * 100);

        foreach (RaycastHit2D hitObject in hitInfo) {
            if (hitObject.collider.name.Equals("Obstacle")) {
                bulletDestination = hitObject.point;
                break;
            }
            else if (hitObject.collider.name.Equals("EnemyMedic") || hitObject.collider.CompareTag("Enemy")) {
                bulletDestination = hitObject.point;
                hitObject.collider.gameObject.GetComponent<Soldier>().Damage(damage);
                break;
            }
            else {
                bulletDestination = (Vector2)hitObject.point;
            }
        }

        bulletLine.SetPosition(1, bulletDestination);
        int randomDigits = Random.Range(-1, 1);
        bulletLine.SetPosition(1, new Vector2(bulletDestination.x , bulletDestination.y + randomDigits));

        canShoot = false;

        bulletLine.enabled = true;
        yield return new WaitForSeconds(0.02f);
        bulletLine.enabled = false;

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

