using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeviceOperator : MonoBehaviour
{
    public float radius = 1.5f;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E)) {
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius);
            foreach(Collider hitCollider in hitColliders) {
                Vector3 hitPosition = hitCollider.transform.position;
                hitPosition.y = transform.position.y;

                Vector3 direction = hitPosition - transform.position;
                if(Vector3.Dot(transform.forward, direction.normalized) > 0.5f) {
                    hitCollider.SendMessage("Operate", SendMessageOptions.DontRequireReceiver);
                }
            }
        }
    }
}
