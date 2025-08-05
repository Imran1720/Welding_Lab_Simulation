using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectBlinkerHandler
{
    private int remainingBlinks;
    private int totalBlinks;
    private float timer;
    private float blinkInterval;

    private Material defaultMaterial;
    private Material highlightMaterial;
    private MeshRenderer meshRenderer;

    public ObjectBlinkerHandler(BlinkHandlerData data)
    {
        meshRenderer = data.renderer;
        this.blinkInterval = data.duration;
        this.totalBlinks = data.blinkCount * 2;
        defaultMaterial = data.renderer.material;
        this.highlightMaterial = data.highlightMaterial;
    }

    public void Update()
    {
        if (remainingBlinks <= 0 || meshRenderer == null) return;

        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            timer = blinkInterval;
            remainingBlinks--;
            ToggleMaterial();
        }
    }

    private void ToggleMaterial()
    {
        if (defaultMaterial == null || highlightMaterial == null) return;

        bool useDefault = remainingBlinks % 2 == 0;
        meshRenderer.material = useDefault ? defaultMaterial : highlightMaterial;
    }

    public void StartBlinking()
    {
        timer = blinkInterval;
        remainingBlinks = totalBlinks;
    }

    public void StopBlinking()
    {
        timer = 0;
        remainingBlinks = 0;
        meshRenderer.material = defaultMaterial;
    }
}
