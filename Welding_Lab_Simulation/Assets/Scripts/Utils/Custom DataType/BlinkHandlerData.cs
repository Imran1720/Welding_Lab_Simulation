using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]

public class BlinkHandlerData
{
    public List<MeshRenderer> rendererlist;
    public Material highlightMaterial;
    public Material defaultMaterial;
    public float duration;
    public int blinkCount;
}
