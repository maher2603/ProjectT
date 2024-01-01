using UnityEngine;
using UnityEngine.SceneManagement;

public class GameModeManager : MonoBehaviour
{
    public static bool Practice { get; private set; }
    public static bool Race { get; private set; }
    public static bool Story { get; private set; }

    public static void SetGameModes(bool isPractice, bool isRace, bool isStory)
    {
        Practice = isPractice;
        Race = isRace;
        Story = isStory;
    }
}

public class MainMenu : MonoBehaviour
{
    public void ExitButton()
    {
        Application.Quit();
        Debug.Log("Game Closed");
    }

    public void StartGame()
    {
        SceneManager.LoadScene("GameMenu");
    }

    public void ReturnMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void Practice()
    {
        SceneManager.LoadScene("MapMenu");
        GameModeManager.SetGameModes(true, false, false);
    }

    public void Race()
    {
        SceneManager.LoadScene("MapMenu");
        GameModeManager.SetGameModes(false, true, false);
    }

    public void Story()
    {
        SceneManager.LoadScene("StoryGame");
        GameModeManager.SetGameModes(false, false, true);
    }

    public void OvalMap()
    {
        if (GameModeManager.Practice)
        {
            SceneManager.LoadScene("OvalPracticeGame");
        }
        else if (GameModeManager.Race)
        {
            SceneManager.LoadScene("OvalRaceGame");
        }
        else
        {
            StartGame();
        }
    }

    public void BlazeMap()
    {
        if (GameModeManager.Practice)
        {
            SceneManager.LoadScene("BlazePracticeGame");
        }
        else if (GameModeManager.Race)
        {
            SceneManager.LoadScene("BlazeRaceGame");
        }
        else
        {
            StartGame();
        }
    }
}
