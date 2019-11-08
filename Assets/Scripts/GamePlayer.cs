using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayer : MonoBehaviour
{
    /*
     * PRIVATE DECLARATIONS
     */

    //Memories - functions pending
    public enum Memory { FULL, BROKEN, DESTROYED };
    private Memory memRed = Memory.FULL;
    private Memory memPurple = Memory.FULL;
    private Memory memBlue = Memory.FULL;
    private Memory memOrange = Memory.FULL;

    //Basic stats to track
    private int healthMax = 50;
    private int healthCurrent = 50;
    private int gold = 120;

    //RPG stats - functions pending

    //Initializer

    public GameObject playerInitializerObj;
    private PlayerInitializer playerInit;

    //UI
    public UIController uiController;


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
        uiController.UpdateUI();
    }

}
