using UnityEngine;

public class RocksFalling : MonoBehaviour
{
    [Header("Velocidad de caída:")]
    [SerializeField]
    float fallSpeed;
    Rigidbody2D rb2d;

    void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        rb2d.velocity = Vector2.down * fallSpeed;
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        IHit go = coll.gameObject.GetComponent<IHit>();
        if (go != null && coll.transform.tag == "Player")
            go.Kill();
        Destroy(gameObject);
    }
}
