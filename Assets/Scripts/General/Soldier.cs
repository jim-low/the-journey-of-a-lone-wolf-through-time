using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Soldier : MonoBehaviour
{
    private const float MAX_HEALTH = 100f;

    [SerializeField]
    private float health = MAX_HEALTH;

    public TextMeshPro floatingText;

    void Update()
    {
        if (health <= 0) {
            Die();
        }
    }

    public static void Damage(Soldier soldier, float damage)
    {
        /* floatingText.text = ("- " + damage); */
        soldier.health -= damage;
    }

    public static void Heal(Soldier soldier, float healAmount)
    {
        /* floatingText.text = ("+ " + healAmount); */
        soldier.health += healAmount;
    }

    private void Die()
    {
        Debug.Log("lmao, yuo is die");
        // play die animation here
    }
}
