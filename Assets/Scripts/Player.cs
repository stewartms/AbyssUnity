using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    /*
     * PRIVATE DECLARATIONS
     */

    //Memories - functions pending
    public enum Memory {RED, PURPLE, BLUE, ORANGE};
    public enum MemoryStatus { FULL, BROKEN, DESTROYED };

    private MemoryStatus[] memArray;

    //Basic stats to track
    private int healthMax = 50;
    private int healthCurrent = 50;
    private int gold = 120;
    
    /*
     * GETTERS AND SETTERS
     */

    public MemoryStatus GetMemoryStatus(Memory memory) {
        return memArray[(int) memory];
    }

    public void SetMemoryStatus(Memory memory, MemoryStatus status) {
        memArray[(int) memory] = status;
    }

    public void ModMemoryStatus(Memory memory, int mod) {
        memArray[(int) memory] = (MemoryStatus) Mathf.Clamp((int) memArray[(int) memory] - mod, 0, 2);
    }

    public MemoryStatus GetRedStatus() {return GetMemoryStatus(Memory.RED);}
    public MemoryStatus GetPurpleStatus() {return GetMemoryStatus(Memory.PURPLE);}
    public MemoryStatus GetBlueStatus() {return GetMemoryStatus(Memory.BLUE);}
    public MemoryStatus GetOrangeStatus() {return GetMemoryStatus(Memory.ORANGE);}

    public void SetRedStatus(MemoryStatus status) {GetMemoryStatus(Memory.RED);}
    public void SetPurpleStatus(MemoryStatus status) {GetMemoryStatus(Memory.PURPLE);}
    public void SetBlueStatus(MemoryStatus status) {GetMemoryStatus(Memory.BLUE);}
    public void SetOrangeStatus(MemoryStatus status) {GetMemoryStatus(Memory.ORANGE);}

    //Basic Stats
    public int GetHealthMax() {return healthMax;}
    public int GetHealthCurrent() {return healthCurrent;}
    public int GetGold() {return gold;}

    public void ModHealthMax(int mod) {healthMax += mod;}
    public void ModHealthCurrent(int mod) {healthCurrent += mod;}
    public void ModGold(int mod) {gold += mod;}

    public static Player INSTANCE;

    //Singleton
    void Awake() {
        memArray = new MemoryStatus[4] { MemoryStatus.FULL, MemoryStatus.FULL, MemoryStatus.FULL, MemoryStatus.FULL };
        INSTANCE = this;
    }
}
