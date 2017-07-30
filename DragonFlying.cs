using System.Collections;
using UnityEngine;

public class DragonFlying : MonoBehaviour, IHit
{
	[Header ("Velocidad de movimiento")]
	[SerializeField]
	float speed = 2f;

	public GameObject projectile;

	[Header ("Tiempo entre disparos")]
	[Range (0f, 30f)]
	public float fireTime = 1f;
	[Header ("Velocidad del proyectil")]
	[Range (.01f, 20f)]
	public float projectileSpeed = 3f;

	bool tracking = false;

	IEnumerator shooting;

	Rigidbody2D rb2d;
	Vector3 target;
	Vector3 targetOffset;
	Vector3 spawnPoint;

	Animator anim;

	void Start ()
	{
		shooting = ShootProjectile ();
		rb2d = GetComponent<Rigidbody2D> ();
		spawnPoint = transform.position;
		target = spawnPoint;
		targetOffset = spawnPoint;

		anim = GetComponent<Animator> ();
	}

	private void Update ()
	{
		Debug.DrawLine (transform.position, target, Color.yellow);
	}

	void FixedUpdate ()
	{
		if (!((targetOffset - transform.position).sqrMagnitude < .7f)) {
			Vector2 dir = targetOffset - transform.position;
			dir.Normalize ();

			rb2d.AddForce (dir * speed, ForceMode2D.Force);
		} else {
			rb2d.velocity = Vector2.zero;
		}
		Debug.DrawLine (transform.position, targetOffset, Color.cyan);
	}

	void OnTriggerStay2D (Collider2D coll)
	{
		GameObject target = coll.gameObject;
		if (target.tag == "Player") {
			if (!tracking) {
				tracking = true;
				StartCoroutine (shooting);
			}
			if (target.transform.position.x < transform.position.x) {
				this.target = (Vector2)target.transform.position;
				this.targetOffset = (Vector2)target.transform.position + new Vector2 (1.8f, 2.2f);

				transform.localScale = new Vector3 (-1, transform.localScale.y, transform.localScale.z);
			} else {
				this.target = (Vector2)target.transform.position;
				this.targetOffset = (Vector2)target.transform.position + new Vector2 (-1.8f, 2.2f);

				transform.localScale = new Vector3 (1, transform.localScale.y, transform.localScale.z);
			}
		}
	}

	void OnTriggerExit2D (Collider2D coll)
	{
		if (coll.gameObject.tag == "Player") {
			StopCoroutine (shooting);
			if (tracking)
				tracking = false;
			rb2d.velocity /= 2;
			targetOffset = spawnPoint;

			anim.SetBool ("attacking", false);
		}
	}


	IEnumerator ShootProjectile ()
	{
		while (gameObject.activeInHierarchy && tracking) {
			Vector2 shootPoint = (Vector2)transform.GetChild (0).position;

			Vector2 dir = (Vector2)target - shootPoint;
			dir.Normalize ();
			if (dir != Vector2.zero) {
				GameObject go = Instantiate (projectile, shootPoint, Quaternion.identity);
				Rigidbody2D rb2d = go.GetComponent<Rigidbody2D> ();
				rb2d.velocity = dir * projectileSpeed;
				Debug.DrawLine (shootPoint, target, Color.green, 1f);
				Debug.DrawRay (shootPoint, dir * projectileSpeed, Color.red, 1f);
				anim.SetBool ("attacking", true);
			}
			yield return new WaitForSeconds (fireTime);
			anim.SetBool ("attacking", false);
		}
	}

	public void Kill (bool die = true)
	{
		StopAllCoroutines ();
		Destroy (gameObject);
	}
}