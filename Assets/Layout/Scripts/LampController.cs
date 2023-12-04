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
        realtimeLight?.SetActive(false);
        if (lampOffMaterial == null) return;
        meshRenderer.material = lampOffMaterial;
    }

    private void TurnOnLamp()
    {
        realtimeLight?.SetActive(true);
        if (lampOnMaterial == null) return;
        meshRenderer.material = lampOnMaterial;
    }
}
