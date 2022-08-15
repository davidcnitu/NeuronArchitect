using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Cell : MonoBehaviour
{
    //********** VARIABLE DEFINITION **********\\
    public int[] cellCoordinates;
    public List<int[,]> blueprint = new List<int[,]>();
    public List<Vector3> vertices = new List<Vector3>(); // Create list to hold vertices for the mesh
    public List<int> triangles = new List<int>(); //Create list to hold triangles for the mesh
    MeshFilter meshFilter = new MeshFilter(); //Create a MeshFilter
    MeshRenderer meshRenderer = new MeshRenderer();
    Mesh mesh;
    MeshCollider meshCollider;

    public float volume;

    //********** METHOD DEFINITION **********\\
    private void Start()
    {
        cellCoordinates = ReadImage.cellsCoordinates[name];
        blueprint = getBlueprint(ReadImage.imageList);
        meshFilter = GetComponent<MeshFilter>();
        meshRenderer = GetComponent<MeshRenderer>();
        meshCollider = GetComponent<MeshCollider>();
        Material myNewMaterial = new Material(Shader.Find("Standard"));
        meshRenderer.material = myNewMaterial;
        RunMarchingCubes();
        ConstructMesh();
        gameObject.GetComponent<MeshRenderer>().material.color = new Color(0.73f, 0.6f, 0.8f, 1);
        meshCollider.sharedMesh = meshFilter.mesh; 
        meshCollider.convex = true;
        meshCollider.isTrigger = true;
        volume = VolumeOfMesh(meshFilter.mesh);
    }

    List<int[,]> getBlueprint(List<int[,]> imageArrays)
    {
        int yStart = Mathf.FloorToInt((cellCoordinates[2]) * ReadImage.heightConv);
        int yEnd = Mathf.FloorToInt((cellCoordinates[3]) * ReadImage.heightConv);
        int xStart = Mathf.FloorToInt((cellCoordinates[0]) * ReadImage.heightConv);
        int xEnd = Mathf.FloorToInt((cellCoordinates[1]) * ReadImage.heightConv);

        int height = yEnd - yStart + 3;
        int width = xEnd - xStart + 3;

        List<int[,]> blueprintPlaceholder = new List<int[,]>();

        blueprintPlaceholder.Add(new int[height, width]);
        
        for (int z = this.cellCoordinates[4]; z <= this.cellCoordinates[5]; z++)
        {
            //Create array to store cube corner data
            int [,] micronCorners = new int[height, width];

            for (int y = 0; y < height - 2; y++)
            {
                for (int x = 0; x < width - 2; x++)
                {
                    if (imageArrays[z][Mathf.FloorToInt((y + yStart) / ReadImage.heightConv), 
                                       Mathf.FloorToInt((x + xStart) / ReadImage.widthConv)] == int.Parse(name))
                    {
                        micronCorners[y + 1, x + 1] = 1;
                    }
                    else 
                    {
                        micronCorners[y + 1, x + 1] = 0;
                    }
                }            
            }
            blueprintPlaceholder.Add(micronCorners);
        }

        blueprintPlaceholder.Add(new int[height, width]);

        return blueprintPlaceholder;
    }

    
    //Iterate through the blueprint and run Marching Cubes for each location
    void RunMarchingCubes()
    {
        int _configurationIndex = 0;

        for (int z = 0; z < blueprint.Count - 1; z++)
        {
            for (int y = 0; y < blueprint[0].GetLength(0) - 1; y++)
            {
                for (int x = 0; x < blueprint[0].GetLength(1) - 1; x++)
                {
                    _configurationIndex = MarchingCubes.GetCubeConfiguration(z, y, x, blueprint);
                    MarchingCubes.MarchCube(new Vector3(x + Mathf.FloorToInt((cellCoordinates[0]) * ReadImage.heightConv), 
                                                                                  y + Mathf.FloorToInt((cellCoordinates[2]) * ReadImage.heightConv), 
                                                                                  z + cellCoordinates[4]), _configurationIndex, vertices, triangles); 
                }
            }
        }
    }

    //Update mesh vertices and triangles
    void ConstructMesh()
    {
        mesh = new Mesh();
        mesh.vertices = vertices.ToArray();
        mesh.triangles = triangles.ToArray();
        mesh.RecalculateNormals();
        meshFilter.mesh = mesh;
    }

         public float SignedVolumeOfTriangle(Vector3 p1, Vector3 p2, Vector3 p3)
     {
         float v321 = p3.x * p2.y * p1.z;
         float v231 = p2.x * p3.y * p1.z;
         float v312 = p3.x * p1.y * p2.z;
         float v132 = p1.x * p3.y * p2.z;
         float v213 = p2.x * p1.y * p3.z;
         float v123 = p1.x * p2.y * p3.z;
 
         return (1.0f / 6.0f) * (-v321 + v231 + v312 - v132 - v213 + v123);
     }
 
     public float VolumeOfMesh(Mesh mesh)
     {
         float volume = 0;
 
         Vector3[] vertices = mesh.vertices;
         int[] triangles = mesh.triangles;
 
         for (int i = 0; i < triangles.Length; i += 3)
         {
             Vector3 p1 = vertices[triangles[i + 0]];
             Vector3 p2 = vertices[triangles[i + 1]];
             Vector3 p3 = vertices[triangles[i + 2]];
             volume += SignedVolumeOfTriangle(p1, p2, p3);
         }
         return Mathf.Abs(volume);
     }
}
