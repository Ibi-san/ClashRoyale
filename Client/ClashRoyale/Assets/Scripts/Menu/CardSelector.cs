using System.Collections.Generic;
using UnityEngine;

public class CardSelector : MonoBehaviour
{
    [SerializeField] private DeckManager _deckManager;
    [SerializeField] private AvailableDeckUI _availableDeckUI;
    [SerializeField] private SelectedDeckUI _selectedDeckUI;
    private List<Card> _availableCards = new();
    private List<Card> _selectedCards = new();
    public IReadOnlyList<Card> AvailableCards => _availableCards;
    public IReadOnlyList<Card> SelectedCards => _selectedCards;
    private int _selectToggleIndex = 0;

    private void OnEnable()
    {
        FillListFromManager();
    }

    private void FillListFromManager()
    {
        _availableCards.Clear();
        for (int i = 0; i < _deckManager.AvailableCards.Count; i++)
        {
            _availableCards.Add(_deckManager.AvailableCards[i]);
        }

        _selectedCards.Clear();
        for (int i = 0; i < _deckManager.SelectedCards.Count; i++)
        {
            _selectedCards.Add(_deckManager.SelectedCards[i]);
        }
    }

    public void SetSelectToggleIndex(int index)
    {
        _selectToggleIndex = index;
    }

    public void SelectCard(int cardID)
    {
        _selectedCards[_selectToggleIndex] = _availableCards[cardID - 1];
        _selectedDeckUI.UpdateCardsList(SelectedCards);
        _availableDeckUI.UpdateCardList(AvailableCards, SelectedCards);
    }

    public void SaveChanges()
    {
        _deckManager.ChangesDeck(SelectedCards, CloseChangesWindows);
    }

    public void CancelChanges()
    {
        FillListFromManager();
        _selectedDeckUI.UpdateCardsList(SelectedCards);
        _availableDeckUI.UpdateCardList(AvailableCards, SelectedCards);
        CloseChangesWindows();
    }

    [Space(24), Header("Canvas switch")]
    [SerializeField] private GameObject _mainCanvas;
    [SerializeField] private GameObject _cardSelectCanvas;
    public void CloseChangesWindows()
    {
        _mainCanvas.SetActive(true);
        _cardSelectCanvas.SetActive(false);
    }
}