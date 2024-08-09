using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController instance;

    [SerializeField] private GameObject fabbricaMobile;
    [SerializeField] private TerrainGenerator terrainGenerator;
    [SerializeField] private float targetX;
    [SerializeField] private float speed = 10;

    private float fabbricaToCameraOffet;

    private void Awake()
    {
        if(instance != null)
            Destroy(instance);
        else
            instance = this;
    }

    private void Start()
    {
        fabbricaToCameraOffet = transform.position.x - fabbricaMobile.transform.position.x;
    }

    void Update()
    {
        var x = Mathf.Lerp(transform.position.x, targetX, speed * Time.deltaTime);
        transform.position = new Vector3(x, transform.position.y, transform.position.z);
        fabbricaMobile.transform.position = new Vector3(transform.position.x - fabbricaToCameraOffet, fabbricaMobile.transform.position.y, fabbricaMobile.transform.position.z);
        terrainGenerator.RendererPositionUpdate(transform.position.x);
    }

    public void SetTargetX(float x)
    {
        if (x <= targetX)
            return;
        targetX = x;
    }

}
