using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeInfoDisplay : MonoBehaviour
{
    [HideInInspector] public bool open = false;
    [SerializeField] private float moveAmount = 150f;
    [SerializeField] private float moveSpeed = 10f;

    [SerializeField] private GameObject openText;
    [SerializeField] private GameObject closeText;

    public void toggleInfo()
    {
        if(open)
        {
            //close
            Vector2 newPos = new Vector2(transform.localPosition.x + moveAmount, transform.localPosition.y);
            StartCoroutine(MoveFromTo(transform.localPosition, newPos));
            open = false;
        }
        else
        {
            Vector2 newPos = new Vector2(transform.localPosition.x - moveAmount, transform.localPosition.y);
            StartCoroutine(MoveFromTo(transform.localPosition, newPos));
            open = true;
        }
    }
    IEnumerator MoveFromTo(Vector2 from, Vector2 to)
    {
        var t = 0f;

        while (t < 1f)
        {
            t += moveSpeed * Time.deltaTime;
            transform.localPosition = Vector3.Lerp(from, to, t);
            yield return null;
        }
        ToggleArrows();
    }
    private void ToggleArrows()
    {
        if (open)
        {
            openText.SetActive(false);
            closeText.SetActive(true);
            //ECG_Display.SetActive(true);
        }
        else
        {
            openText.SetActive(true);
            closeText.SetActive(false);
        }
    }
}
