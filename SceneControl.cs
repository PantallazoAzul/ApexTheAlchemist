using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneControl : MonoBehaviour
{
	bool paused = false;

	Canvas canvas;

	private void Awake ()
	{
		canvas = FindObjectOfType<Canvas> ();
		if (SceneManager.GetActiveScene ().buildIndex != 0 && canvas != null
		    && SceneManager.GetActiveScene ().buildIndex != SceneManager.sceneCountInBuildSettings - 1)
			canvas.gameObject.SetActive (false);
	}

	public void RestartLevel ()
	{
		SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex);
		CheckPaused ();
	}

	public void StartGame ()
	{
		SceneManager.LoadScene (1);
		CheckPaused ();
	}

	public void NextLevel ()
	{
		int nextLvl = SceneManager.GetActiveScene ().buildIndex + 1;
		if (nextLvl > SceneManager.sceneCountInBuildSettings - 1)
			SceneManager.LoadScene (0);
		else
			SceneManager.LoadScene (nextLvl);
	}

	public IEnumerator WinLevel ()
	{
		var winPoint = GameObject.FindGameObjectWithTag ("Finish").GetComponent<Animator> ();
		winPoint.SetBool ("open", true);
		yield return new WaitForSeconds (2f);
		NextLevel ();
	}

	public IEnumerator LoseLevel ()
	{
		yield return new WaitForSeconds (1f);
		RestartLevel ();
	}

	public void PauseGame ()
	{
		//var player = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterMovement>();
		if (!paused)
			Time.timeScale = 0f;
		else
			Time.timeScale = 1f;
		paused = !paused;
		canvas.gameObject.SetActive (paused);
	}

	void CheckPaused ()
	{
		if (Time.timeScale != 1f)
			Time.timeScale = 1f;
	}
	// Cheat
	/*void Update ()
	{
		if (Input.GetKeyDown (KeyCode.O))
			NextLevel ();
	}
	*/
}
