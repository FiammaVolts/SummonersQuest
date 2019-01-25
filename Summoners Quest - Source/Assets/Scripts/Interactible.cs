using UnityEngine;

/// <summary>
/// Class for interactible
/// </summary>
public class Interactible : MonoBehaviour
{
    // Creates a boolean to check if the object is interactible
    public bool isInteractive;
    // Creates a boolean to check if it's a pickable object
    public bool isPickable;
    // Creates a boolean to check if the object allows multiple interactions
    public bool allowsMultipleInteractions;
    // Creates a boolean to consume the requirements
    public bool consumesRequirements;
    // Creates a Sprite for the inventory icons
    public Sprite inventoryIcon;
    // String for the interaction texts
    public string interactionText;
    // An instance of InteractibleType
    public InteractibleType type;

    // Creates an array of Interactible_type for the inventory's requirements
    public InteractibleType[] inventoryRequirements;
    // Creates an array of NPC actions for the indirect activations
    public NPCActions[] indirectActivations;
}