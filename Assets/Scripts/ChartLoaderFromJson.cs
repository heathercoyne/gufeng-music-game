using System.IO;
using UnityEngine;

public class ChartLoaderFromJson : MonoBehaviour
{
    public SongChart targetChart;
    public string fileName = "song1_recorded.json";

    void Awake()
    {
        Load();
    }

    void Load()
    {
        if (targetChart == null)
        {
            Debug.LogError("[CHART] targetChart is NULL. Assign Song1Chart in the Inspector on ChartLoader.");
            return;
        }

        string path = Path.Combine(Application.dataPath, "Charts", fileName);

        if (!File.Exists(path))
        {
            Debug.LogError($"[CHART] File not found: {path}");
            return;
        }

        string json = File.ReadAllText(path);
        RecordedChart data = JsonUtility.FromJson<RecordedChart>(json);

        if (data == null || data.notes == null)
        {
            Debug.LogError("[CHART] Invalid JSON or notes missing.");
            return;
        }

        if (targetChart.notes == null)
            targetChart.notes = new System.Collections.Generic.List<NoteData>();

        targetChart.notes.Clear();

        foreach (var n in data.notes)
        {
            targetChart.notes.Add(new NoteData
            {
                lane = Mathf.Clamp(n.lane, 0, 6),
                startTime = Mathf.Max(0f, n.startTime),
                duration = Mathf.Max(0f, n.duration)
            });
        }

        targetChart.notes.Sort((a, b) => a.startTime.CompareTo(b.startTime));

        Debug.Log($"[CHART] Loaded {targetChart.notes.Count} notes from:\n{path}");
    }
}
