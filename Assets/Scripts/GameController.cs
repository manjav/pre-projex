using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class GameController : MonoBehaviour
{
    [SerializeField] private Slider turnesSlider;
    [SerializeField] private MemoryCard memoryCard;
    [SerializeField] private LevelData currentLevel;
    [SerializeField] private List<Sprite> spritesList;
    [SerializeField] private GridLayoutGroup gridLayoutGroup;
    [SerializeField] private TMPro.TextMeshProUGUI turnesText;
    [SerializeField] private OutcomePanel outcomePanel;
    [SerializeField] private AudioClip turnSFX;
    [SerializeField] private AudioClip matchSFX;
    [SerializeField] private AudioClip wrongSFX;


    private int turnes = 0;
    private int matchs = 0;
    private MemoryCard selectedCard;
    private List<MemoryCard> cardsList;

    /// <summary>
    /// Initialize the game controller
    /// </summary>
    void Awake()
    {
        turnes = currentLevel.turnesThresholds[3];
        turnesSlider.value = turnesSlider.maxValue = turnes;
        turnesText.text = turnes.ToString();

        // Autosize the grid of cards
        Canvas canvas = FindObjectOfType<Canvas>();
        gridLayoutGroup.constraintCount = currentLevel.columnsCount;
        var itemSize = canvas.GetComponent<RectTransform>().rect.width / currentLevel.columnsCount * 0.5f;
        gridLayoutGroup.cellSize = new Vector2(itemSize, itemSize);

        // Initialize the cards
        var cardCount = currentLevel.rowsCount * currentLevel.columnsCount;
        var idsList = new List<int>();
        for (int i = 0; i < cardCount / 2; i++)
        {
            idsList.Add(i);
            idsList.Add(i);
        }
        Utils.Shuffle(idsList);
        cardsList = new List<MemoryCard>(cardCount);
        foreach (var id in idsList)
        {
            var card = Instantiate(memoryCard, gridLayoutGroup.transform);
            card.Initialize(id, spritesList[id], OnCardClick);
            cardsList.Add(card);
        }
    }

    private void OnCardClick(MemoryCard selectedCard)
    {
        // Play turn sound
        SoundManager.instance.Play(turnSFX);

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
        turnes--;
        turnesText.text = turnes.ToString();
        turnesSlider.value = turnes;

        selectedCard = null;
        await secondCard.Select(true);

        // Match cards  
        if (firstCard.id == secondCard.id)
        {
            SoundManager.instance.Play(matchSFX);

            firstCard.Match();
            secondCard.Match();

            matchs++;
            // Win if all cards are matched
            if (matchs >= cardsList.Count / 2)
            {
                var prefsName = $"Level {currentLevel.order}";
                var savedScore = PlayerPrefs.GetInt(prefsName, 0);
                if (turnes > savedScore)
                {
                    PlayerPrefs.SetInt(prefsName, turnes);
                }
                outcomePanel.Win(currentLevel, turnes);
            }
        }
        else
        {
            SoundManager.instance.Play(wrongSFX);
            // Deselect cards
            firstCard.Deselect();
            secondCard.Deselect();

            // Lose if no more turns
            if (turnes <= 0)
            {
                outcomePanel.Lose();
            }
        }
    }

    public void BackToHome() => SceneManager.LoadScene("homeScene");

}
