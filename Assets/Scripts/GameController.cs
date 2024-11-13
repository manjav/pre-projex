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

    private void OnCardClick(int id)
    {
        print(id);
    }
}
