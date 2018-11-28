using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{
    private static CanvasManager _instance;

    public GameObject interactionPanel;
    public Text interactionText;
    public GameObject inventoryPanel;
    public Image[] inventoryIcons;

    public static CanvasManager instance {
        get { return _instance; }
    }

    void Awake()
    {
        DontDestroyOnLoad(gameObject);

        if (_instance == null)
            _instance = this;
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
        HideInteractionPanel();

        ClearAllInventorySlotIcons();
    }

    public void HideInteractionPanel()
    {
        interactionPanel.SetActive(false);
    }

    public void ShowInteractionPanel(string text)
    {
        interactionText.text = text;
        interactionPanel.SetActive(true);
    }

    private void ClearAllInventorySlotIcons()
    {
        for (int i = 0; i < inventoryIcons.Length; ++i)
            ClearInventorySlotIcon(i);
    }

    public void ClearInventorySlotIcon(int slotIndex)
    {
        inventoryIcons[slotIndex].enabled = false;
        inventoryIcons[slotIndex].sprite = null;
    }

    //public void SetInventorySlotIcon(int slotIndex, Sprite icon)
    //{
    //    inventoryIcons[slotIndex].sprite = icon;
    //    inventoryIcons[slotIndex].enabled = true;
    //}

}
