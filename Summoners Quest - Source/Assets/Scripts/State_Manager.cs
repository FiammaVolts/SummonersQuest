using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State_Manager : MonoBehaviour
{
    public Player player;

    private NPC_State _currentStateSister;
    private NPC_State _currentStateGuard;
    private NPC_Actions _currentNpc;
    private Interactible _currentInteractible;
    private CanvasManager _canvasManager;

    void Start()
    {
        _canvasManager = CanvasManager.instance;

        _currentInteractible = null;

        _currentStateSister = NPC_State.State_GivesQuest;

        _currentStateGuard = NPC_State.State_GivesQuest;
    }

    void Update()
    {
        _currentNpc = player.GetNPC();

        if(_currentNpc != null && _currentStateGuard == NPC_State.State_GivesClue && _currentNpc.gameObject.layer == LayerMask.NameToLayer("Guard"))
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
                                _canvasManager.ShowQuestDialogue(_currentNpc.givesQuest);
                                _canvasManager.HideInventory();
                            }

                            StartCoroutine(TimeDialogueBox());

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
                                    _canvasManager.HideInventory();

                                    StartCoroutine(TimeDialogueBox());

                                    _canvasManager.ClearAllInventorySlotIcons();

                                    _currentStateGuard = NPC_State.State_ThankYou;
                                }
                                else
                                {
                                    _canvasManager.ShowQuestDialogue(_currentNpc.askForQuest);
                                    _canvasManager.HideInventory();

                                    StartCoroutine(TimeDialogueBox());

                                    _currentStateGuard = NPC_State.State_AskForQuest;
                                }
                            }
                        }
                        break;

                    case NPC_State.State_ThankYou:
                        if (_currentNpc != null)
                        {
                            _canvasManager.ShowQuestDialogue(_currentNpc.thankyou);
                            _canvasManager.HideInventory();

                            StartCoroutine(TimeDialogueBox());

                            _currentNpc.PlayAnimation("move");

                            _currentStateGuard = NPC_State.State_GivesClue;
                        }
                        break;

                    case NPC_State.State_GivesClue:
                        if (_currentNpc != null)
                        {
                            _canvasManager.ShowQuestDialogue(_currentNpc.givesClue);
                            _canvasManager.HideInventory();

                            StartCoroutine(TimeDialogueBox());
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
                                _canvasManager.ShowQuestDialogue(_currentNpc.givesQuest);
                                _canvasManager.HideInventory();
                            }

                            StartCoroutine(TimeDialogueBox());

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
                                    _canvasManager.HideInventory();

                                    StartCoroutine(TimeDialogueBox());

                                    _canvasManager.ClearAllInventorySlotIcons();

                                    _currentNpc.actionRequirements[0].isFollower = false;

                                    _currentStateSister = NPC_State.State_ThankYou;
                                }
                                else
                                {
                                    _canvasManager.ShowQuestDialogue(_currentNpc.askForQuest);
                                    _canvasManager.HideInventory();

                                    StartCoroutine(TimeDialogueBox());

                                    _currentStateSister = NPC_State.State_AskForQuest;
                                }
                            }
                        }
                        break;

                    case NPC_State.State_ThankYou:
                        if (_currentNpc != null)
                        {
                            _canvasManager.ShowQuestDialogue(_currentNpc.thankyou);
                            _canvasManager.HideInventory();

                            StartCoroutine(TimeDialogueBox());                            

                            _currentStateSister = NPC_State.State_GivesClue;
                        }
                        break;

                    case NPC_State.State_GivesClue:
                        if (_currentNpc != null)
                        {
                            _canvasManager.ShowQuestDialogue(_currentNpc.givesClue);
                            _canvasManager.HideInventory();
                            
                            StartCoroutine(TimeDialogueBox());
                        }
                        break;
                }
            }
        }
    }

    private IEnumerator TimeDialogueBox()
    {
        yield return new WaitForSecondsRealtime(7);
        _canvasManager.HideDialoguePanel();
        _canvasManager.ShowInventory();
        StopAllCoroutines();
    }
}
