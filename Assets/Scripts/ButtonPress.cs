using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPress : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject UIObj;
    public UIController uiController;
    //Player
    public GameObject playerObj;
    private GamePlayer gamePlayer;
    //Button
    public ButtonClass button;


    public string eventID;

    void Start()
    {
        if (playerObj != null)
        {
            gamePlayer = playerObj.GetComponent<GamePlayer>();
        }
    }

    public void PressButton()
    {
        PressButtonProcessFlags();


        uiController.UpdateUI();
    }

    private void PressButtonProcessFlags()
    {
        if(button.newSegment)
        {
            uiController.curIndex = button.newEventIndex;
        }
        if (button.newScene)
        {
            uiController.curIndex = button.newEvent.startIndex;
            uiController.curEvent = button.newEvent;
        }
        if(button.modCurHealth)
        {
            int curHealth = gamePlayer.HealthCurrent;
            gamePlayer.HealthCurrent = curHealth + button.curHealthModifier;
        }
        if(button.modDarkness)
        {
            if(button.mLighten)
            {

            }
            else if(button.mDarken)
            {

            }
        }
    }
}

/*
 * 
 * 
    New portion of same event
    public bool newSegment;
    [ConditionalField("newSegment")] public int newEventIndex;
    //New scene entirely
    public bool newScene;
    [ConditionalField("newScene")] public Event newEvent;

    //MODIFY PLAYER ATTRIBUTES

    //Modify current health
    public bool modCurHealth;
    [ConditionalField("modCurHealth")] public int curHealthModifier;
    //Modify max health
    public bool modMaxHealth;
    [ConditionalField("modMaxHealth")] public int maxHealthModifier;
    //Modify current money
    public bool modMoney;
    [ConditionalField("modMoney")] public int moneyModifier;
    //Modify memories
    public bool modMemory;
 * 
 * 
 */
