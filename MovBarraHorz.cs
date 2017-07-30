using UnityEngine;

public class MovBarraHorz : MonoBehaviour
{
    [SerializeField]
    float maxX;
    [SerializeField]
    float minX;
    [SerializeField]
    float velocidadX = 1;
    bool limiteX = false;

    void FixedUpdate()
    {
        float posx;
        float movimientoX = 1 * velocidadX * Time.fixedDeltaTime;

        if (transform.position.x <= minX) limiteX = true;
        if (transform.position.x >= maxX) limiteX = false;
        if (limiteX)
        {
            posx = transform.position.x + movimientoX;
        }
        else
        {
            posx = transform.position.x - movimientoX;
        }
        transform.position = new Vector3(posx, transform.position.y, transform.position.z);
    }
}
