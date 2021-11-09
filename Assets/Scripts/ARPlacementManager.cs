using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.AI;

public class ARPlacementManager : MonoBehaviour
{

    ARRaycastManager m_ArraycastManager;
    static List<ARRaycastHit> raycastHits = new List<ARRaycastHit>();

    ARPlaneManager _arPlaneManager;
    ARPlacementManager _arPlacementManager;

    public Camera arCamera;
    
    public GameObject GameControlsContainer;
    public GameObject playerCharacter, initialPlayerSpawnPoint, enemyCharacter, initialEnemySpawnPoint, btnSpawnCharacter;

    public GameObject arenaGameObject, btnConfirmPlaceArena, btnConfirmPlaceArena2, btnReArrangeArena, sliderScaleArena, sliderRotateArena, btnStartGame;

    public GameObject txtScore, txtLives;

    public GameObject leftJoyStick;

    public GameObject panelAlerts;
    public TextMeshProUGUI txtAlert;

    public NavMeshSurface navMeshSurface;

    private void Awake()
    {
        _arPlaneManager = GetComponent<ARPlaneManager>();
        _arPlacementManager = GetComponent<ARPlacementManager>();
    }


    // Start is called before the first frame update
    void Start()
    {
        leftJoyStick.SetActive(false);

        txtLives.SetActive(false);
        txtScore.SetActive(false);

        sliderScaleArena.GetComponent<Slider>().value = 2.5f;

        playerCharacter.SetActive(false);
        arenaGameObject.SetActive(false);
        GameControlsContainer.SetActive(false);

        sliderScaleArena.SetActive(false);
        sliderRotateArena.SetActive(false);
        btnConfirmPlaceArena.SetActive(false);
        btnConfirmPlaceArena2.SetActive(false);
        m_ArraycastManager = GetComponent<ARRaycastManager>();

        btnReArrangeArena.SetActive(false);
        btnSpawnCharacter.SetActive(false);

        panelAlerts.SetActive(true);
        txtAlert.text = "Scanning the area... \n\n Move your Camera around to Track flat surfaces. \n\n Please be patient, it can take some time depending on your Camera Quality, Environment Lighting and Conditions";

        btnStartGame.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        PlaceArenaInCenter();
    }

    // NavMesh Baking at Runtime
    public void BakeNavMesh()
    {
        navMeshSurface.BuildNavMesh();
    }

    // Placing Arena in Center of Screen After Tracking
    public void PlaceArenaInCenter()
    {
        Vector3 centerOfScreen = new Vector3(Screen.width / 2, Screen.height / 2);
        Ray ray = arCamera.ScreenPointToRay(centerOfScreen);

        //Check The First Visible Plane
        if (raycastHits.Count > 0)
        {
            txtAlert.text = "Tracking Complete! \nYou can place The Arena now.\n\n (If you're not satisfied with location, move around your camera and find new place)";
            arenaGameObject.SetActive(true);
            sliderScaleArena.SetActive(true);
            sliderRotateArena.SetActive(true);
            btnConfirmPlaceArena.SetActive(true);
            panelAlerts.SetActive(false);
            raycastHits.Clear();
        }

        if (m_ArraycastManager.Raycast(ray, raycastHits, TrackableType.PlaneWithinPolygon))
        {
            Pose hitPose = raycastHits[0].pose;
            Vector3 positionToBePlaced = hitPose.position;
            arenaGameObject.transform.position = positionToBePlaced;
        }
    }

    // Confirm Arena Placement
    public void OnClick_ConfirmArenaPlacement(bool check)
    {
        if(check == true) // Arena Placed First Time
        {
            btnSpawnCharacter.SetActive(true);
            btnStartGame.SetActive(true);
            btnConfirmPlaceArena.SetActive(false);
            btnConfirmPlaceArena2.SetActive(false);
        }
        else
        {
            btnSpawnCharacter.SetActive(false);
            btnStartGame.SetActive(false);
            btnConfirmPlaceArena.SetActive(false);
            btnConfirmPlaceArena2.SetActive(false);
        }

        _arPlaneManager.enabled = false;
        _arPlacementManager.enabled = false;
        ActivateOrDeactivatePlanes(false);

        sliderScaleArena.SetActive(false);
        sliderRotateArena.SetActive(false);
        panelAlerts.SetActive(false);
        //btnReArrangeArena.SetActive(true);

    }

    // On Click Re-Arrange Arena
    public void OnClick_ReArrangeArena()
    {
        GameControlsContainer.SetActive(false);
        btnReArrangeArena.SetActive(false);
        btnSpawnCharacter.SetActive(false);

        sliderScaleArena.SetActive(true);
        sliderRotateArena.SetActive(true);
        panelAlerts.SetActive(true);
        btnConfirmPlaceArena.SetActive(false);
        btnConfirmPlaceArena2.SetActive(true);

        _arPlaneManager.enabled = true;
        _arPlacementManager.enabled = true;
        ActivateOrDeactivatePlanes(true);
        btnStartGame.SetActive(false);

        txtAlert.text = "Move your Camera around to Scan new Place for Arena or Use Slider to change Size of Battle Arena";
    }

    // On Click SpawnCharacter
    public void OnClick_SpawnCharacter()
    {
        btnStartGame.SetActive(true);
        panelAlerts.SetActive(false);
        btnSpawnCharacter.SetActive(false);

        leftJoyStick.SetActive(true);
        playerCharacter.SetActive(true);
    }

    // Start Game
    public void OnClick_StartGame()
    {
        BakeNavMesh();

        txtLives.SetActive(true);
        txtScore.SetActive(true);

        btnStartGame.SetActive(false);
        GameControlsContainer.SetActive(true);

        btnReArrangeArena.SetActive(true);

        // Initial Enemy Spawn
        GameObject enemySpawned = Instantiate(enemyCharacter, initialEnemySpawnPoint.transform.position, Quaternion.identity);
        enemySpawned.transform.parent = initialEnemySpawnPoint.transform;

        SwordColiisionManager.dead = false;
    }

    // Hides all tracked planes from scene
    private void ActivateOrDeactivatePlanes(bool value)
    {
        foreach (var plane in _arPlaneManager.trackables)
        {
            plane.gameObject.SetActive(value);
        }
    }

}