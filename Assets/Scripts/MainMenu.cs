using UnityEngine;
using UnityEngine.SceneManagement;

public class GameModeManager : MonoBehaviour
{
    public static bool Practice { get; private set; }
    public static bool Race { get; private set; }
    public static bool Drift { get; private set; }

    public static void SetGameModes(bool isPractice, bool isRace, bool isDrift)
    {
        Practice = isPractice;
        Race = isRace;
        Drift = isDrift;
    }
}

public class CarModeManager : MonoBehaviour
{

    public static bool Camaro { get; private set; }
    public static bool Porsche { get; private set; }
    public static bool AE86 { get; private set; }

    public static void SetGameModes(bool isCamaro, bool isPorsche, bool isAE86)
    {
        Camaro = isCamaro;
        Porsche = isPorsche;
        AE86 = isAE86;
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

    public void Drift()
    {
        SceneManager.LoadScene("DriftMenu");
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

    public void Camaro()
    {
        SceneManager.LoadScene("OvalDriftCamaro");
        CarModeManager.SetGameModes(true, false, false);
    }

    public void Porsche()
    {
        SceneManager.LoadScene("OvalDriftPorsche");
        CarModeManager.SetGameModes(false, true, false);
    }

    public void AE86()
    {
        SceneManager.LoadScene("OvalDriftCamaro");
        CarModeManager.SetGameModes(false, false, true);
    }

}
