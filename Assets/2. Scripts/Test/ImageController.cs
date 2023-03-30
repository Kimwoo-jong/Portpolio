using UnityEngine;
using UnityEngine.UI;

public class ImageController : MonoBehaviour
{
    public Image image;
    public Transform target;

    private RectTransform rectTransform;

    private Vector3 initialOffset;

    private void Start()
    {
        rectTransform = image.GetComponent<RectTransform>();
        // 이미지의 초기 위치와 타겟 오브젝트 사이의 거리를 계산
        initialOffset = rectTransform.position - target.position;
    }

    private void LateUpdate()
    {
        // 이미지를 타겟 오브젝트와 일정한 거리를 유지하며 움직이게 함.
        rectTransform.position = target.position + initialOffset;
    }
}