using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour
{
    public Button b;

    public void SetActive()
    {
        b.interactable = true;
    }

    public void SetInactive()
    {
        b.interactable = false;
    }
}
