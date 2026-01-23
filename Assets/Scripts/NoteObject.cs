using UnityEngine;

public class NoteObject : MonoBehaviour
{
    public NoteData data;
    public LaneMap laneMap;

    public Transform hitLine;
    public float scrollSpeed = 10f;

    public void Init(NoteData d, LaneMap map, Transform hit, float speed)
    {
        data = d;
        laneMap = map;
        hitLine = hit;
        scrollSpeed = speed;
    }

    public float StartTime => data.startTime;
    public float EndTime => data.startTime + data.duration;

    private void Update()
    {
        if (Conductor.I == null || laneMap == null || hitLine == null) return;

        float t = Conductor.I.songTime;

        // time until hit
        float dt = data.startTime - t;

        // position by time
        float x = hitLine.position.x + dt * scrollSpeed;
        float y = laneMap.GetLaneY(data.lane);

        transform.position = new Vector3(x, y, 0);

        // length encodes duration (bar)
        float len = Mathf.Max(0.2f, data.duration * scrollSpeed);
        var s = transform.localScale;
        s.x = len;
        transform.localScale = s;

        // destroy after passing
        if (t > EndTime + 1.0f) Destroy(gameObject);
    }
}
