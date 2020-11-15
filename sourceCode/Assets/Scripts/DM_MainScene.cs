using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DM_MainScene : MonoBehaviour
{
    public GestureChecker gestureChecker;
    public int selectedModel = 0;
    private bool summonObject = true;
    public int previousModel = 0;

    [SerializeField][Header("Transform Information")]
    public Vector3 positionValue;
    public Quaternion rotationValue;
    public Vector3 zoomDistance, scaleValue;
    private Vector3 maxDistance = new Vector3(0.35f, 0.35f, 0.35f);

    private GameObject workspace;
    private BoxCollider ws_collider, obj_collider;
    private float wsc_size;

    private float play_timer = 0;
    private bool isPlaying = false;
    private bool isNext = false;
    private bool isPrevious = false;
    public bool IsPlaying { get => isPlaying; set => isPlaying = value; }
    public bool IsNext { get => isNext; set => isNext = value; }
    public bool IsPrevious { get => isPrevious; set => isPrevious = value; }

    void Start()
    {
        SelectModel();
    }

    void Update()
    {
        SelectModel();
        if (summonObject==true)
        {
            transform.GetChild(selectedModel).GetComponent<EachObject>().ResetDefault();
            //transform.GetChild(selectedModel).GetComponent<Rigidbody>().freezeRotation = true;
            //transform.GetChild(selectedModel).GetComponent<EachObject>().SummonObject();
            //transform.GetChild(selectedModel).GetComponent<Rigidbody>().freezeRotation = false;
            summonObject = false;
        }

        if (IsPlaying == true)
        {
            play_timer += Time.deltaTime;
            gestureChecker.ExploreActive = false;
            gestureChecker.ThumbActive = false;
            if (play_timer > 5f)
            {
                IsPlaying = false;
                play_timer = 0;
                UnfreezeObject();
            }
        }

        if (gestureChecker.IsZoomActive == true)
        {
            ZoomObject();
            transform.GetChild(selectedModel).GetComponent<EachObject>().GetWorkspace();
        }
        else
        {
            scaleValue = transform.GetChild(selectedModel).GetComponent<EachObject>().transform.localScale;
        }

        if (gestureChecker.ExploreActive == true)
        {
            positionValue = gestureChecker.fistPosition;
            rotationValue = gestureChecker.fistRotation;
        }
        else
        {
            positionValue = transform.GetChild(selectedModel).GetComponent<EachObject>().transform.position;
            rotationValue = transform.GetChild(selectedModel).GetComponent<EachObject>().transform.rotation;
        }
        TransformObject();
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

    public void PreviousObject()
    {
        if (isPlaying == false)
        {
            if (selectedModel <= 0)
            {
                selectedModel = transform.childCount - 1;
            }
            else
            {
                selectedModel = selectedModel - 1;
            }
            summonObject = true;
            previousModel = selectedModel + 1;
            if (previousModel > transform.childCount - 1)
            {
                previousModel = 0;
            }
        }
    }

    public void NextObject()
    {
        if (isPlaying == false)
        {
            if (selectedModel >= transform.childCount - 1)
            {
                selectedModel = 0;
            }
            else
            {
                selectedModel = selectedModel + 1;
            }
            summonObject = true;
            previousModel = selectedModel - 1;
            if (previousModel < 0)
            {
                previousModel = transform.childCount - 1;
            }
        }
    }

    public void TransformObject()
    {
        transform.GetChild(selectedModel).GetComponent<EachObject>().MoveObject(positionValue, gestureChecker.ExploreActive);
        transform.GetChild(selectedModel).GetComponent<EachObject>().RotateObject(rotationValue, gestureChecker.ExploreActive);
        transform.GetChild(selectedModel).GetComponent<EachObject>().ScaleObject(scaleValue);
    }

    public void ZoomObject()
    {
        zoomDistance = gestureChecker.zoomDistance;
        scaleValue.x = zoomDistance.x / maxDistance.x * transform.GetChild(selectedModel).GetComponent<EachObject>().maxScale.x;
        scaleValue.y = zoomDistance.y / maxDistance.y * transform.GetChild(selectedModel).GetComponent<EachObject>().maxScale.y;
        scaleValue.z = zoomDistance.z / maxDistance.z * transform.GetChild(selectedModel).GetComponent<EachObject>().maxScale.z;
    }

    public void ResetObject()
    {
        if((isPlaying == false) & (gestureChecker.IsMainSceneActive == true))
        {
            transform.GetChild(selectedModel).GetComponent<EachObject>().ResetDefault();
        }
    }

    public void PlayAnimation()
    {
        if ((selectedModel == 1 || selectedModel == 7) & (gestureChecker.IsZoomActive == false) & (gestureChecker.IsMainSceneActive == true))
        {
            IsPlaying = true;
            transform.GetChild(selectedModel).GetComponent<EachObject>().ResetDefault();
            FreezeObject();
            transform.GetChild(selectedModel).GetComponent<EachObject>().PlayAnimation();
        }
    }

    public void UnfreezeObject()
    {
        transform.GetChild(selectedModel).GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        transform.GetChild(selectedModel).GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;
    }

    public void FreezeObject()
    {
        transform.GetChild(selectedModel).GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
    }

}
