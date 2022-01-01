using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Object moves to pointed position
/// </summary>
public class ObjectMovement : MonoBehaviour
{
    /// <summary>
    /// Move point
    /// </summary>
    [HideInInspector]
    public Vector3 MovePosition;


    /// <summary>
    /// Clamps min / max position
    /// </summary>
    [SerializeField] Vector2 PositionyClamp = Vector2.zero;

    /// <summary>
    /// Moving speed 
    /// </summary>
    [Range(0,50)]
    [SerializeField] float MoveSpeed = 50;

    /// <summary>
    /// Force speed value
    /// </summary>
    private float CurrentMovementSpeed = 0;

    /// <summary>
    /// Force speed value
    /// </summary>
    [SerializeField]
    public float _CurrentMovementSpeed
    {   get { return CurrentMovementSpeed; }
        set { CurrentMovementSpeed = value; }
    }


    private void Start()
    {
        MovePosition = this.transform.localPosition;//Set first local position to Move Position
    }
   
    void Update()
    {
        MovePosition.y = Mathf.Clamp(MovePosition.y, PositionyClamp.x, PositionyClamp.y);
        
        this.transform.localPosition = Vector3.Lerp(this.transform.localPosition, MovePosition, Time.deltaTime * MoveSpeed);
  
        _CurrentMovementSpeed = Vector3.Distance(this.transform.localPosition, MovePosition);//Calculating move speed for force speed
    }


   
}
