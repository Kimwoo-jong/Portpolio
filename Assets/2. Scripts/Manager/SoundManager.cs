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
    public AudioSource stepSFX;                             //걷기 효과음 오디오소스

    public float bgmVolume;                                //배경음 볼륨
    public float sfxVolume;                                //효과음 볼륨

    public AudioClip[] bgmClips;                //배경음을 저장할 오디오클립 배열
    public AudioClip[] playerSfxClips;          //플레이어 효과음을 저장할 오디오클립 배열
    public AudioClip[] enemySfxClips;           //에너미 효과음을 저장할 오디오클립 배열
    public AudioClip[] environSfxClips;         //환경 효과음
    
    public AudioClip[] moveClips;               //발자국 효과음 배열
    private int playerSfxIndex = 0;             //플레이어 발자국 효과음 인덱스

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
        stepSFX = transform.GetChild(2).GetComponentInChildren<AudioSource>();
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
        stepSFX.volume = sfxVolume + 0.2f;
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
        stepSFX.Stop();
    }
    public void SetVolumeSFX(float _volume)
    {
        sfxVolume = _volume;
        audioSFX.volume = sfxVolume;
        stepSFX.volume = sfxVolume + 0.2f;

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
    #region BGM
    public void TitleBGM()
    {
        PlayBGMSound(bgmClips[0]);
    }
    public void TutorialBGM()
    {
        PlayBGMSound(bgmClips[1]);
    }
    public void DungeonBGM()
    {
        PlayBGMSound(bgmClips[2]);
    }
    #endregion
    #region 플레이어 효과음
    //이동 효과음이 여러 개이므로 인덱스 번호대로 순차 재생 되도록 구현
    public void PlayerMoveSound()
    {
        if(!stepSFX.isPlaying)
        {
            playerSfxIndex = (playerSfxIndex + 1) % moveClips.Length;
            stepSFX.clip = moveClips[playerSfxIndex];
            stepSFX.Play();
        }
    }
    public void PlayerJumpSound()
    {
        PlaySFXSound(playerSfxClips[0]);
        stepSFX.Stop();
    }
    public void PlayerInventorySFX()
    {
        PlaySFXSound(playerSfxClips[1]);
    }
    public void PlayerAttackSound()
    {
        PlaySFXSound(playerSfxClips[2]);
    }
    #endregion
    #region Enemy 효과음
    public void EnemySpawnSound()
    {
        PlaySFXSound(enemySfxClips[0]);
    }
    public void EnemyHitSound()
    {
        PlaySFXSound(enemySfxClips[1]);
    }
    public void EnemyDeathSound()
    {
        PlaySFXSound(enemySfxClips[2]);
    }
    #endregion
    #region 환경 효과음
    public void EnterDungeonSound()
    {
        PlaySFXSound(environSfxClips[0]);
    }
    public void CloseDungeonSound()
    {
        PlaySFXSound(environSfxClips[1]);
    }
    #endregion

    private void OnEnable()
    {
        // 씬 매니저의 sceneLoaded에 체인을 건다.
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    // 체인을 걸어서 이 함수는 매 씬마다 호출된다.
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        //오프닝 씬
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            //처음 시작 시 타이틀 BGM 재생
            TitleBGM();
        }
        //로딩 씬의 경우 소리가 없다.
        if(SceneManager.GetActiveScene().buildIndex == 1)
        {
            audioBGM.Stop();
        }
        //튜토리얼 씬
        if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            //처음 시작 시 타이틀 BGM 재생
            TutorialBGM();
        }
        //인게임 씬
        if (SceneManager.GetActiveScene().buildIndex == 3)
        {
            //처음 시작 시 타이틀 BGM 재생
            DungeonBGM();
        }
    }
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}