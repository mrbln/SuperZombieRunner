using UnityEngine;

public class PauseGame : MonoBehaviour
{
    private bool isGamePaused;

    public void Pause()
    {
        if (!isGamePaused)
        {
            Time.timeScale = 0;
            isGamePaused = true;
        } else
        {
            Time.timeScale = 1;
            isGamePaused = false;
        }
    }
}
