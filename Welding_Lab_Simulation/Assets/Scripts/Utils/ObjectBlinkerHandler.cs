using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectBlinkerHandler
{
    private Material defaultMaterial;
    private Material highlightMaterial;
    private MeshRenderer meshRenderer;
    private float duration;
    private float timer;
    private int blinkCount;
    private int count;

    public ObjectBlinkerHandler(BlinkHandlerData data)
    {
        meshRenderer = data.renderer;
        this.duration = data.duration;
        this.blinkCount = data.blinkCount * 2;
        defaultMaterial = data.renderer.material;
        this.highlightMaterial = data.highlightMaterial;
        count = 0;
        timer = 0;
    }

    public void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0 && count > 0)
        {
            count--;
            timer = duration;
            meshRenderer.material = count % 2 == 0 ? defaultMaterial : highlightMaterial;
        }
    }

    public void StartBlinking()
    {
        count = blinkCount;
        timer = duration;
    }
}
