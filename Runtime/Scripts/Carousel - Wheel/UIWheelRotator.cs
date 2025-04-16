using UnityEngine;
using UnityEngine.EventSystems;

public class UIWheelRotator : MonoBehaviour, IDragHandler
{
    [SerializeField] GameObject _cardPrefb;

    [SerializeField] float _StartingYPos = 700f;
    [SerializeField] int _numberOfCards = 10;

    [SerializeField] float rotationSpeed = 1f; // Adjust to control rotation speed


    RectTransform _wheelRect;

    void Start()
    {
        // Get the RectTransform of the wheel
        _wheelRect = GetComponent<RectTransform>();

        for(int i = 0; i < _numberOfCards; i++)
        {
            // Instantiate the card prefab
            GameObject card = Instantiate(_cardPrefb, _wheelRect);
            card.transform.localPosition = Vector3.zero; // Reset position
            card.transform.localScale = Vector3.one; // Reset scale
            // Set the rotation of the card
            float angle = (360f / _numberOfCards) * i;

            float radians = angle * Mathf.Deg2Rad;
            float sinTheta = Mathf.Sin(radians);
            float cosTheta = Mathf.Cos(radians);

            float xValue = _StartingYPos * sinTheta;
            float yValue = _StartingYPos * cosTheta;

            print($"xValue: {xValue}, yValue: {yValue}, Angle: {angle}");

            card.transform.Rotate(Vector3.forward, -angle);
            card.transform.localPosition = new Vector3(xValue, yValue, 0);
            card.SetActive(true); // Activate the card
        }
        //float degrees = 360f / _numberOfCards;
        //float radians = degrees * Mathf.Deg2Rad;
        //float sinTheta = Mathf.Sin(radians);
        //float cosTheta = Mathf.Cos(radians);

        //float xValue = _StartingYPos * sinTheta;
        //float yValue = _StartingYPos * cosTheta;

        //print($"xValue: {xValue}, yValue: {yValue}");
    }

    public void OnDrag(PointerEventData eventData)
    {
        // Calculate drag direction and apply rotation
        float rotationAmount = -eventData.delta.x * rotationSpeed;
        _wheelRect.Rotate(Vector3.forward, rotationAmount);
    }
}
