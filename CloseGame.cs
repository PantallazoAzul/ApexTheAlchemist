using UnityEngine;

public class CloseGame : MonoBehaviour
{
    public void Quit()
    {
        Debug.Log("Closing game...");
        Application.Quit();
    }
}
