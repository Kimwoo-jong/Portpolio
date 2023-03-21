using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;                  //플레이어의 위치
    [SerializeField] private Vector3 cameraPos;                          //카메라의 위치값

    [SerializeField] private Vector2 center;                             //맵의 중심값
    [SerializeField] private Vector2 mapSize;                            //맵의 크기

    [SerializeField] private float cameraMoveSpeed = 2.0f;               //카메라 이동속도
    private float height;                                                //높이
    private float width;                                                 //너비

    private void Start()
    {
        playerTransform = GameObject.Find("Player").GetComponent<Transform>();
        //카메라가 비출 수 있는 세로영역은 OrthographicSize이다. ex) 2라면 -2 ~ 2
        //카메라가 비추는 영역의 세로 크기의 절반을 높이로 선언
        height = Camera.main.orthographicSize;
        //카메라가 비출 수 있는 가로영역은 OrthographicSize * (Screen.width / Screen/height)이다.
        //카메라가 비추는 영역의 가로 크기를 너비로 선언
        width = height * Screen.width / Screen.height;
    }

    private void FixedUpdate()
    {
        LimitCameraArea();
    }
    //카메라가 볼 수 있는 영역에 제한을 둔다.
    private void LimitCameraArea()
    {
        //카메라 오브젝트가 가장 뒤에 있어야 모든 오브젝트를 비출 수 있으므로,
        //카메라 위치 = 플레이어의 위치 (X,Y,Z) + 임의로 설정한 카메라의 위치 (0,0,-10)
        transform.position = Vector3.Lerp(transform.position,
                                          playerTransform.position + cameraPos,
                                          Time.deltaTime * cameraMoveSpeed);

        //맵의 가로 절반 너비에서 카메라가 비출 수 있는 너비를 빼주면 영역 제한이 가능
        //Clamp 함수를 사용해 범위를 제한해줌
        //min : -lx + center.x, max : lx + center.x로 설정
        float lx = mapSize.x - width;
        float clampX = Mathf.Clamp(transform.position.x, -lx + center.x, lx + center.x);

        float ly = mapSize.y - height;
        float clampY = Mathf.Clamp(transform.position.y, -ly + center.y, ly + center.y);
        
        //설정된 좌표로 카메라를 옮겨준다.
        transform.position = new Vector3(clampX, clampY, -10f);
    }
    //카메라가 볼 수 있는 맵 영역의 기즈모를 그려준다.
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(center, mapSize * 2);
    }
}
