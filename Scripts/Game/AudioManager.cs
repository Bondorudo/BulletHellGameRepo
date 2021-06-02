using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public enum AudioChannel { MASTER, MUSIC, SFX };

    public float masterVolumePercent { get; private set; }
    public float musicVolumePercent { get; private set; }
    public float sfxVolumePercent { get; private set; }

    int activeMusicSourceIndex;

    AudioSource sfx2DSource;
    AudioSource musicSource;

    public static AudioManager instance;

    SoundLibrary library;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

            library = GetComponent<SoundLibrary>();

            GameObject newMusicSource = new GameObject("Music Source");
            musicSource = newMusicSource.AddComponent<AudioSource>();
            newMusicSource.transform.parent = transform;

            GameObject newSfx2DSource = new GameObject("2DsfxSource");
            sfx2DSource = newSfx2DSource.AddComponent<AudioSource>();
            newSfx2DSource.transform.parent = transform;

            masterVolumePercent = PlayerPrefs.GetFloat("masterVolume", 1f);
            musicVolumePercent = PlayerPrefs.GetFloat("musixVolume", 1f);
            sfxVolumePercent = PlayerPrefs.GetFloat("sfxVolume", 1f);
        }
    }

    public void SetVolume(float volumePercent, AudioChannel channel)
    {
        switch (channel)
        {
            case AudioChannel.MASTER:
                {
                    masterVolumePercent = volumePercent;
                    break;
                }
            case AudioChannel.MUSIC:
                {
                    musicVolumePercent = volumePercent;
                    break;
                }
            case AudioChannel.SFX:
                {
                    sfxVolumePercent = volumePercent;
                    break;
                }
        }
        musicSource.volume = musicVolumePercent * masterVolumePercent;

        PlayerPrefs.SetFloat("masterVolume", masterVolumePercent);
        PlayerPrefs.SetFloat("musixVolume", musicVolumePercent);
        PlayerPrefs.SetFloat("sfxVolume", sfxVolumePercent);
        PlayerPrefs.Save();
    }

    public void PlayMusic(AudioClip clip)
    {
        musicSource.PlayOneShot(clip, musicVolumePercent * masterVolumePercent);
        musicSource.playOnAwake = true;
        musicSource.loop = true;
    }

    public void PlaySound(string soundName)
    {
        sfx2DSource.PlayOneShot(library.GetClipFromName(soundName), sfxVolumePercent * masterVolumePercent);
    }
}
