using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KeepSongPlaying : MonoBehaviour
{
    public AudioControls songPlaying;


    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        SceneManager.activeSceneChanged += CheckScene;
    }

    public void CheckScene(Scene current, Scene next)
    {
        if(next.buildIndex == 6 || next.buildIndex == 12)
        {
            songPlaying.AdjustVolume(true);
        }
    }
}
