using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State_Manager : MonoBehaviour
{
    public Player player;
    public NPC_Actions _npc;
    public NPC_Actions _npc2;

    private NPC_State _currentStateSister;
    private NPC_State _currentStateGuard;
    private NPC_State _currentStateChildM;
    private NPC_State _currentStateVillager;
    private NPC_State _currentStateWomen;
    private NPC_Actions _currentNpc;
    private CanvasManager _canvasManager;

    void Start()
    {
        _canvasManager = CanvasManager.instance;
        
        _currentStateSister = NPC_State.State_GivesQuest;
        _currentStateChildM = NPC_State.State_GivesQuest;
        _currentStateGuard = NPC_State.State_GivesQuest;
        _currentStateVillager = NPC_State.State_GivesQuest;
        _currentStateWomen = NPC_State.State_GivesQuest;

        _npc2.transform.GetChild(1).gameObject.SetActive(false);
    }

    void Update()
    {
        _currentNpc = player.GetNPC();

        if(_currentNpc != null && _currentStateGuard == NPC_State.State_GivesClue &&
            _currentNpc.gameObject.layer == LayerMask.NameToLayer("Guard"))
            _currentNpc.transform.position = new Vector3(-161.78f, 1.91f, -25.3f);

        if (Input.GetMouseButtonDown(0))
            DefineState();

        if(_npc2.transform.GetChild(1).gameObject.activeSelf == true)
        {
            _canvasManager.ShowRecipePage();
        }

        CheatMethod();
    }

    private void DefineState()
    {
        if (_currentNpc != null)
        {
            if (_currentNpc.gameObject.layer == LayerMask.NameToLayer("Guard"))
            {
                switch (_currentStateGuard)
                {
                    case NPC_State.State_GivesQuest:
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

                            _currentStateGuard = NPC_State.State_AskForQuest;
                        }
                        break;

                    case NPC_State.State_AskForQuest:
                        if (_currentNpc != null)
                        {
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

                                    _currentStateGuard = NPC_State.State_ThankYou;
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
                                                                        
                                    _currentStateGuard = NPC_State.State_AskForQuest;
                                }
                            }
                        }
                        break;

                    case NPC_State.State_ThankYou:
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
                            
                            _currentStateGuard = NPC_State.State_GivesClue;
                        }
                        break;

                    case NPC_State.State_GivesClue:
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

            if (_currentNpc.gameObject.layer == LayerMask.NameToLayer("Sister"))
            {
                switch (_currentStateSister)
                {
                    case NPC_State.State_GivesQuest:
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

                            _currentStateSister = NPC_State.State_AskForQuest;
                        }
                        break;

                    case NPC_State.State_AskForQuest:
                        if (_currentNpc != null)
                        {
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
                                    _currentStateSister = NPC_State.State_ThankYou;
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
                                    _currentStateSister = NPC_State.State_AskForQuest;
                                    _currentStateChildM = NPC_State.State_AskForQuest;
                                }
                            }
                        }
                        break;

                    case NPC_State.State_ThankYou:
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

                            _currentNpc.PlayAnimation("GirlHappy");
                            _currentStateSister = NPC_State.State_GivesClue;
                        }
                        break;

                    case NPC_State.State_GivesClue:
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
            if (_currentNpc.gameObject.layer == LayerMask.NameToLayer("ChildM"))
            {
                switch (_currentStateChildM)
                {
                    case NPC_State.State_GivesQuest:
                        if (_currentNpc != null)
                        {
                            if (_canvasManager != null)
                            {
                                if (_currentStateSister == NPC_State.State_GivesQuest) {
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
                                else if(_currentStateSister == NPC_State.State_AskForQuest)
                                    _currentStateChildM = NPC_State.State_AskForQuest;

                            }                            
                        }
                        break;

                    case NPC_State.State_AskForQuest:
                        if (_currentNpc != null)
                        {
                            if (_currentNpc.gameObject.layer == LayerMask.NameToLayer("ChildM"))
                            {
                                if (_currentStateSister == NPC_State.State_AskForQuest)
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
                                            _currentNpc.isFollower = true;
                                    }
                                }
                                else if(_currentStateSister == NPC_State.State_ThankYou ||
                                    _currentStateSister == NPC_State.State_GivesClue)

                                    _currentStateChildM = NPC_State.State_ThankYou;
                            }
                        }
                        break;

                    case NPC_State.State_ThankYou:
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

            if (_currentNpc.gameObject.layer == LayerMask.NameToLayer("Villager"))
            {
                switch (_currentStateVillager)
                {
                    case NPC_State.State_GivesQuest:
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

                            _currentStateVillager = NPC_State.State_AskForQuest;
                        }
                        break;

                    case NPC_State.State_AskForQuest:
                        if (_currentNpc != null)
                        {
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
                                    
                                    _currentStateVillager = NPC_State.State_ThankYou;
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
                                    _currentStateVillager = NPC_State.State_AskForQuest;
                                }
                            }
                        }
                        break;

                    case NPC_State.State_ThankYou:
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
                            _currentStateVillager = NPC_State.State_GivesClue;
                        }
                        break;

                    case NPC_State.State_GivesClue:
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

                            _npc2.transform.GetChild(1).gameObject.SetActive(false);
                        }
                        break;
                }
            }

            if (_currentNpc.gameObject.layer == LayerMask.NameToLayer("Women"))
            {
                switch (_currentStateWomen)
                {
                    case NPC_State.State_GivesQuest:
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

                            _currentStateWomen = NPC_State.State_AskForQuest;
                        }
                        break;

                    case NPC_State.State_AskForQuest:
                        if (_currentNpc != null)
                        {
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
                                    
                                    _currentStateWomen = NPC_State.State_ThankYou;
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

                                    _currentStateWomen = NPC_State.State_AskForQuest;
                                }
                            }
                        }
                        break;

                    case NPC_State.State_ThankYou:
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

                            _currentStateWomen = NPC_State.State_GivesClue;
                        }
                        break;

                    case NPC_State.State_GivesClue:
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
        }
    }

    private void CheatMethod()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            _currentStateGuard = NPC_State.State_GivesClue;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            _currentStateSister = NPC_State.State_GivesClue;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            _currentStateVillager = NPC_State.State_GivesClue;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            _currentStateWomen = NPC_State.State_GivesClue;
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            player.transform.position = new Vector3();
        }
    }
}
