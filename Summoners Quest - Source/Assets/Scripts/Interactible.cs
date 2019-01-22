using UnityEngine;

public class Interactible : MonoBehaviour
{
    public bool isInteractive;    
    public bool isPickable;
    public bool allowsMultipleInteractions;
    public bool consumesRequirements;
    public Sprite inventoryIcon;
    public string interactionText;
    public Interactible_type type;

    public Interactible_type[] inventoryRequirements;
    public NPC_Actions[] indirectActivations;
}