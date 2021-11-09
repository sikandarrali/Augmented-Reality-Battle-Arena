using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject enemyCharacter;
    public Transform[] enemySpawnPoints;
    int randomNumber;
    Vector3 randomSpawnPosition;

    AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (SwordColiisionManager.dead == true)
        {
            randomNumber = Random.Range(0, enemySpawnPoints.Length - 1);
            randomSpawnPosition = enemySpawnPoints[randomNumber].position;

            // Enemy Spawn after delay
            StartCoroutine("SpawnAfterDelay");
            SwordColiisionManager.dead = false;

        }
    }

    IEnumerator SpawnAfterDelay()
    {
        yield return new WaitForSeconds(2f);
        audioSource.Play(); //respawn sound
        GameObject enemySpawned = Instantiate(enemyCharacter, randomSpawnPosition, Quaternion.identity);
    }

}
