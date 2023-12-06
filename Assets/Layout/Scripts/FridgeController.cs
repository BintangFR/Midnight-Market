using UnityEngine;

public class FridgeController : MonoBehaviour
{
    [SerializeField] private Material lampOffMaterial;
    [SerializeField] private Material lampOnMaterial;
    [SerializeField] private int targetIndex;

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
        if (lampOffMaterial == null) return;
        Material[] newMaterials = meshRenderer.materials;
        newMaterials[targetIndex] = lampOffMaterial;
        meshRenderer.materials = newMaterials;
    }

    private void TurnOnLamp()
    {
        if (lampOnMaterial == null) return;
        Material[] newMaterials = meshRenderer.materials;
        newMaterials[targetIndex] = lampOnMaterial;
        meshRenderer.materials = newMaterials;
    }
}
