using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController instance;

    [SerializeField] private TerrainGenerator terrainGenerator;
    [SerializeField] private float targetX;
    [SerializeField] private float speed = 10;

    private void Awake()
    {
        if(instance != null)
            Destroy(instance);
        else
            instance = this;
    }

    void Update()
    {
        var x = Mathf.Lerp(transform.position.x, targetX, speed * Time.deltaTime);
        transform.position = new Vector3(x, transform.position.y, transform.position.z);
        terrainGenerator.RendererPositionUpdate(transform.position.x);
    }

    public void SetTargetX(float x)
    {
        if (x <= targetX)
            return;
        targetX = x;
    }

}
