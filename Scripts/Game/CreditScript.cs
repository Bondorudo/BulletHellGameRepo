using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CloseCredits());
    }


    IEnumerator CloseCredits()
    {
        yield return new WaitForSeconds(20);
        SceneManager.LoadScene("MainMenu");
    }
}
