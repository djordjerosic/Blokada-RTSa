using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatSpawnerScript : MonoBehaviour
{
    [SerializeField] private GameObject ratPrefab;
    [SerializeField] private GameObject pigPrefab;
    [SerializeField] private GameObject warningPrefab;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private float spawnInterval = 2f;

    private float timer = 0f;
    bool spawnedFirstWave = false;
    bool spawnedSecondWave = false;
    bool spawnedThirdWave = false;
    bool spawnedFourthWave = false;

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            SpawnRat(getRandomScreenEdgeCoordinates());
            timer = 0f;

            if (ScoreManagerScript.Instance.Propaganda <= 80 && !spawnedFirstWave)
            {
                StartCoroutine(SpawnWave());
                spawnedFirstWave = true;
            }
            if (ScoreManagerScript.Instance.Propaganda <= 60 && !spawnedSecondWave)
            {
                StartCoroutine(SpawnWave());
                spawnedSecondWave = true;
            }
            if (ScoreManagerScript.Instance.Propaganda <= 40 && !spawnedThirdWave)
            {
                StartCoroutine(SpawnWave()); 
                spawnedThirdWave = true;
            }
            if (ScoreManagerScript.Instance.Propaganda <= 20 && !spawnedFourthWave)
            {
                StartCoroutine(SpawnWave());
                spawnedFourthWave = true;
            }
        }
    }

    private System.Collections.IEnumerator SpawnWave()
    {
        Vector3 waveLocation = getRandomScreenEdgeCoordinates();
        Instantiate(warningPrefab, waveLocation * 0.8f, Quaternion.identity);

        yield return new WaitForSeconds(7.0f);

        for (int i = 0; i < 3; i++)
            for (int j = 0; j < 3; j++)
                SpawnRat(waveLocation + new Vector3(i - 1, j - 1, 0));

        for (int i = 0; i < 5; i ++)
            SpawnPig(waveLocation + new Vector3(i - 2, -2, 0));
        for (int i = 0; i < 5; i++)
            SpawnPig(waveLocation + new Vector3(i - 2, 2, 0));
        for (int i = 0; i < 3; i++)
            SpawnPig(waveLocation + new Vector3(2, i-1, 0));
        for (int i = 0; i < 3; i++)
            SpawnPig(waveLocation + new Vector3(-2, i-1, 0));

    }

   
    void SpawnRat(Vector3 position)
    {
        Instantiate(ratPrefab, position, Quaternion.identity);
    }

    void SpawnPig(Vector3 position)
    {
        Instantiate(pigPrefab, position, Quaternion.identity);
    }

    private Vector3 getRandomScreenEdgeCoordinates()
    {
        int edge = UnityEngine.Random.Range(0, 6);
        Vector2 spawnPos = Vector2.zero;

        float camHeight = 2f * mainCamera.orthographicSize;
        float camWidth = camHeight * mainCamera.aspect;

        Vector2 camCenter = mainCamera.transform.position;

        switch (edge)
        {
            case 0: // Top
                spawnPos = new Vector2(
                    UnityEngine.Random.Range(-camWidth / 2f, camWidth / 2f),
                    camHeight / 2f
                );
                break;
            case 4: // Top
                spawnPos = new Vector2(
                    UnityEngine.Random.Range(-camWidth / 2f, camWidth / 2f),
                    camHeight / 2f
                );
                break;
            case 1: // Right
                spawnPos = new Vector2(
                    camWidth / 2f,
                    UnityEngine.Random.Range(-camHeight / 2f, camHeight / 2f)
                );
                break;
            case 2: // Bottom
                spawnPos = new Vector2(
                    UnityEngine.Random.Range(-camWidth / 2f, camWidth / 2f),
                    -camHeight / 2f
                );
                break;
            case 5: // Bottom
                spawnPos = new Vector2(
                    UnityEngine.Random.Range(-camWidth / 2f, camWidth / 2f),
                    -camHeight / 2f
                );
                break;
            case 3: // Left
                spawnPos = new Vector2(
                    -camWidth / 2f,
                    UnityEngine.Random.Range(-camHeight / 2f, camHeight / 2f)
                );
                break;
        }

        return new Vector3(spawnPos.x, spawnPos.y, 0f);
    }

}
