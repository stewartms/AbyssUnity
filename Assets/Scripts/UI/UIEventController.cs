using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[System.Serializable]
public class ButtonTraits {
    public Button button;
    public Text buttonText;
}

public class UIEventController : MonoBehaviour
{
    //Declaring UI Elements

    //MEMORIES
    public GameObject memoriesParent;
    Toggle[] memoryToggles;

    //BASIC STATS
    public Text curHealth;
    public Text curGold;

    //EVENT UI
    public Image eventImage;
    public Text eventText;

    //OPTIONS UI
    public GameObject buttonsParent;
    ButtonTraits[] buttonTraits;

    //PORTRAIT UI
    public Image portraitImage;
    public Text portraitText;

    //BOXES

    public Image backgroundImage;

    public Event curEvent;
    private Dictionary<string, int> titleToIndexDictionary;

    private Player player;
    //private EventList eventList;

    public int curIndex;
    private EventScreen curScreen;

    public static UIEventController INSTANCE;

    //Singleton
    void Awake() {
        INSTANCE = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        player = Player.INSTANCE;
        
        if (curEvent != null) { curIndex = 0;}//curEvent.startIndex; }
        InitUI();
        UpdateUI();
        
    }

    void InitUI() {
        memoryToggles = memoriesParent.GetComponentsInChildren<Toggle>();
        Button[] buttons = buttonsParent.GetComponentsInChildren<Button>();

        SetEvent(curEvent);

        buttonTraits = new ButtonTraits[4];
        for(int i = 0; i < 4; i++) {
            buttonTraits[i] = new ButtonTraits();
            buttonTraits[i].button = buttons[i];
            buttonTraits[i].buttonText = buttons[i].GetComponentInChildren<Text>();
        }
        
    }

    public void SetEvent(Event e) {
        curEvent = e;
        titleToIndexDictionary = new Dictionary<string, int>();
        for(int i = 0; i < curEvent.eventScreens.Count; i++) {
            titleToIndexDictionary.Add(curEvent.eventScreens[i].screenID, i);
        }
    }

    public void UpdateUI()
    {
        UpdateUIMemories();
        UpdateBasicStats();
        UpdateEventUI();
    }

    void UpdateUIMemories()
    {
        for(int i = 0; i < 4; i++) {
            Player.MemoryStatus status = player.GetMemoryStatus((Player.Memory) i);
            memoryToggles[i].isOn = status == Player.MemoryStatus.FULL;
            memoryToggles[i].gameObject.SetActive(status != Player.MemoryStatus.DESTROYED);
        }
    }

    void UpdateBasicStats()
    {
        curHealth.text = "Health: <color=red>"+ player.GetHealthCurrent()+"/"+ player.GetHealthMax() + "</color>";
        curGold.text = "Gold: <color=yellow>"+ player.GetGold() + "</color>";
    }

    void UpdateEventUI()
    {
        //GET CURRENT EVENT SCREEN
        curScreen = curEvent.eventScreens[curIndex];


        //EVENT UI
        eventImage.sprite = curScreen.eventImage;
        eventText.text = curScreen.text;

        DrawButtons(curScreen);

        //PORTRAIT UI
        portraitImage.sprite = curScreen.portraitSprite;
        portraitText.text = curScreen.portraitText;

    }
    
    void DrawButtons(EventScreen eventScreen)
    {
        int numButtons = eventScreen.optionList.Count;

        eventImage.gameObject.SetActive(numButtons < 3);

        for(int i = 0; i < 4; i++) {
            
            bool enabledButton = i < numButtons;
            buttonTraits[i].button.gameObject.SetActive(enabledButton);
            if(enabledButton) {
                buttonTraits[i].buttonText.text = eventScreen.optionList[i].prompt;
            }
        }

    }

    
    public void PressButton(int buttonID)
    {
        Option button = curScreen.optionList[buttonID];

        foreach(OptionEffect optionEffect in button.optionEffects) {
            switch(optionEffect.type) {
                case OptionEffect.OptionEffectType.NONE:
                break;
                case OptionEffect.OptionEffectType.NEW_SCREEN:
                    if(!titleToIndexDictionary.TryGetValue(optionEffect.newScreenID, out curIndex)) {
                        Debug.LogWarning("ScreenID -" + optionEffect.newScreenID + "- not found!");
                    }
                    break;
                case OptionEffect.OptionEffectType.MOD_HP:
                    player.ModHealthCurrent(optionEffect.mod);
                    break;
                case OptionEffect.OptionEffectType.MOD_MAXHP:
                    player.ModHealthMax(optionEffect.mod);
                    break;
                case OptionEffect.OptionEffectType.MOD_MONEY:
                    player.ModGold(optionEffect.mod);
                    break;
                case OptionEffect.OptionEffectType.MOD_MEMORY:
                    Debug.Log("Modding memory!");
                    player.ModMemoryStatus(optionEffect.memory, optionEffect.mod);
                    break;
                case OptionEffect.OptionEffectType.MOD_DARKNESS:
                break;
            }
        }

        UpdateUI();
    }
}

