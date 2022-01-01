using UnityEngine;

/// <summary>
/// Keeps touch and touched object
/// </summary>
[System.Serializable]
public class TouchControl
{
    [SerializeField] public Touch touch;
    [SerializeField] public GameObject touchedObject = null;


    public TouchControl(Touch t)
    {
        this.touch = t;
    }
}
