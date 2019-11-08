using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class ButtonTraits
{
    public GameObject buttonObj;
    public RectTransform buttonRect;
    public Button buttonUI;
    public Text buttonText;
}

public class UIController : MonoBehaviour
{
    //Declaring UI Elements

    //MEMORIES

    //BASIC STATS
    public Text curHealth;
    public Text curGold;

    //EVENT UI
    public Image eventImage;
    public Text eventText;

    //OPTIONS UI
    public List<ButtonTraits> buttons;

    //PORTRAIT UI
    public Image portraitImage;
    public Text portraitText;

    //BOXES

    public Image backgroundImage;
    public GameObject textBox;

    //Declaring PlayerObj
    public GameObject playerObj;
    public Event curEvent;

    private GamePlayer gamePlayer;
    //private EventList eventList;

    public int curIndex;



    // Start is called before the first frame update
    void Start()
    {
        if(playerObj != null)
        {
            gamePlayer = playerObj.GetComponent<GamePlayer>();
        }
        if (curEvent != null)
        {
            curIndex = curEvent.startIndex;
        }
        UpdateUI();

        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("space"))
        {
            EventScreen eventScreen = curEvent.eventScreens[curIndex];
            if (eventScreen.optionList.Length == 0 && ((curIndex+1)<curEvent.eventScreens.Count))
            {
                curIndex++;
                UpdateUI();
            }
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
        
    }

    void UpdateBasicStats()
    {
        curHealth.text = "Health: <color=red>"+ gamePlayer.HealthCurrent+"/"+ gamePlayer.HealthMax + "</color>";
        curGold.text = "Gold: <color=yellow>"+ gamePlayer.Gold + "</color>";
    }

    void UpdateEventUI()
    {
        //GET CURRENT EVENT SCREEN
        EventScreen eventScreen = curEvent.eventScreens[curIndex];


        //EVENT UI
        eventImage.sprite = eventScreen.eventImage;
        eventText.text = eventScreen.text;

        ButtonConfig(eventScreen);

        //PORTRAIT UI
        portraitImage.sprite = eventScreen.portraitSprite;
        portraitText.text = eventScreen.portraitText;

    }
    
    void ButtonConfig(EventScreen eventScreen)
    {
        /*
         * 
         * IF THERE ARE ZERO BUTTONS
         * 
         */
        if (eventScreen.optionList.Length == 0)
        {
            //SET EVENT TEXTBOX/TEXT ACTIVE
            if (!textBox.activeSelf)
            {
                textBox.SetActive(true);
            }
            //

            //SET ALL BUTTONS TO INACTIVE
            foreach (ButtonTraits button in buttons)
            {
                if (button.buttonObj.activeSelf)
                {
                    button.buttonObj.SetActive(false);
                }
            }
        }


        /*
         * 
         * IF THERE IS ONE BUTTON
         * 
         */
        if (eventScreen.optionList.Length == 1)
        {
            //SET EVENT TEXTBOX/TEXT ACTIVE
            if (!textBox.activeSelf)
            {
                textBox.SetActive(true);
            }
            //

            //SET BUTTON 1 Active, rest inactive

            for(int i = 1; i<4;i++)
            {
                if (buttons[i].buttonObj.activeSelf)
                {
                    buttons[i].buttonObj.SetActive(false);
                }
            }

            if (!buttons[0].buttonObj.activeSelf)
            {
                buttons[0].buttonObj.SetActive(true);
            }

            buttons[0].buttonText.text = eventScreen.optionList[0].buttonText;
            buttons[0].buttonRect.anchoredPosition = new Vector2(0,-400);
            buttons[0].buttonUI.GetComponent<ButtonPress>().button = eventScreen.optionList[0].buttonStuff;
        }



        /*
         * 
         * IF THERE ARE TWO BUTTONS
         * 
         */
        else if (eventScreen.optionList.Length == 2)
        {
            //SET EVENT TEXTBOX/TEXT ACTIVE
            if (textBox.activeSelf)
            {
                textBox.SetActive(false);
            }
            //

            //SET BUTTON 1 Active, rest inactive

            for (int i = 2; i < 4; i++)
            {
                if (buttons[i].buttonObj.activeSelf)
                {
                    buttons[i].buttonObj.SetActive(false);
                }
            }

            for (int i = 0; i < 2; i++)
            {
                if (!buttons[i].buttonObj.activeSelf)
                {
                    buttons[i].buttonObj.SetActive(true);
                }

                buttons[i].buttonText.text = eventScreen.optionList[i].buttonText;
                buttons[i].buttonRect.anchoredPosition = new Vector2(0, -220 - (i*100));
                buttons[i].buttonUI.GetComponent<ButtonPress>().button = eventScreen.optionList[i].buttonStuff;
            }
        }


        /*
         * 
         * IF THERE ARE THREE BUTTONS
         * 
         */
        else if (eventScreen.optionList.Length == 3)
        {
            //SET EVENT TEXTBOX/TEXT ACTIVE
            if (textBox.activeSelf)
            {
                textBox.SetActive(false);
            }
            //

            //SET BUTTON 1 Active, rest inactive

            if (buttons[3].buttonObj.activeSelf)
            {
                buttons[3].buttonObj.SetActive(false);
            }

            for (int i = 0; i < 3; i++)
            {
                if (!buttons[i].buttonObj.activeSelf)
                {
                    buttons[i].buttonObj.SetActive(true);
                }

                buttons[i].buttonText.text = eventScreen.optionList[i].buttonText;
                buttons[i].buttonRect.anchoredPosition = new Vector2(0, -200 - (i * 75));
                buttons[i].buttonUI.GetComponent<ButtonPress>().button = eventScreen.optionList[i].buttonStuff;
            }
        }



        /*
         * 
         * IF THERE ARE FOUR BUTTONS
         * 
         */
        else if (eventScreen.optionList.Length == 4)
        {
            //SET EVENT TEXTBOX/TEXT ACTIVE
            if (textBox.activeSelf)
            {
                textBox.SetActive(false);
            }
            //

            //SET BUTTON 1 Active, rest inactive

            for (int i = 0; i < 4; i++)
            {
                if (!buttons[i].buttonObj.activeSelf)
                {
                    buttons[i].buttonObj.SetActive(true);
                }

                buttons[i].buttonText.text = eventScreen.optionList[i].buttonText;
                buttons[i].buttonRect.anchoredPosition = new Vector2(0, -190 - (i * 70));
                buttons[i].buttonUI.GetComponent<ButtonPress>().button = eventScreen.optionList[i].buttonStuff;
            }
        }
    }
}

