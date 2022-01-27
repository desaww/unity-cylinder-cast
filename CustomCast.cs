using UnityEngine;

//A script to send out a Cylinder Cast
//Created by Desaw
namespace Desaw.CustomCasts
{
  public class CylinderCast : MonoBehaviour
  {
    //-------------------------------- VARIABLES -----------------------------------------
    //Public Variables that can be edited by the User
    [Header("Raycast")]
    [SerializeField, Tooltip("How far the rays get send out.")] private float maxDistance = 3f;

    [Header("Cylinder Cast")]
    [SerializeField, Range(0.1f, 10f), Tooltip("The radius/width of the cylinder cast.")] private float cylinderRadius = 1f;
    [SerializeField, Range(0.1f, 10f), Tooltip("The heigth/length of the cylinder cast.")] private float cylinderHeigth = 1f;

    //Private variables for the code
    private Vector3 bSize; //the size of the box gizmo
    private Vector3 p1, p2; //the start and end point of the capsule cast
    private bool bIsHit, cIsHit; //if the box or capsule cast hitted something
    private float bHitDistance, cHitDistance; //the distance to the hitted object

    //-------------------------------- CYLINDER CAST -----------------------------------------
    //The boolean function that returns the cylinder cast
    public bool SendCylinderCast(Vector3 aStart, Vector3 aEnd, float aRadius, float aHeigth, float aMaxDistance)
    {
      //Sends a capsule cast and returns whether something has been hit and how far away this object is
      RaycastHit cHit; //Capsule RaycastHit
      if(!Physics.CapsuleCast(aStart, aEnd, aRadius, -transform.up, out cHit, aMaxDistance))
        cIsHit = false;
      else {
        cIsHit = true;
        cHitDistance = cHit.distance;
      }

      //Sends a box cast and returns whether something has been hit and how far away this object is
      RaycastHit bHit; //Box RaycastHit
      if(!Physics.BoxCast(transform.position, new Vector3(aRadius, aHeigth, aRadius), -transform.up, out bHit, transform.rotation, aMaxDistance))
        bIsHit = false;
      else {
        bIsHit = true;
        bHitDistance = bHit.distance;
      }

      //Checks if both casts hitted and returns a boolean
      if(cIsHit && bIsHit)
        return true;
      else
        return false;
    }

    //-------------------------------- BASIC FUNCTIONS -----------------------------------------
    //Set properties, calls the CylinderCast and the VisualizeCast function
    private void Update()
    {
      //Sets the start and end point of the capsule cast
      p1 = transform.position + (transform.up * cylinderHeigth);
      p2 = transform.position + (-transform.up * cylinderHeigth);

      //Sets the box gizmo size
      bSize = new Vector3(cylinderRadius, cylinderHeigth * 2, cylinderRadius);
    }

    private void FixedUpdate()
    {
      //Calls the CylinderCast with its parametern
      bool doesCylinderHit = SendCylinderCast(p1, p2, cylinderRadius / 2, cylinderHeigth, maxDistance);

      Debug.Log(doesCylinderHit); //<--Here you can do whatever you want with your returned bool
    }

    //-------------------------------- GIZMOS -----------------------------------------

    private void OnDrawGizmos()
    {
      //Box cast gizmo's
      if(bIsHit)
      {
        Gizmos.color = Color.green;
        Gizmos.DrawRay(transform.position, -transform.up * bHitDistance);
        Gizmos.DrawWireCube(transform.position + -transform.up * bHitDistance, bSize);
      }
      else
      {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, -transform.up * maxDistance);
        Gizmos.DrawWireCube(transform.position + -transform.up * maxDistance, bSize);
      }

      //Capsule cast gizmo's
      if(cIsHit)
      {
        Gizmos.color = Color.green;
        //Draws a wire sphere at the top and bottom of the capsule since the is no capsule gizmo
        Gizmos.DrawWireSphere(p1 + -transform.up * cHitDistance, transform.lossyScale.x / 2);
        Gizmos.DrawWireSphere(p2 + -transform.up * cHitDistance, transform.lossyScale.x / 2);
      }
      else
      {
        Gizmos.color = Color.red;
        //Draws a wire sphere at the top and bottom of the capsule since the is no capsule gizmo
        Gizmos.DrawWireSphere(p1 + -transform.up * maxDistance, cylinderRadius / 2);
        Gizmos.DrawWireSphere(p2 + -transform.up * maxDistance, cylinderRadius / 2);
      }
    }
  }
}
