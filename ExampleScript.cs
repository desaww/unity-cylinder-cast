using UnityEngine;

namespace Desaw.CustomCasts
{
  //A script to that shows how to use the CustomCast
  //Created by Desaw
  public class ExampleScript : MonoBehaviour
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

    private Mesh mesh; //the mesh of the gizmo

    //-------------------------------- BASIC FUNCTIONS -----------------------------------------
    //Sets the mesh type to "Cylinder"
    private void Awake()
    {
       GameObject cylinder = GameObject.CreatePrimitive(PrimitiveType.Cylinder); //Creates a cylinder
       mesh = cylinder.GetComponent<MeshFilter>().sharedMesh; //Gets mesh
       Destroy(cylinder); //Deletes it
    }

    //Set properties, calls the CylinderCast
    private void Update()
    {
      //Sets the start and end point of the capsule cast
      p1 = transform.position + (transform.up * cylinderHeigth);
      p2 = transform.position + (-transform.up * cylinderHeigth);

      //Sets the box gizmo size
      bSize = new Vector3(cylinderRadius, cylinderHeigth * 2, cylinderRadius);
    }

    //-------------------------------- GIZMOS -----------------------------------------
    //Draws all gizmos and calls the cylinder cast
    private void OnDrawGizmosSelected()
    {
      //Calls the CylinderCast with its parameter
      bool doesCylinderHit = CylinderCast.Send(transform.position, -transform.up, transform.rotation, p1, p2, cylinderRadius / 2, cylinderHeigth, maxDistance);

      //Draws the cylinder depending on if it hits or not
      if(doesCylinderHit) {
        //Sends a raycast to check how long the distance to the ground is
        RaycastHit hit;
        Physics.Raycast(transform.position, -transform.up, out hit, maxDistance + cylinderHeigth);

        //Sets gizmo
        Gizmos.color = Color.green;
        Gizmos.matrix = transform.localToWorldMatrix;
        Gizmos.DrawWireMesh(mesh, Vector3.zero - new Vector3(0, hit.distance - cylinderHeigth, 0), Quaternion.identity, new Vector3(cylinderRadius, cylinderHeigth, cylinderRadius));
      }
      else {
        //Sets gizmo
        Gizmos.color = Color.red;
        Gizmos.matrix = transform.localToWorldMatrix;
        Gizmos.DrawWireMesh(mesh, Vector3.zero - new Vector3(0, maxDistance, 0), Quaternion.identity, new Vector3(cylinderRadius, cylinderHeigth, cylinderRadius));
      }
    }
  }
}
