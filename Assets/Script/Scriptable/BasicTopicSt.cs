using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Basic Topic", menuName = "Basic Topic")]
public class BasicTopicSt : ScriptableObject
{
    public List<TopicSt> sbsMode; // Shape By Shape Mode
    public List<TopicSt> hsMode; // Hidden Shape Mode
} 
