using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnAsteroid : MonoBehaviour
{
    #region variables
    // Editable from Editor
    [SerializeField] private AsteroidMovement _asteroidMovement;
    [SerializeField] private float spawnFrequency = 2;

    // Private 
    private float spawnDistance;
    #endregion

    #region start/update/physics
    private void Start()
    {
        spawnDistance = Screen.width / 100;
        InvokeRepeating("SpawnElement", 0, spawnFrequency);
    }

    #endregion

    #region privateFuntions
    void SpawnElement()
    {
        Vector2 spawnPoint = Random.insideUnitCircle.normalized * spawnDistance;

        float angle = Random.Range(-15f, 15f);
        Quaternion rotation = Quaternion.AngleAxis(angle, new Vector3(0, 0, 1));
        AsteroidMovement astroid = Instantiate(_asteroidMovement, spawnPoint, rotation);

        // direction and size
        Vector2 direction = rotation * -spawnPoint;
        // 1.5, 1, 0.5;
        //float mass = Random.Range(0.8f, 1.4f);
        astroid.kick(1.5f, direction);
    }
    #endregion
}
