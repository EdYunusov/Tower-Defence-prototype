using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button continueButton;
    private void Start()
    {
        continueButton.interactable = FileHandler.HasFile(MapCompletion.filename);
    }

    public void NewGame()
    {
        FileHandler.Reset(MapCompletion.filename);
        SceneManager.LoadScene(1);
    }
    
    public void Continue()
    {
        SceneManager.LoadScene(1);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
