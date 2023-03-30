using UnityEngine;


//스프라이트 렌더러와 박스콜라이더가 반드시 있어야 함
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(BoxCollider2D))]
//스프라이트의 크기에 맞추어 박스콜라이더 크기를 변경하기 위한 스크립트
public class ResizeCollider : MonoBehaviour
{
    private void Start()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        BoxCollider2D boxCollider = GetComponent<BoxCollider2D>();

        if (spriteRenderer != null && boxCollider != null)
        {
            boxCollider.size = spriteRenderer.bounds.size;
        }
    }
}