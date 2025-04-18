using System.Collections.Generic;
using UnityEngine;

public class CarouselManager : MonoBehaviour
{
    [SerializeField] WheelController _wheelCarousel1;
    [SerializeField] WheelController _wheelCarousel2;
    [SerializeField] WheelController _wheelCarousel3;

    void Start()
    {
        CreateFromManulData(_wheelCarousel1);
    }

    public void CreateCarousel()
    {
        List<CarouselItem> carouselItems = new List<CarouselItem>();

        // Adding 5 dummy items to the carousel
        for (int i = 0; i <= 5; i++)
        {
            CarouselItem item = new CarouselItem(
                i,
                $"Item {i}",
                $"Description for Item {i}",
                $"ImagePath/Item{i}.png" // Replace with actual image path
            );
            carouselItems.Add(item);
        }

        //CarouselEvents.RaiseOnCarouselItemsLoaded(carouselItems);
        CreateFromLoadData(_wheelCarousel2, carouselItems);
        CreateFromLoadData(_wheelCarousel3, carouselItems);
    }

    // Method to create the wheel manually
    void CreateFromManulData(WheelController wheelController)
    {
        // Assign children of WheelRect to ManualItems
        wheelController.ManualItems = new List<GameObject>();
        foreach (Transform child in wheelController.WheelRect)
        {
            wheelController.ManualItems.Add(child.gameObject);
        }

        // Create the wheel manually
        foreach (var item in wheelController.ManualItems)
        {
            item.transform.localScale = Vector3.one; // Reset scale
            float angle;
            if (wheelController.IsAngleManual)
            {
                angle = wheelController.Angle * wheelController.ManualItems.IndexOf(item);
            }
            else
            {
                angle = (360f / wheelController.ManualItems.Count) * wheelController.ManualItems.IndexOf(item);
            }
            float radians = angle * Mathf.Deg2Rad;
            float sinTheta = Mathf.Sin(radians);
            float cosTheta = Mathf.Cos(radians);
            float xValue = wheelController.StartingYPos * sinTheta;
            float yValue = wheelController.StartingYPos * cosTheta;

            item.transform.Rotate(Vector3.forward, -angle);
            item.transform.localPosition = new Vector3(xValue, yValue, 0);
            item.name = wheelController.ManualItems.IndexOf(item).ToString(); // Set the name of the item
            item.SetActive(true); // Activate the card
        }

        if (wheelController.IsAngleManual && wheelController.IsSymmetricEnabled)
        {
            wheelController.WheelRect.rotation = Quaternion.Euler(0, 0, (wheelController.Angle * (wheelController.ManualItems.Count - 1)) / 2); // Rotate the wheel to make it symmetric
        }

        if (!wheelController.IsAngleManual && wheelController.IsSymmetricEnabled)
        {
            wheelController.WheelRect.rotation = Quaternion.Euler(0, 0, (360f / wheelController.ManualItems.Count) * (wheelController.ManualItems.Count - 1) / 2); // Rotate the wheel to make it symmetric
        }
    }

    void CreateFromLoadData(WheelController wheelController, List<CarouselItem> carouselItems)
    {
        if (wheelController.IsManualCreationAllowed || carouselItems == null || carouselItems.Count == 0)
        {
            Debug.LogWarning("No carousel items to load.");
            return;
        }

        // Clear existing items if any
        foreach (Transform child in wheelController.WheelRect)
        {
            Destroy(child.gameObject);
        }

        // Load carousel data and instantiate items
        foreach (CarouselItem item in carouselItems)
        {
            wheelController.WheelRect.rotation = Quaternion.identity; // Reset rotation

            GameObject card = Instantiate(wheelController.CardPrefab, wheelController.WheelRect);
            card.transform.localPosition = Vector3.zero; // Reset position
            card.transform.localScale = Vector3.one; // Reset scale

            float angle;

            if (wheelController.IsAngleManual)
            {
                angle = wheelController.Angle * carouselItems.IndexOf(item);
            }
            else
            {
                angle = (360f / carouselItems.Count) * carouselItems.IndexOf(item);
            }
            float radians = angle * Mathf.Deg2Rad;
            float sinTheta = Mathf.Sin(radians);
            float cosTheta = Mathf.Cos(radians);

            float xValue = wheelController.StartingYPos * sinTheta;
            float yValue = wheelController.StartingYPos * cosTheta;

            //print($"xValue: {xValue}, yValue: {yValue}, Angle: {angle}");

            card.transform.Rotate(Vector3.forward, -angle);
            card.transform.localPosition = new Vector3(xValue, yValue, 0);
            card.GetComponent<CarouselWheelItem>().CarouselItem = item; // Get the CarouselItem component

            card.name = item.ID.ToString(); // Set the name of the item
            card.SetActive(true); // Activate the card
        }

        if (wheelController.IsSymmetricEnabled && wheelController.IsAngleManual)
        {
            wheelController.WheelRect.rotation = Quaternion.Euler(0, 0, (wheelController.Angle * (carouselItems.Count - 1)) / 2); // Rotate the wheel to make it symmetric
        }
    }
}
