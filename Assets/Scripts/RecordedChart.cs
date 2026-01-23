using System;
using System.Collections.Generic;

[Serializable]
public class RecordedChart
{
    public string songId = "song1";
    public List<RecordedNote> notes = new List<RecordedNote>();
}

[Serializable]
public class RecordedNote
{
    public int lane;          // 0..6
    public float startTime;   // seconds
    public float duration;    // seconds
}
