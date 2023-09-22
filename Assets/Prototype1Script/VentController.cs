using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VentController : MonoBehaviour,IInteractable
{
    public String name;

    [SerializeField] private bool isAllow;
    [SerializeField] Transform destination;

    [SerializeField] private Image screenFadeImage; // Reference to a UI image used for screen fading.
    [SerializeField] private float fadeDuration = 1.0f; // Duration of the fade effect in seconds.
    public String interactText;

    private bool isFading = false; // Flag to prevent overlapping fades.

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(destination.position, .4f);
    }

    // private void OnTriggerEnter(Collider other)
    // {
    //     if (other.CompareTag("Player") && !isFading)
    //     {
    //         StartCoroutine(FadeScreenInAndOut());
    //     }
    // }

    private IEnumerator FadeScreenInAndOut()
    {
        isFading = true;

        Color startColor = screenFadeImage.color;
        Color targetColor = new Color(0, 0, 0, 1); // Fully opaque black.

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
        player.transform.position = destination.position;

        // Player Always Crouch
        ApplyCrouching(player);

        yield return new WaitForSeconds(1.0f); // Wait for 1 second.

        // Fade the screen back to transparent.
        elapsedTime = 0f;
        startColor = targetColor;
        targetColor = new Color(0, 0, 0, 0); // Fully transparent.

        while (elapsedTime < fadeDuration)
        {
            screenFadeImage.color = Color.Lerp(startColor, targetColor, elapsedTime / fadeDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        screenFadeImage.color = targetColor;
        isFading = false;
    }

    // This method applies the crouching behavior.
    private void ApplyCrouching(GameObject player)
    {
        PlayerController playerController = player.GetComponent<PlayerController>();
        if (playerController != null)
        {
            // Modify player's speed, layer, and scale for crouching.
            playerController.speed -= (playerController.speed * 50 / 100);
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
        StartCoroutine(FadeScreenInAndOut());

    }
}
