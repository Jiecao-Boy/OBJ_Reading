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
    // Start is called before the first frame update
    void Start()
    {
        string readFromFilePath = Application.streamingAssetsPath +"/"+"teapot"+".obj";

        List<string> fileLines = File.ReadAllLines(readFromFilePath).ToList();

        Instantiate(recallTextObject, contentWindow);

        foreach (string line in fileLines)
        {
            recallTextObject.GetComponent<Text>().text += "\n" + line; 
        } 
    }


}
