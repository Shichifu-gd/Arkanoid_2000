using System.Collections;
using UnityEngine;

public class Brick : MonoBehaviour
{
    private GameController gameController;

    private int HealthBrick;

    private SpriteRenderer spriteRenderer;
    public GameObject PreBonus { get; set; }

    public int SetHealthBrick
    {
        set
        {
            HealthBrick = value;
        }
    }

    private void Awake()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Ball")
        {
            Rigidbody2D rigidbody = collision.GetComponent<Rigidbody2D>();
            rigidbody.velocity = new Vector3(rigidbody.velocity.x, -rigidbody.velocity.y);
            GetDamage();
        }
    }

    public void GetDamage()
    {
        if ((HealthBrick - 1) > 0)
        {
            HealthBrick--;
            if (HealthBrick == 1) spriteRenderer.color = new Color(0f, 0f, 0f, 0.4f);
            if (HealthBrick == 2) spriteRenderer.color = new Color(0f, 0f, 0f, 0.2f);
        }
        else BlockDestruction();
    }

    private void BlockDestruction()
    {
        gameController.SetGameScore(Random.Range(0, 5));
        StartCoroutine(DestroyThisBrick());
    }

    private IEnumerator DestroyThisBrick()
    {
        yield return new WaitForSeconds(.1f);
        if (PreBonus) DropBonus();
        Destroy(gameObject);
    }

    private void DropBonus()
    {
        var random = Random.Range(0, 10);
        if (random == 5) SpawnBonus();
    }

    private void SpawnBonus()
    {
        GameObject NewBonus = Instantiate(PreBonus, gameObject.transform.position, Quaternion.identity);
        NewBonus.transform.parent = GameObject.FindGameObjectWithTag("World").transform;
    }
}