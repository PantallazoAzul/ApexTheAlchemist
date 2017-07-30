using UnityEngine;

public class MovBarraVert : MonoBehaviour
{
    [SerializeField]
    float maxY;
    [SerializeField]
    float minY;
    [SerializeField]
    float velocidadY = 1;
    bool limiteY = false;

    void FixedUpdate()
    {
        float posy;
        float movimientoY = 1 * velocidadY * Time.fixedDeltaTime;

        if (transform.position.y <= minY) limiteY = true;
        if (transform.position.y >= maxY) limiteY = false;
        if (limiteY)
        {
            posy = transform.position.y + movimientoY;
        }
        else
        {
            posy = transform.position.y - movimientoY;
        }
        transform.position = new Vector3(transform.position.x, posy, transform.position.z);
    }
}
