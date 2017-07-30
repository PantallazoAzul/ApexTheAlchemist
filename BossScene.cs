using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossScene : MonoBehaviour
{
	Transform player;

	public GameObject[] bossWalls;
	[SerializeField]
	GameObject boss;

	bool bossBattle = false;

	void Start ()
	{
		player = GameObject.FindGameObjectWithTag ("Player").transform;
		ToggleWalls (false);
	}

	void FixedUpdate ()
	{
		if (player.position.x > 24f && !bossBattle) {
			bossBattle = true;
			ToggleWalls (true);
			Instantiate (boss);
			GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<AudioSource> ().Play ();
		}
	}

	public void ToggleWalls (bool active)
	{
		for (int i = 0; i < bossWalls.Length; i++) {
			bossWalls [i].SetActive (active);
		}
	}
}
