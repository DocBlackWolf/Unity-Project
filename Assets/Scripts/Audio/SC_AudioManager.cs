using System;
using System.Collections.Generic;
using UnityEngine;

public class SoundHandler : MonoBehaviour
{
    [Serializable]
    public class SoundEvent
    {
        public string EventName; // The name of the event
        public AudioClip Clip;   // The audio clip to play
    }

    [Header("Sound Events")]
    [Tooltip("Add events here and associate each with an audio clip.")]
    public List<SoundEvent> SoundEvents = new List<SoundEvent>(); // Expandable list in the Inspector

    private Dictionary<string, AudioClip> soundDictionary;
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        // Create a dictionary for faster event lookups
        soundDictionary = new Dictionary<string, AudioClip>();
        foreach (var soundEvent in SoundEvents)
        {
            if (!string.IsNullOrEmpty(soundEvent.EventName) && soundEvent.Clip != null)
            {
                if (!soundDictionary.ContainsKey(soundEvent.EventName))
                {
                    soundDictionary.Add(soundEvent.EventName, soundEvent.Clip);
                }
                else
                {
                    Debug.LogWarning($"Duplicate EventName found: {soundEvent.EventName}. Only the first instance will be used.");
                }
            }
        }
    }

    /// <summary>
    /// Plays the sound associated with a given event.
    /// </summary>
    /// <param name="eventName">The name of the event to play.</param>
    public void PlaySound(string eventName)
    {
        if (soundDictionary.TryGetValue(eventName, out var clip))
        {
            audioSource.PlayOneShot(clip);
        }
        else
        {
            Debug.LogWarning($"Sound event '{eventName}' not found!");
        }
    }
}
