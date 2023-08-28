using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Node : MonoBehaviour
{
    public NodeData ThisNode;
    public TMP_Text nodeName;
    public GameObject nodeGFX;

    [SerializeField] [Range(1,2)] private float scaleModifier = 1.2f;
    [SerializeField] private float renderDistance = 10f;
    [SerializeField][Tooltip("If true, the text will only render while zoomed out above the render distance")] private bool renderModeLarge;

    private Color targetAlpha;
    private Color oldAlpha;
    private Vector2 targetScale;
    private Vector2 oldScale;
    private float ta = 1;
    private float ts = 1;
    private bool visible;

    private void Start()
    {
        ta = 1;
        ts = 1;

        targetAlpha = new Color(1, 1, 1, 1);
        targetScale = new Vector2(1, 1);

        nodeName.text = ThisNode.Name;
        FadeOutName();
    }

    private void Update()
    {
        if(ta < 1)
        {
            nodeName.color = new Color(1,1,1,Mathf.Lerp(oldAlpha.a, targetAlpha.a, ta));
            ta += 2f * Time.deltaTime;
        }
        if (ts < 1)
        {
            float newScale = Mathf.Lerp(oldScale.x, targetScale.x, ts);
            nodeGFX.transform.localScale = new Vector3(newScale, newScale, 1);
            ts += 2f * Time.deltaTime;
        }

        //Check renda distance
        CheckCameraZoom();
    }

    private void CheckCameraZoom()
    {
        if (!renderModeLarge)
        {
            if (CameraControl.Instance.virtualCam.m_Lens.OrthographicSize > renderDistance && visible)
            {
                FadeOutName();
                visible = false;
            }
            else if (CameraControl.Instance.virtualCam.m_Lens.OrthographicSize < renderDistance && !visible)
            {
                FadeInName();
                visible = true;
            }
        }
        else
        {
            if (CameraControl.Instance.virtualCam.m_Lens.OrthographicSize < renderDistance && visible)
            {
                FadeOutName();
                visible = false;
            }
            else if (CameraControl.Instance.virtualCam.m_Lens.OrthographicSize > renderDistance && !visible)
            {
                FadeInName();
                visible = true;
            }
        }
    }

    private void OnMouseDown()
    {
        SelectThisNode();
    }

    public void SelectThisNode()
    {
        if(NodeInformationManager.Instance.selectedNode == ThisNode) { NodeInformationManager.Instance.toggleNode(); }
        NodeInformationManager.Instance.newNode(ThisNode, this);

        ts = 0;
        targetScale = new Vector2(scaleModifier, scaleModifier);
        oldScale = nodeGFX.transform.localScale;
    }

    public void ResetThisNode()
    {
        ts = 0;
        targetScale = new Vector2(1, 1);
        oldScale = nodeGFX.transform.localScale;
    }

    public void FadeInName()
    {
        ta = 0;
        targetAlpha = new Color(1, 1, 1, 1);
        oldAlpha = nodeName.color;
    }

    public void FadeOutName()
    {
        ta = 0;
        targetAlpha = new Color(1, 1, 1, 0);
        oldAlpha = nodeName.color;
    }
}
