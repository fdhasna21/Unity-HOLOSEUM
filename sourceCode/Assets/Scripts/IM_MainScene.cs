using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IM_MainScene : MonoBehaviour
{
    public DM_MainScene dm_MainScene;
    public int selectedModel = 0;

    void Start()
    {
        selectedModel = dm_MainScene.selectedModel;
        SelectModel();
    }

    void Update()
    {
        selectedModel = dm_MainScene.selectedModel;
        SelectModel();
    }

    void SelectModel()
    {
        int i = 0;
        foreach (Transform model in transform)
        {
            if (i == selectedModel)
            {
                model.gameObject.SetActive(true);
            }
            else
            {
                model.gameObject.SetActive(false);
            }
            i++;
        }
    }

    public void gotoMainMenu()
    {
        Debug.Log("Main Menu");
        SceneManager.LoadScene("MainMenu");
    }
}
