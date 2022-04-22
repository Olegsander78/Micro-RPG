using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject Target;
    

    private void Update()
    {
        transform.position = new Vector3(Target.transform.position.x, Target.transform.position.y, -10);
        //transform.LookAt(Target.transform);
    }
}
