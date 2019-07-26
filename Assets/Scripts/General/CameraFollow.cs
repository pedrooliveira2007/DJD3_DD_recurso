using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    private Transform player;

    [SerializeField]
    private Camera cam;

    [SerializeField]
    private Vector3 offsetPosition;

    [SerializeField]
    private Space offsetPositionSpace = Space.Self;

    [SerializeField]
    private bool lookAt = true;


    void Start()
    {
        cam = Camera.main;
    }

    private void Update()
    {
        Refresh();
    }

    public void Refresh()
    {
        // compute position
        if (offsetPositionSpace == Space.Self)
        {
            transform.position = player.TransformPoint(offsetPosition);
        }
        else
        {
            transform.position = player.position + offsetPosition;
        }

        // compute rotation
        if (lookAt)
        {
            cam.transform.LookAt(player);
        }
        else
        {
            transform.rotation = player.rotation;
        }
    }
}