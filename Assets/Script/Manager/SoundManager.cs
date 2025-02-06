using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class SoundManager : MonoBehaviour
{
    [SerializeField] private List<AudioSource> hooraySound;
    [SerializeField] private List<AudioSource> hooray2Sound;
    [SerializeField] private List<AudioSource> congratulateSound;
    [SerializeField] private List<AudioSource> cheerSound;
    [SerializeField] private List<AudioSource> ingameSound;
    [SerializeField] private AudioSource mainSound;
    [SerializeField] private AudioSource snapSound;
    [SerializeField] private AudioSource touchSound;
    [SerializeField] private AudioSource touchSound2;

    public static SoundManager Instance;

    int rdIngame, rdHooray, rdCongra, rdCheer, rdHooray2;
    float timePlay;
    AudioSource _bgSound;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
    //    PlaySFX(SFXType.Main);
    }
    public void StopMusic()
    {
        _bgSound.Stop();
    }
    public void PlayMusic(SFXType sFXType)
    {
        _bgSound.volume = 1;
        switch (sFXType)
        {
            case SFXType.Ingame:
                rdIngame = Random.Range(0, ingameSound.Count);
                mainSound.Pause();
                if (!ingameSound[rdIngame].isPlaying)
                {
                    ingameSound[rdIngame].Play();
                }
                _bgSound = ingameSound[rdIngame];
                break;
            case SFXType.Main:
                ingameSound[rdIngame].Stop();
                if (!mainSound.isPlaying)
                {
                    mainSound.Play();
                    mainSound.loop = true;
                }
                _bgSound = mainSound;
                break;
        }
    }
    public void PlaySFX(SFXType sFX)
    {
        switch (sFX)
        {
            case SFXType.Win:
                ingameSound[rdIngame].Stop();
                rdHooray = Random.Range(0, hooraySound.Count);
                if (!hooraySound[rdHooray].isPlaying)
                {
                    hooraySound[rdHooray].Play();
                }
                break;
            case SFXType.Snap:
                snapSound.Stop();
                snapSound.Play();
                break;
            case SFXType.OpenNewPicture:
                rdCongra = Random.Range(0, congratulateSound.Count);
                if (!congratulateSound[rdCongra].isPlaying)
                {
                    congratulateSound[rdCongra].Play();
                }
                break;
            case SFXType.OpenNewFrame:
                mainSound.Pause();
                rdCheer = Random.Range(0, cheerSound.Count);
                timePlay = ingameSound[rdCheer].clip.length;
                if (!cheerSound[rdCheer].isPlaying)
                {
                    cheerSound[rdCheer].Play();
                }
                DOVirtual.DelayedCall(timePlay, () =>
                {
                    mainSound.Play();
                });
                break;
            case SFXType.Success:
                rdHooray2 = Random.Range(0, hooray2Sound.Count);
                timePlay = hooray2Sound[rdHooray2].clip.length;
                if (!hooray2Sound[rdHooray2].isPlaying)
                {
                    hooray2Sound[rdHooray2].Play();
                }
                mainSound.Play();
                break;
            case SFXType.Touch:
                touchSound.Stop();
                touchSound.Play();
                break;
            case SFXType.Touch2:
                touchSound2.Stop();
                touchSound2.Play();
                break;
        }
    }
}
public enum SFXType
{
    Snap,
    OpenNewPicture,
    Win,
    Success,
    Ingame,
    Main,
    OpenNewFrame,
    Touch,
    Touch2,
}