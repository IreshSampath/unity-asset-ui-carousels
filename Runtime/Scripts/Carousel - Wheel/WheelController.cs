using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class WheelController : MonoBehaviour, IDragHandler
{
    [Header("Child Creation Settings")]
    public bool IsManualCreationAllowed = false;
    public GameObject CardPrefab;
    [HideInInspector] public List<GameObject> ManualItems;

    [Header("Child Spread Settings")]
    public bool IsSymmetricEnabled = false; // Symmetric rotation
    public bool IsAngleManual = false;
    public float Angle = 0f; // Angle in degrees
    public float StartingYPos = 700f;
    [HideInInspector] public RectTransform WheelRect;

    [Header("Wheel Settings")]
    public float RotationSpeed = 1f; // Adjust to control rotation speed

    void Start()
    {
        WheelRect = GetComponent<RectTransform>();
    }

    public void OnDrag(PointerEventData eventData)
    {
        // Calculate drag direction and apply rotation
        float rotationAmount = -eventData.delta.x * RotationSpeed;
        WheelRect.Rotate(Vector3.forward, rotationAmount);
    }
}
