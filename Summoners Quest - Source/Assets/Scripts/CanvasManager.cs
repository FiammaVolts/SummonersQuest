using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Class that will manage the canvas
/// </summary>
public class CanvasManager : MonoBehaviour
{
    // Creates a static instance of CanvasManager
    private static CanvasManager _instance;

    // a variable for Player
    public Player player;
    // a GameObject variable for the interaction panel
    public GameObject interactionPanel;
    // a Text variable for the interaction text
    public Text interactionText;
    // a GameObject variable needed for the dialogue panel
    public GameObject dialoguePanel;
    // a Text variable for the dialogue text
    public Text dialogueText;
    // a GameObject variable for the npc panel
    public GameObject npcPanel;
    // a Text variable for the npc names
    public Text npcName;
    // a GameObject variable for the inventory panel
    public GameObject inventoryPanel;
    // an array of Images for the inventory icons
    public Image[] inventoryIcons;
    // a GameObject variable for the recipe paper
    public GameObject recipePage;

    // A readonly property that will return the instance
    public static CanvasManager instance
    {
        get { return _instance; }
    }

    /// <summary>
    /// Awake method that will run before Start()
    /// </summary>
    void Awake()
    {
        // An if that doesn't destroy the gameObject when loading a new scene
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }

    /// <summary>
    /// Start method that will start the methods called at the start of the program
    /// </summary>
    void Start()
    {
        // Locks the cursor
        Cursor.lockState = CursorLockMode.Confined;
        // Hides the interaction panel
        HideInteractionPanel();
        // Hides the npc name
        HideNpcName();
        // Hides the dialogue panel
        HideDialoguePanel();
        // Sets the recipe page at false, so it can't be shown
        recipePage.SetActive(false);
        // Clears the inventory icons
        ClearAllInventorySlotIcons();
    }

    /// <summary>
    /// Method to hide the interaction panel
    /// </summary>
    public void HideInteractionPanel()
    {
        // Set at false
        interactionPanel.SetActive(false);
    }

    /// <summary>
    /// Method that will show the interaction panel
    /// </summary>
    /// <param name="text"></param>
    public void ShowInteractionPanel(string text)
    {
        // Sets the interactionText variable to text
        interactionText.text = text;
        // Sets the panel at true
        interactionPanel.SetActive(true);
    }

    /// <summary>
    /// Method that will hide the npc's name
    /// </summary>
    public void HideNpcName()
    {
        // Sets the panel at false
        npcPanel.SetActive(false);
    }

    /// <summary>
    /// Method that will show the npc's name
    /// </summary>
    /// <param name="text"></param>
    public void ShowNpcName(string text)
    {
        // Sets the npcName variable to text
        npcName.text = text;
        // Sets the panel at true
        npcPanel.SetActive(true);
    }

    /// <summary>
    /// Method that will hide the dialogue panel
    /// </summary>
    public void HideDialoguePanel()
    {
        // Sets the panel at false
        dialoguePanel.SetActive(false);
        // Sets the player at true
        player.gameObject.SetActive(true);
    }

    /// <summary>
    /// Method that will show the inventory
    /// </summary>
    public void ShowInventory()
    {
        // Sets the inventory panel at true
        inventoryPanel.SetActive(true);
    }

    /// <summary>
    /// Method to hide the inventory
    /// </summary>
    public void HideInventory()
    {
        // Sets the inventory panel at false
        inventoryPanel.SetActive(false);
    }

    /// <summary>
    /// Method that will show the quest dialogue
    /// </summary>
    /// <param name="text"></param>
    public void ShowQuestDialogue(string text)
    {
        // Sets the dialogue text to text
        dialogueText.text = text;
        // Sets the dialogue panel at true
        dialoguePanel.SetActive(true);
        // Sets the player at false
        player.gameObject.SetActive(false);
    }
    
    /// <summary>
    /// Method for the quest's dialogue
    /// </summary>
    /// <returns></returns>
    public string GetQuestDialogue()
    {
        // Returns the dialogue text
        return dialogueText.text;
    }

    /// <summary>
    /// Method that will clear all the inventory slots
    /// </summary>
    public void ClearAllInventorySlotIcons()
    {
        // For cycle that will run through the inventory
        for (int i = 0; i < inventoryIcons.Length; ++i)
            // And call the method that will clear the slots
            ClearInventorySlotIcon(i);
    }

    /// <summary>
    /// Method that will clear the inventory icons
    /// </summary>
    /// <param name="slotIndex"></param>
    public void ClearInventorySlotIcon(int slotIndex)
    {
        // Sets the inventoryIcons slot index's array at false
        inventoryIcons[slotIndex].enabled = false;
        // Sets the inventoryIcons slot index's sprite array at null
        inventoryIcons[slotIndex].sprite = null;
    }

    /// <summary>
    /// Method to set the inventory's slot icons
    /// </summary>
    /// <param name="slotIndex"></param>
    /// <param name="icon"></param>
    public void SetInventorySlotIcon(int slotIndex, Sprite icon)
    {
        // Sets the sprite for the slot index at icon
        inventoryIcons[slotIndex].sprite = icon;
        // Sets the slot index at true
        inventoryIcons[slotIndex].enabled = true;
    }

    /// <summary>
    /// Method for the recipe page
    /// </summary>
    public void ShowRecipePage()
    {
        // If that will check if the Tab key was pressed
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            // If the page isn't active
            if (!recipePage.activeSelf) 
                // Sets the recipe at true and shows the page
                recipePage.SetActive(true);
            
            // If the page is already active
            else
                // Sets the page at false and hides it
                recipePage.SetActive(false);
            
        }        
    }
}
