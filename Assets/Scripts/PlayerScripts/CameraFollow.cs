using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform playerTransform;
    public float smoothTime = 0.3f;

    private Vector3 velocity = Vector3.zero;
    [SerializeField]
    private const float ROTATION_X = 45f;
    [SerializeField]
    private const float HEIGHT_OFFSET = 15f;
    private float Z_OFFSET = -( HEIGHT_OFFSET / Mathf.Tan(Mathf.Deg2Rad * ROTATION_X) );

    void Start()
    {
        transform.eulerAngles.Set(ROTATION_X, 0f, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 targetPos = playerTransform.position + new Vector3(0f, HEIGHT_OFFSET, Z_OFFSET);
        transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref velocity, smoothTime);
    }
}
