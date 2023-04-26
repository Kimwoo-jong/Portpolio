using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    public GameObject spawner;                  //Enemy 스포너
    public GameObject enemy;                    //Enemy 오브젝트

    public GameObject spawnEffectPrefab;        //스폰 이펙트

    public float randomXPos;                    //생성 위치 랜덤 X값 변수

    private bool isSpawn;                       //몬스터를 스폰했는지 확인

    private void Start()
    {
        spawner = GameObject.Find("EnemySpawner");
        isSpawn = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //플레이어가 범위 내에 접근했을 때
        if (other.CompareTag("Player") && !isSpawn)
        {
            StartCoroutine(SpawnEnemy());
            isSpawn = !isSpawn;
        }
    }

    private IEnumerator SpawnEnemy()
    {
        //X값을 랜덤으로 주기 위한 변수
        randomXPos = Random.Range(-2f, 2f);

        //Enemy 소환 전에 애니메이션 실행
        GameObject eff = Instantiate(spawnEffectPrefab,
        new Vector3(spawner.transform.position.x + randomXPos, spawner.transform.position.y, spawner.transform.position.z)
        , Quaternion.identity);

        yield return new WaitForSeconds(0.75f);

        Destroy(eff);

        //스포너의 위치에 Enemy 소환
        Instantiate(enemy,
        new Vector3(spawner.transform.position.x + randomXPos, spawner.transform.position.y, spawner.transform.position.z)
        , Quaternion.identity);
    }
}
