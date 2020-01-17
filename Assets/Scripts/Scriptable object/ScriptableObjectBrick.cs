using UnityEngine;

public enum LevelBrick { LevelOne, LevelTwo, LevelThree, };

[CreateAssetMenu(menuName = "ScriptableObjects/Brick")]
public class ScriptableObjectBrick : ScriptableObject
{
    public int Health;
    public Color BrickColor;
    public GameObject[] PreBunusDrop;
}