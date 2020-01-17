using UnityEngine;

public class BallController : MonoBehaviour
{
    private GameController gameController;

    private Rigidbody2D Rigidbody;

    private bool SwitchStartingImpulse;

    private void Awake()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
    }

    private void Start()
    {
        Rigidbody = GetComponent<Rigidbody2D>();
        gameController.RegisterBall(gameObject);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !SwitchStartingImpulse) StartingImpulse();
    }

    private void StartingImpulse()
    {
        gameObject.transform.parent = GameObject.FindGameObjectWithTag("World").transform;
        Rigidbody.bodyType = RigidbodyType2D.Dynamic;
        Rigidbody.AddForce(Vector2.up * 300);
        SwitchStartingImpulse = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "DestroyZone") gameController.UnregisterBall(gameObject);
    }
}