using System.Collections;
using UnityEngine;

public class BossIA : MonoBehaviour
{
	Rigidbody2D rb2d;

	[Header ("Dragones")]
	[SerializeField]
	GameObject dragons;
	[Header ("Rocas")]
	[SerializeField]
	GameObject rocks;
	[Header ("Tiempo en segundos entre ataques:")]
	[SerializeField]
	float cooldown = 10f;

	float nextAttack;
	// Jump
	private float nextJump;
	private bool jumping = false;
	private bool destruir = false;
	[Header ("Tiempo entre salto y salto:")]
	public float jumpCooldown = 10f;
	[Header ("Velocidad del salto:")]
	public float jumpVelocity = 10f;

	Transform target;

	Animator anim;

	void Start ()
	{
		rb2d = GetComponent<Rigidbody2D> ();
		target = GameObject.FindGameObjectWithTag ("Player").transform;
		nextAttack = Time.time + cooldown;
		nextJump = Time.time + jumpCooldown;
		anim = GetComponent<Animator> ();

	}

	void FixedUpdate ()
	{
		if (Time.time > nextJump && !jumping) {
			print ("saltando...");
			nextJump = Time.time + jumpCooldown;
			//Vector2 dir = (target.transform.position + (Vector3.up * jumpVelocity)) - transform.position;
			float xOrientation = .8f;
			if (target.position.x - transform.position.x < 0)
				xOrientation = -xOrientation;
			Vector2 dir = new Vector2 (xOrientation, 1f);
			dir.Normalize ();

			rb2d.AddForce (dir * jumpVelocity * 2, ForceMode2D.Impulse);
			//rb2d.velocity = dir * jumpVelocity;

			//rb2d.velocity = Vector2.up * jumpVelocity;
			jumping = true;
			anim.SetTrigger ("jumping");
		}
		if (transform.position.x >= target.transform.position.x - 0.25f &&
		    transform.position.x <= target.transform.position.x + 0.25f) {

			print ("atacando en salto...");
			rb2d.velocity = Vector2.down * 8f;
			destruir = true;
		}
	}

	void OnCollisionEnter2D (Collision2D coll)
	{
		if (coll.gameObject.tag == "Ground") {
			if (destruir)
				Destroy (coll.gameObject);
			destruir = false;
			jumping = false;
		}
	}

	void Update ()
	{
		if (transform.position.y < -7.5) {
			Destroy (gameObject); 
			FindObjectOfType<BossScene> ().ToggleWalls (false);
			return;
		}

		if (Time.time > nextAttack && !jumping) {
			nextAttack = Time.time + cooldown;
			ThrowRocks ();
			if (Random.value < .15f)
				InvokeDragons ();
		}
	}

	void ThrowRocks ()
	{
		CameraFollower cam = GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<CameraFollower> ();
		cam.shaking = true;

		Vector2 playerPos = GameObject.FindGameObjectWithTag ("Player").
			GetComponent<Transform> ().position;

		Instantiate (rocks, new Vector2 (playerPos.x - .75f, playerPos.y + 10f), Quaternion.identity);
		Instantiate (rocks, new Vector2 (playerPos.x + .75f, playerPos.y + 11.5f), Quaternion.identity);

		anim.SetTrigger ("attacking");
	}

	void InvokeDragons ()
	{
		Vector2 playerPos = GameObject.FindGameObjectWithTag ("Player").
			GetComponent<Transform> ().position;

		Instantiate (dragons, new Vector2 (playerPos.x - 1.5f, playerPos.y + 5f), Quaternion.identity);
		Instantiate (dragons, new Vector2 (playerPos.x + 1f, playerPos.y + 3.5f), Quaternion.identity);
		anim.SetTrigger ("attacking");
	}

	void OnTriggerEnter2D (Collider2D coll)
	{
		IHit go = coll.gameObject.GetComponent<IHit> ();
		if (go != null && coll.transform.tag == "Player")
			go.Kill ();
	}
}