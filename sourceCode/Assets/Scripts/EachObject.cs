using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;


public class EachObject : MonoBehaviour
{
    private Animator anim;
    public Vector3 originalPositionValue, originalRotationValue, minScale, maxScale;
    Quaternion originalRotation;

    private Collider m_Collider;
    private Vector3 m_Center, m_Size, m_Min, m_Max;
    private float maxPosition, minPosition;
    public float workspaceMax, workspaceMin;
    private Vector3 positionValue;

    void Start()
    {
        originalRotation = Quaternion.Euler(originalRotationValue.x, originalRotationValue.y, originalRotationValue.z);
        anim = GetComponent<Animator>();
        GetWorkspace();
        ResetDefault();
    }

    void Update()
    {
        workspaceMax = maxPosition;
        workspaceMin = minPosition;
    }

    public void GetWorkspace()
    {
        m_Collider = GetComponent<Collider>();
        m_Center = m_Collider.bounds.center;
        m_Size = m_Collider.bounds.size;
        m_Min = m_Collider.bounds.min;
        m_Max = m_Collider.bounds.max;
        maxPosition = Mathf.Max(m_Max.x, m_Max.y, m_Max.z);
        minPosition = -maxPosition;
    }

    public void ResetDefault()
    {
        RotateObject(originalRotation, false);
        MoveObject(originalPositionValue, false);
        ScaleObject(minScale);
        GetWorkspace();
    }

    public void MoveObject(Vector3 targetPosition, bool state)
    {
        //if (state == true)
        //{
        //    positionValue = transform.position + targetPosition;
        //    if(positionValue.y >= maxPosition)
        //    {
        //        transform.position = new Vector3(0, maxPosition, positionValue.z);
        //    }
        //    else if (positionValue.y <= minPosition)
        //    {
        //        transform.position = new Vector3(0, minPosition, positionValue.z);
        //    }

        //    else if(positionValue.z >= maxPosition)
        //    {
        //        transform.position = new Vector3(0, positionValue.y, maxPosition);
        //    }
        //    else if (positionValue.z <= minPosition)
        //    {
        //        transform.position = new Vector3(0, positionValue.y, minPosition);
        //    }
        //    else
        //    {
        //        transform.position = new Vector3(0, positionValue.y, positionValue.z);
        //    }
        //}
        //else
        //{
        //    transform.position = targetPosition;
        //}
    }

    public void RotateObject(Quaternion targetRotation, bool state)
    {
        if(state == true)
        {
            transform.rotation = transform.rotation * targetRotation;
        }
        else
        {
            transform.rotation = targetRotation;
        }
    }

    public void ScaleObject(Vector3 targetScale)
    {
        if (targetScale.x <= minScale.x)
        {
            targetScale = minScale;
        }
        else if (targetScale.x >= maxScale.x)
        {
            targetScale = maxScale;
        }
        transform.localScale = targetScale;
    }

    public void PlayAnimation()
    {
        anim.PlayInFixedTime("play", 0, 0.5f);
    }

    public void SummonObject()
    {
        
    }
}
