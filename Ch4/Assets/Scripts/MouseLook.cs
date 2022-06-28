using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float sensH = 9.0f;
    public float sensV = 9.0f;

    public float minV = -45;
    public float maxV = 45;

    public float vertRot = 0;

    public enum RotationAxes
    {
        MouseXandY = 0,
        MouseX = 1,
        MouseY = 2
    }

    public RotationAxes axes = RotationAxes.MouseXandY;

    // Update is called once per frame
    void Update()
    {
        if(axes == RotationAxes.MouseX)
        {
            transform.Rotate(0, Input.GetAxis("Mouse X") * sensH, 0);
        } else if (axes == RotationAxes.MouseY)
        {
            vertRot -= Input.GetAxis("Mouse Y") * sensV;
            vertRot = Mathf.Clamp(vertRot, minV, maxV);

            float horizRot = transform.localEulerAngles.y;

            transform.localEulerAngles = new Vector3(vertRot, horizRot, 0);
        } else
        {
            vertRot -= Input.GetAxis("Mouse Y") * sensV;
            vertRot = Mathf.Clamp(vertRot, minV, maxV);

            float delta = Input.GetAxis("Mouse X") * sensH;
            float horizRot = transform.localEulerAngles.y + delta;

            transform.localEulerAngles = new Vector3(vertRot, horizRot, 0);
        }
    }
}
