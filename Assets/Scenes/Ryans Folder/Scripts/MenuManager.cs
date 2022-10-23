using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    string GameScene = new string("Assets/Scenes/Dillan's SandBox/RyanControllersFixed.unity");
    string ConfigScene = new string("Assets/Scenes/Ryans Folder/Scenes/ConfigMenu.unity");

    public void StartGame(string scene)
    {
        scene = GameScene;
        SceneManager.LoadScene(scene);
    }

    public void ConfigMenu(string scene)
    {
        scene = ConfigScene;
        SceneManager.LoadScene(scene);
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Game is exiting");
    }

}
