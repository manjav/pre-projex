using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class GameController : MonoBehaviour
{
    [SerializeField] private Transform parent;
    [SerializeField] private GridLayoutGroup gridLayoutGroup;
    [SerializeField] private MemoryCard memoryCard;
    [SerializeField] private List<Sprite> spritesList;
    [SerializeField] private int rowConstraintCount = 4;
    [SerializeField] private int columnsConstraintCount = 6;

    private MemoryCard selectedCard;

    /// <summary>
    /// Start is called before the first frame update
    /// </summary>
    void Start()
    {
        gridLayoutGroup.constraintCount = columnsConstraintCount;
        var idsList = new List<int>();
        var cardCount = rowConstraintCount * columnsConstraintCount;
        for (int i = 0; i < cardCount / 2; i++)
        {
            idsList.Add(i);
            idsList.Add(i);
        }
        Utils.Shuffle(idsList);
        foreach (var id in idsList)
        {
            var card = Instantiate(memoryCard, parent);
            card.Initialize(id, spritesList[id], OnCardClick);
        }
    }

    private void OnCardClick(MemoryCard selectedCard)
    {
        // Prevent multiple selection
        if (selectedCard.state != MemoryCardState.Normal)
        {
            return;
        }

        // Select the card
        if (this.selectedCard == null)
        {
            this.selectedCard = selectedCard;
            _ = selectedCard.Select(false);
            return;
        }

        // Match cards if they are the same. Deselect cards otherwise
        TryToMatch(this.selectedCard, selectedCard);
    }

    /// <summary>
    /// Match cards if they are the same. Deselect cards otherwise
    /// </summary>
    /// <param name="firstCard"></param>
    /// <param name="secondCard"></param>
    private async void TryToMatch(MemoryCard firstCard, MemoryCard secondCard)
    {
        selectedCard = null;
        await secondCard.Select(true);
        // Match cards
        if (firstCard.id == secondCard.id)
        {
            firstCard.Match();
            secondCard.Match();
        }
        else
        {
            // Deselect cards
            firstCard.Deselect();
            secondCard.Deselect();
        }
    }
}
