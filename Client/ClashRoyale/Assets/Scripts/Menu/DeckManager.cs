using System;
using System.Collections.Generic;
using UnityEngine;

public class DeckManager : MonoBehaviour
{
    [SerializeField] private Card[] _cards;
    [SerializeField] private List<Card> _availableCards = new();
    [SerializeField] private List<Card> _selectedCards = new();
    public IReadOnlyList<Card> AvailableCards => _availableCards;
    public IReadOnlyList<Card> SelectedCards => _selectedCards;
    
    public event Action<IReadOnlyList<Card>> UpdateAvailable;
    public event Action<IReadOnlyList<Card>> UpdateSelected;

    public void Init(List<int> availableCardsIndexes, int[] selectedCardsIndexes)
    {
        for (int i = 0; i < availableCardsIndexes.Count; i++)
        {
            _availableCards.Add(_cards[availableCardsIndexes[i]]);
        }

        for (int i = 0; i < selectedCardsIndexes.Length; i++)
        {
            _selectedCards.Add(_cards[selectedCardsIndexes[i]]);
        }
        
        UpdateAvailable?.Invoke(AvailableCards);
        UpdateSelected?.Invoke(SelectedCards);
    }
}

[Serializable]
public class Card
{
    [field: SerializeField] public string name { get; private set; }
    [field: SerializeField] public Sprite sprite { get; private set; }
}
