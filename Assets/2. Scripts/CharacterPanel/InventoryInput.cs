using UnityEngine;

public class InventoryInput : MonoBehaviour
{
    [SerializeField] GameObject statPanelGO;
    [SerializeField] GameObject inventoryPanelGO;
    [SerializeField] KeyCode[] toggleCharacterPanelKeys;

    private void Start()
    {
        //게임 시작시에는 패널을 비활성화인 채로 시작
        statPanelGO.SetActive(false);
        inventoryPanelGO.SetActive(false);
    }

    private void Update()
    {
        if(Input.GetKeyDown(toggleCharacterPanelKeys[0]))
        {
            ToggleStatPanel();
        }

        if(Input.GetKeyDown(toggleCharacterPanelKeys[1]))
        {
            ToggleEquipmentPanel();
        }
    }
    public void ToggleStatPanel()
    {
        statPanelGO.SetActive(!statPanelGO.activeSelf);
    }
    public void ToggleEquipmentPanel()
    {
        inventoryPanelGO.SetActive(!inventoryPanelGO.activeSelf);
    }
}