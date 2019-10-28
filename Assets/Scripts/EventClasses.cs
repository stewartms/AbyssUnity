using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[System.Serializable]
public class EventScreen
{
    //TEXT DISPLAYED
    [TextArea(3,5)]
    public string text;

    //PORTRAIT
    public Sprite portraitSprite;
    public string portraitText;

    //MAIN IMAGE
    public Sprite eventImage;

    //OPTIONS
    public Option[] optionList;
}
[System.Serializable]
public class Option
{
    public string buttonText;
    public ButtonClass buttonStuff;
}

