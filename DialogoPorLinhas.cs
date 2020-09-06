using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class DialogoPorLinhas : MonoBehaviour
{
    TextMeshProUGUI textComponent;

    [TextArea]
    public String txt;
    public float speed;
    public float waitWhenDone;
    public UnityEvent doWhenDone;
    public GameObject imgClick;
    string[] lines;
    string beforeCoroutine;
    int currentLine = 0;

    private void Awake()
    {
        textComponent = GetComponent<TextMeshProUGUI>();
        lines = txt.Split('\n');
    }

    public void CheckClick()
    {
        if (currentLine < lines.Length)
        {
            StopAllCoroutines();
            textComponent.text = beforeCoroutine + lines[currentLine] + "\n\n";
            if (currentLine < lines.Length - 1)
            {
                currentLine++;
                beforeCoroutine = textComponent.text;
                StartCoroutine(TypeWriter(lines[currentLine]));
            }
            if (currentLine == lines.Length - 1) {
                Debug.Log("entrou na linha final");
                imgClick.SetActive(false);
                StartCoroutine(WaitUntilDone());
            };
        }
    }

    public void StartTyping()
    {
        StartCoroutine(TypeWriter(txt));
    }

    IEnumerator TypeWriter(string line)
    {
        float colorFloat = 0.1f;
        string aux = textComponent.text;

        while (colorFloat < 1.0f)
        {
            colorFloat += Time.deltaTime * 1f;
            int colorInt = (int)(Mathf.Lerp(0.0f, 1.0f, colorFloat) * 255.0f);
            textComponent.text = aux + "<color=#000000" + string.Format("{0:X}", colorInt) + ">" + lines[currentLine] + "</color>" +"\n\n";
            yield return null;
        }

        yield return new WaitForSeconds(speed);
        if (currentLine < lines.Length - 1)
        {
            currentLine++;
            beforeCoroutine = textComponent.text;
            StartCoroutine(TypeWriter(lines[currentLine]));
            if (currentLine == lines.Length - 1)
            {
                imgClick.SetActive(false);
                yield return new WaitForSeconds(waitWhenDone);
                doWhenDone.Invoke();
            }
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
