using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Control : MonoBehaviour
{
    public int sceneIndex;
    public void NextScene()
    {
        SceneManager.LoadScene(sceneIndex);
    }
}
