using System.Collections;
using UnityEngine;

public class Gargoyle : MonoBehaviour
{
    enum Direction { Izquierda, Derecha, Arriba, Abajo }

    public GameObject projectile;

    [Header("Tiempo entre disparos")]
    [Range(0f, 30f)]
    public float fireTime = 1f;
    [Header("Velocidad del proyectil")]
    [Range(.01f, 20f)]
    public float projectileSpeed = 3f;
    [Header("Dirección del proyectil")]
    [SerializeField]
    Direction direction = Direction.Izquierda;

    Vector2 dir;
    Vector2 offset;

    //Animator anim;

    void Awake()
    {
        switch (direction)
        {
            case Direction.Izquierda:
                dir = Vector2.left;
                offset = new Vector2(-1f, .5f);
                break;
            case Direction.Derecha:
                dir = Vector2.right;
                offset = new Vector2(1f, .5f);
                break;
            case Direction.Arriba:
                dir = Vector2.up;
                offset = new Vector2(0f, 1.5f);
                break;
            case Direction.Abajo:
                dir = Vector2.down;
                offset = new Vector2(0f, -1.5f);
                break;
        }
    }

    void Start()
    {
        //anim = GetComponent<Animator>();
        StartCoroutine(ShootProjectile());
    }

    IEnumerator ShootProjectile()
    {
        while (gameObject.activeInHierarchy)
        {
            //anim.SetBool("attacking", true);

            GameObject go = Instantiate(projectile, (Vector2)transform.position + offset, Quaternion.identity);
            Rigidbody2D rb2d = go.GetComponent<Rigidbody2D>();
            rb2d.velocity = dir * projectileSpeed;

            yield return new WaitForSeconds(fireTime);
        }
    }
}
