using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class inputManager : MonoBehaviour
{
    [SerializeField] private Camera arCam;
    [SerializeField] private ARRaycastManager _raycastManager;
    private GameObject crosshair;

    private List<ARRaycastHit> _hits = new List<ARRaycastHit>();

    private Touch touch;
    private Pose pose;




    // Start is called before the first frame update
    void Start()
    {

        // get the components
        _raycastManager = FindObjectOfType<ARRaycastManager>();
        crosshair = transform.GetChild(0).gameObject;

        // hide the placement indicator visual
        crosshair.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

        _raycastManager.Raycast(new Vector2(Screen.width / 2, Screen.height / 2), _hits, TrackableType.Planes);



        touch = Input.GetTouch(0);

        if (Input.touchCount < 0 || touch.phase != TouchPhase.Began)
            return;


        if (IsPointerOverUI(touch)) return;

        transform.position = _hits[0].pose.position;
        transform.rotation = _hits[0].pose.rotation;

        // enable the visual if it's disabled
        if (!crosshair.activeInHierarchy)
            crosshair.SetActive(true);

        Instantiate(DataHandler.Instance.GetFurniture(), transform.position, transform.rotation);

    }


    bool IsPointerOverUI(Touch touch)
    {
        PointerEventData eventData = new PointerEventData(EventSystem.current);
        eventData.position = new Vector2(touch.position.x, touch.position.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);
        return results.Count > 0;
    }



}