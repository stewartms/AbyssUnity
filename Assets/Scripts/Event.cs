using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Event", menuName = "Event")]
public class Event : ScriptableObject
{
    //EVENT INFORMATION
    public List<EventScreen> eventScreens;
    public int startIndex = 0;
}