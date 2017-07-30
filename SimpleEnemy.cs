using UnityEngine;

public class SimpleEnemy : MonoBehaviour
{

    Rigidbody2D rb2d;

    bool right = true;
    [Header("Velocidad de movimiento")]
    [Range(0f, 10f)]
    public float speed = 2f;

    // Use this for initialization
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (right)
            rb2d.velocity = new Vector2(speed, rb2d.velocity.y);
        else
            rb2d.velocity = new Vector2(speed * -1, rb2d.velocity.y);
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        right = !right;
        if (coll.gameObject.tag == "Player")
        {
            coll.gameObject.GetComponent<CharacterMovement>().Kill();
        }
    }
}
