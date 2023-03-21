using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdPool : MonoBehaviour
{
    public GameObject birdPrefab;                                    //새 오브젝트 프리팹을 담을 변수
    public List<GameObject> poolingBird = new List<GameObject>();    //메인화면에서 날아가는 새를 담기 위한 List 변수
    private BoxCollider2D m_RangeCollider;                           //새가 스폰될 범위를 보여주는 BoxCollider 변수

    private int m_BirdCount;                                         //날아가는 새의 마릿수를 저장할 Int 변수
    private int m_Count = 0;

    // Start is called before the first frame update
    void Start()
    {
        //박스 콜라이더를 먼저 불러와준다.
        m_RangeCollider = gameObject.GetComponent<BoxCollider2D>();

        //랜덤값은 2부터 5까지
        m_BirdCount = Random.Range(2, 6);

        //리스트에 랜덤 숫자만큼의 새 오브젝트를 미리 생성해둔다.
        for (int i = 0; i < m_BirdCount; ++i)
        {
            GameObject bird = Instantiate(birdPrefab, RandomPosition(), Quaternion.identity);
            bird.SetActive(false);
            bird.transform.parent = GameObject.Find("BirdBox").GetComponent<Transform>();
            bird.name = "Bird" + i;
            poolingBird.Add(bird);
        }
        StartCoroutine("SpawnAndMoveBird");
    }
    //새 스폰 위치값을 랜덤으로 만들어주는 Vector3 함수
    Vector3 RandomPosition()
    {
        //현재 오브젝트의 위치값
        Vector3 m_OriginPos = gameObject.transform.position;
        
        //콜라이더의 사이즈를 가져오는 bounds.size를 사용한다.
        float m_RangeX = m_RangeCollider.bounds.size.x;
        float m_RangeY = m_RangeCollider.bounds.size.y;

        m_RangeX = Random.Range((m_RangeX / 2) * -1, m_RangeX / 2);
        m_RangeY = Random.Range((m_RangeY / 2) * -1, m_RangeY / 2);

        Vector3 m_RandomPos = new Vector3(m_RangeX, m_RangeY, 0f);

        //기존 위치에서 랜덤으로 정해진 포지션 값을 더해줌
        //생각했던 위치로 생성되지 않아 X의 값을 임의로 빼줌.
        Vector3 m_SpawnPos = (m_OriginPos + m_RandomPos) + new Vector3(-7.0f, 0.0f, 0.0f);

        return m_SpawnPos;
    }
    //생성한 새 오브젝트를 화면에 보여줄 코루틴 함수
    IEnumerator SpawnAndMoveBird()
    {
        //3초의 대기시간을 가지고 출력해준다.
        yield return new WaitForSeconds(3.0f);
        //리스트의 처음부터 순차적으로 활성화시킴.
        poolingBird[m_Count++].SetActive(true);

        if (m_Count == m_BirdCount)
            m_Count = 0;
        //활성화가 끝났다면 다시 처음으로 돌아가서 반복
        StartCoroutine("SpawnAndMoveBird");
    }
}
