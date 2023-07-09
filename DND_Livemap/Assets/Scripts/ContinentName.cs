using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ContinentName : MonoBehaviour
{
    public TMP_Text continentText;
    [SerializeField] private float renderDistance = 25f;

    private Color targetAlpha;
    private Color oldAlpha;
    private float t;
    private bool visible;

    private void Start()
    {
        t = 1;

        targetAlpha = new Color(1, 1, 1, 1);
    }

    private void Update()
    {
        if (t < 1)
        {
            continentText.color = new Color(1, 1, 1, Mathf.Lerp(oldAlpha.a, targetAlpha.a, t));
            t += 2f * Time.deltaTime;
        }

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

    public void FadeInName()
    {
        t = 0;
        targetAlpha = new Color(1, 1, 1, 1);
        oldAlpha = continentText.color;
    }

    public void FadeOutName()
    {
        t = 0;
        targetAlpha = new Color(1, 1, 1, 0);
        oldAlpha = continentText.color;
    }
}
