using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObject : MonoBehaviour
{
    public float rotateSpeed;
    public float inputan;
    private Vector3 tmpRotation;
    public int selectedModel = 0;
    public Quaternion start;
    private int x = 0;
    public float NilaiTransformRotation;

    private void Start()
    {
        SelectModel();
        //transform.rotation = Quaternion.identity;
}

    // Update is called once per frame
    void Update()
    {
        tmpRotation.y += rotateSpeed * Time.deltaTime * Input.GetAxisRaw("Horizontal");
        //tmpRotation.x += rotateSpeed * Time.deltaTime * inputan;
        transform.rotation = Quaternion.Euler(tmpRotation);

    }

    public void SelectModel()
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
}
