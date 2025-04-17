using System;
using System.Collections.Generic;

public class CarouselEvents
{
    public static event Action<List<CarouselItem>> OnCarouselItemsLoaded;
    public static void RaiseOnCarouselItemsLoaded(List<CarouselItem> carouselItems)
    {
        OnCarouselItemsLoaded?.Invoke(carouselItems);
    }

    public static event Action<CarouselItem> OnItemSelected;
    public static void RaiseOnItemSelected(CarouselItem item)
    {
        OnItemSelected?.Invoke(item);
    }

    public static event Action<CarouselItem> OnItemDeselected;
    public static void RaiseOnItemDeselected(CarouselItem item)
    {
        OnItemDeselected?.Invoke(item);
    }

    public static event Action<CarouselItem> OnItemClicked;
    public static void RaiseOnItemClicked(CarouselItem item)
    {
        OnItemClicked?.Invoke(item);
    }
}
