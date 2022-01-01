using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


/// <summary>
/// Following camera using ball height
/// If the ball move 5m height distance from camera, camera will keep height
/// </summary>
public class CamFollower : MonoBehaviour
{
    /// <summary>
    /// Ball max height value
    /// </summary>
    float MaxHeight = 0;


    /// <summary>
    /// Background material for serialize moving up
    /// </summary>
    [SerializeField]
    Material BackgroundMat;
    
    void Start()
    {
        MaxHeight = BallPhysics.instance.gameObject.transform.position.y;//First amount
    }

    // Update is called once per frame
    void Update()
    {

      
        if (this.transform.position.y + 5 < MaxHeight)//if the ball reach the top move up 
        {
          
            this.transform.DOMove(
                        new Vector3(
                        this.transform.position.x,
                        MaxHeight - 1,
                        this.transform.position.z), 1f);
        }


        if (MaxHeight < BallPhysics.instance.gameObject.transform.position.y)//Control is the ball move height
        {
           
            MaxHeight = BallPhysics.instance.gameObject.transform.position.y;//Set max height from ball height
            BackgroundMat.mainTextureOffset = new Vector2(0, this.transform.position.y * -1);//Material texture offset changing
        }
    }
}
