using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State_Manager : MonoBehaviour
{
    public Player player;
    public NPC_Actions _npc;

    private NPC_State _currentStateSister;
    private NPC_State _currentStateGuard;
    private NPC_State _currentStateChildM;
    private NPC_State _currentStateVillager;
    private NPC_Actions _currentNpc;
    private Interactible _currentInteractible;
    private CanvasManager _canvasManager;

    void Start()
    {
        _canvasManager = CanvasManager.instance;

        _currentInteractible = null;

        _currentStateSister = NPC_State.State_GivesQuest;

        _currentStateChildM = NPC_State.State_GivesQuest;

        _currentStateGuard = NPC_State.State_GivesQuest;

        _currentStateVillager = NPC_State.State_GivesQuest;
    }

    void Update()
    {
        _currentNpc = player.GetNPC();

        if(_currentNpc != null && _currentStateGuard == NPC_State.State_GivesClue &&
            _currentNpc.gameObject.layer == LayerMask.NameToLayer("Guard"))
            _currentNpc.transform.position = new Vector3(-161.78f, 1.91f, -25.3f);

        if (Input.GetMouseButtonDown(0))
            DefineState();
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
                                if (player.HasRequirements(_currentInteractible))
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

                                    _canvasManager.ClearAllInventorySlotIcons();
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
                                _currentNpc.PlayAnimation("move");
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

                                    _canvasManager.ClearAllInventorySlotIcons();
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
                        }
                        break;
                }
            }
        }
    }
}
