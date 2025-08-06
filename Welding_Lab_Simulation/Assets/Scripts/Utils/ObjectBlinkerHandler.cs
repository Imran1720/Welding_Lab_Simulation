using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectBlinkerHandler
{
    private int remainingBlinks;
    private int totalBlinks;
    private float timer;
    private float blinkInterval;

    private Material highlightMaterial;
    private List<MeshRenderer> meshRendererList;
    private Material defaultMaterial;

    public ObjectBlinkerHandler(BlinkHandlerData data)
    {
        meshRendererList = data.rendererlist;
        this.blinkInterval = data.duration;
        this.totalBlinks = data.blinkCount * 2;
        defaultMaterial = data.defaultMaterial;
        this.highlightMaterial = data.highlightMaterial;
    }

    public void Update()
    {
        if (remainingBlinks <= 0 || meshRendererList.Count <= 0) return;

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
        for (int i = 0; i < meshRendererList.Count; i++)
        {
            meshRendererList[i].material = useDefault ? defaultMaterial : highlightMaterial;
        }
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
        for (int i = 0; i < meshRendererList.Count; i++)
        {
            meshRendererList[i].material = defaultMaterial;
        }
    }
}
