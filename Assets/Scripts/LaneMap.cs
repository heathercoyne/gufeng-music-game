using UnityEngine;

public class LaneMap : MonoBehaviour
{
    [Tooltip("Assign 7 lane marker transforms: Lane0..Lane6")]
    public Transform[] lanes = new Transform[7];

    public float GetLaneY(int lane)
    {
        lane = Mathf.Clamp(lane, 0, lanes.Length - 1);
        return lanes[lane].position.y;
    }
}
