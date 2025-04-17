using DG.Tweening;
using UnityEngine;

public class CarouselWheelItem : MonoBehaviour
{
    public CarouselItem CarouselItem;

    [SerializeField] float _selectedXPos = 355f;
    [SerializeField] float _selectedThreshold = 75f;
    [SerializeField] Vector3 _selectedScale = new Vector3(1.2f, 1.2f, 1.2f); // Target scale when selected
    [SerializeField] Vector3 _defaultScale = Vector3.one; // Default scale
    [SerializeField] float _scaleDuration = 0.5f; // Duration for scaling animation

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.position;

        if (pos.x >= _selectedXPos - _selectedThreshold && pos.x <= _selectedXPos + _selectedThreshold)
        {
            // Animate scale to the selected scale
            transform.DOScale(_selectedScale, _scaleDuration);
        }
        else
        {
            // Animate scale back to the default scale
            transform.DOScale(_defaultScale, _scaleDuration);
        }
    }
}
