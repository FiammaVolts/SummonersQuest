using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Class for the state manager
/// </summary>
public class StateManager : MonoBehaviour
{
    // Instance of player
    public Player player;
    // Instance of NPC_Actions
    public NPCActions _npc;
    // A second instance of NPC_Actions, needed for the villager's mission
    public NPCActions _npc2;

    // Variable of NPC_State needed for the sister's mission
    private NPCState _currentStateSister;
    // Variable of NPC_State needed for the guard's mission
    private NPCState _currentStateGuard;
    // Variable of NPC_State needed for the lost boy's mission
    private NPCState _currentStateChildM;
    // Variable of NPC_State needed for the villager's mission
    private NPCState _currentStateVillager;
    // Variable of NPC_State needed for the woman's mission
    private NPCState _currentStateWomen;
    // Variable of NPC_State needed for the old man's mission
    private NPCState _currentStateOldMan;
    // Variable of NPC_State needed for the dragon's mission
    private NPCState _currentStateDragon;
    // Variable of NPC_Actions
    private NPCActions _currentNpc;
    // Variable of CanvasManager
    private CanvasManager _canvasManager;

    /// <summary>
    /// Start method
    /// </summary>
    void Start()
    {
        // Sets _canvasManager to the CanvasManager's instance
        _canvasManager = CanvasManager.instance;
        
        // Sets the sister's state to GivesQuest
        _currentStateSister = NPCState.State_GivesQuest;
        // Sets the lost boy's state to GivesQuest
        _currentStateChildM = NPCState.State_GivesQuest;
        // Sets the guard's state to GivesQuest
        _currentStateGuard = NPCState.State_GivesQuest;
        // Sets the villager's state to GivesQuest
        _currentStateVillager = NPCState.State_GivesQuest;
        // Sets the woman's state to GivesQuest
        _currentStateWomen = NPCState.State_GivesQuest;
        // Sets the old man's state to GivesQuest
        _currentStateOldMan = NPCState.State_GivesQuest;
        // Sets the dragon's state to GivesQuest
        _currentStateDragon = NPCState.State_GivesQuest;

        // Fetches the villager's recipe and sets it at false
        _npc2.transform.GetChild(1).gameObject.SetActive(false);
    }

    /// <summary>
    /// Update method
    /// </summary>
    void Update()
    {
        // Sets the _currentNpc to the player's GetNPC, that will check which NPC we are looking at
        _currentNpc = player.GetNPC();

        // An if to check if the npc we are looking at is the guard and if the state is GivesClue
        if(_currentNpc != null && _currentStateGuard == NPCState.State_GivesClue &&
            _currentNpc.gameObject.layer == LayerMask.NameToLayer("Guard"))
            // Moves the guard so the player can pass through
            _currentNpc.transform.position = new Vector3(-161.78f, 1.91f, -25.3f);

        // An if to check the input of the player
        if (Input.GetMouseButtonDown(0))
            // Calls the DefineState() method
            DefineState();

        // If the repice is active
        if(_npc2.transform.GetChild(1).gameObject.activeSelf == true)
        {
            // Will show the the recipe on the canvas
            _canvasManager.ShowRecipePage();
        }

        // Calls CheatMode()
        CheatMethod();
    }

    /// <summary>
    /// Method to define the states
    /// </summary>
    private void DefineState()
    {
        // If the player is looking at a npc
        if (_currentNpc != null)
        {
            // If the npc the player is looking at is the guard
            if (_currentNpc.gameObject.layer == LayerMask.NameToLayer("Guard"))
            {
                // Switch case for the different states
                switch (_currentStateGuard)
                {
                    // Case for GivesQuest
                    case NPCState.State_GivesQuest:
                        // If the player is looking at a npc
                        if (_currentNpc != null)
                        {
                            // If the canvas isn't null
                            if (_canvasManager != null)
                            {
                                // If the current dialogue is equal to GivesQuest
                                if (_canvasManager.GetQuestDialogue() == _currentNpc.givesQuest) {
                                    // Hides the dialogue panel
                                    _canvasManager.HideDialoguePanel();
                                    // Shows inventory
                                    _canvasManager.ShowInventory();
                                }
                                // if the canvas is null
                                else {
                                    // Hides npc's name
                                    _canvasManager.HideNpcName();
                                    // Shows the dialogue
                                    _canvasManager.ShowQuestDialogue(_currentNpc.givesQuest);
                                    // Hides inventory
                                    _canvasManager.HideInventory();
                                }                                
                            }

                            // Changes state to AskForQuest
                            _currentStateGuard = NPCState.State_AskForQuest;
                        }
                        break;

                    // Case for AskForQuest
                    case NPCState.State_AskForQuest:
                        // If the player is looking at a npc
                        if (_currentNpc != null)
                        {
                            // If the npc the player is looking at is the guard
                            if (_currentNpc.gameObject.layer == LayerMask.NameToLayer("Guard"))
                            {
                                if (player.HasInInventory(_currentNpc.inventoryRequirements[0], 
                                    _currentNpc.inventoryRequirements.Length))
                                {
                                    if (_canvasManager.GetQuestDialogue() == _currentNpc.askForQuest) {
                                        _canvasManager.HideDialoguePanel();
                                        _canvasManager.ShowInventory();
                                    }
                                    else {
                                        _canvasManager.HideNpcName();
                                        _canvasManager.ShowQuestDialogue(_currentNpc.askForQuest);
                                        _canvasManager.HideInventory();                                                                            
                                    }

                                    // Changes state to ThankYou
                                    _currentStateGuard = NPCState.State_ThankYou;
                                }
                                else
                                {
                                    if(_canvasManager.GetQuestDialogue() == _currentNpc.askForQuest) {
                                        _canvasManager.HideDialoguePanel();
                                        _canvasManager.ShowInventory();
                                    }
                                    else {
                                        _canvasManager.HideNpcName();
                                        _canvasManager.ShowQuestDialogue(_currentNpc.askForQuest);
                                        _canvasManager.HideInventory();                                       
                                    }
                                                   
                                    // Changes state to AskForQuest
                                    _currentStateGuard = NPCState.State_AskForQuest;
                                }
                            }
                        }
                        break;

                    // Case for ThankYou
                    case NPCState.State_ThankYou:
                        // If the player is looking at a npc
                        if (_currentNpc != null)
                        {
                            if (_canvasManager.GetQuestDialogue() == _currentNpc.thankyou) {
                                _canvasManager.HideDialoguePanel();
                                _canvasManager.ShowInventory();
                            }
                            else {
                                _canvasManager.HideNpcName();
                                _canvasManager.ShowQuestDialogue(_currentNpc.thankyou);
                                _canvasManager.HideInventory();
                            }

                            // Changes state to GivesClue
                            _currentStateGuard = NPCState.State_GivesClue;
                        }
                        break;

                    // Case for GivesClue
                    case NPCState.State_GivesClue:
                        // If the player is looking at a npc
                        if (_currentNpc != null)
                        {
                            if (_canvasManager.GetQuestDialogue() == _currentNpc.givesClue) {
                                _canvasManager.HideDialoguePanel();
                                _canvasManager.ShowInventory();
                            }
                            else {
                                _canvasManager.HideNpcName();
                                _canvasManager.ShowQuestDialogue(_currentNpc.givesClue);
                                _canvasManager.HideInventory();
                            }
                        }
                        break;
                }
            }

            // If the npc the player is looking at is the sister
            if (_currentNpc.gameObject.layer == LayerMask.NameToLayer("Sister"))
            {
                // Switch case for the sister
                switch (_currentStateSister)
                {
                    // Case for GivesQuest
                    case NPCState.State_GivesQuest:
                        // If the player is looking at a npc
                        if (_currentNpc != null)
                        {
                            if (_canvasManager != null)
                            {
                                if (_canvasManager.GetQuestDialogue() == _currentNpc.givesQuest) {
                                    _canvasManager.HideDialoguePanel();
                                    _canvasManager.ShowInventory();
                                }
                                else {
                                    _canvasManager.HideNpcName();
                                    _canvasManager.ShowQuestDialogue(_currentNpc.givesQuest);
                                    _canvasManager.HideInventory();
                                }                                
                            }

                            // Changes state to AskForQuest
                            _currentStateSister = NPCState.State_AskForQuest;
                        }
                        break;

                    // Case for AskForQuest
                    case NPCState.State_AskForQuest:
                        if (_currentNpc != null)
                        {
                            // If it's the Sister
                            if (_currentNpc.gameObject.layer == LayerMask.NameToLayer("Sister"))
                            {
                                if (!player.HasNPC(player.GetNPC()))
                                {
                                    if (_canvasManager.GetQuestDialogue() == _currentNpc.askForQuest) {
                                        _canvasManager.HideDialoguePanel();
                                        _canvasManager.ShowInventory();
                                    }
                                    else {
                                        _canvasManager.HideNpcName();
                                        _canvasManager.HideInventory();                                    
                                    }

                                    _currentNpc.actionRequirements[0].isFollower = false;
                                    // Changes state to ThankYou
                                    _currentStateSister = NPCState.State_ThankYou;
                                }
                                else
                                {
                                    if (_canvasManager.GetQuestDialogue() == _currentNpc.askForQuest) {
                                        _canvasManager.HideDialoguePanel();
                                        _canvasManager.ShowInventory();
                                    }
                                    else {
                                        _canvasManager.HideNpcName();
                                        _canvasManager.ShowQuestDialogue(_currentNpc.askForQuest);
                                        _canvasManager.HideInventory();
                                    }

                                    _npc.isComplete = false;
                                    // Changes state to AskForQuest
                                    _currentStateSister = NPCState.State_AskForQuest;
                                    // Changes state to AskForQuest
                                    _currentStateChildM = NPCState.State_AskForQuest;
                                }
                            }
                        }
                        break;

                    // Case for ThankYou
                    case NPCState.State_ThankYou:
                        if (_currentNpc != null)
                        {
                            if (_canvasManager.GetQuestDialogue() == _currentNpc.thankyou) {
                                _canvasManager.HideDialoguePanel();
                                _canvasManager.ShowInventory();
                            }
                            else {
                                _canvasManager.HideNpcName();
                                _canvasManager.ShowQuestDialogue(_currentNpc.thankyou);
                                _canvasManager.HideInventory();
                            }
                            
                            _npc.isComplete = true;

                            
                            _currentNpc.PlayAnimation("GirlHappy", true);
                            // Changes state to GivesClue
                            _currentStateSister = NPCState.State_GivesClue;
                        }
                        break;

                    // Case for GivesClue
                    case NPCState.State_GivesClue:
                        if (_currentNpc != null)
                        {
                            if (_canvasManager.GetQuestDialogue() == _currentNpc.givesClue) {
                                _canvasManager.HideDialoguePanel();
                                _canvasManager.ShowInventory();
                            }
                            else {
                                _canvasManager.HideNpcName();
                                _canvasManager.ShowQuestDialogue(_currentNpc.givesClue);
                                _canvasManager.HideInventory();

                            }
                        }
                        break;
                }
            }
            // If it's the lost child
            if (_currentNpc.gameObject.layer == LayerMask.NameToLayer("ChildM"))
            {
                // Switch case for the lost child
                switch (_currentStateChildM)
                {
                    // Case for GivesQuest
                    case NPCState.State_GivesQuest:
                        if (_currentNpc != null)
                        {
                            if (_canvasManager != null)
                            {
                                if (_currentStateSister == NPCState.State_GivesQuest) {
                                    if (_canvasManager.GetQuestDialogue() == _currentNpc.givesQuest)
                                    {
                                        _canvasManager.HideDialoguePanel();
                                        _canvasManager.ShowInventory();
                                    }
                                    else
                                    {
                                        _canvasManager.HideNpcName();
                                        _canvasManager.ShowQuestDialogue(_currentNpc.givesQuest);
                                        _canvasManager.HideInventory();
                                    }
                                }
                                else if(_currentStateSister == NPCState.State_AskForQuest)
                                    // Changes state to AskForQuest
                                    _currentStateChildM = NPCState.State_AskForQuest;

                            }                            
                        }
                        break;

                    // Case for AskForQuest
                    case NPCState.State_AskForQuest:
                        if (_currentNpc != null)
                        {
                            // If it's the lost boy
                            if (_currentNpc.gameObject.layer == LayerMask.NameToLayer("ChildM"))
                            {
                                if (_currentStateSister == NPCState.State_AskForQuest)
                                {
                                    if (_canvasManager.GetQuestDialogue() == _currentNpc.askForQuest)
                                    {
                                        _canvasManager.HideDialoguePanel();
                                        _canvasManager.ShowInventory();
                                    }
                                    else
                                    {
                                        _canvasManager.HideNpcName();
                                        _canvasManager.ShowQuestDialogue(_currentNpc.askForQuest);
                                        _canvasManager.HideInventory();
                                        if (_currentNpc.isComplete == false)
                                        {
                                            _currentNpc.isFollower = true;
                                        }
                                    }
                                }
                                else if(_currentStateSister == NPCState.State_ThankYou ||
                                    _currentStateSister == NPCState.State_GivesClue)

                                    // Changes state to ThankYou
                                    _currentStateChildM = NPCState.State_ThankYou;
                            }
                        }
                        break;

                    // Case for ThankYou
                    case NPCState.State_ThankYou:
                        if (_currentNpc != null)
                        {
                            if (_canvasManager.GetQuestDialogue() == _currentNpc.thankyou)
                            {
                                _canvasManager.HideDialoguePanel();
                                _canvasManager.ShowInventory();
                            }
                            else
                            {
                                _canvasManager.HideNpcName();
                                _canvasManager.ShowQuestDialogue(_currentNpc.thankyou);
                                _canvasManager.HideInventory();
                            }
                        }
                        break;
                }
            }

            // If it's the Villager
            if (_currentNpc.gameObject.layer == LayerMask.NameToLayer("Villager"))
            {
                // Switch case for the Villager
                switch (_currentStateVillager)
                {
                    // Case for GivesQuest
                    case NPCState.State_GivesQuest:
                        if (_currentNpc != null)
                        {
                            if (_canvasManager != null)
                            {
                                if (_canvasManager.GetQuestDialogue() == _currentNpc.givesQuest)
                                {
                                    _canvasManager.HideDialoguePanel();
                                    _canvasManager.ShowInventory();
                                }
                                else
                                {
                                    _canvasManager.HideNpcName();
                                    _canvasManager.ShowQuestDialogue(_currentNpc.givesQuest);
                                    _canvasManager.HideInventory();
                                }
                            }

                            // Changes state to AskForQuest
                            _currentStateVillager = NPCState.State_AskForQuest;
                        }
                        break;

                    // Case for AskForQuest
                    case NPCState.State_AskForQuest:
                        if (_currentNpc != null)
                        {
                            // If it's the Villager
                            if (_currentNpc.gameObject.layer == LayerMask.NameToLayer("Villager"))
                            {
                                if (_currentNpc.isActive)
                                {
                                    if (_canvasManager.GetQuestDialogue() == _currentNpc.askForQuest)
                                    {
                                        _canvasManager.HideDialoguePanel();
                                        _canvasManager.ShowInventory();
                                    }
                                    else
                                    {
                                        _canvasManager.HideNpcName();
                                        _canvasManager.ShowQuestDialogue(_currentNpc.askForQuest);
                                        _canvasManager.HideInventory();
                                    }

                                    // Changes state to ThankYou
                                    _currentStateVillager = NPCState.State_ThankYou;
                                }
                                else
                                {
                                    if (_canvasManager.GetQuestDialogue() == _currentNpc.askForQuest)
                                    {
                                        _canvasManager.HideDialoguePanel();
                                        _canvasManager.ShowInventory();
                                    }
                                    else
                                    {
                                        _canvasManager.HideNpcName();
                                        _canvasManager.ShowQuestDialogue(_currentNpc.askForQuest);
                                        _canvasManager.HideInventory();
                                    }

                                    _npc2.transform.GetChild(1).gameObject.SetActive(true);
                                    // Changes state to AskForQuest
                                    _currentStateVillager = NPCState.State_AskForQuest;
                                }
                            }
                        }
                        break;

                    // Case for ThankYou
                    case NPCState.State_ThankYou:
                        if (_currentNpc != null)
                        {
                            if (_canvasManager.GetQuestDialogue() == _currentNpc.thankyou)
                            {
                                _canvasManager.HideDialoguePanel();
                                _canvasManager.ShowInventory();
                            }
                            else
                            {
                                _canvasManager.HideNpcName();
                                _canvasManager.ShowQuestDialogue(_currentNpc.thankyou);
                                _canvasManager.HideInventory();
                            }

                            _currentNpc.isActive = false;
                            _canvasManager.recipePage.SetActive(false);
                            // Changes state to GivesClue
                            _currentStateVillager = NPCState.State_GivesClue;
                        }
                        break;

                    // Case for GivesClue
                    case NPCState.State_GivesClue:
                        if (_currentNpc != null)
                        {
                            if (_canvasManager.GetQuestDialogue() == _currentNpc.givesClue)
                            {
                                _canvasManager.HideDialoguePanel();
                                _canvasManager.ShowInventory();
                            }
                            else
                            {
                                _canvasManager.HideNpcName();
                                _canvasManager.ShowQuestDialogue(_currentNpc.givesClue);
                                _canvasManager.HideInventory();
                            }

                            _currentNpc.PlayAnimation("Thankful", true);
                            _npc2.transform.GetChild(1).gameObject.SetActive(false);
                        }
                        break;
                }
            }

            // If the NPC is the Woman
            if (_currentNpc.gameObject.layer == LayerMask.NameToLayer("Women"))
            {
                // Switch case for the Woman
                switch (_currentStateWomen)
                {
                    // Case for GivesQuest
                    case NPCState.State_GivesQuest:
                        if (_currentNpc != null)
                        {
                            if (_canvasManager != null)
                            {
                                if (_canvasManager.GetQuestDialogue() == _currentNpc.givesQuest)
                                {
                                    _canvasManager.HideDialoguePanel();
                                    _canvasManager.ShowInventory();
                                }
                                else
                                {
                                    _canvasManager.HideNpcName();
                                    _canvasManager.ShowQuestDialogue(_currentNpc.givesQuest);
                                    _canvasManager.HideInventory();
                                }
                            }

                            // Changes state to AskForQuest
                            _currentStateWomen = NPCState.State_AskForQuest;
                        }
                        break;

                    // Case for AskForQuest
                    case NPCState.State_AskForQuest:
                        if (_currentNpc != null)
                        {
                            // If it's the Woman
                            if (_currentNpc.gameObject.layer == LayerMask.NameToLayer("Women"))
                            {
                                if (player.HasInInventory(_currentNpc.inventoryRequirements[0],
                                    _currentNpc.inventoryRequirements.Length))
                                {
                                    if (_canvasManager.GetQuestDialogue() == _currentNpc.askForQuest)
                                    {
                                        _canvasManager.HideDialoguePanel();
                                        _canvasManager.ShowInventory();
                                    }
                                    else
                                    {
                                        _canvasManager.HideNpcName();
                                        _canvasManager.ShowQuestDialogue(_currentNpc.askForQuest);
                                        _canvasManager.HideInventory();
                                    }

                                    // Changes state to ThankYou
                                    _currentStateWomen = NPCState.State_ThankYou;
                                }
                                else
                                {
                                    if (_canvasManager.GetQuestDialogue() == _currentNpc.askForQuest)
                                    {
                                        _canvasManager.HideDialoguePanel();
                                        _canvasManager.ShowInventory();
                                    }
                                    else
                                    {
                                        _canvasManager.HideNpcName();
                                        _canvasManager.ShowQuestDialogue(_currentNpc.askForQuest);
                                        _canvasManager.HideInventory();
                                    }

                                    // Changes state to AskForQuest
                                    _currentStateWomen = NPCState.State_AskForQuest;
                                }
                            }
                        }
                        break;

                    // Case for ThankYou
                    case NPCState.State_ThankYou:
                        if (_currentNpc != null)
                        {
                            if (_canvasManager.GetQuestDialogue() == _currentNpc.thankyou)
                            {
                                _canvasManager.HideDialoguePanel();
                                _canvasManager.ShowInventory();
                            }
                            else
                            {
                                _canvasManager.HideNpcName();
                                _canvasManager.ShowQuestDialogue(_currentNpc.thankyou);
                                _canvasManager.HideInventory();
                            }

                            // Changes state to GivesClue
                            _currentStateWomen = NPCState.State_GivesClue;
                        }
                        break;

                    // Case for GivesClue
                    case NPCState.State_GivesClue:
                        if (_currentNpc != null)
                        {
                            if (_canvasManager.GetQuestDialogue() == _currentNpc.givesClue)
                            {
                                _canvasManager.HideDialoguePanel();
                                _canvasManager.ShowInventory();
                            }
                            else
                            {
                                _canvasManager.HideNpcName();
                                _canvasManager.ShowQuestDialogue(_currentNpc.givesClue);
                                _canvasManager.HideInventory();
                            }
                        }
                        break;
                }
            }

            // If the NPC is the Old man
            if (_currentNpc.gameObject.layer == LayerMask.NameToLayer("OldMan"))
            {
                // Switch case for the Old man
                switch (_currentStateOldMan)
                {
                    // Case for GivesQuest
                    case NPCState.State_GivesQuest:
                        if (_currentNpc != null)
                        {
                            if (_canvasManager != null)
                            {
                                if (_canvasManager.GetQuestDialogue() == _currentNpc.givesQuest)
                                {
                                    _canvasManager.HideDialoguePanel();
                                    _canvasManager.ShowInventory();
                                }
                                else
                                {
                                    _canvasManager.HideNpcName();
                                    _canvasManager.ShowQuestDialogue(_currentNpc.givesQuest);
                                    _canvasManager.HideInventory();
                                }
                            }

                            // Changes state to AskForQuest
                            _currentStateOldMan = NPCState.State_AskForQuest;
                        }
                        break;

                    // Case for AskForQuest
                    case NPCState.State_AskForQuest:
                        if (_currentNpc != null)
                        {
                            // If it's the Old man
                            if (_currentNpc.gameObject.layer == LayerMask.NameToLayer("OldMan"))
                            {
                                    if (_canvasManager.GetQuestDialogue() == _currentNpc.askForQuest)
                                    {
                                        _canvasManager.HideDialoguePanel();
                                        _canvasManager.ShowInventory();
                                    }
                                    else
                                    {
                                        _canvasManager.HideNpcName();
                                        _canvasManager.ShowQuestDialogue(_currentNpc.askForQuest);
                                        _canvasManager.HideInventory();
                                    }

                                // Changes state to ThankYou
                                _currentStateOldMan = NPCState.State_ThankYou;
                            }
                        }
                        break;

                    // Case for ThankYou
                    case NPCState.State_ThankYou:
                        if (_currentNpc != null)
                        {
                            if (_canvasManager.GetQuestDialogue() == _currentNpc.thankyou)
                            {
                                _canvasManager.HideDialoguePanel();
                                _canvasManager.ShowInventory();
                            }
                            else
                            {
                                _canvasManager.HideNpcName();
                                _canvasManager.ShowQuestDialogue(_currentNpc.thankyou);
                                _canvasManager.HideInventory();
                            }

                            // Changes state to GivesClue
                            _currentStateOldMan = NPCState.State_GivesClue;
                        }
                        break;

                    // Ccase for GivesClue
                    case NPCState.State_GivesClue:
                        if (_currentNpc != null)
                        {
                            if (_canvasManager.GetQuestDialogue() == _currentNpc.givesClue)
                            {
                                _canvasManager.HideDialoguePanel();
                                _canvasManager.ShowInventory();
                            }
                            else
                            {
                                _canvasManager.HideNpcName();
                                _canvasManager.ShowQuestDialogue(_currentNpc.givesClue);
                                _canvasManager.HideInventory();
                            }
                        }
                        break;
                }
            }

            // If the NPC is the Dragon
            if (_currentNpc.gameObject.layer == LayerMask.NameToLayer("Dragon"))
            {
                // Switch case for the Dragon
                switch (_currentStateDragon)
                {
                    // Case for GivesQuest
                    case NPCState.State_GivesQuest:
                        if (_currentNpc != null)
                        {
                            if (_canvasManager != null)
                            {
                                if (_canvasManager.GetQuestDialogue() == _currentNpc.givesQuest)
                                {
                                    _canvasManager.HideDialoguePanel();
                                    _canvasManager.ShowInventory();
                                }
                                else
                                {
                                    _canvasManager.HideNpcName();
                                    _canvasManager.ShowQuestDialogue(_currentNpc.givesQuest);
                                    _canvasManager.HideInventory();
                                }
                            }

                            // Changes state to AskForQuest
                            _currentStateDragon = NPCState.State_AskForQuest;
                        }
                        break;

                    // Case for AskForQuest
                    case NPCState.State_AskForQuest:
                        if (_currentNpc != null)
                        {
                            // If it's the Dragon
                            if (_currentNpc.gameObject.layer == LayerMask.NameToLayer("Dragon"))
                            {
                                if (player.HasInInventory(_currentNpc.inventoryRequirements[0],
                                    _currentNpc.inventoryRequirements.Length))
                                {
                                    if (_canvasManager.GetQuestDialogue() == _currentNpc.askForQuest)
                                    {
                                        _canvasManager.HideDialoguePanel();
                                        _canvasManager.ShowInventory();
                                    }
                                    else
                                    {
                                        _canvasManager.HideNpcName();
                                        _canvasManager.ShowQuestDialogue(_currentNpc.askForQuest);
                                        _canvasManager.HideInventory();
                                    }

                                    // Plays the dragon's audio
                                    _currentNpc.GetComponent<AudioSource>().Play();
                                    // Changes state to ThankYou
                                    _currentStateDragon = NPCState.State_ThankYou;
                                }
                                else
                                {
                                    if (_canvasManager.GetQuestDialogue() == _currentNpc.askForQuest)
                                    {
                                        _canvasManager.HideDialoguePanel();
                                        _canvasManager.ShowInventory();
                                    }
                                    else
                                    {
                                        _canvasManager.HideNpcName();
                                        _canvasManager.ShowQuestDialogue(_currentNpc.askForQuest);
                                        _canvasManager.HideInventory();
                                    }

                                    // Changes state to AskForQuest
                                    _currentStateDragon = NPCState.State_AskForQuest;
                                }
                            }
                        }
                        break;

                    // Case for ThankYou
                    case NPCState.State_ThankYou:
                        if (_currentNpc != null)
                        {
                            if (_canvasManager.GetQuestDialogue() == _currentNpc.thankyou)
                            {
                                _canvasManager.HideDialoguePanel();
                                _canvasManager.ShowInventory();
                            }
                            else
                            {
                                _canvasManager.HideNpcName();
                                _canvasManager.ShowQuestDialogue(_currentNpc.thankyou);
                                _canvasManager.HideInventory();
                            }

                            // Changes state to GivesClue
                            _currentStateDragon = NPCState.State_GivesClue;
                        }
                        break;

                    // Case for GivesClue
                    case NPCState.State_GivesClue:
                        if (_currentNpc != null)
                        {
                            // If it's the Dragon
                            if (_canvasManager.GetQuestDialogue() == _currentNpc.givesClue)
                            {
                                _canvasManager.HideDialoguePanel();
                                _canvasManager.ShowInventory();
                            }
                            else
                            {
                                _canvasManager.HideNpcName();
                                _canvasManager.ShowQuestDialogue(_currentNpc.givesClue);
                                _canvasManager.HideInventory();

                                _canvasManager.HideDialoguePanel();
                                // Opens the next scene
                                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                            }
                        }
                        break;
                }
            }
        }
    }

    /// <summary>
    /// Cheat method
    /// </summary>
    private void CheatMethod()
    {
        // If the player presses 1
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            // Changes the Guard's state to GivesClue
            _currentStateGuard = NPCState.State_GivesClue;
        }
        // If the player presses 2
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            // Changes the Sister's state to GivesClue
            _currentStateSister = NPCState.State_GivesClue;
        }
        // If the player presses 3
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            // Changes the Villager's state to GivesClue
            _currentStateVillager = NPCState.State_GivesClue;
        }
        // If the player presses 4
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            // Changes the Woman's state to GivesClue
            _currentStateWomen = NPCState.State_GivesClue;
        }
        // If the player presses 5
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            // Moves the player next to the old man's position
            player.transform.position = new Vector3(-31.947f, 13.168f, -55.283f);
        }
        // If the player presses 6
        else if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            // Moves the player next to the dragon's position
            player.transform.position = new Vector3(57.5f, 55.0f, 66.8f);
        }
    }
}
