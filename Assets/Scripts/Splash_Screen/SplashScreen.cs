using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashScreen : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SplashRoutine());
    }

    IEnumerator SplashRoutine()
    {
        yield return new WaitForSeconds(3.0f);
        SceneManager.LoadScene(1);
    }


}
