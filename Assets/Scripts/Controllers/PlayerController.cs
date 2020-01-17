using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameController gameController;

    private Rigidbody2D Rigidbody;

    private float CurrentSpeedMove;
    private float BaseSpeedMove = 10f;
    private float Acceleration = 2f;
    private float BonusSpeed = 0f;
    private float Force = 5f;

    private bool SwitchForSpeed;
    private bool SwitchForBonusSpeed;
    private bool SwitchForBonusSize;

    private Vector2 BaseDirection = new Vector2(0f, 1f);
    private Vector2 DirectionLeft = new Vector2(-1f, 1f);
    private Vector2 DirectionRight = new Vector2(1f, 1f);
    private Vector2 CurrentDirection;
    private Vector2 HorizontalMove;

    private void Start()
    {
        Rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        HorizontalMove = new Vector2(Input.GetAxis("Horizontal"), 0f);
    }

    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) IncreaseSpeed();
        else CurrentSpeedMove = BaseSpeedMove;
        if (Input.GetAxis("Horizontal") > 0)
        {
            CurrentDirection = DirectionRight;
            MovePlatform();
        }
        else if (Input.GetAxis("Horizontal") < 0)
        {
            CurrentDirection = DirectionLeft;
            MovePlatform();
        }
        else CurrentDirection = BaseDirection;
    }

    private void MovePlatform()
    {
        Rigidbody.velocity = HorizontalMove * (CurrentSpeedMove + BonusSpeed);
    }

    private void IncreaseSpeed()
    {
        if (SwitchForSpeed) CurrentSpeedMove += Acceleration;
        SwitchForSpeed = false;
    }

    private void ReturnBaseSpeed()
    {
        if (!SwitchForSpeed) CurrentSpeedMove = BaseSpeedMove;
        SwitchForSpeed = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Bonus")
        {
            BonusType bonusType = collision.GetComponent<Bonus>().GetBonus();
            Destroy(collision.gameObject);
            if (BonusType.HealthSupplement == bonusType) TakeBonusHealth();
            if (BonusType.SpeedIncrease == bonusType) TakeBonusSpeed();
            if (BonusType.IncreaseSize == bonusType) TakeBonusSize();
        }
        if (collision.tag == "Ball")
        {
            Rigidbody2D rigidbodyBall = collision.GetComponent<Rigidbody2D>();
            rigidbodyBall.velocity = CurrentDirection.normalized * Force;
            gameController.SetGameBonusScore();
        }
    }

    private void TakeBonusHealth()
    {
        gameController.SetPlayerLifeBonus();
    }

    private void TakeBonusSpeed()
    {
        if (!SwitchForBonusSpeed) StartCoroutine(ActiveBonusSpeed());
        SwitchForBonusSpeed = true;
    }

    private IEnumerator ActiveBonusSpeed()
    {
        BonusSpeed = Random.Range(2f, 5f);
        yield return new WaitForSeconds(Random.Range(1f, 10f));
        BonusSpeed = 0f;
        SwitchForBonusSpeed = false;
    }

    private void TakeBonusSize()
    {
        if (!SwitchForBonusSize) StartCoroutine(ActiveBonusSize());
        SwitchForBonusSize = true;
    }

    private IEnumerator ActiveBonusSize()
    {
        Vector3 baseScale = transform.localScale;
        transform.localScale = new Vector3(Random.Range(0.5f, 1.2f), baseScale.y, baseScale.z);
        yield return new WaitForSeconds(Random.Range(1f, 10f));
        transform.localScale = baseScale;
        SwitchForBonusSize = false;
    }
}