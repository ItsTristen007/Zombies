using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] GameObject camera;
    [SerializeField] Vector3 offset = Vector3.zero;

    void Update()
    {
        camera.transform.position = transform.position + offset;
    }
}
