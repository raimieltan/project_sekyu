using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // Singleton instance
    public static AudioManager instance;

    // Audio clips
    public AudioClip[] backgroundSounds;

    public AudioClip victorySound;

    public AudioClip defeatSound;

    public AudioClip drawSound;

    // Audio source
    private AudioSource audioSource;

    private void Awake()
    {
        // Check if instance already exists
        if (instance == null)
        {
            // If not, set instance to this
            instance = this;
        }
        else if (instance != this)
        {
            // If instance already exists and it's not this, destroy this instance
            Destroy (gameObject);
        }

        // Don't destroy the AudioManager GameObject when a new scene is loaded
        DontDestroyOnLoad (gameObject);

        // Get the AudioSource component
        audioSource = GetComponent<AudioSource>();

        PlayBackgroundSound(0);
    }

    public void PlayBackgroundSound(int index)
    {
        if (index < 0 || index >= backgroundSounds.Length)
        {
            Debug.LogWarning("Invalid background sound index!");
            return;
        }

        // Set the audio clip and play it
        audioSource.clip = backgroundSounds[index];
        audioSource.Play();
    }

    public void PlayVictorySound()
    {
        // Set the audio clip and play it
        audioSource.clip = victorySound;
        audioSource.Play();
    }

    public void PlayDefeatSound()
    {
        // Set the audio clip and play it
        audioSource.clip = defeatSound;
        audioSource.Play();
    }

    public void PlayDrawSound()
    {
        // Set the audio clip and play it
        audioSource.clip = drawSound;
        audioSource.Play();
    }
}
