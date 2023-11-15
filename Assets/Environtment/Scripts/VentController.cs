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
    

    private bool isFading = false; 

    public EnvirontmentController environmentController; 

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
            rb.velocity = Vector3.zero; // Stop the player's current movement.
            rb.position = destination.position;
        }
        else
        {
            player.transform.position = destination.position;
        }
    }


    private IEnumerator FadeScreenInAndOut()
    {
        isFading = true;

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

        // Move the player to the destination position.
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        TeleportPlayer(player);

        // Player Always Crouch
        ApplyCrouching(player);

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
        isFading = false;
    }

   
    private void ApplyCrouching(GameObject player)
    {
        PlayerController playerController = player.GetComponent<PlayerController>();
        if (playerController != null)
        {
            // Modify player's speed, layer, and scale for crouching.
            playerController.canRun = false;
            playerController.speed = 2.5f;

            player.transform.localScale = new Vector3(player.transform.localScale.x, 0.5f, player.transform.localScale.z);
            player.gameObject.layer = 0; // Change the layer to the default layer.
            playerController.canCrouch = !playerController.canCrouch;

            Debug.Log(playerController.speed);
        }
    }
    public string GetInteractText()
    {
        return interactText;
    }

    public void Interact()
    {
        if (environmentController.HasInteractedWithFan())
        {
        StartCoroutine(FadeScreenInAndOut());
        }
        else
        {
            Debug.Log("Matiin Fan dulu"); 
        }
    }
}

