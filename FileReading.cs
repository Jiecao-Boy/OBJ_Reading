using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 
using System.IO;
using System.Linq;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class FileReading : MonoBehaviour
{
	public int xSize, ySize, zSize;
	private Mesh mesh;
	private Vector3[] vertices;

    private void Awake () {
		Generate();
	}

	private void Generate () {
		GetComponent<MeshFilter>().mesh = mesh = new Mesh();
		mesh.name = "Procedural Cube";
		CreateMesh ();
    }

    // Start is called before the first frame update
	private void setVertex (int i, float x, float y, float z) {
		vertices[i] = new Vector3(x, y, z);
	}

    private void CreateMesh()
    {
        string readFromFilePath = Application.streamingAssetsPath +"/"+"teddy"+".obj";//replace with any obj file
        List<string> fileLines = File.ReadAllLines(readFromFilePath).ToList();


        //Parse the list
        char[] separator = {' '}; 
        int v = 0; 
        int f = 0; 
        //Determine the length of vertices and faces/triangles
        foreach (string line in fileLines)
        {
            //split the string
            string[] splitArray = line.Split(separator);
            string head = splitArray[0];
            if (head == "v" && line.Length > 1){
                v++;
            }
            if (head == "f" && line.Length > 1){
                f++;
            }
        }

        //Set up vertices
        vertices = new Vector3[v];
        int i = 0;
        foreach (string line in fileLines)
        {
            //split the string
            string[] splitArray = line.Split(separator);
            string head = splitArray[0];

            if (head == "v" && line.Length > 1){
                float x=float.Parse(splitArray[1]);
                float y=float.Parse(splitArray[2]);
                float z=float.Parse(splitArray[3]);
                setVertex(i++,x,y,z);
            }
        }
        mesh.vertices = vertices;

        //Set up faces/triangles
        //first have to determine the number of triangles\
        //here we make an assumple that one face only features one triangle
        Debug.Log(f);
        int[] triangles = new int[f*3];
        Debug.Log("so far so good");
        int j = 0; 
        foreach (string line in fileLines)
        {
            //split the string
            string[] splitArray = line.Split(separator);
            string head = splitArray[0];
            if (head == "f" && line.Length > 1){
                triangles[j*3] = int.Parse(splitArray[1])-1;
                triangles[j*3+1] = int.Parse(splitArray[2])-1;
                Debug.Log(splitArray[2]);
                Debug.Log(splitArray[3]);
                triangles[j*3+2] = int.Parse(splitArray[3])-1;
                j++;

                mesh.triangles = triangles;
            }
        }
        Debug.Log(j);

    }

    //draw gizmos to see if vertices are renderred correctly
	private void OnDrawGizmos () {
		if (vertices == null) {
			return;
		}
		for (int i = 0; i < vertices.Length; i++) {
			Gizmos.color = Color.black;
			Gizmos.DrawSphere(vertices[i], 0.1f);
		}
	}

}



