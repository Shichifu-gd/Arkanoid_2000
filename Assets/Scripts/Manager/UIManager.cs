using UnityEngine.UI;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public Text LifePlayerText;
    public Text BrickText;
    public Text GameScoreText;
    public Text LabelText;
    public Text EndGameScoreText;
    public Text CountDestoyText;

    public GameObject PanelForEndGame;

    private void Start()
    {
        PanelForEndGame.SetActive(false);
    }

    public void SetLifePlayerText(string value)
    {
        LifePlayerText.text = $"Life - {value}";
    }

    public void SetBrickText(string valueCur, string valueMax)
    {
        BrickText.text = $"Bricks - {valueCur} / {valueMax}";
    }

    public void SetGameScoreText(string value)
    {
        GameScoreText.text = $"Score - {value}";
    }

    public void EndGame(string label, string countDestroy, string score)
    {
        LabelText.text = label;
        EndGameScoreText.text = $"Game score - {score}";
        CountDestoyText.text = $"Number destroyed - {countDestroy}";
        PanelForEndGame.SetActive(true);
    }
}