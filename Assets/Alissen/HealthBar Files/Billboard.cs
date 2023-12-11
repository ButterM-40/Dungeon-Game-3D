using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    public Transform cam;
    // Start is called before the first frame update
    public Camera _camera;

    // Update is called once per frame
    // void LateUpdate()
    // {
    //     transform.forward = _camera.transform.forward;
    //     transform.LookAt(transform.position+cam.forward);
    // }
}
