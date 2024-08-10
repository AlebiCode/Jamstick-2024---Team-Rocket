using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotPart : MonoBehaviour
{
    [SerializeField] private Collider coll;
    [SerializeField] private Rigidbody rb;

    public float Height => coll.bounds.size.y;
    public float HalfHeight => coll.bounds.extents.y;
    public Rigidbody Rigidbody => rb;
    public Collider Collider => coll;
}
