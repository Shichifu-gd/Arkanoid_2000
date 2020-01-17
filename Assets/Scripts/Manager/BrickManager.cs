using System.Collections.Generic;
using UnityEngine;

public class BrickManager : MonoBehaviour
{
    private Brick CurrentBrick;

    private SpriteRenderer SpriteRendererCurrentBrick;

    public ScriptableObjectBrick[] BricksLevels;
    public List<GameObject> BricksList;

    public void StartBrickManager()
    {
        ChangeBricks();
    }

    private void ChangeBricks()
    {
        for (int index = 0; index < BricksList.Count; index++)
        {
            CurrentBrick = BricksList[index].GetComponent<Brick>();
            SpriteRendererCurrentBrick = BricksList[index].GetComponent<SpriteRenderer>();
            BrickComponents();
        }
    }

    private void BrickComponents()
    {
        var randomLevel = Random.Range(0, BricksLevels.Length);
        CurrentBrick.SetHealthBrick = BricksLevels[randomLevel].Health;
        if (BricksLevels[randomLevel].PreBunusDrop.Length > 0) CurrentBrick.PreBonus = BricksLevels[randomLevel].PreBunusDrop[Random.Range(0, BricksLevels[randomLevel].PreBunusDrop.Length)];
        SpriteRendererCurrentBrick.color = BricksLevels[randomLevel].BrickColor;
    }

    public int GetBrickCount()
    {
        BricksList.RemoveAll(x => x == null);
        return BricksList.Count;
    }
}