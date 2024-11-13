using System;
using UnityEngine;
using UnityEngine.UI;

public class MemoryCard : MonoBehaviour
{
    // The unique id of the card to detect cards equality 
    [NonSerialized] public int id;
    // The icon image of the card
    [SerializeField] Image iconImage;

    // The callback when the card is clicked
    public delegate void CardClickCallBack(int id);
    private CardClickCallBack callback;

    /// <summary>
    /// Initialize the card
    /// </summary>
    /// <param name="id"></param>
    /// <param name="sprite"></param>
    /// <param name="callback"></param>
    public void Initialize(int id, Sprite sprite, CardClickCallBack callback)
    {
        this.id = id;
        this.callback = callback;
        iconImage.sprite = sprite;
    }

    /// <summary>
    /// Callback when the card is clicked
    /// </summary>
    public void OnClick() => callback?.Invoke(id);
}
