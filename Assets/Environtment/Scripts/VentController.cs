using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VentController : MonoBehaviour,IInteractable
{
    public String name;

    PlayerController playerController;

    [SerializeField] private bool isAllow;
    [SerializeField] Transform destination;

    [SerializeField] private Image screenFadeImage; 
    [SerializeField] private float fadeDuration = 1.0f;
    public String interactText;

    public EnvirontmentController environmentController; 


    private void Start()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(destination.position, .4f);
    }

    private void TeleportPlayer(GameObject player)
    {
        Rigidbody rb = player.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.position = destination.position;
        }
        else
        {
            player.transform.position = destination.position;
        }
    }


    private IEnumerator FadeScreenInAndOut()
    {
        Color startColor = screenFadeImage.color;
        Color targetColor = new Color(0, 0, 0, 1); 

        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            screenFadeImage.color = Color.Lerp(startColor, targetColor, elapsedTime / fadeDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        screenFadeImage.color = targetColor;

        GameObject player = GameObject.FindGameObjectWithTag("Player");

        TeleportPlayer(player);
        if (!playerController.notInVent) ApplyCrouching(player);

        yield return new WaitForSeconds(1.0f); 

     
        elapsedTime = 0f;
        startColor = targetColor;
        targetColor = new Color(0, 0, 0, 0);

        while (elapsedTime < fadeDuration)
        {
            screenFadeImage.color = Color.Lerp(startColor, targetColor, elapsedTime / fadeDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        screenFadeImage.color = targetColor;
       
    }

    private void ApplyCrouching(GameObject player)
    {
        if (playerController != null)
        {         
            playerController.speed = 2.5f;
            player.transform.localScale = new Vector3(player.transform.localScale.x, 0.5f, player.transform.localScale.z);
        }
    }

    public string GetInteractText()
    {
        return interactText;
    }

    public void Interact()
    {
        playerController.notInVent = !playerController.notInVent;

        if (environmentController.HasInteractedWithFan())
        {
        StartCoroutine(FadeScreenInAndOut());
        }

    }
}

