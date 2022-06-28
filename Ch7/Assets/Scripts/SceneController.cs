using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour
{
    [SerializeField] GameObject enemyPrefab;

    private GameObject enemy;
    private float speed = 3.0f;
    private float baseSpeed = 3.0f;

    // Update is called once per frame
    void Update()
    {
        if (enemy == null)
        {
            enemy = Instantiate(enemyPrefab) as GameObject;
            enemy.transform.position = new Vector3(0, 1, 7);
            float angle = Random.Range(0, 360);
            enemy.transform.Rotate(0, angle, 0);
            WanderingAI ai = enemy.GetComponent<WanderingAI>();
            ai.SetSpeed(speed);
        }    
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
