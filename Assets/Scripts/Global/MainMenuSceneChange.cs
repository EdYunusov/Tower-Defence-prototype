using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuSceneChange : MonoBehaviour
{
    public void Change()
    {
        SceneManager.LoadScene(0);
    }
}
