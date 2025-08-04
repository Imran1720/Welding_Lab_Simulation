using System;
using UnityEngine;

[Serializable]

public class BlinkHandlerData
{
    public MeshRenderer renderer;
    public Material highlightMaterial;
    public float duration;
    public int blinkCount;
}
