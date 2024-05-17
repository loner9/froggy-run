using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnityEngine;
using Random = UnityEngine.Random;

public class ObstacleSpawner : MonoBehaviour
{
    // public GameObject obstaclePrefabs;
    [SerializeField] private GameObject[] _obstaclePrefabs;
    [SerializeField] private float[] obstacleWeight;
    private Vector2 screenBounds;
    private float respawnTime;
    private Camera _cam;
    private float startTime;
    private int randomPrefabs;
    private Dictionary<GameObject, float> _obstacleWeights;
    float currentAmount = 0.1f;
    float targetAmount = 100.0f;
    float growthRate = 0.3f;
    int timeCount = 0;

    private obstacle _obs;

    private void Awake()
    {
        respawnTime = 1.4f;
        _cam = Camera.main;
        _obstacleWeights = new Dictionary<GameObject, float>();

        for (int i = 0; i < _obstaclePrefabs.Length; i++)
        {
            _obstacleWeights.Add(_obstaclePrefabs[i], obstacleWeight[i]);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        startTime = Time.time;
        // valueOverTime();
        StartCoroutine(obstacleWave());
    }

    private void spawnObstacle()
    {
        // randomPrefabs = Random.Range(0, _obstaclePrefabs.Length);
        GameObject chosenPrefab = GetWeightedRandomValue(_obstacleWeights);
        // GameObject a = Instantiate(_obstaclePrefabs[randomPrefabs]) as GameObject;
        GameObject a = Instantiate(chosenPrefab) as GameObject;
        _obs = a.GetComponent<obstacle>();
        float yPos = this.transform.position.y;
        float xPos = this.transform.position.x;
        float sawPos = yPos - 0.27f;
        if (a.name.Contains("Saw"))
        {
            a.transform.position = new Vector2(xPos, sawPos);
        }
        else
        {
            a.transform.position = new Vector2(xPos, yPos);
        }
    }

    IEnumerator obstacleWave()
    {
        while (true)
        {
            yield return new WaitForSeconds(respawnTime);
            if (timeCount == 1)
            {
                // Debug.Log("speed 1");
                respawnTime = Random.Range(1.0f, 1.6f);
            }
            else if (timeCount == 2)
            {
                // Debug.Log("speed 2");
                respawnTime = Random.Range(0.6f, 1.3f);
            }
            else
            {
                // Debug.Log("speed default");
                respawnTime = Random.Range(1.0f, 1.8f);

            }
            spawnObstacle();
            float increase = GetExponentialIncrease(currentAmount, targetAmount, growthRate);
            if (timeCount < 2)
            {
                if (currentAmount < targetAmount)
                {
                    currentAmount += increase;
                }
                else
                {
                    currentAmount = 0.1f;
                    timeCount++;
                }
            }

            // Debug.Log(currentAmount);
            if (HasChanceToOccur())
            {
                yield return new WaitForSeconds(0.2f);
                spawnObstacle();
            }
        }
    }
    // void Start()
    // {
    //     startTime = Time.time;
    // }

    private static T GetWeightedRandomValue<T>(Dictionary<T, float> options)
    {
        if (options == null || options.Count == 0)
        {
            throw new ArgumentNullException(nameof(options), "Options dictionary cannot be null or empty.");
        }

        var totalWeight = options.Values.Sum();
        var randomValue = Random.value * totalWeight;

        foreach (var option in options)
        {
            randomValue -= option.Value;
            if (randomValue <= 0)
            {
                return option.Key;
            }
        }

        // Should not reach here, but throw an exception just in case
        throw new InvalidOperationException("Failed to select a random value from options.");
    }

    public static bool HasChanceToOccur(double probability = 0.15)
    {
        if (probability < 0 || probability > 1)
        {
            throw new ArgumentOutOfRangeException(nameof(probability), "Probability must be between 0 and 1.");
        }

        return Random.value <= probability;
    }

    public static float GetExponentialIncrease(float current, float target, float growthRate)
    {
        if (current >= target)
        {
            return target;
        }

        return target * (1 - Mathf.Pow(1 - growthRate, current / target));
    }
}
