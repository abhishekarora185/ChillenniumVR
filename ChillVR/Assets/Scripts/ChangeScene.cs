using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour {

    private const string MainLevelName = "WilliamSandbox";

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<GenericObject>() != null)
        {
            SceneManager.LoadScene(MainLevelName);
        }
    }

    public void doExitGame()
    {
        Application.Quit();
    }
}
