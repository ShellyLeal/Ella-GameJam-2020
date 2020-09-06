using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MenuAppearScript : MonoBehaviour
{
    public Animator menu; // Assign in inspector
    public AudioSource effect;
    public bool isShowing;
    public Image backgroundFade;

    void Update()
    {
        if (Input.GetKeyDown("escape"))
        {
            if (!isShowing)
            {
                ShowExitMenu();
            }
            else
            {
                HideExitMenu();
            }
        }
    }

    public void ShowExitMenu()
    {
        isShowing = true;
        menu.Play("IntroExit");
        effect.Play();
        backgroundFade.raycastTarget = true;
    }

    public void HideExitMenu()
    {
        isShowing = false;
        menu.Play("OutroExit");
        effect.Play();
        backgroundFade.raycastTarget = false;
    }
}