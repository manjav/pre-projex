using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
public enum MemoryCardState
{
    Normal,
    Select,
    Match,
    Disposed,
}

public class MemoryCard : MonoBehaviour
{


    // The unique id of the card to detect cards equality 
    [NonSerialized] public int id;
    // The unique id of the card to detect cards equality 
    public MemoryCardState state = MemoryCardState.Normal;
    // The icon image of the card
    [SerializeField] Image iconImage;
    // The icon image of the card
    [SerializeField] Animator animator;

    // The callback when the card is clicked
    public delegate void CardClickCallBack(MemoryCard card);
    private CardClickCallBack callback;
    private TaskCompletionSource<int> selectedCompletion;

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
    /// Set the state of the card
    /// </summary>
    /// <param name="state"></param>
    private void SetState(MemoryCardState state)
    {
        this.state = state;
        animator.SetTrigger(state.ToString());
    }

    /// <summary>
    /// Select the card
    /// Change to selected state
    /// </summary>
    public async Task Select(bool isSecond)
    {
        SetState(MemoryCardState.Select);
        if (isSecond)
        {
            selectedCompletion = new TaskCompletionSource<int>(0);
            await selectedCompletion.Task;
        }
    }

    /// <summary>
    /// Deselect the card
    /// Return to normal state
    /// </summary>
    public void Deselect() => SetState(MemoryCardState.Normal);

    /// <summary>
    /// Match the card and destroy it
    /// </summary>
    public void Match() => SetState(MemoryCardState.Match);

    /// <summary>
    /// Callback when the card is clicked
    /// </summary>
    public void OnClick() => callback?.Invoke(this);

    /// <summary>
    /// Destroy the game object
    /// </summary>
    void Dispose() { }

    /// <summary>
    /// Callback when the card is selected
    /// </summary>
    void Selected() => selectedCompletion.SetResult(0);
}
