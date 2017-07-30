using UnityEngine;

public class Projectile : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D coll)
    {
        IHit go = coll.gameObject.GetComponent<IHit>();
        if (go != null && coll.transform.tag == "Player")
            go.Kill();
        Destroy(gameObject);
    }
}
