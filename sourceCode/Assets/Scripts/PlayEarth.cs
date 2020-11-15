using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayEarth : MonoBehaviour
{
    public Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
        anim.Play("play");
    }

    // Update is called once per frame
    void Update()
    {
    }
}
