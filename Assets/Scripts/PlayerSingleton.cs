using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSingleton : MonoBehaviour
{
    #region Singleton

    public static PlayerSingleton playerInstance;

    void Awake()
    {
        playerInstance = this;
    }

    #endregion

    public GameObject player;
}
