using UnityEngine;
using UnityEngine.Serialization;

public class Music : MonoBehaviour
{
    [SerializeField] private AudioClip introAudioClip;
    [SerializeField] private AudioClip loopingAudioClip;
    [SerializeField] private float volume = 0.3f;
    [FormerlySerializedAs("playOnStart")] [SerializeField] private bool playOnAwake = true;

    private AudioSource introSource;
    private AudioSource loopingSource;
    
    private void Awake()
    {
        Initialize();

        if (playOnAwake)
        {
            Play();
        }
    }

    private void Initialize()
    {
        // Skip if initialized.
        if (introSource != null) return;
        
        // Initialize.
        introSource = gameObject.AddComponent<AudioSource>();
        introSource.clip = introAudioClip;
        introSource.volume = volume;
        
        loopingSource = gameObject.AddComponent<AudioSource>();
        loopingSource.clip = loopingAudioClip;
        loopingSource.loop = true;
        loopingSource.volume = volume;
    }

    public void Play()
    {
        // Ensure initialized.
        Initialize();
        
        // Skip if already playing.
        if (introSource.isPlaying || loopingSource.isPlaying) return;
        
        // Play only the intro if no loop clip is provided.
        if (introAudioClip != null && loopingAudioClip == null)
        {
            introSource.Play();
        }
        // Play only the loop if no intro clip is provided.
        else if (introAudioClip == null && loopingAudioClip != null)
        {
            loopingSource.Play();
        }
        // Play the intro and schedule the loop to be played after the intro.
        else
        {
            var introTime = AudioSettings.dspTime;
            var loopTime = introTime + introAudioClip.length;

            introSource.PlayScheduled(introTime);
            loopingSource.PlayScheduled(loopTime);
        }
    }
    
    public void Stop()
    {
        // Ensure initialized.
        Initialize();
        
        // Stop all audio sources.
        introSource.Stop();
        loopingSource.Stop();
    }
}
