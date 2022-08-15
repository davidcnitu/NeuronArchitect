using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public static class ReadImage
{
    //********** VARIABLE DEFINITION **********\\
    //Conversion factor pixel to micrometers
    public static float heightConv = (NewProjectMenu.micronHeight/NewProjectMenu.samplingRate)/NewProjectMenu.pixelHeight;
    public static float widthConv = (NewProjectMenu.micronWidth/NewProjectMenu.samplingRate)/NewProjectMenu.pixelWidth;
    public static List<int> cellNumbers = new List<int>();
    public static List<int[,]> imageList = new List<int[,]>();
    public static Dictionary<string, int[]> cellsCoordinates = new Dictionary<string, int[]>();
    // public static List<GameObject> cellsList;

    //********** METHOD DEFINITION **********\\

    //Read 16bit binary file into 2D integer array and save array into image list. At the same time find coordinates of each different cell
    public static void readAllImagesFromFolder(string path, float width, float height)
    {

        //********** VARIABLE DEFINITION **********\\
        //Creat variable to keep track of image number 
        int z = 0;
        //Create variable to store key
        string key = "";

        //********** FUNCTIONALITY **********\\

        //Get the path of each file in the folder
        var fileNames = Directory.EnumerateFiles(path);

        foreach (string file in fileNames)
        {
            //Create int array for storing image data
            int[,] image = new int[(int)width, (int)height];
            //Read data from binary file and store to integer array
            byte[] byteData = System.IO.File.ReadAllBytes(file);
            // Create index to track byte number
            int index = 0;

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    //Convert bytes to int
                    image[y, x] = BitConverter.ToInt16(byteData, index);

                    //Get key name
                    key = image[y, x].ToString();
                    
                    //Add cell to dictionary or update coordinates
                    if (!cellsCoordinates.ContainsKey(key) && key != "0")
                    {
                        cellsCoordinates.Add(key, new int[6] {x, x, y, y, z, z});
                    }
                    else if (key != "0")
                    {
                        updateCellCoordinates(x, y, z, cellsCoordinates, key);
                    }
                    
                    if (!cellNumbers.Contains(image[y, x]) && image[y,x] != 0)
                    {
                        cellNumbers.Add(image[y,x]);
                    }
                    
                    //Move to the next 2 bytes. We read every two bytes because our data format is INT16
                    index+=2;
                }
            }
            //Add image int array to the list
            imageList.Add(image);
            //Increment number of images
            z++;
        }
    }

    //Check cell bounds and update them
    private static void updateCellCoordinates(int _x, int _y, int _z, Dictionary<string, int[]> _cellsCoordinates, string _key)
    {
        if (_z > _cellsCoordinates[_key][5])
        {
            _cellsCoordinates[_key][5] =  _z;
        }
        if (_y > _cellsCoordinates[_key][3])
        {
            _cellsCoordinates[_key][3] = _y;
        }
        if (_y < _cellsCoordinates[_key][2])
        {
            _cellsCoordinates[_key][2] = _y;
        }
        if (_x > _cellsCoordinates[_key][1])
        {
            _cellsCoordinates[_key][1] = _x;
        }
        if (_x < _cellsCoordinates[_key][0])
        {
            _cellsCoordinates[_key][0] = _x;
        }
    }
}
