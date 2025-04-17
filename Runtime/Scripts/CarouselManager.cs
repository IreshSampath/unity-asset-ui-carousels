using System.Collections.Generic;
using UnityEngine;

public class CarouselManager : MonoBehaviour
{
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

        CarouselEvents.RaiseOnCarouselItemsLoaded(carouselItems);
    }
}
