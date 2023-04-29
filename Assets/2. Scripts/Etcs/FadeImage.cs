using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeImage : MonoBehaviour
{
    private Image fadeImg;

    private void Start()
    {
        fadeImg = GetComponentInChildren<Image>();
        fadeImg.color = new Color(0, 0, 0, 0f);
    }
    //private void FixedUpdate()
    //{
    //    if(FindObjectOfType<DungeonInn>() != null)
    //    {
    //        FadeOut();
    //    }
    //}
    IEnumerator FadeOutCoroutine()
    {
        float fadeCount = 0;
        while (fadeCount <= 1.0f)
        {
            fadeCount += 0.015f;
            yield return new WaitForSeconds(0.01f);
            fadeImg.color = new Color(0, 0, 0, fadeCount);
        }
    }
    public void FadeOut()
    {
        StartCoroutine(FadeOutCoroutine());
    }
}
