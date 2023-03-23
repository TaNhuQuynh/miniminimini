using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] public float speed;

    void LateUpdate()
    {
        var pos = target.position;
        pos.z = transform.position.z;
        transform.position = Vector3.Slerp(transform.position, pos, speed);
    }
}
