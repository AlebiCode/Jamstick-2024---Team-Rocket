using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class RagdollUser : MonoBehaviour
{
    [SerializeField] private Collider[] partColliders;
    [SerializeField] private Rigidbody[] rigidbodies;
    [SerializeField] private Rigidbody pushReciever;

    public void SetActivation(bool value)
    {
        for (int i = 0; i < partColliders.Length; i++)
        {
            rigidbodies[i].isKinematic = !value;
            partColliders[i].enabled = value;
        }
    }

    public void Impulse(Vector3 force)
    {
        pushReciever.AddForce(force, ForceMode.Impulse);
    }

}


[CustomEditor(typeof(RagdollUser))]
[CanEditMultipleObjects]
public class RagdollUser_Editor : Editor
{
    private RagdollUser Target => (RagdollUser)target;

    SerializedProperty colliders;
    SerializedProperty rigidbodies;

    private void OnEnable()
    {
        colliders = serializedObject.FindProperty("partColliders");
        rigidbodies = serializedObject.FindProperty("rigidbodies");
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        serializedObject.Update();
        GUILayout.Space(16);

        if (GUILayout.Button("Get Resources"))
        {
            var foundRbs = Target.GetComponentsInChildren<Rigidbody>();

            colliders.arraySize = foundRbs.Length;
            rigidbodies.arraySize = foundRbs.Length;
            for (int i = 0; i < colliders.arraySize; i++)
            {
                rigidbodies.GetArrayElementAtIndex(i).objectReferenceValue = foundRbs[i];
                colliders.GetArrayElementAtIndex(i).objectReferenceValue = foundRbs[i].GetComponent<Collider>();
            }
        }
        if (GUILayout.Button("Set Active"))
        {
            Activation(true);
        }
        if (GUILayout.Button("Set Unactive"))
        {
            Activation(false);
        }

        serializedObject.ApplyModifiedProperties();
    }

    private void Activation(bool val)
    {
        for (int i = 0; i < colliders.arraySize; i++)
        {
            ((Rigidbody)rigidbodies.GetArrayElementAtIndex(i).objectReferenceValue).isKinematic = !val;
            ((Collider)colliders.GetArrayElementAtIndex(i).objectReferenceValue).enabled = val;
        }
    }

}
