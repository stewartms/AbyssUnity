using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EventScreen
{
    public string title;

    //TEXT DISPLAYED
    [TextArea(5,8)]
    public string text = "Fill in description of event here.";

    //PORTRAIT
    public Sprite portraitSprite;
    public string portraitText;

    //MAIN IMAGE
    public Sprite eventImage;

    //OPTIONS
    public Option[] optionList;
    
}

[System.Serializable]
public class Option {
    public string prompt;
    public IOptionEffect[] optionEffects;

}

[CreateAssetMenu(fileName = "New Event", menuName = "Event")]
public class Event : ScriptableObject
{
    public enum EventModifierType {
        NONE,
        NEW_SCREEN,
        NEW_EVENT,
        MOD_HP,
        MOD_MAXHP,
        MOD_MONEY,
        MOD_MEMORY,
        MOD_DARKNESS
    }
    public List<EventScreen> eventScreens;
}

//Effect Class
public interface IOptionEffect {
    void CauseEffect(Player player, UIEventController uiController);
}

[System.Serializable]
public class NewScreenEffect : IOptionEffect {
    public int newIndex;
    public void CauseEffect(Player player, UIEventController uiController) {
        uiController.curIndex = newIndex;
    }
}

[System.Serializable]
public class NewEventEffect : IOptionEffect {
    public Event newEvent;
    public void CauseEffect(Player player, UIEventController uiController) {
        uiController.curEvent = newEvent;
    }
}

[System.Serializable]
public class ModMoneyEffect : IOptionEffect {
    public int mod;
    public void CauseEffect(Player player, UIEventController uiController) {
        player.ModGold(mod);
    }
}

[System.Serializable]
public class ModHealthEffect : IOptionEffect {
    public int mod;
    public enum HealthType {CURRENT, MAX};
    public HealthType healthType;
    public void CauseEffect(Player player, UIEventController uiController) {
        if(healthType == HealthType.CURRENT) {
            player.ModHealthCurrent(mod);
        } else {
            player.ModHealthMax(mod);
        }
    }
}

[System.Serializable]
public class ModMemoryEffect : IOptionEffect {
    public Player.Memory memory;
    public int mod;
    public void CauseEffect(Player player, UIEventController uiController) {
        player.ModMemoryStatus(memory, mod);
    }
}



