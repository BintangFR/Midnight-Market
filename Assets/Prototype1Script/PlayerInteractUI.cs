using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class PlayerInteractUI : MonoBehaviour
{
    // Update is called once per frame

    [SerializeField] private GameObject containerGameObject;
    [SerializeField] private TextMeshProUGUI interactText;

    void Update()
    {
        if (Interactor.instance.GetInteractableObject() != null){
            Show(Interactor.instance.GetInteractableObject());
        }
        else{
            Hide();
        }
    }

    void Show(IInteractable interact){
        containerGameObject.SetActive(true);
        interactText.text = interact.GetInteractText();
    }
    void Hide(){
        containerGameObject.SetActive(false);

    }
}
