using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactor : MonoBehaviour
{
    [SerializeField] private LayerMask interactLayer;
    public static Interactor instance;
    // Update is called once per frame

    [SerializeField] private float interactRange;
    [SerializeField] private Vector3 offset;
    [SerializeField] private GameObject orientation;

    private void Awake() {
        instance = this;
    }
    void Update()
    {
        //transform.rotation = orientation.transform.rotation;
        
        if (Input.GetKeyDown(KeyCode.E))
        {

            // Calculate the position of the sphere in front of the player
            Vector3 spherePosition = transform.position + orientation.transform.forward * interactRange + offset;

            // Set the position of the sphere
            //transform.position = spherePosition;

            // Find all colliders within the interact range that are on the interact layer              
            Collider[] colliderArray = Physics.OverlapSphere(spherePosition, interactRange, interactLayer);
            foreach (Collider collider in colliderArray)
            {
                // Check if the collider has a component that implements the IInteractable interface
                if (collider.TryGetComponent(out IInteractable interactable))
                {
                    interactable.Interact();
                }
            }
        }
    }

    private void OnDrawGizmosSelected() {
        //Gizmos.DrawWireSphere(transform.position + offset, interactRange);
        Gizmos.DrawWireSphere(transform.position + orientation.transform.forward * interactRange + offset, interactRange);

    }
    //this method is for showing a interact text
    public  IInteractable GetInteractableObject(){

        Collider[] colliderArray = Physics.OverlapSphere(transform.position + orientation.transform.forward * interactRange + offset, interactRange, interactLayer);
        foreach (Collider collider in colliderArray){
            if (collider.TryGetComponent(out IInteractable interactable)){
                return interactable;
            }

        }
        return null;
    }

}
