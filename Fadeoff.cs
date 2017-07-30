using UnityEngine;
using System.Collections;

public class Fadeoff : MonoBehaviour
{
    BoxCollider2D bc;
    SpriteRenderer[] sr;
    ParticleSystem particles;

    [Header("Tiempo en desvanecer:")]
    [Range(0f, 10f)]
    [SerializeField]
    float fadeoutTime = 1f;
    [Header("Tiempo en reaparecer:")]
    [Range(0f, 10f)]
    [SerializeField]
    float fadeinTime = 1f;


    void Awake()
    {
        bc = GetComponent<BoxCollider2D>();
        sr = transform.GetComponentsInChildren<SpriteRenderer>();
        particles = GetComponent<ParticleSystem>();
    }


    void OnCollisionEnter2D(Collision2D c)
    {
        if (c.gameObject.tag == "Player")
        {
            for (int i = 0; i < sr.Length; i++)
            {
                StartCoroutine(Fadeout(i));
            }
        }
    }

    IEnumerator Fadeout(int i)
    {
        particles.Play();
        yield return new WaitForSeconds(fadeoutTime);
        sr[i].enabled = false;
        bc.enabled = false;
        yield return new WaitForSeconds(fadeinTime);
        sr[i].enabled = true;
        bc.enabled = true;
    }
}
