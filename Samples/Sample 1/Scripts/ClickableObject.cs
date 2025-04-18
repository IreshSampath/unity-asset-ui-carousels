using UnityEngine;
using UnityEngine.EventSystems;

public class ClickableObject : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        if (transform.parent == null || transform.parent.GetComponent<CarouselWheelItem>()?.CarouselItem == null)
            return;

        Debug.Log($"Clicked GameObject: {transform.parent.GetComponent<CarouselWheelItem>()?.CarouselItem.Image}");
    }
}