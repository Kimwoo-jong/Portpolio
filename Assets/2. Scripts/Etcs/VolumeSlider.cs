using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    [SerializeField] private Slider bgmSld;
    [SerializeField] private Slider sfxSld;

    private void Start()
    {
        bgmSld.onValueChanged.AddListener(bgmVolume => SoundManager.instance.SetVolumeBGM(bgmVolume));
        sfxSld.onValueChanged.AddListener(sfxVolume => SoundManager.instance.SetVolumeSFX(sfxVolume));

        bgmSld.value = SoundManager.instance.bgmVolume;
        sfxSld.value = SoundManager.instance.sfxVolume;
    }
}
