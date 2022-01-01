using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Bounce ball physiscs
/// </summary>
public class BallPhysics : MonoBehaviour
{
    public static BallPhysics instance;


    /// <summary>
    /// Min force for bouncing
    /// </summary>
    [SerializeField]
    private float minForce = 5f;

    /// <summary>
    /// Last frame velocity vector
    /// </summary>
    private Vector3 lastFrameVelocity;

    /// <summary>
    /// Rigidbody of ball
    /// </summary>
    private Rigidbody rb;

    private void Awake()
    {
        if (instance == null)
            instance = this;


        rb = GetComponent<Rigidbody>();
      
    }


    /// <summary>
    /// Starting ball movement
    /// </summary>
    public void StartGame()
    {
        if(!GameManager.isGameStarted && !GameManager.isGameEnded)//Controlled is the game is already started or is the game already ended
        {
            GameManager.isGameStarted = true;
            rb.isKinematic = false;
            Debug.Log("Ball is falling");
            if (Random.value > 0.5f)//Send ball left or right
            {
                rb.AddForce(this.transform.position + this.transform.right * 50);
            }
            else
            {
                rb.AddForce(this.transform.position + this.transform.right * -50);
            }
        }
    }

   /* private void OnEnable()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = initialVelocity;
    }*/

    private void Update()
    {
        lastFrameVelocity = rb.velocity;
        
    }

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.transform.tag.Equals("Player"))//Is the triggered to player cubes
        {
            Bounce(collision.contacts[0].normal, collision.transform.GetComponent<ObjectMovement>()._CurrentMovementSpeed * 50.0f) ;//Multiplying force speed
        }


        if (collision.transform.tag.Equals("Destroyer"))//Game ended time
        {
            GameManager.instance.OnGameEnded();
            this.gameObject.SetActive(false);
        }
      
    }


    /// <summary>
    /// Bouncing calculation using area normal and force speed
    /// </summary>
    /// <param name="collisionNormal"></param>
    /// <param name="speed"></param>
    private void Bounce(Vector3 collisionNormal,float speed)
    {
       
       //Reflecting from normal and added some up values
        var direction = Vector3.Reflect(lastFrameVelocity.normalized * 2, collisionNormal + (Vector3.up * Mathf.Clamp(speed,1,5)));
      
        
        rb.AddForce(direction * minForce); //* Mathf.Max(speed, minVelocity));
       
    }
}
