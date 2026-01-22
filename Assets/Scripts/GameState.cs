using UnityEngine;

public class GameState : MonoBehaviour
{
    public static GameState I { get; private set; }

    [Header("Selection")]
    public string selectedSongId = "song1";
    public string selectedSongScene = "Song1Gameplay";


    [Header("Run results")]
    public int score = 0;
    public int maxCombo = 0;
    public bool failed = false;

    private void Awake()
    {
        if (I != null && I != this)
        {
            Destroy(gameObject);
            return;
        }
        I = this;
        DontDestroyOnLoad(gameObject);
    }

    public void ResetRun()
    {
        score = 0;
        maxCombo = 0;
        failed = false;
    }
}
