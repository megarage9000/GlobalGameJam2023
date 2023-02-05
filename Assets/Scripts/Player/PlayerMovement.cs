using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float PlayerSpeed = 5f;
    public float MouseSpeed = 2f;
    private CharacterController characterController;
    private Plane plane;

    RaycastHit groundHit;
    float distanceFromGround;
    public GameObject RayTracker;
       
           
    void Start()
    {
        plane = new Plane(Vector3.up, Vector3.zero);
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        // Simulate gravity
        Ray playerRay = new Ray(transform.position, Vector3.down);
        if (Physics.Raycast(playerRay, out groundHit))
        {
            distanceFromGround = groundHit.distance; 
        }
        else
        {
            distanceFromGround = 0;
        }
        
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), -distanceFromGround, Input.GetAxis("Vertical"));
        characterController.Move(move * Time.deltaTime * PlayerSpeed);

        // rotate character with mouse cursor
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (plane.Raycast(ray, out float enter))
        {
            Vector3 worldPosition = ray.GetPoint(enter);
            Vector3 playerPosition = plane.ClosestPointOnPlane(transform.position);
            transform.rotation = Quaternion.LookRotation(worldPosition - playerPosition);
            RayTracker.transform.position = worldPosition;
        }
    }
}
