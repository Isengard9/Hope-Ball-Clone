using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Player manager script control left and right cubes
/// Control touches
/// </summary>
public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;

    /// <summary>
    /// Keep touch list
    /// </summary>
    [SerializeField]
    List<TouchControl> touchControlList = new List<TouchControl>();



    private void Update()
    {
        TouchControl();

        for (int i = 0; i < touchControlList.Count; i++)
        {
            TouchStates(i, touchControlList[i].touch);
        }
    }


    /// <summary>
    /// Controlling all touches adding to list
    /// </summary>
    void TouchControl()
    {
        if (Input.touchCount > 0)
        {

        
            foreach(Touch t in Input.touches)//Adding all Touches
            {
                if (!controlTouclList2IsInList(t))//If the touch is not added already
                {
                    touchControlList.Add(new TouchControl(t));
                }
                else
                {
                    ChangeState(t);
                }
            }
        }
    }

    /// <summary>
    /// Is touch already added or not
    /// </summary>
    /// <param name="t"></param>
    /// <returns></returns>
    bool controlTouclList2IsInList(Touch t) {

        for (int i = 0; i < touchControlList.Count; i++)
        {
            if (touchControlList[i].touch.fingerId == t.fingerId)
                return true;
        }
        return false;
    }

    /// <summary>
    /// Changing touch phase
    /// </summary>
    /// <param name="t"></param>
    void ChangeState(Touch t)
    {
        for (int i = 0; i < touchControlList.Count; i++)
        {
            if (touchControlList[i].touch.fingerId == t.fingerId)
                touchControlList[i].touch = t;
        }

    }

    /// <summary>
    /// Getting touch index and touch parameter
    /// Controlling touch phase and work on touch phase parameter
    /// </summary>
    /// <param name="index"></param>
    /// <param name="t"></param>
    void TouchStates(int index,Touch t)
    {
       
        GameObject selectedObject = null;
        switch (t.phase)
        {
            case TouchPhase.Began:
                selectedObject = RayToObject(t.position);
                touchControlList[index].touchedObject = selectedObject;
                if (selectedObject != null && !isObjectAlreadySelected(t, selectedObject))
                {
                    MoveObject(t, selectedObject);
                }
                break;
            case TouchPhase.Moved:
                if (touchControlList[index].touchedObject == null)
                {
                    selectedObject = RayToObject(t.position);
                    touchControlList[index].touchedObject = selectedObject;
                    if (selectedObject != null && !isObjectAlreadySelected(t, selectedObject))
                    {
                        MoveObject(t, selectedObject);
                    }
                }
                else
                {
                    MoveObject(t, touchControlList[index].touchedObject);
                }
                break;
            case TouchPhase.Ended:
                touchControlList.RemoveAt(index);
                break;
            case TouchPhase.Stationary:
                if (touchControlList[index].touchedObject == null)
                {
                    selectedObject = RayToObject(t.position);
                    touchControlList[index].touchedObject = selectedObject;
                    if (selectedObject != null && !isObjectAlreadySelected(t, selectedObject))
                    {
                        MoveObject(t, selectedObject);
                    }
                }
                else
                {
                    MoveObject(t, touchControlList[index].touchedObject);
                }
                break;
            case TouchPhase.Canceled:
                touchControlList.RemoveAt(index);
                break;
        }
    }


    RaycastHit hit;
    Ray ray;

    /// <summary>
    /// Layer mask for cubes
    /// </summary>
    [SerializeField] LayerMask movableLayer;

    /// <summary>
    /// Raycast to movable touch cubes
    /// </summary>
    /// <param name="touchPosition"></param>
    /// <returns></returns>
    GameObject RayToObject(Vector2 touchPosition)
    {
        GameObject selected = null;

       ray = Camera.main.ScreenPointToRay(touchPosition);

        if (Physics.Raycast(ray, out hit,100,movableLayer))
        {
            selected = hit.transform.gameObject;
            Debug.Log(selected.name);
        }
        return selected;
    }


    /// <summary>
    /// Move cube touched position
    /// </summary>
    /// <param name="t"></param>
    /// <param name="obj"></param>
    void MoveObject(Touch t,GameObject obj)
    {
        obj.GetComponent<ObjectMovement>().MovePosition = new Vector3(obj.transform.localPosition.x, calculateYPosition(t.position.y), 0);
    }


    /// <summary>
    /// Controlled is the object already selected
    /// Saving from selecting two finger same object
    /// </summary>
    /// <param name="t"></param>
    /// <param name="obj"></param>
    /// <returns></returns>
    bool isObjectAlreadySelected(Touch t, GameObject obj)
    {
        for (int i = 0; i < touchControlList.Count; i++)
        {
            if (!touchControlList[i].touch.Equals(t) && touchControlList[i].touchedObject == obj)
                return true;
        }

        return false;
    }

    /// <summary>
    /// Clamping cube position
    /// </summary>
    [SerializeField]
    Vector2 ScreenPositionClamp = Vector2.zero;

    /// <summary>
    /// Calculating touch position to y position
    /// </summary>
    /// <param name="height"></param>
    /// <returns></returns>
    float calculateYPosition(float height)
    {
        float y = 0;
        y = (height * ScreenPositionClamp.y * 2) / Screen.height + ScreenPositionClamp.x;
        return y;
    }
}
