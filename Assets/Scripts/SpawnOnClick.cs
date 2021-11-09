using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnOnClick : MonoBehaviour
{
    [SerializeField]
    public GameObject arthurOBJ, arenaOBJ, spawnPoint;
    public GameObject GameControlsContainer;
    public GameObject panelAlerts;
    public GameObject btnSpawnCharacter;


    private void Start()
    {
        GameControlsContainer.SetActive(false);
        //btnSpawnCharacter.SetActive(false);
    }

    public void OnClickSpawnPlayer()
    {
        GameObject arthurSpawned = Instantiate(arthurOBJ, spawnPoint.transform.position, Quaternion.identity);
        arthurSpawned.transform.parent = arenaOBJ.transform;

        GameControlsContainer.SetActive(true);
        panelAlerts.SetActive(false);
        btnSpawnCharacter.SetActive(false);
    }

}
