using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayAndNight : MonoBehaviour
{
    [SerializeField] private Light mainLight;
    [SerializeField] private float speed = 5;
    [SerializeField] private float nightTimeSpeedMult = 3;
    [SerializeField] private float minTemp = 2000;
    [SerializeField] private float maxTemp = 4000;

    private float CurrentRot => transform.rotation.eulerAngles.x;
    private float CurrentRotPerc => 1 - Mathf.Sin(Mathf.Abs(90 - CurrentRot) * Mathf.Deg2Rad);

    float delta;
    private void Update()
    {
        if (CurrentRot > 0 && CurrentRot < 180)
        {
            delta = speed * Time.deltaTime;
            mainLight.colorTemperature = minTemp + CurrentRotPerc * (maxTemp - minTemp);
        }
        else
        {
            delta = speed * nightTimeSpeedMult * Time.deltaTime;
            mainLight.colorTemperature = minTemp;
        }
        transform.Rotate(Vector3.right, -delta);
    }

}
