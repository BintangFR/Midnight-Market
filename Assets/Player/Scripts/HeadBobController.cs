using UnityEngine;

public class HeadBobController : MonoBehaviour
{
    [SerializeField] private bool enable = true;

    [SerializeField, Range(0, 0.1f)] private float amplitude = 0.015f;
    [SerializeField, Range(0, 30.0f)] private float frequency = 10.0f;

    private float walkAmplitude;
    private float walkFrequency;
    private float sprintAmplitude;
    private float sprintFrequency;
    private float crouchAmplitude;
    private float crouchFrequency;

    private Vector3 startPos;
    private PlayerController playerController;

    private void Start()
    {
        playerController = GetComponentInParent<PlayerController>();
        startPos = transform.localPosition;

        walkAmplitude = amplitude;
        walkFrequency = frequency;
        sprintAmplitude = amplitude * 1.5f;
        sprintFrequency = frequency * 1.5f;
        crouchAmplitude = amplitude * 0.5f;
        crouchFrequency = frequency * 0.5f;
    }

    private void PlayMotion(Vector3 motion)
    {
        transform.localPosition += motion;
    }

    private Vector3 FootstepMotion()
    {
        Vector3 pos = Vector3.zero;
        pos.y = Mathf.Sin(Time.time * frequency) * amplitude;
        pos.x = Mathf.Cos(Time.time * frequency * 0.5f) * amplitude;
        return pos;
    }

    private Vector3 IdleMotion()
    {
        Vector3 pos = Vector3.zero;
        pos.y = Mathf.Sin(Time.time * frequency * 0.125f) * amplitude * 0.125f;
        pos.x = Mathf.Cos(Time.time * frequency * 0.0625f) * amplitude * 0.0625f;
        return pos;
    }

    private void CheckMotion()
    {
        if (!playerController.isGrounded) return;

        if (playerController.isWalking)
        {
            PlayMotion(FootstepMotion());
        }
        else
        {
            PlayMotion(IdleMotion());
        }
    }

    private void ResetPosition()
    {
        if (transform.localPosition == startPos) return;

        transform.localPosition = Vector3.Lerp(transform.localPosition, startPos, Time.deltaTime);
    }

    private void Update()
    {
        if (!enable) return;

        if (playerController.isRunning)
        {
            amplitude = sprintAmplitude;
            frequency = sprintFrequency;
        }
        else if (playerController.isCrouching)
        {
            amplitude = crouchAmplitude;
            frequency = crouchFrequency;
        }
        else if (playerController.isWalking)
        {
            amplitude = walkAmplitude;
            frequency = walkFrequency;
        }

        CheckMotion();
        ResetPosition();
    }
}
