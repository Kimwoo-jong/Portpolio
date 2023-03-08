using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//사운드 종류 열거형
public enum Sound
{
    BGM,
    Effect,
    maxCount,           // 열거형의 갯수를 세기 위함
}

[System.Serializable]
public class BGMManager : MonoBehaviour
{
    //BGM과 이펙트 효과음을 재생하기 위한 2개의 오디오소스를 가진다.
    AudioSource[] audioSources = new AudioSource[(int)Sound.maxCount];
    //원하는 Key와 Value값을 사용하기 위하여 Dictionary를 사용
    Dictionary<string, AudioClip> audioClips = new Dictionary<string, AudioClip>();

    public void Init()
    {
        //최상위 오브젝트를 찾는다.
        GameObject parent = GameObject.Find("Sound");
        //만약 해당 오브젝트가 없다면 Sound라는 이름을 가진 오브젝트를 생성해준다.
        if(parent == null)
        {
            parent = new GameObject { name = "Sound" };
            Object.DontDestroyOnLoad(parent);
            
            //열거형에 있는 이름을 스트링형 변수에 저장한다.
            string[] soundNames = System.Enum.GetNames(typeof(Sound));

            for (int i = 0; i < soundNames.Length - 1; ++i)
            {
                GameObject obj = new GameObject { name = soundNames[i] };
                audioSources[i] = obj.AddComponent<AudioSource>();
                obj.transform.parent = parent.transform;
            }

            //BGM은 반복 재생되도록 한다.
            audioSources[(int)Sound.BGM].loop = true;
        }
    }
    public void Clear()
    {
        //오디오소스 전부 스탑 및 클립 제거
        foreach(AudioSource audio in audioSources)
        {
            audio.clip = null;
            audio.Stop();
        }
        //효과음 Dictionary도 비워준다.
        //새로운 효과음이 계속 재생된다면
        //Dictionary에 쌓여서 메모리가 부족해질 수도 있는 문제를 생각하자.
        audioClips.Clear();
    }
    public void LoadScene()
    {

    }
}