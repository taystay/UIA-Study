using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSInput : MonoBehaviour
{
    public float speed = 6.0f;
    public float grav = -9.8f;
    public const float baseSpeed = 6.0f;
    private CharacterController charController;

    void Start()
    {
        charController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        float dx = Input.GetAxis("Horizontal") * speed;
        float dz = Input.GetAxis("Vertical") * speed;
        Vector3 move = new Vector3(dx, 0, dz);
        move = Vector3.ClampMagnitude(move, speed);

        move.y = grav;

        move *= Time.deltaTime;
        move = transform.TransformDirection(move);
        charController.Move(move);
    }

    void OnEnable()
    {
        Messenger<float>.AddListener(GameEvent.SPEED_CHANGED, OnSpeedChange);
    }
    private void OnDisable()
    {
        Messenger<float>.RemoveListener(GameEvent.SPEED_CHANGED, OnSpeedChange);
    }

    private void OnSpeedChange(float value)
    {
        speed = baseSpeed * value;
    }
}
