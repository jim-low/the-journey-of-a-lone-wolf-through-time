using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextScene : MonoBehaviour
{
    IEnumerator GoNextLevel()
    {
        yield return new WaitForSeconds(1f);
        int nextIndex = SceneManager.GetActiveScene().buildIndex + 1;

        if (nextIndex > 3) {
            SceneManager.LoadScene("WinScreen");
        } else {
            SceneManager.LoadScene(nextIndex);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            StartCoroutine(GoNextLevel());
        }
    }
}
