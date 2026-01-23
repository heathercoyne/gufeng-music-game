using UnityEngine;

public class NoteSpawner : MonoBehaviour
{
    public SongChart chart;
    public LaneMap laneMap;
    public NoteObject notePrefab;

    public Transform hitLine;
    public float scrollSpeed = 10f;
    public float spawnAheadTime = 2.5f;

    private int nextIndex = 0;

    private void Update()
    {
        if (Conductor.I == null || chart == null) return;

        float t = Conductor.I.songTime;

        // spawn notes within lookahead window
        while (nextIndex < chart.notes.Count && chart.notes[nextIndex].startTime <= t + spawnAheadTime)
        {
            Spawn(chart.notes[nextIndex]);
            nextIndex++;
        }
    }

    private void Spawn(NoteData d)
    {
        var n = Instantiate(notePrefab, transform);
        n.Init(d, laneMap, hitLine, scrollSpeed);
    }
}
