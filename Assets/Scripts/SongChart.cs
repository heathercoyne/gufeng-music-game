using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Rhythm/Song Chart", fileName = "SongChart")]
public class SongChart : ScriptableObject
{
    public AudioClip audioClip;
    public double leadIn = 1.0;
    public List<NoteData> notes = new List<NoteData>();
}

[Serializable]
public class NoteData
{
    [Range(0, 6)] public int lane;
    [Min(0f)] public float startTime;
    [Min(0f)] public float duration;
}

