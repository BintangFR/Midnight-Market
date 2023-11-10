using System;
using System.Collections.Generic;
using UnityEngine;

public class LightController : MonoBehaviour
{
    public static LightController Instance { get; private set; }

    public event Action OnLightsOn;
    public event Action OnLightsOff;

    [SerializeField] private Texture2D[] brightLightmapDir, brightLightmapColor;
    [SerializeField] private Texture2D[] darkLightmapDir, darkLightmapColor;

    private LightmapData[] brightLightmap, darkLightmap;
    public bool isLightingOn = true;

    private void Awake()
    {
        if (Instance == null)
        {
            Debug.Log("udah");
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void Start()
    {
        List<LightmapData> lightmapDatas = new List<LightmapData>();

        for (int i = 0; i < brightLightmapDir.Length; i++)
        {
            LightmapData temp = new LightmapData();

            temp.lightmapDir = brightLightmapDir[i];
            temp.lightmapColor = brightLightmapColor[i];

            lightmapDatas.Add(temp);
        }

        brightLightmap = lightmapDatas.ToArray();

        lightmapDatas.Clear();

        for (int i = 0; i < darkLightmapDir.Length; i++)
        {
            LightmapData temp = new LightmapData();

            temp.lightmapDir = darkLightmapDir[i];
            temp.lightmapColor = darkLightmapColor[i];

            lightmapDatas.Add(temp);
        }
        
        darkLightmap = lightmapDatas.ToArray();
    }

    public void SetLighting(bool isOn)
    {
        if (isOn)
        {
            LightmapSettings.lightmaps = brightLightmap;
            isLightingOn = true;
            OnLightsOn?.Invoke();
        }
        else
        {
            LightmapSettings.lightmaps = darkLightmap;
            isLightingOn = false;
            OnLightsOff?.Invoke();
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            SetLighting(!isLightingOn);
        }
    }
}
