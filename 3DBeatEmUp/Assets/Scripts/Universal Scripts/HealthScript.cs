﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthScript : MonoBehaviour
{
    public float health = 100f;

    private CharacterAnimation animationScript;
    private EnemyMovement enemyMovement;

    private bool characterDied;

    public bool is_Player;

    private HealthUI health_UI;

    void Awake()
    {
        animationScript = GetComponentInChildren<CharacterAnimation>();

        health_UI = GetComponent<HealthUI>();
    }


    // Apply damage to player or enemy
    public void ApplyDamage(float damage, bool knockDown)
    {
        if (characterDied)
            return;

        // Applying health damage
        health -= damage;

        //display health UI
        if(is_Player)
        {
            health_UI.DisplayHealth(health);
        }

        // If character has died
        if(health <= 0f)
        {
            // Play death animation
            animationScript.Death();

            // Detect the character has died
            characterDied = true;

            // If it's the player deactivate the enemy script
            // so the enemy stops trying to fight
            if(is_Player)
            {
                GameObject.FindWithTag(Tags.ENEMY_TAG).GetComponent<EnemyMovement>().enabled = false;
            }

            return;
        }

        // If enemy has taken damage
        if(!is_Player)
        {
            if(knockDown)
            {
                // If knocked down, play knock down script
                if(Random.Range(0,2) > 0)
                {
                    animationScript.KnockDown();
                }

                else
                {
                    // If not knocked down, play hit animation
                    if(Random.Range(0,3) > 1)
                    {
                        animationScript.Hit();
                    }
                }
            } // Randomized so the hit animation isn't always played or the 
              //character isn't always knocked down with the left hand, right foot, etc
        }
    }

} // class
