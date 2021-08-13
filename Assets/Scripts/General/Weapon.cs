using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    private Transform weapon;
    public Transform firePoint;
    public LineRenderer bulletLine;

    void Awake()
    {
        weapon = transform.Find("Weapon");
    }

    void Update()
    {
        Aim();
        if (Input.GetMouseButtonDown(0)) {
            StartCoroutine(Shoot());
        }
    }

    void Aim()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 aimDirection = (mousePos - transform.position).normalized;
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        weapon.eulerAngles = new Vector3(0, 0, angle);
        /* Debug.Log(angle); */
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
    }
}
