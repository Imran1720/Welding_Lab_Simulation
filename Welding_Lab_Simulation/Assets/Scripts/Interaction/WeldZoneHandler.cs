using UnityEngine;

public class WeldZoneHandler : MonoBehaviour
{
    [SerializeField] private Transform weldDragPoint;
    [SerializeField] private Transform weldStartPoint;
    [SerializeField] private Transform smallWeldPrefab;
    [SerializeField] private Transform bigWeldPrefab;

    private Transform currentWeldPrefab;
    private PowerLevel currentPowerLevel;

    private Vector3 initialScale;
    private Vector3 initialBigScale;
    private Vector3 initialSmallScale;

    private void Start()
    {
        initialBigScale = bigWeldPrefab.transform.localScale;
        initialSmallScale = smallWeldPrefab.transform.localScale;
        smallWeldPrefab.gameObject.SetActive(false);
        bigWeldPrefab.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (weldDragPoint.hasChanged)
        {
            currentWeldPrefab = GetWeldPrefab(currentPowerLevel);
            if (currentWeldPrefab == null)
            {
                return;
            }
            UpdateTransformForWeld(currentWeldPrefab);
        }
    }

    private Transform GetWeldPrefab(PowerLevel level)
    {
        switch (level)
        {
            case PowerLevel.MEDIUM:
            case PowerLevel.LOW:
            default:
                return smallWeldPrefab;
            case PowerLevel.HIGH:
                return bigWeldPrefab;
        }
    }

    private void UpdateTransformForWeld(Transform weldPrefab)
    {
        float distance = Vector3.Distance(weldDragPoint.position, weldStartPoint.position);
        weldPrefab.localScale = new Vector3(initialScale.x, initialScale.y, distance / 2f);

        Vector3 middlePoint = (weldStartPoint.position + weldDragPoint.position) / 2f;
        weldPrefab.position = middlePoint;

        Vector3 rotation = (weldDragPoint.position - weldStartPoint.position);
        weldPrefab.forward = rotation;
    }


    public void EnableWeld(PowerLevel powerLevel)
    {
        currentPowerLevel = powerLevel;
        if (powerLevel == PowerLevel.MEDIUM || powerLevel == PowerLevel.LOW)
        {
            initialScale = initialSmallScale;
        }
        else
        {
            initialScale = initialBigScale;
        }
        currentWeldPrefab.gameObject.SetActive(true);
    }

    public void DisableWeld()
    {
        if (currentWeldPrefab == null) return;
        currentWeldPrefab.gameObject.SetActive(false);
    }

    public Transform GetDragPoint() => weldDragPoint;
}
