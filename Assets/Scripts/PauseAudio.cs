using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseAudio : MonoBehaviour
{
    
    void Start()
    {
        BGMusicScript.Instance.gameObject.GetComponent<AudioSource>().Pause();
    }

    // Update is called once per frame
   
}
