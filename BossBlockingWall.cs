using UnityEngine;

public class BossBlockingWall : MonoBehaviour
{
    void Start()
    {
        ToggleTrap(false);
    }

    public void ToggleTrap(bool active)
    {
        gameObject.SetActive(active);
    }
}
