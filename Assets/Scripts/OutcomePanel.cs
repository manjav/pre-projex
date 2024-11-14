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
    /// <param name="levelData"></param>
    /// <param name="score"></param>
    public void Win(LevelData levelData, int turnes)
    {
        gameObject.SetActive(true);
        titleText.text = "You Won!";
        SoundManager.instance.Play(winSFX);
        UpdateStars(levelData, turnes);
    }

    /// <summary>
    /// Show the lose panel
    /// </summary>
    public void Lose()
    {

        gameObject.SetActive(true);
        titleText.text = "You Lose!";
        SoundManager.instance.Play(loseSFX);
    }

    /// <summary>
    /// Update the stars
    /// </summary>
    /// <param name="levelData"></param>
    /// <param name="score"></param>
    void UpdateStars(LevelData levelData, int turnes)
    {
        var score = levelData.GetScore(turnes);
        for (int i = 0; i < score; i++)
        {
            starImages[i].color = Color.white;
        }

        print($"{score} {turnes},  {levelData.turnesThresholds}");
    }
}
