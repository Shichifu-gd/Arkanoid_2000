using UnityEngine;

public enum BonusType { HealthSupplement, SpeedIncrease, IncreaseSize, };

public class Bonus : MonoBehaviour
{
    private Rigidbody2D Rigidbody;

    [SerializeField]
    private BonusType bonus = new BonusType();

    private void Start()
    {
        Rigidbody = GetComponent<Rigidbody2D>();
        GravityChange();
    }

    private void GravityChange()
    {
        var randomGravityScale = Random.Range(0.1f, 0.5f);
        Rigidbody.gravityScale = randomGravityScale;
    }

    public BonusType GetBonus()
    {
        return bonus;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "DestroyZone") Destroy(gameObject);
    }
}