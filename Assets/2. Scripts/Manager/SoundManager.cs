using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    public AudioSource audioBGM;                            //배경음 오디오소스
    public AudioSource audioSFX;                            //효과음 오디오소스

    public float bgmVolume;                                //배경음 볼륨
    public float sfxVolume;                                //효과음 볼륨

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
        
        //자식 객체에 있는 오디오 소스를 가져온다.
        audioBGM = transform.GetChild(0).GetComponentInChildren<AudioSource>();
        audioSFX = transform.GetChild(1).GetComponentInChildren<AudioSource>();
    }
    private void Start()
    {
        //BGM과 SFX라는 키 값이 PlayerPrefs에 없다면 init 함수 실행
        //저장값이 있다면 Load함수 실행
        if(PlayerPrefs.HasKey("BGM") && PlayerPrefs.HasKey("SFX"))
        {
            LoadVolumeState();
        }
        else if(!PlayerPrefs.HasKey("BGM") && !PlayerPrefs.HasKey("SFX"))
        {
            InitVolumeSet();
        }
    }
    //초기 세팅
    private void InitVolumeSet()
    {
        audioBGM.volume = bgmVolume;
        audioSFX.volume = sfxVolume;
    }
    //배경 사운드 재생 함수
    public void PlayBGMSound(AudioClip _clip)
    {
        audioBGM.clip = _clip;
        audioBGM.loop = true;
        audioBGM.Play();
    }
    //이펙트 사운드 재생 함수
    public void PlaySFXSound(AudioClip _clip)
    {
        audioSFX.PlayOneShot(_clip);
    }
    //모든 사운드를 멈춰주는 함수
    public void StopSound()
    {
        audioBGM.Stop();
        audioSFX.Stop();
    }
    public void SetVolumeSFX(float _volume)
    {
        sfxVolume = _volume;
        audioSFX.volume = sfxVolume;

        PlayerPrefs.SetFloat("SFX", audioSFX.volume);
    }
    public void SetVolumeBGM(float _volume)
    {
        bgmVolume = _volume;
        audioBGM.volume = bgmVolume;

        PlayerPrefs.SetFloat("BGM", audioBGM.volume);
    }
    //저장된 볼륨의 상태를 불러오는 함수
    public void LoadVolumeState()
    {
        bgmVolume = PlayerPrefs.GetFloat("BGM");
        sfxVolume = PlayerPrefs.GetFloat("SFX");

        SetVolumeBGM(bgmVolume);
        SetVolumeSFX(sfxVolume);
    }
}