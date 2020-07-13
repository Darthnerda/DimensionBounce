using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DeathController : MonoBehaviour, DeathHandler
{
    private AudioManager d_AudioManager;

    private void Awake()
    {
        d_AudioManager = FindObjectOfType<AudioManager>();
    }

    public void OnDied()
    {
        Debug.Log("YOU DIED!");
        //d_AudioManager.Play("vproc");
    }
}
