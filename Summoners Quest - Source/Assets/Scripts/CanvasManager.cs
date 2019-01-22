using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{
    private static CanvasManager _instance;

    public Player player;
    public GameObject interactionPanel;
    public Text interactionText;
    public GameObject dialoguePanel;
    public Text dialogueText;
    public GameObject npcPanel;
    public Text npcName;
    public GameObject inventoryPanel;
    public Image[] inventoryIcons;
    public GameObject recipePage;

    public static CanvasManager instance
    {
        get { return _instance; }
    }

    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
        HideInteractionPanel();
        HideNpcName();
        HideDialoguePanel();
        recipePage.SetActive(false);

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

    public void HideNpcName()
    {
        npcPanel.SetActive(false);
    }

    public void ShowNpcName(string text)
    {
        npcName.text = text;
        npcPanel.SetActive(true);
    }

    public void HideDialoguePanel()
    {
        dialoguePanel.SetActive(false);
        player.gameObject.SetActive(true);
    }

    public void ShowInventory()
    {
        inventoryPanel.SetActive(true);
    }

    public void HideInventory()
    {
        inventoryPanel.SetActive(false);
    }

    public void ShowQuestDialogue(string text)
    {
        dialogueText.text = text;
        dialoguePanel.SetActive(true);
        player.gameObject.SetActive(false);
    }

    public string GetQuestDialogue()
    {
        return dialogueText.text;
    }

    public void ClearAllInventorySlotIcons()
    {
        for (int i = 0; i < inventoryIcons.Length; ++i)
            ClearInventorySlotIcon(i);
    }

    public void ClearInventorySlotIcon(int slotIndex)
    {
        inventoryIcons[slotIndex].enabled = false;
        inventoryIcons[slotIndex].sprite = null;
    }

    public void SetInventorySlotIcon(int slotIndex, Sprite icon)
    {
        inventoryIcons[slotIndex].sprite = icon;
        inventoryIcons[slotIndex].enabled = true;
    }

    public void ShowRecipePage()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (!recipePage.activeSelf)
            {
                recipePage.SetActive(true);
            }
            else
            {
                recipePage.SetActive(false);
            }
        }        
    }
}
