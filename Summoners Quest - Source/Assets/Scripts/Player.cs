using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// Class for Player
/// </summary>
public class Player : MonoBehaviour
{
    // Creates an instance of player
    private static Player _instance;

    // Float for the maximum distance for the interactions
    public float maxInteractionDistance;
    // Variable for the inventory size
    public int inventorySize;
    // Instance of NPC_Actions
    public NPCActions npc;

    // Instance of NPC_Actions
    private NPCActions _currentNpc;
    // Instance of NPC_State
    private NPCState _currentState;
    // Instance of CanvasManager
    private CanvasManager _canvasManager;
    // Instance of Cameras
    private Camera _camera;
    // Instance of RayCastHit
    private RaycastHit _raycastHit;
    // Instance of Interactible
    private Interactible _currentInteractible;
    // List of Interactibles
    private List<Interactible> _inventory;

    /// <summary>
    /// Readonly property that returns instance
    /// </summary>
    public static Player instance {
        get { return _instance; }
    }

    /// <summary>
    /// Awake method that will run before Start
    /// </summary>
    private void Awake()
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
    /// Start method
    /// </summary>
    private void Start()
    {
        // Sets the _canvasManager to CanvasManager's instance
        _canvasManager = CanvasManager.instance;

        // Sets _camera to the player's camera
        _camera = GetComponentInChildren<Camera>();

        // Sets it at null
        _currentInteractible = null;

        // Sets it at null
        _currentNpc = null;

        // Instanciates the list
        _inventory = new List<Interactible>(inventorySize);
    }

    /// <summary>
    /// Void method that runs in every frame
    /// </summary>
    void Update()
    {
        // Calls CheckForInteractible()
        CheckForInteractible();
        // Calls CheckForInteractionClick()
        CheckForInteractionClick();        
    }

    /// <summary>
    /// FixedUpdate method that will run in less frames than Update
    /// </summary>
    private void FixedUpdate()
    {
        // Calls CheckForNPC()
        CheckForNPC();
    }

    /// <summary>
    /// NPC_Actions's method that will return the current NPC
    /// </summary>
    /// <returns></returns>
    public NPCActions GetNPC()
    {
        return _currentNpc;
    }

    /// <summary>
    /// NPC_Action's method tht returns the current NPC
    /// </summary>
    /// <param name="npc"></param>
    /// <returns></returns>
    public NPCActions GetNPC(NPCActions npc)
    {
        return _currentNpc;
    }

    /// <summary>
    /// Method that will check for interactibles
    /// </summary>
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

    /// <summary>
    /// Method that will check for NPCs
    /// </summary>
    private void CheckForNPC()
    {
        if (Physics.Raycast(_camera.transform.position,
            _camera.transform.forward, out _raycastHit,
            maxInteractionDistance))
        {
            NPCActions newNpc = _raycastHit.collider.GetComponent<NPCActions>();

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

    /// <summary>
    /// Method that will check for interaction clicks
    /// </summary>
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

    /// <summary>
    /// Method that will set the new interactibles
    /// </summary>
    /// <param name="newInteractible"></param>
    private void SetInteractible(Interactible newInteractible)
    {
        _currentInteractible = newInteractible;

        // Will show the interaction panel
        _canvasManager.ShowInteractionPanel(_currentInteractible.interactionText);
    }

    /// <summary>
    /// Method that will clear the interactible
    /// </summary>
    private void ClearInteractible()
    {
        // Sets the interactible at null
        _currentInteractible = null;

        // Hides the interaction panel
        _canvasManager.HideInteractionPanel();
    }

    /// <summary>
    /// Method that will set the NPCs
    /// </summary>
    /// <param name="newNpc"></param>
    private void SetNpc(NPCActions newNpc)
    {
        // Sets the currentNPC to newNpc
        _currentNpc = newNpc;

        // Shows the npc's name
        _canvasManager.ShowNpcName(_currentNpc.npcName);
    }

    /// <summary>
    /// Method that will hide the Npc's name
    /// </summary>
    private void ClearNpc()
    {
        // Sets currentNpc to null
        _currentNpc = null;

        // Hides the npc's name
        _canvasManager.HideNpcName();
    }

    /// <summary>
    /// Bool method that will check if the player has the objects
    /// </summary>
    /// <param name="interactible"></param>
    /// <returns></returns>
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

    /// <summary>
    /// Bool method that wull check if the player has the NPC, needed for the sister's mission
    /// </summary>
    /// <param name="sister"></param>
    /// <returns></returns>
    public bool HasNPC(NPCActions sister)
    {
        for (int i = 0; i < sister.actionRequirements.Length; ++i)
            if (sister.actionRequirements[i].isFollower == true)
                return false;

        return true;
    }

    /// <summary>
    /// Method for interactions
    /// </summary>
    /// <param name="interactible"></param>
    private void Interact(Interactible interactible)
    {
        // If the current interactible isn't null
        if(_currentInteractible != null)
        {
            // A for cycle that will run through the interactibles
            for (int i = 0; i < _currentInteractible.inventoryRequirements.Length; ++i)
            {
                // Will remove the interactible from the inventory
                RemoveFromInventory(_currentInteractible.inventoryRequirements[i]);                
            }
        }        
    }

    /// <summary>
    /// Method for the interactions
    /// </summary>
    private void Interact()
    {
        // If the player is looking at a npc
        if (_currentNpc != null)
        {           
            if (_currentNpc.consumesRequirements)
            {
                // A for that will run through the inventory
                for (int i = 0; i < _currentNpc.inventoryRequirements.Length; ++i)
                {
                    // Will remove the requirement from the inventory
                    RemoveFromInventory(_currentNpc.inventoryRequirements[i]);
                }
            }
        }
    }

    /// <summary>
    /// Method that will add interactibles to the inventory
    /// </summary>
    /// <param name="pickable"></param>
    private void AddToInventory(Interactible pickable)
    {
        // An if that will check if the amount of objects is bigger than the
        // size of the inventory
        if (_inventory.Count < inventorySize)
        {
            // Will add the pickable to the inventory
            _inventory.Add(pickable);
            // Will set the pickable to false
            pickable.gameObject.SetActive(false);

            // Will play an audio for when the player picks up an object
            GetComponent<AudioSource>().Play();
            // Updates the inventory icons
            UpdateInventoryIcons();
        }
    }

    /// <summary>
    /// Bool that will check if the player has an interactible on the inventory
    /// </summary>
    /// <param name="pickable"></param>
    /// <returns></returns>
    private bool HasInInventory(InteractibleType pickable)
    {
        foreach (Interactible item in _inventory)
        {
            if (item.type == pickable)
                return true;
        }

        return false;

    }

    /// <summary>
    /// Bool that will check if the player has an interactible on the inventory
    /// </summary>
    /// <param name="pickable"></param>
    /// <param name="nRequirements"></param>
    /// <returns></returns>
    public bool HasInInventory(InteractibleType pickable, int nRequirements)
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

    /// <summary>
    /// Method that will remove the interactibles from the inventory
    /// </summary>
    /// <param name="interType"></param>
    private void RemoveFromInventory(InteractibleType interType)
    {
        Interactible inter = _inventory.Find(it => it.type == interType);

        if (inter != null)
            _inventory.Remove(inter);

        // Calls UpdateInventoryIcons
        UpdateInventoryIcons();
    }

    /// <summary>
    /// Method that will update the inventory icons
    /// </summary>
    private void UpdateInventoryIcons()
    {
        // A for cycle that will run through the inventory
        for (int i = 0; i < inventorySize; ++i)
        {
            // If it's less than the spaces available
            if (i < _inventory.Count)
                // Will add it to the inventory, adding the icon to the slots
                _canvasManager.SetInventorySlotIcon(i, _inventory[i].inventoryIcon);
            else
                //Will clear the inventory's slots
                _canvasManager.ClearInventorySlotIcon(i);
        }
    }
}

