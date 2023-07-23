using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NodeInformationManager : MonoBehaviour
{
    private static NodeInformationManager _instance;
    public static NodeInformationManager Instance { get { return _instance; } }

    public bool showRecommendedLevel;

    public NodeInfoDisplay disp;

    [HideInInspector] public NodeData selectedNode;
    private Node previousNode;

    public TMP_Text Heading;
    public TMP_Text body;
    public TMP_Text RecommendedLevel;

    public PageHandler pageHandler;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    private void Start()
    {
        if(!showRecommendedLevel)
        {
            RecommendedLevel.gameObject.SetActive(false);
        }
    }

    public void newNode(NodeData newNode, Node node)
    {
        if(previousNode != null)
        {
            previousNode.ResetThisNode();
        }
        if(disp.open != true)
        {
            disp.toggleInfo();
        }

        selectedNode = newNode;
        previousNode = node;
        StartCoroutine(UpdateNode());
    }

    IEnumerator UpdateNode()
    {
        Heading.text = selectedNode.Name;
        body.text = selectedNode.Body;
        body.pageToDisplay = 1;
        RecommendedLevel.text = "Recommended Lvl: " + selectedNode.recommendedLevel.ToString();

        yield return new WaitForEndOfFrame();

        pageHandler.NewNode();
    }

}
