using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadSceneManager : MonoBehaviour
{
    [SerializeField] private Image loadingBar;                   //로딩바로 사용될 이미지
    private Text loadingTxt;                    //로딩 텍스트
    [SerializeField] private float _fillAmount;

    private string[] loadText;                  //Loading. . . << 을 출력하기 위한 String 배열

    private int count = 0;                      //배열 인덱스를 위한 Int 변수
    private float curLoadTime = 0.0f;                  //현재 로딩시간
    private float maxLoadTime = 10.0f;                  //로딩시간

    private void Start()
    {
        loadingBar = transform.GetChild(0).GetComponent<Image>();
        loadingTxt = GetComponentInChildren<Text>();
        //배열 초기화
        loadText = new string[] { "Loading.", "Loading..", "Loading..." };

        loadingBar.fillAmount = 0f;

        StartCoroutine(LoadAsyncScene());
    }
    IEnumerator LoadAsyncScene()
    {
        //현재 빌드 인덱스가 1번씬이므로 다음씬인 2번을 골라준다.
        AsyncOperation async = SceneManager.LoadSceneAsync(2);
        //allowSceneActivation이 트루가 되어야 다음씬으로 진행가능
        async.allowSceneActivation = false;

        _fillAmount = 0.0f;

        //씬 로딩이 완료될 때까지 반복
        while(!async.isDone)
        {
            loadingTxt.text = loadText[count++];
            curLoadTime += Time.time;

            _fillAmount = curLoadTime / maxLoadTime;
            loadingBar.fillAmount = _fillAmount;

            Debug.Log(_fillAmount);

            if (count > 2)
            {
                count = 0;
            }

            if (_fillAmount >= 1.0f)
            {
                loadingBar.fillAmount = 1.0f;
            }

            if(curLoadTime >= maxLoadTime)
            {
                //바로 씬 전환이 일어나는 경우가 있어 의도적으로 지연시키기 위함.
                yield return new WaitForSeconds(1.0f);

                async.allowSceneActivation = true;
            }
            yield return new WaitForSeconds(0.3f);
        }
    }
}
