using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public BrickManager brickManager;
    public UIManager uIManager;

    public int LifePlayer { get; set; }
    public int CountBrick { get; set; }
    public int BaseCountBrick { get; set; }
    public int MainGameScore { get; set; }
    public int BonusPoints { get; set; }

    private bool RespawnSwitch;
    private bool IsLosing;

    public GameObject PreBall;
    public Transform SpawnPositionForBall;
    private List<GameObject> Balls = new List<GameObject>();

    private void Start()
    {
        brickManager.StartBrickManager();
        LifePlayer = 3;
        BaseCountBrick = brickManager.GetBrickCount();
    }

    private void Update()
    {
        if (Balls.Count <= 0 && !RespawnSwitch)
        {
            RespawnSwitch = true;
            Respawn();
        }
        UpdeteComponent();
        if (CountBrick <= 0) Win();
    }

    private void Respawn()
    {
        SetPlayerLife();
        SpawnBall();
    }

    private void SpawnBall()
    {
        if (!IsLosing)
        {
            GameObject NewBall = Instantiate(PreBall, SpawnPositionForBall.transform.position, Quaternion.identity);
            NewBall.transform.parent = GameObject.FindGameObjectWithTag("Player").transform;
            RespawnSwitch = false;
        }
    }

    private void UpdeteComponent()
    {
        CountBrick = brickManager.GetBrickCount();
        uIManager.SetLifePlayerText(LifePlayer.ToString());
        uIManager.SetBrickText(CountBrick.ToString(), BaseCountBrick.ToString());
        uIManager.SetGameScoreText(MainGameScore.ToString());
    }

    public void RegisterBall(GameObject ball)
    {
        Balls.Add(ball);
    }

    public void UnregisterBall(GameObject ball)
    {
        if (Balls.Count > 0)
        {
            Destroy(ball);
            Balls.RemoveAt(0);
            BonusPoints = 0;
        }
    }

    public void SetPlayerLife()
    {
        LifePlayer--;
        if ((LifePlayer - 1) < 0) Losing();
    }

    public void SetPlayerLifeBonus()
    {
        LifePlayer++;
    }

    private void Losing()
    {
        IsLosing = true;
        uIManager.EndGame("You Losing!!", (BaseCountBrick - CountBrick).ToString(), MainGameScore.ToString());
    }

    private void Win()
    {
        uIManager.EndGame("You Win!!", BaseCountBrick.ToString(), MainGameScore.ToString());
    }

    public void SetGameScore(int num)
    {
        MainGameScore += num;
        BonusPoints += (num / 2);
    }

    public void SetGameBonusScore()
    {
        MainGameScore += (BonusPoints * 2);
        BonusPoints = 0;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}