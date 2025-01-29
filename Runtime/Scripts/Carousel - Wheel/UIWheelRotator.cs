using UnityEngine;
using UnityEngine.EventSystems;

public class UIWheelRotator : MonoBehaviour, IDragHandler
{
    [SerializeField] float rotationSpeed = 1f; // Adjust to control rotation speed
    RectTransform _wheelRect;

    void Start()
    {
        // Get the RectTransform of the wheel
        _wheelRect = GetComponent<RectTransform>();
    }

    public void OnDrag(PointerEventData eventData)
    {
        // Calculate drag direction and apply rotation
        float rotationAmount = -eventData.delta.x * rotationSpeed;
        _wheelRect.Rotate(Vector3.forward, rotationAmount);
    }
}
