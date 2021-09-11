using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class Soldier : MonoBehaviour
{

    public static float moveSpeed = 7f;


    //public Animator animator;

    private const float MAX_HEALTH = 100f;
    private const float FADE_TIME = .5f;

    public float health = MAX_HEALTH;

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
        if (CompareTag("Player")) {
            playerHealthSlider = GameObject.Find("PlayerHealth").GetComponent<Slider>();
            playerHealthSlider.maxValue = playerHealthSlider.value = MAX_HEALTH;
        }
    }

    void Update()
    {
        if (playerHealthSlider != null) {
            playerHealthSlider.value = health;
        }
    }

    public void Damage(float damage)
    {
        Debug.Log(this.gameObject.name + " has been damaged");
        this.health -= damage;

        // get object references
        GameObject floatingTextObject = Instantiate(floatingTextPrefab, transform.position, Quaternion.identity, popUpTexts.transform);
        TextMeshProUGUI floatingText = floatingTextObject.GetComponent<TextMeshProUGUI>();

        // set text and color
        floatingText.text = "-" + damage;
        floatingText.color = Color.red;

        // start countdown to destroy text
        StartCoroutine(DestroyText(floatingTextObject));

        if (this.health <= 0) {
            Die();
        }
    }

    public IEnumerator Heal(float healAmount)
    {
        if (!healing) {
            yield break;
        }

        yield return new WaitForSeconds(1f);

        if (this.health >= 100) {
            StartCoroutine(Heal(healAmount));
            yield break;
        }

        this.health += healAmount;

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

    public void Die()
    {
        // play animation
        Destroy(this.gameObject);

        if (this.gameObject.CompareTag("Player")) {
            Debug.Log(Menu.prevSceneIndex);
            Menu.prevSceneIndex = SceneManager.GetActiveScene().buildIndex;
            Debug.Log(Menu.prevSceneIndex);
            SceneManager.LoadScene("LoseScreen");
        }
    }
}
