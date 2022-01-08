using UnityEngine;
using UnityEditor;

//A simple Script to send out a Cylinder Cast
//Created by Desaw
public class CustomCast : MonoBehaviour
{
  //-------------------------------- VARIABLES -----------------------------------------
  [Header("Cylinder Cast")]
  [SerializeField, Range(0.1f, 10f)] private float cylinderRadius = 1f;
  [SerializeField, Range(0.1f, 10f)] private float cylinderHeigth = 1f;

  private Vector3 p1, p2;

  //-------------------------------- CYLINDER CAST -----------------------------------------
  //The function that handles the cylinder cast
  public static bool CylinderCast(Vector3 aStart, Vector3 aEnd, float aRadius)
  {
    if (!Physics.CheckCapsule(aStart, aEnd, aRadius))
      return false;
    Vector3 dir = aEnd - aStart;

    Quaternion q = Quaternion.LookRotation(dir);
    Quaternion q2 = Quaternion.AngleAxis(45f, dir);
    Vector3 size = new Vector3(aRadius, aRadius / (1f + Mathf.Sqrt(2f)), dir.magnitude * 0.5f);
    for (int i = 0; i < 4; i++)
    {
      if (Physics.CheckBox(aStart + dir * 0.5f, size, q))
        return true;
      q = q2 * q;
    }
    return false;
  }

  //-------------------------------- BASIC FUNCTIONS -----------------------------------------
  //Set properties and calls the Cylindercast and Visualizecast Function
  private void Update()
  {
    p1 = transform.position + (transform.up * cylinderHeigth);
    p2 = transform.position + (-transform.up * cylinderHeigth);

    VisualizeCast();
  }

  private void FixedUpdate()
  {
    Debug.Log(CylinderCast(p1, p2, cylinderRadius));
  }

  //-------------------------------- VISUALIZE CAST -----------------------------------------
  //Sets the Size of the Object, when its a cylinder mesh, to the Cylindercast size
  private void VisualizeCast()
  {
    Mesh mesh = GetComponent<MeshFilter>().sharedMesh;
    if(mesh == null || mesh.name != "Cylinder")
      return;

    transform.localScale = new Vector3(cylinderRadius, cylinderHeigth, cylinderRadius);
  }
}
