using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public delegate void loadScene(int scene);
    public static loadScene load;

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
    }
    private void OnDisable()
    {
        load -= LoadScene;
    }
}
