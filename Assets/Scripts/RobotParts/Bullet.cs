using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private AttacksKeys attackKey;
    [SerializeField] private float speed;
    // private IController controller
    private void Update()
    {
        transform.position += Vector3.right * Time.deltaTime * speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        //GetController(/*other*/);


    }

    private void GetController(/*Collider other*/) 
    { 
        //if(controller == null)
        //  controller = Collider.gameObject.GetComponent<IController>();
    }
}
