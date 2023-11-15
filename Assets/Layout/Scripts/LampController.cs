using UnityEngine;

public class LampController : MonoBehaviour
{
    [SerializeField] private Material lampOffMaterial;
    [SerializeField] private Material lampOnMaterial;
    [SerializeField] private GameObject realtimeLight;

    private MeshRenderer meshRenderer;

    private void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();

        LightController.Instance.OnLightsOn += TurnOnLamp;
        LightController.Instance.OnLightsOff += TurnOffLamp;
    }

    private void OnDisable()
    {
        LightController.Instance.OnLightsOn -= TurnOnLamp;
        LightController.Instance.OnLightsOff -= TurnOffLamp;
    }

    private void TurnOffLamp()
    {
        meshRenderer.material = lampOffMaterial;
        realtimeLight?.SetActive(false);
    }

    private void TurnOnLamp()
    {
        meshRenderer.material = lampOnMaterial;
        realtimeLight?.SetActive(true);
    }
}
