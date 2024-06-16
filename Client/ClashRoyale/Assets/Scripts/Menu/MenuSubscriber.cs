using System;
using UnityEngine;

public class MenuSubscriber : MonoBehaviour
{
    [SerializeField] private DeckManager _deckManager;
    [SerializeField] private SelectedDeckUI _selectedDeckUI;

    private void Start()
    {
        _deckManager.UpdateSelected += _selectedDeckUI.UpdateCardsList;
    }

    private void OnDestroy()
    {
        _deckManager.UpdateSelected -= _selectedDeckUI.UpdateCardsList;
    }
}