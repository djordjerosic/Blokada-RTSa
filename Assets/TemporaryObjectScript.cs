using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TemporaryObjectScript : MonoBehaviour
{
    private float spawnTime;
    [SerializeField] private float duration = 4;
    void Start()
    {
        spawnTime = Time.realtimeSinceStartup;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.realtimeSinceStartup > spawnTime + duration)
            GameObject.Destroy(this.gameObject);
    }
}
