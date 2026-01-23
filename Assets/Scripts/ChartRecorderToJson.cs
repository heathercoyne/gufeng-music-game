using System;
using System.IO;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class ChartRecorderToJson : MonoBehaviour
{
    [Header("References")]
    public JudgeSystem judge;

    [Header("Save Settings")]
    [Tooltip("Saved to <Project>/Assets/Charts/<fileName>.")]
    public string fileName = "song1_recorded.json";

    [Tooltip("Use F5 by default (less likely to conflict with editor shortcuts).")]
    public KeyCode saveKey = KeyCode.F5;

    [Tooltip("Clear current recording with this key.")]
    public KeyCode clearKey = KeyCode.F9;

    [Tooltip("If true, automatically saves when the song ends.")]
    public bool autoSaveOnSongEnd = false;

    private RecordedChart chart = new RecordedChart();

    private readonly float[] downTime = new float[7];
    private readonly bool[] isDown = new bool[7];

    private bool savedOnce = false;

    private void Start()
    {
        if (judge == null)
            Debug.LogWarning("[REC] JudgeSystem not assigned on ChartRecorderToJson.");

        // Helpful log so you know exactly where it will save
        Debug.Log($"[REC] Recorder active. Save key={saveKey}. Will save to: {GetFullPath()}");
    }

    void Update()
    {
        if (Conductor.I == null || judge == null) return;

        // Record hold notes: key down = start, key up = end
        for (int i = 0; i < 7; i++)
        {
            if (Input.GetKeyDown(judge.laneKeys[i]))
            {
                downTime[i] = (float)Conductor.I.songTime;
                isDown[i] = true;
            }

            if (Input.GetKeyUp(judge.laneKeys[i]) && isDown[i])
            {
                float up = (float)Conductor.I.songTime;
                float dur = Mathf.Max(0f, up - downTime[i]);

                chart.notes.Add(new RecordedNote
                {
                    lane = i,
                    startTime = downTime[i],
                    duration = dur
                });

                Debug.Log($"[REC] lane={i} start={downTime[i]:F3} dur={dur:F3}");
                isDown[i] = false;
            }
        }

        // Manual save
        if (Input.GetKeyDown(saveKey))
        {
            Debug.Log($"[REC] Save key pressed ({saveKey}) -> saving...");
            Save();
        }

        // Clear recording
        if (Input.GetKeyDown(clearKey))
        {
            chart = new RecordedChart();
            Array.Clear(isDown, 0, isDown.Length);
            Debug.Log("[REC] cleared");
        }

        // Optional auto-save when music ends (only save once)
        if (autoSaveOnSongEnd && !savedOnce)
        {
            // If you have Conductor.I.songLengthSec, use that; otherwise this works if audioSource.clip exists.
            var src = Conductor.I.audioSource;
            if (src != null && src.clip != null)
            {
                if (Conductor.I.songTime >= src.clip.length - 0.01f)
                {
                    Debug.Log("[REC] Song ended -> auto-saving...");
                    Save();
                    savedOnce = true;
                }
            }
        }
    }

    private string GetFullPath()
    {
        // <Project>/Assets/Charts/fileName
        string dir = Path.Combine(Application.dataPath, "Charts");
        return Path.Combine(dir, fileName);
    }

    void Save()
    {
        try
        {
            // Ensure folder exists
            string dir = Path.Combine(Application.dataPath, "Charts");
            Directory.CreateDirectory(dir);

            string fullPath = Path.Combine(dir, fileName);

            // Sort notes by time
            chart.notes.Sort((a, b) => a.startTime.CompareTo(b.startTime));

            string json = JsonUtility.ToJson(chart, true);
            File.WriteAllText(fullPath, json);

            Debug.Log($"[REC] saved {chart.notes.Count} notes to:\n{fullPath}");

#if UNITY_EDITOR
            // Make file appear immediately in Unity Project window
            AssetDatabase.Refresh();
#endif
        }
        catch (Exception e)
        {
            Debug.LogError($"[REC] FAILED to save chart.\nPath: {GetFullPath()}\nError: {e}");
        }
    }
}
