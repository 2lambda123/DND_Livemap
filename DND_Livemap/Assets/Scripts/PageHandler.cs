using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PageHandler : MonoBehaviour
{
    public GameObject BackPageButton;
    public GameObject NextPageButton;
    public TMP_Text pageCount;

    public void NewNode()
    {
        UpdatePageCount();
    }

    public void UpdatePageCount()
    {
        string newString = (NodeInformationManager.Instance.body.pageToDisplay) + "/" + NodeInformationManager.Instance.body.textInfo.pageCount;
        pageCount.text = newString;

        if(NodeInformationManager.Instance.body.pageToDisplay > 1) { BackPageButton.SetActive(true); }
        else { BackPageButton.SetActive(false); }

        if(NodeInformationManager.Instance.body.pageToDisplay < NodeInformationManager.Instance.body.textInfo.pageCount) { NextPageButton.SetActive(true); }
        else { NextPageButton.SetActive(false); }
    }

    public void NextPage()
    {
        if(NodeInformationManager.Instance.body.pageToDisplay < NodeInformationManager.Instance.body.textInfo.pageCount)
        {
            NodeInformationManager.Instance.body.pageToDisplay++;
            UpdatePageCount();
        }
    }

    public void BackPage()
    {
        if (NodeInformationManager.Instance.body.pageToDisplay > 1)
        {
            NodeInformationManager.Instance.body.pageToDisplay--;
            UpdatePageCount();
        }
    }
}
