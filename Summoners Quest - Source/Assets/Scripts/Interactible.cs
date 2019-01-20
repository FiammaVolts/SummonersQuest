using UnityEngine;

public class Interactible : MonoBehaviour
{
    public bool isInteractive;
    public bool isActive;
    public bool isPickable;
    public bool allowsMultipleInteractions;
    public Sprite inventoryIcon;
    public string interactionText;
    
    public Interactible[] indirectInteractibles;
    public Interactible[] indirectActivations;
   

    public void Activate()
    {
        isActive = true;
    }

    public void Interact()
    {
        if (isActive)
            InteractActive();
        else
            InteractInactive();
    }

    private void InteractActive()
    {
        InteractIndirects();

        ActivateIndirects();

        if (!allowsMultipleInteractions)
            isInteractive = false;
    }

    private void InteractInactive()
    {
        PlayAnimation("InteractInactive");
    }

    private void PlayAnimation(string animationName)
    {
        Animator animator = GetComponent<Animator>();

        if (animator != null)
            animator.SetTrigger(animationName);
    }

    private void InteractIndirects()
    {
        if (indirectInteractibles != null)
        {
            for (int i = 0; i < indirectInteractibles.Length; ++i)
                indirectInteractibles[i].Interact();
        }
    }

    private void ActivateIndirects()
    {
        if (indirectActivations != null)
        {
            for (int i = 0; i < indirectActivations.Length; ++i)
                indirectActivations[i].Activate();
        }
    }
}