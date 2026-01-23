using UnityEngine;

public class JudgeSystem : MonoBehaviour
{
    public SongChart chart;

    public KeyCode[] laneKeys = new KeyCode[7]
    {
        KeyCode.Z, KeyCode.X, KeyCode.A, KeyCode.S, KeyCode.D, KeyCode.W, KeyCode.E
    };

    [Header("Judgement Windows (seconds)")]
    public float perfect = 0.05f;
    public float good = 0.10f;
    public float bad = 0.18f;

    [Header("Score")]
    public int score { get; private set; }
    public int combo { get; private set; }
    public int maxCombo { get; private set; }

    [Header("Life")]
    public float life = 100f;
    public float missPenalty = 8f;
    public float wrongPenalty = 4f;

    public string lastJudge { get; private set; } = "";

    private int judgeIndex = 0;     // track progress through sorted notes
    private NoteData holding = null; // current hold note (if any)

    private void Update()
    {
        if (Conductor.I == null || chart == null) return;
        float t = Conductor.I.songTime;

        // 1) auto-miss notes that pass beyond bad window
        while (judgeIndex < chart.notes.Count)
        {
            var n = chart.notes[judgeIndex];
            if (t <= n.startTime + bad) break;
            Miss();
            judgeIndex++;
        }

        // 2) keydown tries to hit a note in that lane
        for (int lane = 0; lane < 7; lane++)
        {
            if (Input.GetKeyDown(laneKeys[lane]))
                TryHitLane(lane, t);
        }

        // 3) hold enforcement (if note has duration)
        if (holding != null)
        {
            int lane = holding.lane;
            float endTime = holding.startTime + holding.duration;

            bool stillHeld = Input.GetKey(laneKeys[lane]);

            // early release
            if (!stillHeld && t < endTime - 0.02f)
            {
                lastJudge = "Early Release";
                life -= 10f;
                combo = 0;
                holding = null;
            }

            // finished hold
            if (t >= endTime)
            {
                lastJudge = "Hold OK";
                holding = null;
            }
        }
    }

    private void TryHitLane(int lane, float t)
    {
        // find closest upcoming note in this lane near current time
        int bestIdx = -1;
        float bestAbs = 999f;

        for (int i = judgeIndex; i < chart.notes.Count && i < judgeIndex + 12; i++)
        {
            var n = chart.notes[i];
            if (n.lane != lane) continue;

            float diff = Mathf.Abs(t - n.startTime);
            if (diff < bestAbs)
            {
                bestAbs = diff;
                bestIdx = i;
            }
        }

        if (bestIdx < 0 || bestAbs > bad)
        {
            // pressed a lane with no matching note nearby
            lastJudge = "Wrong";
            life -= wrongPenalty;
            combo = 0;
            return;
        }

        // score judgement
        if (bestAbs <= perfect)
        {
            lastJudge = "Perfect";
            AddScore(300);
        }
        else if (bestAbs <= good)
        {
            lastJudge = "Good";
            AddScore(150);
        }
        else
        {
            lastJudge = "Bad";
            AddScore(50);
            life -= 2f;
        }

        combo++;
        maxCombo = Mathf.Max(maxCombo, combo);

        // start hold if needed
        var hit = chart.notes[bestIdx];
        if (hit.duration > 0.05f)
            holding = hit;

        // advance judgeIndex
        if (bestIdx == judgeIndex) judgeIndex++;
        else judgeIndex = Mathf.Max(judgeIndex, bestIdx + 1);
    }

    private void AddScore(int basePoints)
    {
        int mult = 1 + combo / 10;
        score += basePoints * mult;
    }

    private void Miss()
    {
        lastJudge = "Miss";
        life -= missPenalty;
        combo = 0;
    }
}
