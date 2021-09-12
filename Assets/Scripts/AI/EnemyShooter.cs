using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooter : MonoBehaviour
{
    public float damage = 3;
    public Transform firePoint;
    public float recoilTime = 0.125f;
    [SerializeField] private AudioSource shoot;

    public int ammo = 30;
    private const int MAX_AMMO = 30;
    private const float RELOAD_TIME = 3f;
    private Enemy enemy;
    private LineRenderer bulletLinePrefab;
    private Transform player;

    bool reloading = false;
    bool canShoot = true;

    void Start()
    {
        enemy = GetComponent<Enemy>();
        player = GameObject.Find("Player").GetComponent<Transform>();
        bulletLinePrefab = GameObject.Find("BulletLine").GetComponent<LineRenderer>();
    }

    void Update()
    {
        if (enemy.hasDetectedPlayer && canShoot && !reloading) {
            StartCoroutine(Shoot());
        }
    }

    IEnumerator Reload()
    {
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

        shoot.Play();

        RaycastHit2D[] hitInfo = Physics2D.RaycastAll(firePoint.position, (player.position - firePoint.position));

        Vector2 bulletDestination = Vector2.zero;
        foreach (RaycastHit2D hitObject in hitInfo) {
            Debug.Log(hitObject.collider.name);
            Debug.Log(hitObject.collider.tag);
            if (hitObject.collider.name.Equals("Obstacle") || hitObject.collider.CompareTag("Obstacle")) {
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
        }
        if (bulletDestination == Vector2.zero) {
            yield break;
        }

        canShoot = false;
        yield return new WaitForSeconds(recoilTime);

        LineRenderer bulletLine = Instantiate(bulletLinePrefab);
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

