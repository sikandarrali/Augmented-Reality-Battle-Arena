using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordColiisionManager : MonoBehaviour
{
    public static bool dead = false;

    AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            audioSource.Play(); //enemy kill sound
            Destroy(other.gameObject);
            dead = true;
            ScoreManager.score += 1;
        }

    }

}