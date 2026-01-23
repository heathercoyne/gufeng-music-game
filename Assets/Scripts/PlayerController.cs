using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public LaneMap laneMap;

    public KeyCode[] laneKeys = new KeyCode[7]
    {
        KeyCode.Z, KeyCode.X, KeyCode.A, KeyCode.S, KeyCode.D, KeyCode.W, KeyCode.E
    };

    [Header("Rest position (when no key held)")]
    [Tooltip("How far below Lane0 the player rests (world units).")]
    public float restOffsetBelowLane0 = 0.6f;

    private float restY;

    private void Start()
    {
        if (laneMap == null) return;

        // Compute rest position below Lane0 once
        restY = laneMap.GetLaneY(0) - restOffsetBelowLane0;

        // Start at rest (very bottom)
        SetY(restY);
    }

    // choose "topmost held" so if multiple keys pressed, higher lane wins
    private int GetHeldLane()
    {
        for (int lane = 6; lane >= 0; lane--)
            if (Input.GetKey(laneKeys[lane])) return lane;
        return -1;
    }

    private void Update()
    {
        if (laneMap == null) return;

        int held = GetHeldLane();

        // If nothing pressed -> go to rest (below Lane0)
        if (held < 0)
        {
            SetY(restY);
            return;
        }

        // Otherwise snap to the held lane
        float y = laneMap.GetLaneY(held);
        SetY(y);
    }

    private void SetY(float y)
    {
        var p = transform.position;
        transform.position = new Vector3(p.x, y, p.z);
    }
}
