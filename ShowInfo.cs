using System.Collections;
using UnityEngine;

public class ShowInfo : MonoBehaviour
{
	public GameObject info;

	bool toggle = false;

	void Start ()
	{
		ToggleInfo ();
	}

	public void ToggleInfo ()
	{
		info.SetActive (toggle);
		toggle = !toggle;
	}
}
