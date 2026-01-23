using UnityEngine;
using UnityEngine.SceneManagement;

public class SongEndController : MonoBehaviour
{
    public JudgeSystem judge;

    void Update()
    {
        if (judge != null && judge.life <= 0f)
        {
            if (GameState.I != null)
            {
                GameState.I.failed = true;
                GameState.I.score = judge.score;
                GameState.I.maxCombo = judge.maxCombo;
            }
            SceneManager.LoadScene("Fail");
            return;
        }

        if (Conductor.I != null && Conductor.I.SongEnded())
        {
            if (GameState.I != null)
            {
                GameState.I.failed = false;
                GameState.I.score = judge != null ? judge.score : 0;
                GameState.I.maxCombo = judge != null ? judge.maxCombo : 0;
            }
            SceneManager.LoadScene("Results");
        }
    }
}

