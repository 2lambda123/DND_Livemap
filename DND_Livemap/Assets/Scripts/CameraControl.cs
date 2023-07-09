using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraControl : MonoBehaviour
{
    private static CameraControl _instance;
    public static CameraControl Instance { get { return _instance; } }

    private Vector3 Origin;
    private Vector3 Difference;
    private Vector3 ResetCamera;

    public CinemachineVirtualCamera virtualCam;
    private Camera cam;

    private bool drag = false;

    public float ZoomChange;
    public float SmoothChange;
    public float MinSize, MaxSize;

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
        cam = Camera.main;
        virtualCam = GetComponent<CinemachineVirtualCamera>();
        ResetCamera = Camera.main.transform.position;
    }

    private void Update()
    {
        if (Input.mouseScrollDelta.y > 0)
        {
            virtualCam.m_Lens.OrthographicSize -= ZoomChange * Time.deltaTime * SmoothChange;
        }
        if (Input.mouseScrollDelta.y < 0)
        {
            virtualCam.m_Lens.OrthographicSize += ZoomChange * Time.deltaTime * SmoothChange;
        }

        virtualCam.m_Lens.OrthographicSize = Mathf.Clamp(virtualCam.m_Lens.OrthographicSize, MinSize, MaxSize);
    }

    private void LateUpdate()
    {
        if (Input.GetMouseButton(1))
        {
            Difference = cam.ScreenToWorldPoint(Input.mousePosition) - cam.transform.position;
            if (drag == false)
            {
                drag = true;
                Origin = cam.ScreenToWorldPoint(Input.mousePosition);
            }
        }
        else
        {
            drag = false;
        }

        if (drag)
        {
            virtualCam.transform.position = Origin - Difference;
        }
    }
}
