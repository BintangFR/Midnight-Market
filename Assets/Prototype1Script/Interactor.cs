using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactor : MonoBehaviour
{
    [SerializeField] private LayerMask interactLayer;
    public static Interactor instance;
    // Update is called once per frame

    private void Awake() {
        instance = this;
    }
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.E))
        {

            float interactRange = 2f;
            // Find all colliders within the interact range that are on the interact layer              
            Collider[] colliderArray = Physics.OverlapSphere(transform.position, interactRange, interactLayer);
            foreach (Collider collider in colliderArray)
            {
                // Check if the collider has a component that implements the IInteractable interface
                if (collider.TryGetComponent(out IInteractable interactable))
                {
                    interactable.Interact();
                }
                Debug.Log(collider);
            }
        }
    }
    //this method is for showing a interact text
    public  IInteractable GetInteractableObject(){
        float interactRange = 2f;
        Collider[] colliderArray = Physics.OverlapSphere(transform.position, interactRange, interactLayer);
        foreach (Collider collider in colliderArray){
            if (collider.TryGetComponent(out IInteractable interactable)){
                return interactable;
            }
        }
        return null;
    }

}
