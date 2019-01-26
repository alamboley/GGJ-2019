using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public enum AudioTrack
{
    ARP1,
    ARP2,
    BASS,
    KICK,
    HHAT
}

[Serializable]
public class AudioSourceRef
{
    public AudioSource source;
    public AudioTrack track;
    public int beatDelay = 0;
}

public class AudioManager : MonoBehaviour
{

    public List<AudioSourceRef> sources = new List<AudioSourceRef>();

    float bpm = 127f;
    double beattime;
    double startTime;
    int beat = 0;

    public UnityEvent OnBeat;

    public void ToggleMute(AudioTrack track)
    {
        foreach(AudioSourceRef sr in sources)
        {
            if (sr.track == track)
                sr.source.mute = !sr.source.mute;
        }
    }

    private void Awake()
    {
       
        beattime = 60.0 / (double)bpm;

        PlayAll();

        lastDSPTime = startTime - 4 * beattime;
    }

    double lastDSPTime;

    private void Update()
    {
        if( AudioSettings.dspTime - lastDSPTime > beattime) {
            lastDSPTime = AudioSettings.dspTime;

            if (OnBeat != null)
                OnBeat.Invoke();

            beat++;

            ProcessTracks();
        }

    }


    void ProcessTracks()
    {
        foreach (AudioSourceRef sr in sources)
        {
            if (beat > sr.beatDelay && sr.source.mute)
                sr.source.mute = false;
        }
    }


    void PlayAll()
    {
        startTime = AudioSettings.dspTime + 4 * beattime;
        foreach(AudioSourceRef sr in sources)
        {
            sr.source.mute = true;
            sr.source.PlayScheduled(startTime);
        }
    }
}
