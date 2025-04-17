using System.Collections.Generic;

public class CarouselData
{
    public List<CarouselItem> CarouselItems;
}

public class CarouselItem
{
    public int ID;
    public string Title;
    public string Description;
    public string Image;

    public CarouselItem(int id, string title, string description, string image)
    {
        ID = id;
        Title = title;
        Description = description;
        Image = image;
    }
}
