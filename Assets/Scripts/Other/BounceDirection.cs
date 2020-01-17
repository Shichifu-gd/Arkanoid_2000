using UnityEngine;

public class BounceDirection : MonoBehaviour
{
    [SerializeField]
    private Vector2 Ditection;

    private float force = 5f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Ball")
        {
            Rigidbody2D rigidbody = collision.GetComponent<Rigidbody2D>();
            rigidbody.velocity = Ditection.normalized * force;
        }
    }
}