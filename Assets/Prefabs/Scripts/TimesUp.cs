using UnityEngine;
using UnityEngine.SceneManagement;

public class TimesUp : MonoBehaviour
{
    public static bool TimeIsUp = false;
    public GameObject timeUpUI;

    private void Update()
    {
        if (Timer.timerActive == false)
            TimeIsUp = true;

        if (TimeIsUp)
            TurnMenuOn();
    }

    public void TurnMenuOn()
    {
        timeUpUI.SetActive(true);
        Time.timeScale = 0f; //Pause time
    }

    public void NewGame()
    {
        Debug.Log("Loadng game...");
        Time.timeScale = 1f; //Because i paused the game, the time is 0. I need to get it back to normal
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        Debug.Log("Quiting Game");
        Application.Quit();
    }
}
