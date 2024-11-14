using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class LevelItemRenderer : MonoBehaviour
{
    [SerializeField] private List<Image> starImages;
    [SerializeField] private TMPro.TextMeshProUGUI titleText;

    private LevelData levelData;

    // The callback when the item is clicked
    public delegate void ItemClickCallBack(LevelData levelData);
    private ItemClickCallBack onClickCallback;

    // Initialize the item renderer
    public void Initialize(LevelData levelData, ItemClickCallBack onClickCallback)
    {
        this.levelData = levelData;
        this.onClickCallback = onClickCallback;
        titleText.text = $"Level {levelData.order}";

    }

    // Internal click callback method 
    public void OnClick()
    {
        onClickCallback?.Invoke(levelData);
    }
}
