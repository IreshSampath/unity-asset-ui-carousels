using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIWheelRotator : MonoBehaviour, IDragHandler
{
    [SerializeField] GameObject _cardPrefb;
    [SerializeField] bool _isAngleManual = false;
    [SerializeField] float _angle = 0f; // Angle in degrees
    [SerializeField] bool _isSymmetricEnabled = false; // symmetric rotation
    [SerializeField] bool _isManualCreationAllowed = false;
    [SerializeField] List<GameObject> _manualItems;
    [SerializeField] float _StartingYPos = 700f;
    [SerializeField] float rotationSpeed = 1f; // Adjust to control rotation speed

    RectTransform _wheelRect;

    private void OnEnable()
    {
        // Subscribe to the event when the carousel data is loaded
        CarouselEvents.OnCarouselItemsLoaded += LoadCarouselData;
    }

    private void OnDisable()
    {
        // Unsubscribe from the event to avoid memory leaks
        CarouselEvents.OnCarouselItemsLoaded -= LoadCarouselData;
    }

    void Start()
    {
        // Get the RectTransform of the wheel
        _wheelRect = GetComponent<RectTransform>();

        // Check if manual creation is enabled
        if (_isManualCreationAllowed)
        {
            CreateFromManulData();
        }
    }

    // Method to create the wheel manually
    void CreateFromManulData()
    {
        // Create the wheel manually
        foreach (var item in _manualItems) {

            item.transform.localScale = Vector3.one; // Reset scale
            float angle;
            if (_isAngleManual)
            {
                angle = _angle * _manualItems.IndexOf(item);
            }
            else
            {
                angle = (360f / _manualItems.Count) * _manualItems.IndexOf(item);
            }
            float radians = angle * Mathf.Deg2Rad;
            float sinTheta = Mathf.Sin(radians);
            float cosTheta = Mathf.Cos(radians);
            float xValue = _StartingYPos * sinTheta;
            float yValue = _StartingYPos * cosTheta;
            //print($"xValue: {xValue}, yValue: {yValue}, Angle: {angle}");
            item.transform.Rotate(Vector3.forward, -angle);
            item.transform.localPosition = new Vector3(xValue, yValue, 0);
            //item.GetComponent<CarouselWheelItem>().CarouselItem = item.GetComponent<; // Get the CarouselItem component
            item.name = _manualItems.IndexOf(item).ToString(); // Set the name of the item
            item.SetActive(true); // Activate the card
        }
        if (_isAngleManual && _isSymmetricEnabled)
        {
            _wheelRect.rotation = Quaternion.Euler(0, 0, (_angle * (_manualItems.Count - 1)) / 2); // Rotate the wheel to make it symmetric
        }

        if (!_isAngleManual && _isSymmetricEnabled)
        {
            _wheelRect.rotation = Quaternion.Euler(0, 0, (360f / _manualItems.Count) * (_manualItems.Count - 1) / 2); // Rotate the wheel to make it symmetric
        }
    }

    void LoadCarouselData(List<CarouselItem> carouselItems)
    {
        if (_isManualCreationAllowed || carouselItems == null || carouselItems.Count == 0)
        {
            Debug.LogWarning("No carousel items to load.");
            return;
        }

        // Clear existing items if any
        foreach (Transform child in _wheelRect)
        {
            Destroy(child.gameObject);
        }

        // Load carousel data and instantiate items
        foreach (CarouselItem item in carouselItems)
        {
            _wheelRect.rotation = Quaternion.identity; // Reset rotation

            GameObject card = Instantiate(_cardPrefb, _wheelRect);
            card.transform.localPosition = Vector3.zero; // Reset position
            card.transform.localScale = Vector3.one; // Reset scale

            float angle;

            if (_isAngleManual)
            {
                angle = _angle * carouselItems.IndexOf(item);
            }
            else
            {
                angle = (360f / carouselItems.Count) * carouselItems.IndexOf(item);
            }
            float radians = angle * Mathf.Deg2Rad;
            float sinTheta = Mathf.Sin(radians);
            float cosTheta = Mathf.Cos(radians);

            float xValue = _StartingYPos * sinTheta;
            float yValue = _StartingYPos * cosTheta;

            //print($"xValue: {xValue}, yValue: {yValue}, Angle: {angle}");

            card.transform.Rotate(Vector3.forward, -angle);
            card.transform.localPosition = new Vector3(xValue, yValue, 0);
            card.GetComponent<CarouselWheelItem>().CarouselItem = item; // Get the CarouselItem component

            card.name = item.ID.ToString(); // Set the name of the item
            card.SetActive(true); // Activate the card
        }

        if (_isSymmetricEnabled && _isAngleManual)
        {
            _wheelRect.rotation = Quaternion.Euler(0, 0, (_angle * (carouselItems.Count - 1))/2); // Rotate the wheel to make it symmetric
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        // Calculate drag direction and apply rotation
        float rotationAmount = -eventData.delta.x * rotationSpeed;
        _wheelRect.Rotate(Vector3.forward, rotationAmount);
    }
}
