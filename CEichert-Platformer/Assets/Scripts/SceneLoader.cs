using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    private int currentScene;

    public delegate void loadScene(int scene);
    public static loadScene load;

    public delegate void RestartScene();
    public static RestartScene reloadScene;

    private void Awake()
    {
        currentScene = SceneManager.GetActiveScene().buildIndex;
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(currentScene);
    }
    public void LoadScene(int scene)
    {
        SceneManager.LoadScene(scene);
    }
    public void Quit()
    {
        Application.Quit();
    }
    private void OnEnable()
    {
        load += LoadScene;
        reloadScene += ReloadScene;
    }
    private void OnDisable()
    {
        load -= LoadScene;
        reloadScene -= ReloadScene;
    }
}
