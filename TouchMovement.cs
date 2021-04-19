using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using MoreMountains.NiceVibrations;


public class TouchMovement : MonoBehaviour
{
    public GameObject moveable;
    public GameObject cursor;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        TouchAndGo();
        ClickAndGo();
        MMVibrationManager.UpdateContinuousHaptic(Mathf.Sin(Time.deltaTime), Mathf.Sin(Time.deltaTime));
    }

    private void TouchAndGo()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Ended)
            {
                Ray ray = Camera.main.ScreenPointToRay(touch.position);
                RaycastHit hit;
                MMVibrationManager.ContinuousHaptic(.5f, .5f, 100);
                
                if (Physics.Raycast(ray, out hit))
                {
                    Debug.Log(hit.point);
                    cursor.transform.position = hit.point;
                    moveable.GetComponent<NavMeshAgent>().SetDestination(hit.point);
                }
            }
            

        }
    }

    private void ClickAndGo()
    {
        if (Input.GetMouseButtonUp(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            MMVibrationManager.ContinuousHaptic(.5f, .5f, 100);

            if (Physics.Raycast(ray, out hit))
            {
                Debug.Log(hit.point);
                cursor.transform.position = hit.point;
                moveable.GetComponent<NavMeshAgent>().SetDestination(hit.point);
            }
        }
    }


    protected virtual void UpdateContinuous()
    {

        //Vector3.Distance(moveable.transform.position, new Vector3(0,0,0));
        
    }

}
