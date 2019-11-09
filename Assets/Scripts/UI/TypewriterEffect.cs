using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TypewriterEffect : MonoBehaviour
{
    private string theText = 
"You've heard wonderous tales of the mysteries of the Abyss...\n\t\t\t" +
"\nThat those who venture within are able to fulfill their wildest dreams...\n\t\t\t" +
"\nYet, for all the stories of those who enter, very few are told of those who exit...\n\t\t\t" +
"\nYou are a Wanderer, and like those before, you have entered the Abyss...\n\t\t\t" +
"\nWhere others have failed, you will succeed.\n\t\t\t" +
"\nYou will return...\t\t\t";
    public Text theUI;
    private int maxLength;
    private int progress = 0;
    public float interval;
    public float timer;
    public float titledroptimer;
    public Image fadeOut;
    public bool fadeOutStart = false;
    public float transparency = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        maxLength = theText.Length;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (progress < maxLength && timer >= interval)
        {
            theUI.text += theText[progress];
            progress++;
            timer = 0;
        }
        else if (progress >= maxLength && timer > titledroptimer && !fadeOutStart)
        {
            fadeOutStart = true;
        }
        else if(fadeOutStart && transparency < 2)
        {
            transparency += Time.deltaTime;

            var tempColor = fadeOut.color;
            tempColor.a = transparency;
            if (transparency > 1)
            {
                tempColor.a = 1;
            }
            fadeOut.color = tempColor;
        }
        else if(transparency >= 2)
        {
            SceneManager.LoadScene("TitleScene");
        }
    }
}
