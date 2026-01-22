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
        string scene = DEFAULT_SONG_SCENE;

        if (GameState.I != null && !string.IsNullOrEmpty(GameState.I.selectedSongScene))
            scene = GameState.I.selectedSongScene;

        SceneManager.LoadScene(scene);
    }

    /// <summary>
    /// Retry the current song (reloads the selected song scene).
    /// </summary>
    public void Retry()
    {
        string scene = DEFAULT_SONG_SCENE;

        if (GameState.I != null)
        {
            GameState.I.ResetRun();

            if (!string.IsNullOrEmpty(GameState.I.selectedSongScene))
                scene = GameState.I.selectedSongScene;
        }

        SceneManager.LoadScene(scene);
    }

    public void Quit()
    {
        Application.Quit();
        Debug.Log("Quit (won't quit in editor)");
    }
}
