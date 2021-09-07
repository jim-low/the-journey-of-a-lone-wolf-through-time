using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Soldier : MonoBehaviour
{
    public static float moveSpeed = 5f;

    private const float MAX_HEALTH = 100f;
    private const float FADE_TIME = .5f;

    [SerializeField]
    private float health = MAX_HEALTH;

    private static GameObject floatingTextPrefab;
    private static GameObject popUpTexts;

    private Slider playerHealthSlider;

    bool healing = false;

    void Awake()
    {
        floatingTextPrefab = (GameObject)Resources.Load("FloatingText", typeof(GameObject));
        popUpTexts = GameObject.Find("PopUpTexts");
    }

    void Start()
    {
        playerHealthSlider = GameObject.Find("PlayerHealth").GetComponent<Slider>();
        playerHealthSlider.maxValue = playerHealthSlider.value = MAX_HEALTH;
    }

    void Update()
    {
        if (playerHealthSlider != null) {
            playerHealthSlider.value = health;
        }

        if (health <= 0) {
            Die();
        }
    }

    public void Damage(float damage)
    {
        health -= damage;

        // get object references
        GameObject floatingTextObject = Instantiate(floatingTextPrefab, transform.position, Quaternion.identity, popUpTexts.transform);
        TextMeshProUGUI floatingText = floatingTextObject.GetComponent<TextMeshProUGUI>();

        // set text and color
        floatingText.text = "-" + damage;
        floatingText.color = Color.red;

        // start countdown to destroy text
        StartCoroutine(DestroyText(floatingTextObject));
    }

    public IEnumerator Heal(float healAmount)
    {
        if (!healing) {
            yield break;
        }

        yield return new WaitForSeconds(1f);

        health += healAmount;

        // get object references
        GameObject floatingTextObject = Instantiate(floatingTextPrefab, transform.position, Quaternion.identity, popUpTexts.transform);
        TextMeshProUGUI floatingText = floatingTextObject.GetComponent<TextMeshProUGUI>();

        // set text and color
        floatingText.text = "+" + healAmount;
        floatingText.color = Color.green;

        // start countdown to destroy text
        StartCoroutine(DestroyText(floatingTextObject));

        // start countdown to heal again
        StartCoroutine(Heal(healAmount));
    }

    public void setHeal(bool healValue)
    {
        healing = healValue;
    }

    private IEnumerator DestroyText(GameObject textObject)
    {
        yield return new WaitForSeconds(FADE_TIME);
        Destroy(textObject);
    }

    private void Die()
    {
        Debug.Log("lmao, yuo is die");
        // play die animation here
    }
}
