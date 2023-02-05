using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    private Transform Cam;

    void LateUpdate()
    {
        Cam = GameObject.FindGameObjectWithTag("MainCamera").transform;
        transform.LookAt(transform.position + Cam.forward);
    }
}
