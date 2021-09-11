using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooter : MonoBehaviour
{
    public float damage = 3;
    public Transform firePoint;
    public float recoilTime = 0.125f;

    public int ammo = 30;
    private const int MAX_AMMO = 30;
    private const float RELOAD_TIME = 3f;
    private Enemy enemy;
    private LineRenderer bulletLinePrefab;
    private Transform player;

    bool reloading = false;
    bool canShoot = true;

    float angle = 0;

    void Start()
    {
        enemy = GetComponent<Enemy>();
        player = GameObject.Find("Player").GetComponent<Transform>();
        bulletLinePrefab = GameObject.Find("BulletLine").GetComponent<LineRenderer>();
    }

    void LateUpdate()
    {
        if (enemy.hasDetectedPlayer && canShoot && !reloading) {
            StartCoroutine(Shoot());
        }
    }

    IEnumerator Reload()
    {
        if (reloading) {
            yield return null;
        }

        yield return new WaitForSeconds(RELOAD_TIME);
        ammo = MAX_AMMO;
        reloading = false;
        canShoot = true;
    }

    IEnumerator Shoot()
    {
        if (reloading || !canShoot) {
            yield break;
        }

        LineRenderer bulletLine = Instantiate(bulletLinePrefab);
        Vector2 playerPos = player.position;
        playerPos.y += 1.75f;
        RaycastHit2D[] hitInfo = Physics2D.RaycastAll(firePoint.position, playerPos);

        Vector2 bulletDestination = (firePoint.position + firePoint.right * 100);
        foreach (RaycastHit2D hitObject in hitInfo) {
            if (hitObject.collider.name.Equals("Obstacle")) {
                bulletDestination = hitObject.point;
                break;
            }
            else if (hitObject.collider.CompareTag("Player")) {
                bulletDestination = hitObject.transform.position;
                Soldier soldier = hitObject.collider.GetComponent<Soldier>();
                if (soldier) {
                    soldier.Damage(damage);
                }
                break;
            }
            else {
                bulletDestination = hitObject.point;
            }

        }
        canShoot = false;
        yield return new WaitForSeconds(recoilTime);

        bulletLine.SetPosition(0, firePoint.position);
        bulletLine.SetPosition(1, bulletDestination);

        bulletLine.enabled = true;
        yield return new WaitForSeconds(0.02f);
        bulletLine.enabled = false;

        Destroy(bulletLine.gameObject);

        canShoot = true;
        --ammo;

        if (ammo <= 0) {
            canShoot = false;
            reloading = true;
            StartCoroutine(Reload());
        }
    }
}

