using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Leap;

public class GestureChecker : MonoBehaviour
{
    //Kalau ubah hand model, ganti HandModel di Detector :
    //Kanan(Left Capsule Hand), Kiri (Right Capsule Hand)
    //Kanan (Right Rigged Hand), Kiri (Left Rigged Hand)

    public DM_MainScene dM_MainScene;
    public IM_MainScene iM_MainScene;
    public GameObject rightHand, leftHand;
    public GameObject help_layout, exit_layout;
    [SerializeField] [Header("Main Menu Button")]
    public Button prev;
    public Button next;
    public Button home;
    public Button help;
    private bool isMainSceneActive = true;
    public bool IsMainSceneActive { get => isMainSceneActive; set => isMainSceneActive = value; }

    [SerializeField][Header("Zoom Object Settings")]
    public Vector3 zoomDistance;
    private Transform pinchLeft, pinchRight;
    private bool isZoomActive = false;
    private bool isExploreRight = false;
    private bool isExploreLeft = false;
    private bool zoomActive = false;
    public bool IsZoomActive { get => isZoomActive; set => isZoomActive = value; }
    public bool IsExploreRight { get => isExploreRight; set => isExploreRight = value; }
    public bool IsExploreLeft { get => isExploreLeft; set => isExploreLeft = value; }
    public bool ZoomActive { get => zoomActive; set => zoomActive = value; }

    [SerializeField][Header("Explore Object Settings")]
    public Vector3 fistPosition;
    public Quaternion fistRotation;
    private Transform fist;
    private Vector3 prevPosition, nextPosition;
    private Quaternion prevRotation, nextRotation;
    private bool exploreActiveState = false;
    private bool isFistAND = false;
    private bool isFistOR = false;
    private bool exploreActive = false;
    public bool IsFistAND { get => isFistAND; set => isFistAND = value; }
    public bool IsFistOR { get => isFistOR; set => isFistOR = value; }
    public bool ExploreActive { get => exploreActive; set => exploreActive = value; }

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

    //[Header("OK Cancel Button Settings")]
    private bool isBackActive = false;
    private bool isOkActive = false;
    public bool IsBackActive { get => isBackActive; set => isBackActive = value; }
    public bool IsOkActive { get => isOkActive; set => isOkActive = value; }

    [SerializeField][Header("Exit Main Menu Settings")]
    public float indexDistance, thumbDistance;
    private float minHomeDistance = 0.03f;
    private bool isHomeActive = false;
    public bool IsHomeActive { get => isHomeActive; set => isHomeActive = value; }

    void Start()
    {
        exit_layout.gameObject.SetActive(false);
        help_layout.gameObject.SetActive(false);
    }

    void Update()
    {
        //Hand Child Index : 0=palm, 1=thumb, 2=index, 3=middle, 4=ring, 5=pinky
        pinchLeft = leftHand.transform.GetChild(2);
        pinchRight = rightHand.transform.GetChild(2);
        thumbLeft = leftHand.transform.GetChild(1);
        thumbRight = rightHand.transform.GetChild(1);

        indexDistance = Vector3.Distance(pinchLeft.position, pinchRight.position);
        thumbDistance = Vector3.Distance(thumbLeft.position, thumbRight.position);

        if ((IsZoomActive == true) & (dM_MainScene.IsPlaying == false) & (IsMainSceneActive == true))
        {
            zoomDistance = new Vector3(indexDistance, indexDistance, indexDistance);
            ZoomActive = true;
        }
        else
        {
            ZoomActive = false;
        }

        if ((IsFistAND == false) & (IsFistOR == true) & (dM_MainScene.IsPlaying == false) & (IsMainSceneActive == true))
        {
            ExploreActive = true;
            ThumbActive = false;
            if (exploreActiveState == true)
            {
                if (IsExploreLeft == true)
                {
                    fist = leftHand.transform.GetChild(0);
                }
                else if (IsExploreRight == true)
                {
                    fist = rightHand.transform.GetChild(0);
                }
                prevPosition = fist.transform.position;
                prevRotation = fist.transform.rotation;
                exploreActiveState = false;
            }
            else
            {
                nextPosition = fist.transform.position;
                nextRotation = fist.transform.rotation;

                fistPosition = (nextPosition - prevPosition);
                fistRotation = nextRotation * Quaternion.Inverse(prevRotation);

                prevPosition = fist.transform.position;
                prevRotation = fist.transform.rotation;
            }
        }
        else
        {
            ExploreActive = false;
            exploreActiveState = true;
        }

        if((IsThumbAND == false) & (IsThumbOR == true))
        {
            ExploreActive = false;
            if (ThumbActive == true)
            {
                if (IsPrevActive == true)
                {
                    if ((IsMainSceneActive == true) & (dM_MainScene.IsPlaying == false)) { dM_MainScene.PreviousObject();}
                    if ((IsMainSceneActive == false) & (help_layout.transform.GetChild(1).gameObject.activeSelf == true))
                    {
                        help_layout.transform.GetChild(0).gameObject.SetActive(true);
                        help_layout.transform.GetChild(1).gameObject.SetActive(false);
                    }
                }
                else if (isNextActive == true)
                {
                    if ((IsMainSceneActive == true) & (dM_MainScene.IsPlaying == false)) { dM_MainScene.NextObject();}
                    if ((IsMainSceneActive == false) & (help_layout.transform.GetChild(0).gameObject.activeSelf == true))
                    {
                        help_layout.transform.GetChild(0).gameObject.SetActive(false);
                        help_layout.transform.GetChild(1).gameObject.SetActive(true);
                    }
                }
            }
            ThumbActive = false;
        }
        else
        {
            ThumbActive = true;
        }

        if((isHomeActive == true) & (IsMainSceneActive == true))
        {
            if((indexDistance <= minHomeDistance) & (thumbDistance <= minHomeDistance))
            {
                exit_layout.gameObject.SetActive(true);
                MainOff();
            }
        }

        if ((IsBackActive == true) & (IsMainSceneActive == false) & (exit_layout.activeSelf == true || help_layout.transform.GetChild(0).gameObject.activeSelf == true))
        {
            MainOn();
        }

        if ((IsOkActive == true) & (IsMainSceneActive == false) & (exit_layout.activeSelf == true))
        {
            iM_MainScene.gotoMainMenu();
        }
    }

    public void MainOn()
    {
        exit_layout.gameObject.SetActive(false);
        help_layout.gameObject.SetActive(false);
        isMainSceneActive = true;

        prev.interactable = true;
        next.interactable = true;
        home.interactable = true;
        help.interactable = true;
    }

    public void MainOff()
    {
        IsMainSceneActive = false;
        IsBackActive = false;
        IsOkActive = false;

        prev.interactable = false;
        next.interactable = false;
        home.interactable = false;
        help.interactable = false;
    }
}
