using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject rightHand, leftHand;

    [SerializeField][Header("Main Menu Layout")]
    public GameObject premenu;
    public GameObject mainmenu;
    public GameObject earthUI;
    private bool isMainMenuActive = true;
    public bool IsMainMenuActive { get => isMainMenuActive; set => isMainMenuActive = value; }
    public Button but_start, but_htp, but_about, but_exit;

    [SerializeField][Header("HTP Layout")]
    public GameObject htp;
    private bool isHTPActive = true;
    public bool IsHTPActive { get => isHTPActive; set => isHTPActive = value; }
    //public Button htp_back;

    [SerializeField][Header("About Layout")]
    public GameObject about;
    private bool isAboutActive = false;
    public bool IsAboutActive { get => isAboutActive; set => isAboutActive = value; }
    //public Button about_back;

    [SerializeField][Header("Exit Layout")]
    public GameObject exit;
    private bool isExitActive = true;
    public bool IsExitActive { get => isExitActive; set => isExitActive = value; }
    //public Button exit_cancel, exit_ok;

    [Header("Previous-Next Object Settings")]
    private Transform thumbLeft, thumbRight;
    private bool isPrevActive = false;
    private bool isNextActive = false;
    private bool isThumbAND = false;
    private bool isThumbOR = false;
    private bool thumbActive = false;
    public bool IsPrevActive { get => isPrevActive; set => isPrevActive = value; }
    public bool IsNextActive { get => isNextActive; set => isNextActive = value; }
    public bool IsThumbAND { get => isThumbAND; set => isThumbAND = value; }
    public bool IsThumbOR { get => isThumbOR; set => isThumbOR = value; }
    public bool ThumbActive { get => thumbActive; set => thumbActive = value; }

    [Header("OK Cancel Button Settings")]
    private bool isBackActive = false;
    private bool isOkActive = false;
    public bool IsBackActive { get => isBackActive; set => isBackActive = value; }
    public bool IsOkActive { get => isOkActive; set => isOkActive = value; }

    void Start()
    {
        Debug.Log("Displays Connected: " + Display.displays.Length);

        if (Display.displays.Length > 1)
            Display.displays[1].Activate(1920, 1080, 60);
        if (Display.displays.Length > 2)
            Display.displays[2].Activate(1366, 768, 60);

        //premenu.gameObject.SetActive(true);
        //mainmenu.gameObject.SetActive(false);
        //htp.gameObject.SetActive(false);
        //htp.gameObject.SetActive(false);
        //exit.gameObject.SetActive(false);
        ActivatedMain();
    }

    void Update()
    {

        if ((IsThumbAND == false) & (IsThumbOR == true))
        {
            if (ThumbActive == true)
            {
                if (IsPrevActive == true)
                {
                    Debug.Log("Previous");
                }
                else if (isNextActive == true)
                {
                    Debug.Log("Next");
                }
            }
            ThumbActive = false;
        }
        else
        {
            ThumbActive = true;
        }

        if ((IsBackActive == true) & (IsMainMenuActive == false))
        {
            ActivatedMain();
            IsBackActive = false;
        }

        if ((IsOkActive == true) & (IsMainMenuActive == false))
        {
            if (IsExitActive == true) {Application.Quit();}
            IsOkActive = false;
        }
    }

    public void ButtonOn()
    {
        but_start.interactable = true;
        but_htp.interactable = true;
        but_about.interactable = true;
        but_exit.interactable = true;
    }

    public void ButtonOff()
    {
        but_start.interactable = false;
        but_htp.interactable = false;
        but_about.interactable = false;
        but_exit.interactable = false;
    }


    public void ActivatedMain()
    {
        mainmenu.gameObject.SetActive(true);
        earthUI.gameObject.SetActive(true);
        IsMainMenuActive = true;
        ButtonOn();

        IsAboutActive = false;
        htp.gameObject.SetActive(false);

        IsHTPActive = false;
        about.gameObject.SetActive(false);

        IsExitActive = false;
        exit.gameObject.SetActive(false);
    }

    public void DeadactiveMain()
    {
        mainmenu.gameObject.SetActive(false);
        earthUI.gameObject.SetActive(false);
        IsMainMenuActive = false;
        ButtonOff();
    }

    public void ActivatedGame()
    {
        //nyalain HTP
        SceneManager.LoadScene("MainScene");
    
    }

    public void ActivatedHTP()
    {
        DeadactiveMain();
        IsHTPActive = true;
        htp.gameObject.SetActive(true);
        htp.transform.GetChild(0).gameObject.SetActive(true);
        htp.transform.GetChild(1).gameObject.SetActive(false);
        htp.transform.GetChild(2).gameObject.SetActive(false);
    }

    public void ActivatedAbout()
    {
        DeadactiveMain();
        about.gameObject.SetActive(true);
    }

    public void ActivatedExit()
    {
        ButtonOff();
        IsExitActive = true;
        IsMainMenuActive = false;
        exit.gameObject.SetActive(true);
    }
}
