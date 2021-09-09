using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee : MonoBehaviour
{

    private Enemy enemy;
    private static float knifeDamage = 20f;
    private static float cooldown = 1.8f;

    private void Start()
    {
        enemy = GetComponent<Enemy>();

    }

    public IEnumerator Attack(Soldier soldier)
    {
        if (!enemy.hasDetectedPlayer)
        {
            yield break;
        }

        yield return new WaitForSeconds(cooldown);
        if (!enemy.hasDetectedPlayer)
        {
            yield break;
        }
        soldier.Damage(knifeDamage);
        StartCoroutine(Attack(soldier));
    }
}