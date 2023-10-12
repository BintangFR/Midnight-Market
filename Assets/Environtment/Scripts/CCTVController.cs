using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class CCTVController : MonoBehaviour, IInteractable
{
    public string name;
    public String interactText;

   
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public string GetInteractText()
    {
        return interactText;
    }

    public void Interact()
    {
        SceneManager.LoadScene("Prototype-CCTV System");
    }

}
