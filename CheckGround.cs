using UnityEngine;

public class CheckGround : MonoBehaviour
{

    bool grounded = false;
    bool floor = true;

    public bool Grounded
    {
        get
        {
            return grounded;
        }

        protected set
        {
            grounded = value;
        }
    }

    void OnCollisionStay2D(Collision2D coll)
    {
        Vector2 pos = transform.position;
        Vector2 normal = coll.contacts[0].normal;

        Debug.DrawRay(coll.contacts[0].point, normal, Color.red, 1f);
        if (normal == Vector2.right && normal == Vector2.left)
            floor = false;
        else
            floor = true;
        /* // En caso de necesitar todos los puntos de contacto:
        for (int i = 0; i < coll.contacts.Length; i++)
        {
            Vector2 point = coll.contacts[i].point - pos;
            Vector2 normal = coll.contacts[i].normal;
            float angle = Vector2.Angle(point, normal);

            Debug.DrawRay(coll.contacts[i].point, normal, Color.cyan, 1f);
            if (normal == Vector2.right)
            { floor = false; print("Golpe izq"); }
            else if (normal == Vector2.left)
            { floor = false; print("Golpe der"); }
            else { floor = true; }
        }*/
        if (coll.gameObject.tag == "Ground" && floor)
        {
            Grounded = true;
        }
    }

    void OnCollisionExit2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Ground" && floor)
        {
            Grounded = false;
        }
    }
}
