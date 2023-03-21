using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEditor;

public enum ItemValue
{
    //흰색
    일반,
    //파란색
    고급,
    //초록색
    영웅,
    //노란색
    희귀,
    //빨간색
    전설
}
//스크립터블 오브젝트 사용
//유니티에서 제공하는 대량의 데이터를 저장하는데 사용할 수 있는 데이터 컨테이너.
//아이템의 정보를 가지고 있는 에셋을 만든다.
[CreateAssetMenu(fileName = "New Item", menuName = "Item/Create Item")]
public class Item : ScriptableObject
{
    [SerializeField] string id;
    public string ID { get { return id; } }

    public string itemName;                         //아이템 이름
    public Sprite itemImage;                        //아이템 이미지
    public ItemValue m_ItemValue;                   //아이템 등급
    public string itemDescription;                  //아이템 설명

    private void OnValidate()
    {
        string path = AssetDatabase.GetAssetPath(this);
        id = AssetDatabase.AssetPathToGUID(path);
    }
    public virtual void Destroy()
    {

    }
    public virtual string GetItemType()
    {
        return "";
    }
}