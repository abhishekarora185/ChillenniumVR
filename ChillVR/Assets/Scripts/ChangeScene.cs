using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour {

    public void changeScene(int SceneChange)
    {
        SceneManager.LoadScene(SceneChange);

    }

    public void doExitGame()
    {
        Application.Quit();
    }
}
