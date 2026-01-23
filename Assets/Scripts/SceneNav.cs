using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneNav : MonoBehaviour
{
    // Scene name constants (helps avoid typos)
    private const string START_MENU = "StartMenu";
    private const string SONG_SELECT = "SongSelect";
    private const string RESULTS = "Results";
    private const string FAIL = "Fail";
    private const string DEFAULT_SONG_SCENE = "Song1Gameplay"; // <- your song1 gameplay scene

    public void GoStartMenu() => SceneManager.LoadScene(START_MENU);
    public void GoSongSelect() => SceneManager.LoadScene(SONG_SELECT);
    public void GoResults() => SceneManager.LoadScene(RESULTS);
    public void GoFail() => SceneManager.LoadScene(FAIL);

    /// <summary>
    /// For convenience: go to the currently selected song scene.
    /// If GameState doesn't exist yet, fallback to Song1Gameplay.
    /// </summary>
    public void GoGameplay()
    {
        SceneManager.LoadScene(GetSelectedSongSceneOrDefault());
    }

    /// <summary>
    /// Retry the current song (reloads the selected song scene).
    /// Works from Fail screen too.
    /// </summary>
    public void Retry()
    {
        // Reset run state if available
        if (GameState.I != null)
            GameState.I.ResetRun();

        SceneManager.LoadScene(GetSelectedSongSceneOrDefault());
    }

    /// <summary>
    /// Fail screen button: go back to song select.
    /// Also resets run state so you don't carry "failed" state forward.
    /// </summary>
    public void FailToSongSelect()
    {
        if (GameState.I != null)
            GameState.I.ResetRun();

        SceneManager.LoadScene(SONG_SELECT);
    }

    /// <summary>
    /// Fail screen button: replay (alias for Retry).
    /// Useful if you want a clearer button hookup name in Inspector.
    /// </summary>
    public void FailReplay()
    {
        Retry();
    }

    /// <summary>
    /// Optional helper if you want a Fail button to go back to Start Menu.
    /// </summary>
    public void FailToStartMenu()
    {
        if (GameState.I != null)
            GameState.I.ResetRun();

        SceneManager.LoadScene(START_MENU);
    }

    public void Quit()
    {
        Application.Quit();
        Debug.Log("Quit (won't quit in editor)");
    }

    // --------------------
    // Helpers
    // --------------------

    private static string GetSelectedSongSceneOrDefault()
    {
        // Default fallback
        string scene = DEFAULT_SONG_SCENE;

        // Use selectedSongScene if your GameState stores it
        if (GameState.I != null && !string.IsNullOrEmpty(GameState.I.selectedSongScene))
            scene = GameState.I.selectedSongScene;

        return scene;
    }
}
