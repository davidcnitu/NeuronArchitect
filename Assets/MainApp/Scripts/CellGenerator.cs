using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CellGenerator
{
    public static List<GameObject> cellsList = new List<GameObject>();
    static GameObject cameraRig;
    public static void generateCells()
    {
        ReadImage.readAllImagesFromFolder(NewProjectMenu.path, NewProjectMenu.pixelWidth, NewProjectMenu.pixelHeight);
        GameObject cells = new GameObject("Cells");
        for (int i = 0; i < ReadImage.cellNumbers.Count; i++)
        {
            cellsList.Add(new GameObject((ReadImage.cellNumbers[i]).ToString()));
            cellsList[i].AddComponent<Cell>();
            cellsList[i].AddComponent<MeshFilter>();
            cellsList[i].AddComponent<MeshRenderer>();
            cellsList[i].AddComponent<MeshCollider>();
            cellsList[i].transform.parent = cells.gameObject.transform;
        }

        cells.transform.Rotate(-90.0f, 0.0f, 0.0f, Space.World);
        cells.transform.localScale = new Vector3(1f, 1f, NewProjectMenu.gapBetweenImages/NewProjectMenu.samplingRate);

        cameraRig = GameObject.Find("CameraRig");
        cameraRig.transform.Translate((NewProjectMenu.micronWidth/NewProjectMenu.samplingRate)/2f, (ReadImage.imageList.Count*(NewProjectMenu.gapBetweenImages/NewProjectMenu.samplingRate))/2f, -(NewProjectMenu.micronWidth/NewProjectMenu.samplingRate)/2f, Space.World);
        cameraRig.transform.Find("Main Camera").gameObject.transform.Translate(0f, (NewProjectMenu.micronWidth/NewProjectMenu.samplingRate) + (ReadImage.imageList.Count*(NewProjectMenu.gapBetweenImages/NewProjectMenu.samplingRate))/2f, 0f, Space.World);
    }
}
