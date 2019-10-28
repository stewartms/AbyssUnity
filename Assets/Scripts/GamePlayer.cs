﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayer : MonoBehaviour
{
    /*
     * PRIVATE DECLARATIONS
     */

    //Memories - functions pending
    public enum Memory { full, broken, destroyed };
    private Memory memRed = Memory.full;
    private Memory memPurple = Memory.full;
    private Memory memBlue = Memory.full;
    private Memory memOrange = Memory.full;

    //Basic stats to track
    private int healthMax = 50;
    private int healthCurrent = 50;
    private int gold = 120;

    //RPG stats - functions pending

    //Initializer

    public GameObject playerInitializerObj;
    private PlayerInitializer playerInit;

    //UI

    public GameObject canvas;
    private UIController uiController;


    /*
     * GETTERS AND SETTERS
     */

    //Memories
    public Memory MemRed { get => memRed; set => memRed = value; }
    public Memory MemPurple { get => memPurple; set => memPurple = value; }
    public Memory MemBlue { get => memBlue; set => memBlue = value; }
    public Memory MemOrange { get => memOrange; set => memOrange = value; }

    //Basic Stats
    public int HealthMax { get => healthMax; set => healthMax = value; }
    public int HealthCurrent { get => healthCurrent; set => healthCurrent = value; }
    public int Gold { get => gold; set => gold = value; }

    // Start is called before the first frame update
    void Start()
    {
        if (playerInitializerObj != null)
        {
            playerInit = playerInitializerObj.GetComponent<PlayerInitializer>();
        }
        MemRed = playerInit.memRedStart;
        MemPurple = playerInit.memPurpleStart;
        MemBlue = playerInit.memBlueStart;
        MemOrange = playerInit.memOrangeStart;

        HealthMax = playerInit.startHealth;
        HealthCurrent = playerInit.startHealth;
        Gold = playerInit.startGold;

        if (canvas != null)
        {
            uiController = canvas.GetComponent<UIController>();
        }
        uiController.UpdateUI();
    }

}