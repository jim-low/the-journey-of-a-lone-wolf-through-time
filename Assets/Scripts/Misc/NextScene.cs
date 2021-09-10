using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextScene : MonoBehaviour
{
    /* public Animator animator; */

    IEnumerator GoNextLevel()
    {
        yield return new WaitForSeconds(1f);
        /* SceneManager.LoadScene(); */
        Debug.Log("Curent active scene = " + SceneManager.GetActiveScene().name);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            /* animator.SetBool("isOpen", true); */
            StartCoroutine(GoNextLevel());
        }
    }
}
