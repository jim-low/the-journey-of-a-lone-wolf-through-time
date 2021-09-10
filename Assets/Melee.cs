using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee : MonoBehaviour
{
    public Animator animator;
    private Enemy enemy;
    private static float knifeDamage = 20f;
    private static float cooldown = 1f;

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
        animator.SetBool("isAttacking", true);
        if (!enemy.hasDetectedPlayer)
        {
            yield break;
        }
        yield return new WaitForSeconds(cooldown);
        animator.SetBool("isAttacking", false);
        soldier.Damage(knifeDamage);
        StartCoroutine(Attack(soldier));
    }
}