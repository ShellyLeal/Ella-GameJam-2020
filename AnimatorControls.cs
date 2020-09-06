using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorControls : MonoBehaviour
{
    Animator controller;
    float currSpeed = 1;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<Animator>();
    }

    public void IncreaseSpeed()
    {
        currSpeed += 2.0f;
        controller.SetFloat("speed", currSpeed);
    }
}
