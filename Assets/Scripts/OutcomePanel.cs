using UnityEngine;

public class OutcomePanel : MonoBehaviour
{
    [SerializeField] private TMPro.TextMeshProUGUI titleText;

    public void Win()
    {
        gameObject.SetActive(true);
        titleText.text = "You Win!";
    }

    public void Lose()
    {
        gameObject.SetActive(true);
        titleText.text = "You Lose!";
    }
}
