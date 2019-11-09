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

        buttonTraits = new ButtonTraits[4];
        for(int i = 0; i < 4; i++) {
            buttonTraits[i] = new ButtonTraits();
            buttonTraits[i].button = buttons[i];
            buttonTraits[i].buttonText = buttons[i].GetComponentInChildren<Text>();
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
        int numButtons = eventScreen.optionList.Length;

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

        foreach(IOptionEffect optionEffect in button.optionEffects) {
            optionEffect.CauseEffect(player, this);
        }

        UpdateUI();
    }
}

