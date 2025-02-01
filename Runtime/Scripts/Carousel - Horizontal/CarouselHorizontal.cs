using UnityEngine;
 using DG.Tweening;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CarouselHorizontal : MonoBehaviour, IPointerDownHandler
{
    [Header("Cards")]

    [SerializeField] Transform _carouselCardsParent;

    [SerializeField] List<GameObject> _carouselCards = new List<GameObject>();

    [SerializeField] float _cardsMaxScale = 1.2f;
    [SerializeField] float _cardsMinScale = 0.8f;
    [SerializeField] int _horizontalSpacing = 15;
    [SerializeField] int _cardsGapX = 350;
    [SerializeField] int _cardsGapY = 100;
    [SerializeField] float _transitionTime = 0.25f;

    [Header("Paginations")]
    [SerializeField] Transform _paginationDotParent;
    [SerializeField] GameObject _paginationDot;

    [SerializeField] Sprite _paginationDotDefaultImg;
    [SerializeField] Sprite _paginationDotSelectedImg;

    List<GameObject> _paginationDots = new List<GameObject>();


    Vector3 _mouseClickedPos;

    bool _isDragging = false;
    int _currentCardIndex = 1;

    void Start()
    {
        InitiateCards();
        CreatePaginationDots();
        //ClickRight();

         _carouselCards[_currentCardIndex].GetComponent<RectTransform>().DOScale(new Vector2(_cardsMaxScale, _cardsMaxScale), 0);
         _carouselCards[_currentCardIndex].transform.GetChild(0).GetComponent<RectTransform>().DOAnchorPosY(-_cardsGapY, 0);

    }

    void Update()
    {
        _carouselCardsParent.gameObject.GetComponent<HorizontalLayoutGroup>().spacing = _horizontalSpacing;

        OnMouseDrag();
    }

    void InitiateCards()
    {
        // Clear the list to avoid duplicates
        _carouselCards.Clear();

        // Iterate through all children of this GameObject
        foreach (Transform child in _carouselCardsParent)
        {
            _carouselCards.Add(child.gameObject);
        }

        Debug.Log("Collected " + _carouselCards.Count + " child objects.");
    }

    void CreatePaginationDots()
    {
        int i = 0;
        foreach (var child in _carouselCards)
        {
            GameObject dot = Instantiate(_paginationDot, _paginationDotParent);
            dot.SetActive(true);
            _paginationDots.Add(dot);

            if (_currentCardIndex == i)
            {
                dot.GetComponent<Image>().sprite = _paginationDotSelectedImg;
                RectTransform dotRectTransform = _paginationDots[i].GetComponent<RectTransform>();

                 dotRectTransform.DOScale(new Vector3(1.5f, 1.5f), 0.25f);

            }
            else
            {
                dot.GetComponent<Image>().sprite = _paginationDotDefaultImg;
            }

            i++;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        // Check if the pointer is over the target panel
        if (RectTransformUtility.RectangleContainsScreenPoint(_carouselCardsParent.GetComponent<RectTransform>(), eventData.position, eventData.pressEventCamera))
        {
            _isDragging = true;
            _mouseClickedPos = eventData.position;
        }
    }

    //public void OnPointerUp(PointerEventData eventData)
    //{
    //    _isDragging = false;
    //}

    void OnMouseDrag()
    {
        //if (Input.GetMouseButtonDown(0))
        //{
        //    _isDragging = true;
        //    _mouseClickedPos = Input.mousePosition;
        //}

        if (Input.GetMouseButtonUp(0))
        {
            _isDragging = false;
        }

        if (_isDragging && Input.mousePosition.x - _cardsGapX > _mouseClickedPos.x && _currentCardIndex > 0)
        {
            Debug.Log("You dragged right!");
            ClickRight();
        }
        else if (_isDragging && Input.mousePosition.x + _cardsGapX < _mouseClickedPos.x && _currentCardIndex < _carouselCards.Count-1)
        {
            Debug.Log("You dragged left!");
            ClickLeft();
        }
    }

    void ClickRight()
    {
        _isDragging = false;
        _currentCardIndex--;
        //print(_currentCardIndex);
        HandleCarousel(1);
    }

    void ClickLeft()
    {
        _isDragging = false;
        _currentCardIndex++;
        HandleCarousel(-1);
    }

    void HandleCarousel(int direction)
    {
        print(_currentCardIndex);

        int i = 0;
        foreach (GameObject card in _carouselCards)
        {
            RectTransform cardRectTransform = card.GetComponent<RectTransform>();
             cardRectTransform.DOAnchorPos(new Vector2(cardRectTransform.anchoredPosition.x + (_cardsGapX * direction), cardRectTransform.anchoredPosition.y), _transitionTime);

            RectTransform dotRectTransform = _paginationDots[i].GetComponent<RectTransform>();
            // print($"i = {i}, _currentCardIndex = {_currentCardIndex}, _currentCardIndex-1 = {_currentCardIndex-1}");
            if (i == _currentCardIndex)
            {
                //cardRectTransform.DOAnchorPos(new Vector2(cardRectTransform.anchoredPosition.x + (_cardsGapX * direction), cardRectTransform.anchoredPosition.y - _cardsGapY), _transitionTime);
                 cardRectTransform.GetChild(0).GetComponent<RectTransform>().DOAnchorPosY(-_cardsGapY, _transitionTime);
                 cardRectTransform.DOScale(new Vector2(_cardsMaxScale, _cardsMaxScale), 0.25f);

                _paginationDots[i].GetComponent<Image>().sprite = _paginationDotSelectedImg;
                 dotRectTransform.DOScale(new Vector3(1.5f, 1.5f), 0.25f);
            }
            else
            {
                //cardRectTransform.DOAnchorPos(new Vector2(cardRectTransform.anchoredPosition.x + (_cardsGapX * direction), cardRectTransform.anchoredPosition.y + _cardsGapY), _transitionTime);
                 cardRectTransform.GetChild(0).GetComponent<RectTransform>().DOAnchorPosY(_cardsGapY, _transitionTime);
                 cardRectTransform.DOScale(new Vector2(_cardsMinScale, _cardsMinScale), 0.25f);

                _paginationDots[i].GetComponent<Image>().sprite = _paginationDotDefaultImg;
                 dotRectTransform.DOScale(new Vector3(1f, 1f), 0.25f);

            }
            i++;
        }

        //int i = 0;
        //foreach (GameObject card in _carouselCards)
        //{
        //    RectTransform cardRectTransform = card.GetComponent<RectTransform>();
        //    cardRectTransform.DOAnchorPos(new Vector2(cardRectTransform.anchoredPosition.x + (_cardsGapX * direction), cardRectTransform.anchoredPosition.y), _transitionTime);

        //    // print($"i = {i}, _currentCardIndex = {_currentCardIndex}, _currentCardIndex-1 = {_currentCardIndex-1}");
        //    if (i == _currentCardIndex)
        //    {
        //        //cardRectTransform.DOAnchorPos(new Vector2(cardRectTransform.anchoredPosition.x + (_cardsGapX * direction), cardRectTransform.anchoredPosition.y), _transitionTime);

        //        //cardRectTransform.DOScale(new Vector2(1.7f, 1.7f), 0.25f);
        //        cardRectTransform.DOScale(new Vector2(1.2f, 1.2f), 0.25f);
        //    }
        //    else
        //    {
        //        //cardRectTransform.DOAnchorPos(new Vector2(cardRectTransform.anchoredPosition.x + (_cardsGapX * direction), cardRectTransform.anchoredPosition.y), _transitionTime);

        //        cardRectTransform.DOScale(new Vector2(0.8f, 0.8f), 0.25f);
        //    }
        //    i++;
        //}
    }
}
