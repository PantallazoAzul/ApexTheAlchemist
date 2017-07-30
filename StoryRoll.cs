using System.Collections;
using UnityEngine;

public class StoryRoll : MonoBehaviour
{
	public float speed = 10f;
	public float delay = 10f;

	//Transform[] images;
	int index = 0;

	void Start ()
	{
		StartCoroutine (DoRoll ());
		StartCoroutine (EndIntro ());
	}

	IEnumerator DoRoll ()
	{
		while (true) {
			if (index < transform.childCount) {
				if (transform.GetChild (index).position.x > 0.01f) {
					//images [index].Translate (Vector3.left * Time.deltaTime);
					transform.GetChild (index).Translate (Vector3.left * speed * Time.deltaTime);
				} else {
					index += 1;
					yield return new WaitForSeconds (delay);
					StartCoroutine (DoRollOut ()); 
				}
			}
			yield return null;
		}
	}

	IEnumerator DoRollOut ()
	{
		while (transform.GetChild (index - 1).position.x > -13f) {
			transform.GetChild (index - 1).Translate (Vector3.left * speed * Time.deltaTime);
			yield return null;
		}
	}

	IEnumerator EndIntro ()
	{
		while (true) {
			yield return new WaitForSeconds (60f);
			SceneControl sc = FindObjectOfType<SceneControl> ();
			sc.NextLevel ();
		}
	}
}
