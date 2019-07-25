using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLock : MonoBehaviour
{

    Vector3 StartAngle;

    // Start is called before the first frame update
    void Start()
    {
        StartAngle = transform.eulerAngles;
    }

    // Update is called once per frame
    void Update()
    {
        transform.eulerAngles = StartAngle;
    }
}
