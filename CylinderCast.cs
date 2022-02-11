using UnityEngine;

//A script to send out a Cylinder Cast
//Created by Desaw
namespace Desaw.CustomCasts
{
  public static class CylinderCast
  {
    //-------------------------------- CYLINDER CAST -----------------------------------------
    //The boolean function that returns the cylinder cast
    public static bool Send(Vector3 pos, Vector3 dir, Quaternion rot, Vector3 start, Vector3 end, float radius, float heigth, float maxDistance)
    {
      //Private variables for the code
      bool bIsHit, cIsHit; //if the box or capsule cast hitted something
      float bHitDistance, cHitDistance; //the distance to the hitted object

      //Sends a capsule cast and returns whether something has been hit and how far away this object is
      RaycastHit cHit; //Capsule RaycastHit
      if(!Physics.CapsuleCast(start, end, radius, dir, out cHit, maxDistance))
        cIsHit = false;
      else {
        cIsHit = true;
        cHitDistance = cHit.distance;
      }

      //Sends a box cast and returns whether something has been hit and how far away this object is
      RaycastHit bHit; //Box RaycastHit
      if(!Physics.BoxCast(pos, new Vector3(radius, heigth, radius), dir, out bHit, rot, maxDistance))
        bIsHit = false;
      else {
        bIsHit = true;
        bHitDistance = bHit.distance;
      }

      //Checks if both casts hitted and returns a boolean
      if(cIsHit && bIsHit) return true;
      else return false;
    }
  }
}
