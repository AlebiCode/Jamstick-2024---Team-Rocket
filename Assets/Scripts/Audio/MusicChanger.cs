using FMOD.Studio;
using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicChanger : MonoBehaviour
{
    [field: SerializeField] public EventReference MusicToPlay { get; private set; }

    public void ChangeMusic() 
    { 
        AudioManager.Instance.SetMusic(MusicToPlay);
    }
}
