using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSettings : MonoBehaviour
{
    public GestureChecker gestureChecker;
    public int selectedButton = 0;

    void Start()
    {
        SelectButton();
    }

    void Update()
    {
        SelectButton();
        
    }

    void SelectButton()
    {
        int i = 0;
        foreach (Transform button in transform)
        {
            if (i == selectedButton)
            {
                button.gameObject.SetActive(true);
            }
            else
            {
                button.gameObject.SetActive(false);
            }
            i++;
        }
    }

    public void PreviousButton()
    {
        if (selectedButton <= 0)
        {
            selectedButton = transform.childCount - 1;
        }
        else
        {
            selectedButton = selectedButton - 1;
        }
    }

    public void NextButton()
    {
        if (selectedButton >= transform.childCount - 1)
        {
            selectedButton = 0;
        }
        else
        {
            selectedButton = selectedButton + 1;
        }
    }
}
