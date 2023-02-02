using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 
using System.IO;
using System.Linq;

public class FileReading : MonoBehaviour
{
    public Transform contentWindow;
    public GameObject recallTextObject; 

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
	private void setVertex (int i, int x, int y, int z) {
		vertices[i] = new Vector3(x, y, z);
	}

    private void CreateMesh()
    {
        string readFromFilePath = Application.streamingAssetsPath +"/"+"teapot"+".obj";

        List<string> fileLines = File.ReadAllLines(readFromFilePath).ToList();

        Instantiate(recallTextObject, contentWindow);

       //foreach (string line in fileLines)
        //{s
        //    recallTextObject.GetComponent<Text>().text += "\n" + line; 
        //} 


        
        //Try to parse the list
        char[] separator = {' '}; 
        int v = 0; 

        foreach (string line in fileLines)
        {
            //First split the string
            string[] splitArray = line.Split(separator);
            string head = splitArray[0];
            if (head == "v" && line.Length > 1){
                //Vector3 vertex = new Vector3(0,0,0); 
                setVertex(v++,0,0,0);
            }
            //if (head == "f"){

            //}
            mesh.vertices = vertices; 
            Debug.Log(vertices);
            recallTextObject.GetComponent<Text>().text += "\n"+ head;
            //foreach(char c in line){
                //new string:
                //StringBuilder sb = new StringBuilder();
                //append:
                //sb.append(c);
                //char.IsLetter(c) && sb.Length == 0;
                //char.IsNumber(c)


        }
    }

}



