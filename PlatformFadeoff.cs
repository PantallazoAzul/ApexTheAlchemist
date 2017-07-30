using System.Collections;
using UnityEngine;

public class PlatformFadeoff : MonoBehaviour
{
    SpriteRenderer[] sr;

    [Header("Duración en desaparecer:")]
    [SerializeField]
    float fadeDuration = 1f;

    float startTime;

    void Start()
    {
        sr = transform.GetComponentsInChildren<SpriteRenderer>();
        StartCoroutine(Fadeout());
    }
    /*
    void FixedUpdate()
    {
        float time = (Time.time - startTime) / fadeDuration;
        for (int i = 0; i < sr.Length; i++)
        {
            sr[i].color = new Color(1f, 1f, 1f, Mathf.SmoothStep(1f, 0f, time));
            print(sr[i].color.a);
        }
    }
    */
    IEnumerator Fadeout()
    {
        startTime = Time.time;
        while (true)
        {
            for (int i = 0; i < sr.Length; i++)
            {
                float time = (Time.time - startTime) / fadeDuration;
                sr[i].color = new Color(1f, 1f, 1f, Mathf.SmoothStep(1f, 0f, time));
                if (sr[i].color.a == 0f)
                    StopCoroutine(Fadeout());
                print("Alpha: " + sr[i].color.a);
                yield return new WaitForFixedUpdate();
            }
        }
    }
}
