using UnityEngine;
using UnityEngine.SceneManagement;

public class SongSelectController : MonoBehaviour
{
    /// <summary>
    /// Called by Song Select buttons.
    /// </summary>
    /// <param name="songId">Logical song id (e.g. "song1")</param>
    /// <param name="sceneName">Scene to load (e.g. "Song1Gameplay")</param>
    public void SelectSongAndLoad(string songId, string sceneName)
    {
        if (GameState.I != null)
        {
            GameState.I.selectedSongId = songId;
            GameState.I.selectedSongScene = sceneName;
            GameState.I.ResetRun();
        }

        SceneManager.LoadScene(sceneName);
    }
}
