﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerOneHealthBar : MonoBehaviour
{

    // Use this for initialization
    public Image playerHealthBar;
    private float healthPoints = 100;
    private float maxHealthPoints = 100;
    public PlayerOneFightScript playerScript;
    public RoundsScript roundScript;
    bool playerBlocking;

    // Audio Variables
    public AudioClip musicSoundClip;

    public AudioSource musicSoundSource;
    void Start()
    {
        UpdateHealthBar();
       

        musicSoundSource.clip = musicSoundClip;
        musicSoundSource.Play();
    }


    // Update is called once per frame
    void Update()
    {
        GameOver(healthPoints);
        if (Input.GetKeyDown(KeyCode.Home))
        {
            Scene currentScene = SceneManager.GetActiveScene(); SceneManager.LoadScene(currentScene.name);
        }

        playerBlocking = playerScript.checkBlocking();

    }
    private void UpdateHealthBar()
    {
        float ratio = healthPoints / maxHealthPoints;
        playerHealthBar.rectTransform.localScale = new Vector3(ratio, 1, 1);
    }

    private void RecieveDamage(float damage)
    {
        if (playerBlocking == false)
        {
            healthPoints -= damage;
            if (healthPoints < 0)
            {
                healthPoints = 0;
            }

            UpdateHealthBar();
        }
    }


    private void GameOver(float healthPoints)
    {
        if (healthPoints == 0)
        {

            roundScript.nextRound();
            
        }
    }

   public float amountOfHealthPoints()
    {
        return healthPoints;
    }
    
    public void resetHealth()
    {
        healthPoints = 100;
    }

}
