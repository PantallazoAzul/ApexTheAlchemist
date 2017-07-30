using UnityEngine;

public class WinPoint : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            //StartCoroutine(FindObjectOfType<SceneControl>().WinLevel());
            coll.GetComponent<CharacterMovement>().Kill(false);
        }
    }
}
