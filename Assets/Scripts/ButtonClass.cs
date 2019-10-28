using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyBox;

[System.Serializable]
public class ButtonClass
{
    //FLAGS FOR BUTTON ACTIONS

    //TEXT + PROGRESSION 

    //New portion of same event
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
    //Modify Darkness
    public bool modDarkness;
    [ConditionalField("modDarkness")] public bool mLighten;
    [ConditionalField("modDarkness")] public bool mDarken;
}
