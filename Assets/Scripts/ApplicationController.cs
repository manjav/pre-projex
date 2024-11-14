using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class ApplicationController : MonoBehaviour
{

    [SerializeField] private LevelData currentLevel;
    [SerializeField] private List<LevelData> levels;
    [SerializeField] private GridLayoutGroup gridLayoutGroup;
    [SerializeField] private LevelItemRenderer levelItemTemplate;

    // Start is called before the first frame update
    void Awake()
    {
        SoundManager.instance.PlayMusic();
        foreach (var level in levels)
        {
            var levelItemRenderer = Instantiate(levelItemTemplate, gridLayoutGroup.transform);
            levelItemRenderer.Initialize(level, OnCardClick);
        }
    }

    void OnCardClick(LevelData levelData)
    {
        currentLevel.CopyFrom(levelData);
        SceneManager.LoadScene("GameScene");
    }
}
