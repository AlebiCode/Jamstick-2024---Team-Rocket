using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        var r = other.transform.GetComponent<Robot>();
        if (!r.enabled)
            return;
        r.Kill();
    }
}
