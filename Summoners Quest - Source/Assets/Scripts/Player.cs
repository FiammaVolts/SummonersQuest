using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float maxInteractionDistance;
    public int inventorySize;

    private CanvasManager _canvasManager;
    private Camera _camera;
    private RaycastHit _raycastHit;
    private Interactible _currentInteractible;
    private List<Interactible> _inventory;

    private void Start()
    {
        _canvasManager = CanvasManager.instance;

        _camera = GetComponentInChildren<Camera>();

        _currentInteractible = null;

        _inventory = new List<Interactible>(inventorySize);
    }

    void Update()
    {
        CheckForInteractible();
        CheckForInteractionClick();
    }

    private void CheckForInteractible()
    {
        if (Physics.Raycast(_camera.transform.position,
            _camera.transform.forward, out _raycastHit,
            maxInteractionDistance))
        {
            Interactible newInteractible = _raycastHit.collider.GetComponent<Interactible>();

            if (newInteractible != null && newInteractible.isInteractive)
                SetInteractible(newInteractible);
            else
                ClearInteractible();
        }
        else
            ClearInteractible();
    }

    private void CheckForInteractionClick()
    {
        if (Input.GetMouseButtonDown(0) && _currentInteractible != null)
        {
            if (_currentInteractible.isPickable)
            {
                AddToInventory(_currentInteractible);
                //_currentInteractible.PlayAnimation("InteractActive");
            }
            else if (HasRequirements(_currentInteractible))
            {
                Interact(_currentInteractible);
                _currentInteractible.PlayAnimation("InteractActive");
            }
        }
    }

    private void SetInteractible(Interactible newInteractible)
    {
        _currentInteractible = newInteractible;

        if (HasRequirements(_currentInteractible))
            _canvasManager.ShowInteractionPanel(_currentInteractible.interactionText);
        else
            _canvasManager.ShowInteractionPanel(_currentInteractible.requirementText);
    }

    private void ClearInteractible()
    {
        _currentInteractible = null;

        _canvasManager.HideInteractionPanel();
    }

    private bool HasRequirements(Interactible interactible)
    {
        for (int i = 0; i < interactible.inventoryRequirements.Length; ++i)
            if (!HasInInventory(interactible.inventoryRequirements[i]))
                return false;

        return true;
    }

    private void Interact(Interactible interactible)
    {
        if (interactible.consumesRequirements)
        {
            for (int i = 0; i < interactible.inventoryRequirements.Length; ++i)
            {
                RemoveFromInventory(interactible.inventoryRequirements[i]);                
            }
        } 

        interactible.Interact();
    }

    private void AddToInventory(Interactible pickable)
    {
        if (_inventory.Count < inventorySize)
        {
            _inventory.Add(pickable);
            pickable.gameObject.SetActive(false);

            //UpdateInventoryIcons();
        }
    }

    private bool HasInInventory(Interactible pickable)
    {
        return _inventory.Contains(pickable);
    }

    private void RemoveFromInventory(Interactible pickable)
    {
        pickable.gameObject.SetActive(true);
        _inventory.Remove(pickable);     
        pickable.GetComponent<Animator>().SetTrigger("InteractActivate");
        _inventory.Add(pickable);

        //UpdateInventoryIcons();
    }

    //private void UpdateInventoryIcons()
    //{
    //    for (int i = 0; i < inventorySize; ++i)
    //    {
    //        if (i < _inventory.Count)
    //            _canvasManager.SetInventorySlotIcon(i, _inventory[i].inventoryIcon);
    //        else
    //            _canvasManager.ClearInventorySlotIcon(i);
    //    }
    //}

}
