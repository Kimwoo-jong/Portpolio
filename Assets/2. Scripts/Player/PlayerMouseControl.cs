using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMouseControl : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer player;
    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        //마우스 포인터의 위치와 오브젝트의 위치를 뺀 값
        //ScreenToWorldPoint >> 카메라가 비추고 있는 화면 내의 좌표값.
        Vector3 diffrence = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;

        diffrence.Normalize();
        //Atan2 두 점 사이의 각도를 측정한다
        //이 때의 각도는 라디안 값이므로 Rad2Deg로 라디안에서 도로 변경해준다.
        float rotationZ = Mathf.Atan2(diffrence.y, diffrence.x) * Mathf.Rad2Deg;
        //오일러각을 쿼터니언으로 변경하여 사용
        transform.rotation = Quaternion.Euler(0, 0, rotationZ);

        if(rotationZ < -90 || rotationZ > 90)
        {
            if(player.transform.eulerAngles.y == 0)
            {
                transform.localRotation = Quaternion.Euler(180, 0, -rotationZ);
            }
            else if(player.transform.eulerAngles.y == 180)
            {
                transform.localRotation = Quaternion.Euler(180, 180, -rotationZ);
            }
            player.flipX = true;
        }
        else
        {
            player.flipX = false;
        }
    }
}
