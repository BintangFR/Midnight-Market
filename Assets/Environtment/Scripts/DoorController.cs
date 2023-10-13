using System.Collections;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class DoorController : MonoBehaviour, IInteractable
{
    public bool isOpen = false;
    public bool isLocked = false;
    [SerializeField] private float Speed = 1f;
    [SerializeField] private float Rotation = 90f;
    [SerializeField] private Transform pivot;

    private Vector3 StartRotation;
    private Coroutine AnimationCoroutine;

    private void Awake()
    {
        StartRotation = pivot.localRotation.eulerAngles;
    }

    public string GetInteractText()
    {
        if (isOpen == false)
        {
            return ("Open");
        }
        else
        {
            return ("Close");
        }
    }

    public void InteractDoor() //Interact without audio
    {
        if (AnimationCoroutine != null)
        {
            StopCoroutine(AnimationCoroutine);
        }

        if (!isOpen)
        {
            AnimationCoroutine = StartCoroutine(DoRotationOpen());
        }
        else
        {
            AnimationCoroutine = StartCoroutine(DoRotationClose());
        }
    }

    public void Interact()
    {
        if (!isLocked)
        {
            InteractDoor();

            if (!isOpen)
            {
                AudioManager.Instance.PlaySFX("Door-Open", transform.position);
            }
            else
            {
                AudioManager.Instance.PlaySFX("Door-Close", transform.position);
            }
        }
        else
        {
            Debug.Log("Pintu Terkunci");
            AudioManager.Instance.PlaySFX("Door-Locked", transform.position);
        }
        
    }


    private IEnumerator DoRotationOpen()
    {
        Quaternion startRotation = pivot.localRotation;
        Quaternion endRotation = Quaternion.Euler(new Vector3(0, StartRotation.y + Rotation, 0));

        isOpen = true;

        float time = 0;
        while (time < 1)
        {
            pivot.localRotation = Quaternion.Slerp(startRotation, endRotation, time);
            yield return null;
            time += Time.deltaTime * Speed;
        }
    }

    private IEnumerator DoRotationClose()
    {
        Quaternion startRotation = pivot.localRotation;
        Quaternion endRotation = Quaternion.Euler(StartRotation);

        isOpen = false;

        float time = 0;
        while (time < 1)
        {
            pivot.localRotation = Quaternion.Slerp(startRotation, endRotation, time);
            yield return null;
            time += Time.deltaTime * Speed;
        }
    }
}
