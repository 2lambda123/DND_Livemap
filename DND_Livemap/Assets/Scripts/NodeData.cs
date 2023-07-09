using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Node", menuName = "Node")]
public class NodeData : ScriptableObject
{
    public string Name;
    [TextArea(3,10)] public string Body;
    [Range(1,20)] public int recommendedLevel = 1;
}
