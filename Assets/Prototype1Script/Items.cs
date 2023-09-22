using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName ="Item/Create New Item")]

public class Items : ScriptableObject
{
    public String name;
    public String description;
    public bool isObtained;

    public Sprite icon;

}
