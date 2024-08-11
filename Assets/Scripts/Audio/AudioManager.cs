using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;
using UnityEngine.Rendering;

public enum AudioBuses { MASTER, SFX, MUSIC};
public class AudioManager : MonoBehaviour
{
    private List<EventInstance> eventInstances = new();
    private EventInstance music;

    private Dictionary<AudioBuses, string> audioBuses = new()
    {
        { AudioBuses.MASTER, "bus:/Master" },
        { AudioBuses.SFX, "bus:/Master/SFX" },
        { AudioBuses.MUSIC, "bus:/Master/Music" }
    };

    public static AudioManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }

        else
        {
            Instance = this;
        }
        DontDestroyOnLoad(gameObject);
        Init();
    }

    private void OnDestroy()
    {
        CleanUp();
    }

    private void Init()
    {
        SetMusic(FmodEvents.Instance.MainMenuMusic);
        SetVolume(AudioBuses.MASTER, 1);
        SetVolume(AudioBuses.SFX, 1);
        SetVolume(AudioBuses.MUSIC, 1);
    }

    private void CleanUp() 
    {
        foreach (EventInstance instance in eventInstances)
        {
            instance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            instance.release();
        }
    }

    public EventInstance CreateInstance(EventReference sound)
    {
        EventInstance instance = RuntimeManager.CreateInstance(sound);
        eventInstances.Add(instance);
        return instance;
    }

    public void SetMusic(EventReference sound) 
    {
        if (music.isValid()) 
        {
            music.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            music.release();
        }

        music = CreateInstance(sound);
        music.start();
    }

    public void SetVolume(AudioBuses busToSet, float volumeToSet) 
    { 
        Bus masterBus = RuntimeManager.GetBus(audioBuses[busToSet]);
        masterBus.setVolume(volumeToSet);
    }
}
