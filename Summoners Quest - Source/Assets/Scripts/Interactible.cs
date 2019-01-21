using UnityEngine;

public class Interactible : MonoBehaviour
{
    public bool isInteractive;    
    public bool isPickable;
    public bool allowsMultipleInteractions;
    public Sprite inventoryIcon;
    public string interactionText;
    public Interactible_type type;
    
    public Interactible[] indirectInteractibles;
    public NPC_Actions[] indirectActivations;


    public void Interact()
    {
        if (indirectActivations[0].isActive)
            InteractActive();
    }

    private void InteractActive()
    {
        InteractIndirects();

        ActivateIndirects();

        if (!allowsMultipleInteractions)
            isInteractive = false;
    }

    // Verifica se tem todos os requisitos
    private void InteractIndirects()
    {
        if (indirectInteractibles != null)
        {
            for (int i = 0; i < indirectInteractibles.Length; ++i)
                indirectInteractibles[i].Interact();
        }
    }

    //Ativa NPC
    private void ActivateIndirects()
    {
        if (indirectActivations != null)
        {
            for (int i = 0; i < indirectActivations.Length; ++i)
                indirectActivations[i].Activate();
        }
    }
}