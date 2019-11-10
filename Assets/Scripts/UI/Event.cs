using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EventScreen
{
    public string screenID;

    //TEXT DISPLAYED
    [TextArea(5,8)]
    public string text = "Fill in description of event here.";

    //PORTRAIT
    public Sprite portraitSprite;
    public string portraitText;

    //MAIN IMAGE
    public Sprite eventImage;

    //OPTIONS
    public List<Option> optionList;
    
}

[System.Serializable]
public class Option {
    public string prompt;
    public List<OptionEffect> optionEffects;

}

[CreateAssetMenu(fileName = "New Event", menuName = "Event")]
public class Event : ScriptableObject
{
    public List<EventScreen> eventScreens;
}

//Effect Class
[System.Serializable]
public class OptionEffect {
    public enum OptionEffectType {
        NONE,
        NEW_SCREEN,
        MOD_HP,
        MOD_MAXHP,
        MOD_MONEY,
        MOD_MEMORY,
        MOD_DARKNESS
    }

    public OptionEffectType type;
    public string newScreenID;
    public int mod;
    public Player.Memory memory;

}