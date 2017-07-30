using UnityEngine;

public class RotatingHammer : MonoBehaviour
{

    [Header("Velocidad de giro")]
    [Range(-720f, 720f)]
    public float torque = -180f;

    Rigidbody2D rb2d;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        rb2d.angularVelocity = torque;
    }
    /*
    public void Kill()
    {
        print("Objeto matado");
    }
    */
    void OnTriggerEnter2D(Collider2D coll)
    {
        IHit go = coll.gameObject.GetComponent<IHit>();
        if (go != null && coll.transform.tag == "Player")
            go.Kill();
    }
}
