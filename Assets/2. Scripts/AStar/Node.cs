using UnityEngine;
using System.Collections;

public class Node
{
    public bool walkable;                            //걸을 수 있는지 여부 판단
    public Vector2 worldPosition;                    //월드 좌표
    public int gridX;                                //그리드의 X 좌표
    public int gridY;                                //그리드의 Y 좌표
    public int movementPenalty;                      //이동 가중치를 주기 위한 변수

    public int gCost;
    public int hCost;
    public Node parent;                              //부모 노드

    public Node(bool _walkable, Vector2 _worldPos, int _gridX, int _gridY, int _penalty)
    {
        walkable = _walkable;
        worldPosition = _worldPos;
        gridX = _gridX;
        gridY = _gridY;
        movementPenalty = _penalty;
    }
    //A* 알고리즘은 시작 노드와 목적지 노드를 분명히 지정하여 최단 경로 파악이 가능
    //시작 노드 ~ 타겟 노드까지의 실제 소요 경비값 : GCost
    //휴리스틱 추정값, 현재 노드에서 최종 목적지까지 소요될 것으로 예상되는 값 : HCost
    //FCost = GCost + HCost;
    public int fCost
    {
        get
        {
            return gCost + hCost;
        }
    }
}