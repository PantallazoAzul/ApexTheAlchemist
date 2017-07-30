using UnityEngine;

public class Spikes : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D coll)
    {
        IHit go = coll.gameObject.GetComponent<IHit>();
        if (go != null && coll.transform.tag == "Player")
            go.Kill();
    }
}
