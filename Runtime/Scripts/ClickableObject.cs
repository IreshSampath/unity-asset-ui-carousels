using UnityEngine;
using UnityEngine.EventSystems;

public class ClickableObject : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log($"Clicked GameObject: {gameObject.name}");
        //AppEvents.RiseOnIngredientSelected(gameObject);
    }
}