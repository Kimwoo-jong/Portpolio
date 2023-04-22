using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CanvasManager : MonoBehaviour
{
    public static CanvasManager instance;

    [SerializeField] private GameObject pnlMain;
    [SerializeField] private GameObject pnlOption;

    private bool isOptionOpen;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else if(instance != null)
        {
            Destroy(this);
        }    

        DontDestroyOnLoad(this.gameObject);
    }

    private void Start()
    {
        pnlMain = GameObject.Find("Main");
        //옵션 패널은 비활성화된 상태로 시작하기 때문에 부모 오브젝트인 Canvas에서 자식으로 접근한다.
        pnlOption = GameObject.Find("Canvas").transform.GetChild(1).gameObject;

        //메인 패널은 활성화, 옵션 패널은 비활성화인 상태로 시작
        pnlMain.SetActive(true);
        pnlOption.SetActive(false);

        isOptionOpen = false;
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && !isOptionOpen)
        {
            Option();
            isOptionOpen = !isOptionOpen;
        }
        else if(Input.GetKeyDown(KeyCode.Escape) && isOptionOpen)
        {
            OptionClose();
            isOptionOpen = !isOptionOpen;
        }
    }
    //게임씬을 로드한다.
    public void GameStart()
    {
        //씬의 이름이 바뀌면 계속 수정해줘야 하기 때문에 Build Setting에 있는 씬 번호를 넣어줬다.
        //중간에 로딩씬이 추가되었지만, 상관없음
        SceneManager.LoadScene(1);
    }
    //설정 버튼을 누르면 메인 메뉴들은 비활성화
    //설정창이 활성화된다.
    public void Option()
    {
        pnlMain.SetActive(false);
        pnlOption.SetActive(true);
    }
    //게임 종료
    public void Exit()
    {
        Application.Quit();
    }
    //설정창을 끄는 버튼에 들어갈 함수
    public void OptionClose()
    {
        if(SceneManager.GetActiveScene().buildIndex == 0)
        {
            pnlMain.SetActive(true);
            pnlOption.SetActive(false);
        }
        else
        {
            pnlMain.SetActive(false);
            pnlOption.SetActive(false);
            isOptionOpen = !isOptionOpen;
        }
    }
}
