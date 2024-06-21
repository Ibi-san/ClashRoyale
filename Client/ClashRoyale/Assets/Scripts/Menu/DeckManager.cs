using System;
using System.Collections.Generic;
using UnityEngine;

public class DeckManager : MonoBehaviour
{
    [SerializeField] private GameObject _lockScreenCanvas;
    [SerializeField] private Card[] _cards;
    [SerializeField] private List<Card> _availableCards = new();
    [SerializeField] private List<Card> _selectedCards = new();
    public IReadOnlyList<Card> AvailableCards => _availableCards;
    public IReadOnlyList<Card> SelectedCards => _selectedCards;

    public event Action<IReadOnlyList<Card>, IReadOnlyList<Card>> UpdateAvailable;
    public event Action<IReadOnlyList<Card>> UpdateSelected;

    #region Editor

#if UNITY_EDITOR
    [SerializeField] private AvailableDeckUI _availableDeckUI;

    private void OnValidate()
    {
        _availableDeckUI.SetAllCardsCount(_cards);
    }
#endif

    #endregion

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

        UpdateAvailable?.Invoke(AvailableCards, SelectedCards);
        UpdateSelected?.Invoke(SelectedCards);
        
        _lockScreenCanvas.SetActive(false);
    }

    public void ChangesDeck(IReadOnlyList<Card> selectedCards, Action success)
    {
        _lockScreenCanvas.SetActive(true);
        int[] IDs = new int[selectedCards.Count];

        for (int i = 0; i < selectedCards.Count; i++)
        {
            IDs[i] = selectedCards[i].id;
        }

        string json = JsonUtility.ToJson(new Wrapper(IDs));
        string uri = URLLibrary.MAIN + URLLibrary.SETSELECTDECK;
        Dictionary<string, string> data = new()
        {
            { "userID", UserInfo.Instance.ID.ToString() },
            { "json", json }
        };

        success += () =>
        {
            for (int i = 0; i < _selectedCards.Count; i++)
            {
                _selectedCards[i] = selectedCards[i];
            }

            UpdateSelected?.Invoke(SelectedCards);
        };
        
        Network.Instance.Post(uri, data, (s) => SendSuccess(s, success), Error);
    }

    private void Error(string err)
    {
        Debug.LogError("Failed send new deck: " + err);
        _lockScreenCanvas.SetActive(false);
    }

    private void SendSuccess(string obj, Action success)
    {
        if (obj != "ok")
        {
            Error(obj);
            return;
        }

        success?.Invoke();
        _lockScreenCanvas.SetActive(false);
    }

    [Serializable]
    private class Wrapper
    {
        public int[] IDs;

        public Wrapper(int[] IDs)
        {
            this.IDs = IDs;
        }
    }
}

[Serializable]
public class Card
{
    [field: SerializeField] public string name { get; private set; }
    [field: SerializeField] public int id { get; private set; }
    [field: SerializeField] public Sprite sprite { get; private set; }
}