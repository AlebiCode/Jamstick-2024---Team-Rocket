using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeChanger : MonoBehaviour
{
    [field: SerializeField] public AudioBuses BusToSet {  get; private set; }

    public void SetVolume(Single value) 
    { 
        AudioManager.Instance.SetVolume(BusToSet, value);
    }
}
