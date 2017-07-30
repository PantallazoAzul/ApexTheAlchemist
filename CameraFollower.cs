using System.Collections;
using UnityEngine;

public class CameraFollower : MonoBehaviour
{
	public Transform playerToFollow;
	public float offsetX = 0f;
	public float offsetY = 1.5f;

	[Header ("Duración de la sacudida:")]
	[SerializeField]
	float shakeDuration = 1f;
	float _shakeDuration;
	[Header ("Amplitud de la sacudida:")]
	[SerializeField]
	float shakeAmount = 0.5f;
	[SerializeField]
	float decreaseFactor = 1.0f;

	public bool shaking = false;

	void Awake ()
	{
		_shakeDuration = shakeDuration;
	}

	void FixedUpdate ()
	{
		transform.position = new Vector3 (playerToFollow.position.x + offsetX, playerToFollow.position.y + offsetY, transform.position.z);

		if (shaking) {
			if (shakeDuration > 0) {
				transform.position = transform.position + Random.insideUnitSphere * shakeAmount;
				shakeDuration -= Time.deltaTime * decreaseFactor;
			} else {
				shakeDuration = _shakeDuration;
				shaking = false;
			}
		}
	}

}