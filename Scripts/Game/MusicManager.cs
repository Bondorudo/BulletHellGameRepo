using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AudioClip music;

    private void Start()
    {
        AudioManager.instance.PlayMusic(music);
    }

}
