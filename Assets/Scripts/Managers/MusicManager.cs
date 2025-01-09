using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [SerializeField] private AudioClip[] musicClips;
    // Need to have 2 audio sources to swap between, so we don't try to schedule music
    // on a source that is currently playing music
    [SerializeField] private AudioSource[] audioSources;
    private int audioToggle = 0; // toggle 0/1

    public static MusicManager instance;
    private int currentClipIndex = 0;
    private double nextClipStartTime = 0;

    private void Awake()
    {
        /*
         * The basic idea of this music manager is to start music clip 1 on initial start and
         * queue music clip 2 to play right when clip 1 ends.
         */
        if (instance == null)
        {
            // Created music manager for the first time
            instance = this;
            currentClipIndex = Random.Range(0, musicClips.Length);
            DontDestroyOnLoad(gameObject);

            // Start playing the first clip in 0.5 seconds
            nextClipStartTime = AudioSettings.dspTime + 0.5;
            AudioClip firstClip = musicClips[currentClipIndex];
            audioSources[audioToggle].clip = firstClip;
            audioSources[audioToggle].PlayScheduled(nextClipStartTime);

            // Queue the next clip
            audioToggle = 1 - audioToggle;
            // UpdateNextClipStartTime(firstClip); // TODO: delete?
            ScheduleNextClip();
        }
    }

    private void Update()
    {
        if (AudioSettings.dspTime > instance.nextClipStartTime)
        {
            // Just started the "next clip" that was previously scheduled. Update the current clip index
            // to reflect, and schedule the clip after it to start playing when this one ends.
            instance.currentClipIndex = GetNextClipIndex();
            ScheduleNextClip();
        }
    }

    // Strategy for music clips based on https://www.youtube.com/watch?v=3yKcrig3bU0
    private void ScheduleNextClip()
    {
        // schedule next clip start time based on currently playing clip
        AudioClip currClip = instance.musicClips[instance.currentClipIndex];
        UpdateNextClipStartTime(currClip);

        // Get the next clip to play and schedule it for the new time
        int nextClipIndex = GetNextClipIndex();
        AudioClip nextClip = instance.musicClips[nextClipIndex];
        instance.audioSources[instance.audioToggle].clip = nextClip;
        instance.audioSources[instance.audioToggle].PlayScheduled(instance.nextClipStartTime);

        // update which audio source to play out of next
        instance.audioToggle = 1 - instance.audioToggle;
    }

    private int GetNextClipIndex() => (instance.currentClipIndex + 1) % instance.musicClips.Length;

    private double GetMusicDuration(AudioClip clip) => (double)clip.samples / clip.frequency;

    private void UpdateNextClipStartTime(AudioClip currentClip)
    {
        // Updates the time to start the next clip at
        instance.nextClipStartTime += GetMusicDuration(currentClip);
    }
}
