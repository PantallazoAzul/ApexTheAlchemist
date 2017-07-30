using System.Collections;
using UnityEngine;

public class CharacterMovement : MonoBehaviour, IHit
{
    #region VARIABLES

    [Header("Input")]
    public KeyCode up = KeyCode.W;
    public KeyCode down = KeyCode.S;
    public KeyCode attack = KeyCode.Space;
    [SerializeField]
    private KeyCode pause = KeyCode.Escape;

    Rigidbody2D rb2d;

    [Header("Player properties")]
    [Range(.1f, 10f)]
    public float speed = 2f;
    [Range(0f, 50f)]
    public float jumpVelocity = 2f;
    [Header("Player properties")]
    [Range(.1f, 18f)]
    public float fallMultiplier = 3f;
    [Range(.1f, 8f)]
    public float lowJumpMultiplier = 1.5f;

    CheckGround checkGround;
    [SerializeField]
    bool grounded;

    [SerializeField]
    GameObject dieParticles;

    GameObject attackPoint;

    Animator anim;

    #endregion

    #region INITIALIZATION

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        checkGround = GetComponentInChildren<CheckGround>();
        attackPoint = transform.GetChild(0).gameObject;
        attackPoint.GetComponent<SpriteRenderer>().enabled = false;
        anim = GetComponent<Animator>();
    }

    #endregion

    void Update()
    {
        if (Input.GetKeyDown(pause))
        {
            FindObjectOfType<SceneControl>().PauseGame();
            return;
        }

        bool doAttack = Input.GetKeyDown(attack);

        bool moveUp = Input.GetKeyDown(up);
        bool moveDown = Input.GetKeyDown(down);

        if (grounded != checkGround.Grounded && rb2d.velocity.y == 0f)
            grounded = checkGround.Grounded;

        if (moveUp && grounded)
            Jump();
        else if (moveDown && grounded)
            GoDown();

        if (doAttack && !attackPoint.GetComponent<SpriteRenderer>().enabled)
            Attack();
        // Only if player fall away.
        if (transform.position.y < -40f)
            Kill();
    }

    void FixedUpdate()
    {
        float moveX = Input.GetAxis("Horizontal") * speed;

        if (moveX < 0) rb2d.transform.localScale = new Vector2(-1, 1);
        else if (moveX > 0) rb2d.transform.localScale = new Vector2(1, 1);
        rb2d.velocity = new Vector2(moveX, rb2d.velocity.y);

        if (rb2d.velocity.y < 0)
            rb2d.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1f) * Time.fixedDeltaTime;
        else if (rb2d.velocity.y > 0 && !Input.GetKey(up))
            rb2d.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1f) * Time.fixedDeltaTime;
    }

    void LateUpdate()
    {
        anim.SetFloat("velocity", Mathf.Abs(rb2d.velocity.x));
    }

    #region VERTICAL MOVEMENT

    void Jump()
    {
        if (rb2d.velocity.y == 0f)
        {
            rb2d.velocity = Vector2.up * jumpVelocity;
        }
    }

    void GoDown()
    {
        PlatformEffector2D platform = GetComponentInParent<PlatformEffector2D>();
        if (platform != null)
        {
            platform.surfaceArc = 0f;
            StartCoroutine(RestorePlatform(platform));
        }
        else
        {
            Debug.Log("Player wasn't on a platform.");
        }
    }

    IEnumerator RestorePlatform(PlatformEffector2D platform)
    {
        yield return new WaitForSeconds(.3f);
        platform.surfaceArc = 180f;
        StopCoroutine(RestorePlatform(platform));
    }

    #endregion

    #region ATTACK

    void Attack()
    {
        attackPoint.GetComponent<SpriteRenderer>().enabled = true;
        attackPoint.GetComponent<AudioSource>().Play();

        /*int layerMask = 8;*/
        RaycastHit2D[] hit = Physics2D.BoxCastAll(
            /*attackPoint.*/transform.position,
            new Vector2(1f, 1.4f),
            0f,
            new Vector2(transform.localScale.x, 0f),
            1.5f/*,
            layerMask*/);
        for (int i = 0; i < hit.Length; i++)
        {
            if (hit[i].collider != null)
            {
                //Debug.DrawRay(hit[i].point, hit[i].normal, Color.red, 1f);
                if (!hit[i].collider.isTrigger)
                {
                    GameObject target = hit[i].transform.gameObject;
                    if (target.tag != "Player")
                    {
                        //print("Objetivo: " + hit[i].collider);
                        IHit go = target.GetComponent<IHit>();
                        if (go != null)
                            go.Kill();
                    }
                }
            }
        }
        StartCoroutine(RefreshAttack());
    }

    IEnumerator RefreshAttack()
    {
        yield return new WaitForSeconds(.25f);
        attackPoint.GetComponent<SpriteRenderer>().enabled = false;
    }

    #endregion

    void OnCollisionEnter2D(Collision2D coll)
    {/*
        Vector2 pos = rb2d.transform.position;
        for (int i = 0; i < coll.contacts.Length; i++)
        {
            Vector2 point = coll.contacts[i].point - pos;
            Vector2 normal = coll.contacts[i].normal;
            float angle = Vector2.Angle(point, normal);

            Debug.DrawRay(coll.contacts[i].point, normal, Color.red, 1f);
            if (normal == Vector2.right)
                print("Golpe izq");
            else if (normal == Vector2.left)
                print("Golpe der");
        }*/

        var platform = coll.gameObject.GetComponent<PlatformEffector2D>();
        if (platform != null)
            transform.SetParent(coll.gameObject.transform);
    }

    void OnCollisionExit2D(Collision2D coll)
    {
        var platform = coll.gameObject.GetComponent<PlatformEffector2D>();
        if (platform != null && gameObject.activeInHierarchy)
        {
            transform.SetParent(null);
        }
    }

    public void Kill(bool die = true)
    {
        GetComponent<CharacterMovement>().enabled = false;
        GetComponent<Rigidbody2D>().simulated = false;
        GetComponent<BoxCollider2D>().enabled = false;
        SceneControl scenes = FindObjectOfType<SceneControl>();
        if (die)
        {
            Instantiate(dieParticles, transform);
            StartCoroutine(scenes.LoseLevel());
        }
        else { StartCoroutine(scenes.WinLevel()); }
    }
}
