using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Player : MonoBehaviour
{
    private static Player _instance;

    public float maxInteractionDistance;
    public int inventorySize;
    public NPC_Actions npc;

    private NPC_Actions _currentNpc;
    private NPC_State _currentState;
    private CanvasManager _canvasManager;
    private Camera _camera;
    private RaycastHit _raycastHit;
    private Interactible _currentInteractible;
    private List<Interactible> _inventory;

    public static Player instance {
        get { return _instance; }
    }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        _canvasManager = CanvasManager.instance;

        _camera = GetComponentInChildren<Camera>();

        _currentInteractible = null;

        _currentNpc = null;

        _inventory = new List<Interactible>(inventorySize);
    }

    void Update()
    {
        CheckForInteractible();
        CheckForInteractionClick();        
    }

    private void FixedUpdate()
    {
        CheckForNPC();
    }

    public NPC_Actions GetNPC()
    {
        return _currentNpc;
    }

    public NPC_Actions GetNPC(NPC_Actions npc)
    {
        return _currentNpc;
    }

    private void CheckForInteractible()
    {
        if (Physics.Raycast(_camera.transform.position,
            _camera.transform.forward, out _raycastHit,
            maxInteractionDistance))
        {
            Interactible newInteractible = _raycastHit.collider.GetComponent<Interactible>();

            if (newInteractible != null && newInteractible.isInteractive)
            {
                SetInteractible(newInteractible);
            }
            else
                ClearInteractible();
        }
        else
            ClearInteractible();
    }

    private void CheckForNPC()
    {
        if (Physics.Raycast(_camera.transform.position,
            _camera.transform.forward, out _raycastHit,
            maxInteractionDistance))
        {
            NPC_Actions newNpc = _raycastHit.collider.GetComponent<NPC_Actions>();

            if (newNpc != null)
            {
                SetNpc(newNpc);
            }
            else
                ClearNpc();
        }
        else
            ClearNpc();
    }

    private void CheckForInteractionClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (_currentInteractible != null)
            {
                if (_currentInteractible.isPickable)
                    AddToInventory(_currentInteractible);
                else if (HasRequirements(_currentInteractible))
                    Interact(_currentInteractible);
            }
            else if (_currentNpc != null)
            {
                if (_currentNpc.inventoryRequirements.Length > 0)
                {
                    if (HasInInventory(_currentNpc.inventoryRequirements[0], _currentNpc.inventoryRequirements.Length))
                    {
                        Interact();
                    }
                }
            }
        }
    }

    private void SetInteractible(Interactible newInteractible)
    {
        _currentInteractible = newInteractible;

        _canvasManager.ShowInteractionPanel(_currentInteractible.interactionText);
    }

    private void ClearInteractible()
    {
        _currentInteractible = null;

        _canvasManager.HideInteractionPanel();
    }

    private void SetNpc(NPC_Actions newNpc)
    {
        _currentNpc = newNpc;

        _canvasManager.ShowNpcName(_currentNpc.npcName);
    }

    private void ClearNpc()
    {
        _currentNpc = null;

        _canvasManager.HideNpcName();
    }

    public bool HasRequirements(Interactible interactible)
    {
        if (_currentInteractible != null)
            for (int i = 0; i < _currentInteractible.inventoryRequirements.Length; ++i) {
                if (!HasInInventory(_currentInteractible.inventoryRequirements[i]))
                    return false;
                else
                    npc.isActive = true;
            }

        return true;
    }

    public bool HasNPC(NPC_Actions sister)
    {
        for (int i = 0; i < sister.actionRequirements.Length; ++i)
            if (sister.actionRequirements[i].isFollower == true)
                return false;

        return true;
    }

    private void Interact(Interactible interactible)
    {
        if(_currentInteractible != null)
        {
            for (int i = 0; i < _currentInteractible.inventoryRequirements.Length; ++i)
            {
                RemoveFromInventory(_currentInteractible.inventoryRequirements[i]);                
            }
        }        
    }

    private void Interact()
    {
        if (_currentNpc != null)
        {
            if (_currentNpc.consumesRequirements)
            {
                for (int i = 0; i < _currentNpc.inventoryRequirements.Length; ++i)
                {
                    RemoveFromInventory(_currentNpc.inventoryRequirements[i]);
                }
            }
        }
    }

    private void AddToInventory(Interactible pickable)
    {
        if (_inventory.Count < inventorySize)
        {
            _inventory.Add(pickable);
            pickable.gameObject.SetActive(false);

            UpdateInventoryIcons();
        }
    }

    private bool HasInInventory(Interactible_type pickable)
    {
        foreach (Interactible item in _inventory)
        {
            if (item.type == pickable)
                return true;
        }

        return false;

    }

    public bool HasInInventory(Interactible_type pickable, int nRequirements)
    {
        if (_inventory.Count < nRequirements) return false;

        int n = 0;

        for (int i = 0; i < _inventory.Count; i++)
        {
            if (_inventory[i].type == pickable)
            {
                n++;
                if (n >= nRequirements)
                {
                    return true;
                }
            }
        }

        return false;

    }

    private void RemoveFromInventory(Interactible_type interType)
    {
        Interactible inter = _inventory.Find(it => it.type == interType);

        if (inter != null)
            _inventory.Remove(inter);

        UpdateInventoryIcons();
    }

    private void UpdateInventoryIcons()
    {
        for (int i = 0; i < inventorySize; ++i)
        {
            if (i < _inventory.Count)
                _canvasManager.SetInventorySlotIcon(i, _inventory[i].inventoryIcon);
            else
                _canvasManager.ClearInventorySlotIcon(i);
        }
    }
}

