using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class TextManager : MonoBehaviour
{
    [TextArea]
    public string[] lines;
    public UnityEvent[] events;

    public AudioSource soundDialogue;

    public TextMeshProUGUI textComponent;
    public float speed; //Velocidade entre caracteres;
    public GameObject imgClick;

    public int currentLine = 0;

    public float waitWhenDone;
    public UnityEvent doWhenDone;

    public bool deleteText;

    public string beforeCoroutine;

    public Animator selfAnim;

    public void StartTyping()
    {
        StartCoroutine(TypeWriter(lines[currentLine]));
    }

    public void CheckClick()
    {
            if (currentLine < lines.Length)
            {
                if (textComponent.text != lines[currentLine])
                {
                    StopAllCoroutines();
                    textComponent.text = lines[currentLine];
                    if (currentLine == lines.Length - 1)
                    {
                        StartCoroutine(WaitUntilDone());
                    }
                }
                else if (currentLine < lines.Length - 1 && selfAnim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
                {
                    soundDialogue.Play();
                    currentLine++;
                    beforeCoroutine = textComponent.text;
                    StartCoroutine(TypeWriter(lines[currentLine]));
                    if (events[currentLine] != null)
                    {
                        events[currentLine].Invoke();
                    }
                    if (currentLine == lines.Length - 1) { imgClick.SetActive(false); };
                }
            }  
    }

    public void StopTyping()
    {
        StopAllCoroutines();
    }

    IEnumerator TypeWriter(string line)
    {
        textComponent.text = "";
        char[] chars = line.ToCharArray();
        foreach(char c in chars)
        {
            yield return new WaitForSeconds(speed);
            textComponent.text += c;
        }
       
        if (currentLine == lines.Length - 1)
        {
            imgClick.SetActive(false);
            StartCoroutine(WaitUntilDone());
        }
    }

    IEnumerator WaitUntilDone()
    {
        if (doWhenDone != null && waitWhenDone != 0)
        {
            yield return new WaitForSeconds(waitWhenDone);
            doWhenDone.Invoke();
        }
    }
}
