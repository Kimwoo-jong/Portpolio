using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdControl : MonoBehaviour
{
    //오브젝트가 진행할 방향
    private Vector3 m_Direction;

    //초기 위치값을 저장할 변수
    private Vector3 m_OriginPos;
    private void OnEnable()
    {
        StartCoroutine("DisableObject");
    }
    private void Start()
    {
        //초기 위치를 저장
        m_OriginPos = this.gameObject.transform.position;
        //X 방향으로 이동하도록
        m_Direction = new Vector3(Random.Range(0.01f, 0.05f), 0.0f, 0.0f);
        StartCoroutine("DisableObject");
    }
    private void FixedUpdate()
    {
        transform.position += m_Direction;
    }
    //오브젝트를 비활성화 시키는 코루틴 함수
    IEnumerator DisableObject()
    {
        yield return new WaitForSeconds(20.0f);
        gameObject.SetActive(false);

        //비활성화 되면 다시 초기 위치로 돌아가도록 설정
        gameObject.transform.position = m_OriginPos;
    }
}
