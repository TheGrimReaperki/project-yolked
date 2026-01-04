using System;
using Unity.VisualScripting;
using UnityEngine;

public class playerMain : MonoBehaviour
{
    static float startingExpPerLevel = 450;
    [SerializeField] public float expRequiredForLevelUp = startingExpPerLevel;

    [SerializeField] float playerHealth = 100f;
    [SerializeField] float playerMaxHealth = 100f;

    [SerializeField] float playerStamina = 100f;
    [SerializeField] float playerMaxStamina = 100f;

    [SerializeField] float playerEXP = 0;


    public int playerLevel = 1;
    private int maxLevel = 5;
    public float playerTotalExp = 0;

    private void Start()
    {
        UpdateExpRequirement();
    }

    private void Update()
    {
        AddExperience(120f);
        System.Threading.Thread.Sleep(500);
    }

    public void AddExperience(float addExp)
    {
        if (playerLevel < maxLevel)
        {
            playerEXP += addExp;
            Debug.LogWarning(addExp + "xp gained!");

            CheckXpForLevelUp();

            if (playerLevel < maxLevel)
            {
                Debug.Log("You are now Level: " + playerLevel + " at " + playerEXP + "/" + expRequiredForLevelUp + " exp");
                Debug.Log("Remaining EXP required for next level (" + (playerLevel + 1) + "): " + (expRequiredForLevelUp - playerEXP));
                Debug.Log("Total EXP required for next level (" + (playerLevel + 1) + "): " + expRequiredForLevelUp + "\n\n\n");
            }                
        }
        else
        {
            Debug.LogError("You have reached max level!");
        }
    }

    private void CheckXpForLevelUp()
    {
        if (playerEXP >= expRequiredForLevelUp && playerLevel < maxLevel)
        {
            playerLevel++;
            playerTotalExp += playerEXP;
            playerHealth = playerMaxHealth; //reset health on level up

            if (playerEXP > expRequiredForLevelUp) //add remaining EXP to next leve, i.e. +150exp when only 120 is required will still award the extra 30
            {
                playerEXP -= expRequiredForLevelUp; //playerEXP = (150 - 120) = 30
            }
            else
            {
                playerEXP = 0; //if no extra xp, reset the counter
            }
                
            UpdateExpRequirement();
            if (playerLevel < maxLevel)
            {
                Debug.LogAssertion("LEVEL UP!!! YOU ARE NOW LEVEL: " + playerLevel + ", with " + playerEXP + "exp leftover!");
            }
            else
            {
                Debug.LogWarning("CONGRATULATIONS!");
            }
                
        }        
    }

    private void UpdateExpRequirement() //450exp per level, + current level * 25 , + current level ^ 3
    {
        /* 
         * example at level 4
         * 450 base exp + 100 + 64 = 614 required to reach level 16   
         *
         * example at level 9
         * 450 base exp + 225 + 729 = 1404 required to reach level 16 
         * 
         * example at level 15  
         * 450 base exp + 375 + 3375 = 4200 required to reach level 16     
         * 
         * example at level 25  
         * 450 base exp + 625 + 15625 = 16700 required to reach level 26
         * 
         * etc. etc. etc.
         * 
         * easy to level up early, progressively gets harder and harder
         */

        expRequiredForLevelUp = startingExpPerLevel + (playerLevel * 25) + (playerLevel * playerLevel * playerLevel);
    }
}
