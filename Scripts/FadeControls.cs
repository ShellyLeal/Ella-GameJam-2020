using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FadeControls : MonoBehaviour
{
    public Image fadeOutUIImage;
    public float fadeSpeed = 0.8f;
    public int sceneToLoadWhenDone;
    public bool needsIntro;
    public UnityEvent doWhenIntroDone;


    void Start()
    {
        if (needsIntro){ StartCoroutine(FadeIn());}
    }

    public void ExitOutro()
    {
        StartCoroutine(FadeOut());
    }

    // Ajustes das cores das pecas de acordo com a posicao
    private IEnumerator FadeIn()
    {
        float alpha = 1;
        float fadeEndValue = 0;
        while (alpha >= fadeEndValue)
        {
            SetColorImage(ref alpha, -1);
            yield return null;
        }
        doWhenIntroDone.Invoke();
    }

    private IEnumerator FadeOut()
    {
        float alpha = 0;
        float fadeEndValue = 1;
        while (alpha <= fadeEndValue)
        {
            SetColorImage(ref alpha, 1);
            yield return null;
            StartCoroutine(WaitToLoadScene());
        }
    }

    private void SetColorImage(ref float alpha, int fadeDirection)
    {
        fadeOutUIImage.color = new Color(fadeOutUIImage.color.r, fadeOutUIImage.color.g, fadeOutUIImage.color.b, alpha);
        alpha += Time.deltaTime * (1.0f / fadeSpeed) * (fadeDirection);
    }

    IEnumerator WaitToLoadScene()
    {
        yield return new WaitForSeconds(fadeSpeed);
        SceneManager.LoadScene(sceneToLoadWhenDone);
    }
}
