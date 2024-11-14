using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class OutcomePanel : MonoBehaviour
{
    [SerializeField] private List<Image> starImages;
    [SerializeField] private TMPro.TextMeshProUGUI titleText;
    [SerializeField] private AudioClip winSFX;
    [SerializeField] private AudioClip loseSFX;

    /// <summary>
    /// Show the win panel
    /// </summary>
    /// <param name="turnesThresholds"></param>
    /// <param name="score"></param>
    public void Win(List<int> turnesThresholds, int score)
    {
        gameObject.SetActive(true);
        titleText.text = "You Won!";
        SoundManager.instance.Play(winSFX);
        UpdateStars(turnesThresholds, score);
    }

    /// <summary>
    /// Show the lose panel
    /// </summary>
    public void Lose()
    {

        gameObject.SetActive(true);
        titleText.text = "You Lose!";
        SoundManager.instance.Play(winSFX);
    }

    /// <summary>
    /// Update the stars
    /// </summary>
    /// <param name="turnesThresholds"></param>
    /// <param name="score"></param>
    void UpdateStars(List<int> turnesThresholds, int score)
    {
        for (int i = 0; i < starImages.Count; i++)
        {
            starImages[i].color = score > turnesThresholds[i] ? Color.white : new Color(0.8f, 0.8f, 0.8f, 1);
        }
    }
}
