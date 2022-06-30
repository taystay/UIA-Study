using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitCamera : MonoBehaviour
{
    [SerializeField] Transform target;

    public float keyRotSpeed = 0.5f;
    public float mouseRotSpeed = 1.5f;

    private float rotY;
    private Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        rotY = transform.eulerAngles.y;
        offset = target.position - transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        float horInput = Input.GetAxis("Horizontal");
        if(!Mathf.Approximately(horInput, 0))
        {
            rotY += horInput * keyRotSpeed;        
        } 
        rotY += Input.GetAxis("Mouse X") * mouseRotSpeed * 3;

        Quaternion rotation = Quaternion.Euler(0, rotY, 0);
        transform.position = target.position - (rotation * offset);
        transform.LookAt(target);
    }
}
