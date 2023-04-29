using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    private SpriteRenderer sprite;
    public Transform target;
    float speed = 1;
    Vector2[] path;
    int targetIndex;

    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        target = GameObject.Find("Player").transform;
    }

    private void Update()
    {
        //오브젝트의 위치가 우측으로 가는 중이면 FlipX
        //아닐 경우 그대로
        CheckCurrentGrid();
    }
    private void FixedUpdate()
    {
        if (target == null)
        {
            StopAllCoroutines();
        }
    }

    void CheckCurrentGrid()
    {
        //시작점이 목표보다 우측에 있을 경우
        if (transform.position.x > target.position.x)
        {
            sprite.flipX = true;
        }
        else
        {
            sprite.flipX = false;
        }
    }

    public void OnPathFound(Vector2[] newPath, bool pathSuccessful)
    {
        if (pathSuccessful)
        {
            path = newPath;
            StopCoroutine("FollowPath");
            StartCoroutine("FollowPath");
        }
    }

    public void StopPathFound(Vector2[] nullPath, bool nullSuccessful)
    {
        nullPath = null;

        path = nullPath;
        StopAllCoroutines();
    }

    IEnumerator FollowPath()
    {
        Vector2 currentWaypoint = path[0];

        while (true)
        {
            if ((Vector2)transform.position == currentWaypoint)
            {
                targetIndex++;

                if (targetIndex >= path.Length)
                {
                    yield break;
                }
                currentWaypoint = path[targetIndex];
            }

            transform.position = Vector2.MoveTowards(transform.position, currentWaypoint, speed * Time.deltaTime);
            yield return null;
        }
    }

    public void OnDrawGizmos()
    {
        if (path != null)
        {
            for (int i = targetIndex; i < path.Length; i++)
            {
                Gizmos.color = Color.magenta;
                Gizmos.DrawCube(path[i], Vector2.one * 0.25f);

                if (i == targetIndex)
                {
                    Gizmos.DrawLine(transform.position, path[i]);
                }
                else
                {
                    Gizmos.DrawLine(path[i - 1], path[i]);
                }
            }
        }
    }
    //플레이어가 감지 반경에 들어오면 실행되도록.
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            PathRequestManager.RequestPath(transform.position, target.position, OnPathFound);
        }
    }
    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            PathRequestManager.RequestPath(transform.position, target.position, OnPathFound);
        }
    }
    //플레이어가 감지 반경에서 나간 경우 멈춤
    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            PathRequestManager.RequestPath(transform.position, transform.position, StopPathFound);
        }
    }
    private void OnDestroy()
    {
        StopAllCoroutines();
        sprite = null;
        target = null;
        path = null;
    }
}