using UnityEngine;
using UnityEngine.SceneManagement;

public class TimesUp : MonoBehaviour
{
    public static bool TimeIsUp = false;
    public GameObject timeUpUI;

    public UnityEngine.UI.Text PlayerScoreText;

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

        var player1Score = GameObject.Find("Directional Light").GetComponent<CameraAndGame>().Player1Score;
        var player2Score = GameObject.Find("Directional Light").GetComponent<CameraAndGame>().Player2Score;

        PlayerScoreText.text = ($"Player 1: {player1Score}\nPlayer 2: {player2Score}");
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
