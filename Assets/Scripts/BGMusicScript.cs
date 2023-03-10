using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMusicScript : MonoBehaviour
{
    private static BGMusicScript instance = null;
    public static BGMusicScript Instance
    {
        get { return instance; }
    }
    void Awake()
    {
        if (instance != null && instance != this) {
            this.gameObject.GetComponent<AudioSource>().Pause();
            return;
        } else {
            instance = this;
        }

        DontDestroyOnLoad(this.gameObject);
    }
}
