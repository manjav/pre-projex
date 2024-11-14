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

        var savedScore = PlayerPrefs.GetInt($"Level {levelData.order}", 0);
        for (int i = 0; i < starImages.Count; i++)
        {
            starImages[i].color = savedScore > levelData.turnesThresholds[i] ? Color.white : new Color(0.8f, 0.8f, 0.8f, 1);
        }
    }

    // Internal click callback method 
    public void OnClick()
    {
        onClickCallback?.Invoke(levelData);
    }
}
