using UnityEngine;

public class Conductor : MonoBehaviour
{
    public static Conductor I { get; private set; }

    public SongChart chart;
    public AudioSource audioSource;

    public float songTime { get; private set; }
    public double dspSongStart { get; private set; }

    private bool started;

    private void Awake()
    {
        I = this;
    }

    private void Start()
    {
        Play();
    }

    public void Play()
    {
        if (chart == null || chart.audioClip == null)
        {
            Debug.LogError("Conductor: chart/audioClip missing.");
            return;
        }

        audioSource.clip = chart.audioClip;

        double dspNow = AudioSettings.dspTime;
        dspSongStart = dspNow + chart.leadIn;

        audioSource.PlayScheduled(dspSongStart);
        started = true;
    }

    private void Update()
    {
        if (!started) return;
        songTime = (float)(AudioSettings.dspTime - dspSongStart);
    }

    public bool SongEnded()
    {
        if (!started) return false;
        return songTime > chart.audioClip.length + 0.05f;
    }
}
