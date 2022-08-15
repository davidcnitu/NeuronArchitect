using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ClickCell : MonoBehaviour
{
    RaycastHit hit;
    public GameObject overView;
    public GameObject cellView;
    public static bool overViewActive = true;
    RaycastHit previousHit;
    Color previousHitColor;
    public static bool firstHit = true;

    void Update()
    {
        if (overViewActive)
        {
            overView.SetActive(true);
            cellView.SetActive(false);
        }
        if (Input.GetMouseButtonDown(0)) 
        {
             Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
             
             if (Physics.Raycast(ray, out hit)) 
             {
                if(!firstHit)
                {
                    previousHit.transform.gameObject.GetComponent<MeshRenderer>().material.color = previousHitColor;
                    previousHit = hit;
                    previousHitColor = hit.transform.gameObject.GetComponent<MeshRenderer>().material.color;
                    UpdateCellViewContent(hit);
                    cellView.SetActive(true);
                    overView.SetActive(false);
                    overViewActive = false;
                }
                else 
                {
                    previousHit = hit;
                    previousHitColor = hit.transform.gameObject.GetComponent<MeshRenderer>().material.color;
                    UpdateCellViewContent(hit);
                    cellView.SetActive(true);
                    overView.SetActive(false);
                    overViewActive = false;
                    firstHit = false;
                }
                
             }
             else if (!overViewActive)
             {
                previousHit.transform.gameObject.GetComponent<MeshRenderer>().material.color = previousHitColor;
                overView.SetActive(true);
                cellView.SetActive(false);

             }
        }   
    }

    void UpdateCellViewContent(RaycastHit _hit)
    {
        TextMeshProUGUI cellTitle = cellView.transform.Find("Title").GetComponent<TextMeshProUGUI>();
        cellTitle.text = "Cell " + _hit.transform.name;
        _hit.transform.gameObject.GetComponent<MeshRenderer>().material.color = new Color(0.24f, 0.09f, 0.38f, 1);
        TextMeshProUGUI cellContent = cellView.transform.Find("Content").GetComponent<TextMeshProUGUI>();
        string content = "";
        Vector3 center = _hit.transform.GetComponent<MeshFilter>().mesh.bounds.center*NewProjectMenu.samplingRate;
        Vector3 max = _hit.transform.GetComponent<MeshFilter>().mesh.bounds.max*NewProjectMenu.samplingRate;
        Vector3 min = _hit.transform.GetComponent<MeshFilter>().mesh.bounds.min*NewProjectMenu.samplingRate;
        double volume = System.Math.Round((double)_hit.transform.GetComponent<Cell>().volume, 2);
        content += "Cell center: " + "\nX: " +  (float)center[0] + "\n";
        content += "             " + "\nY: " +  (float)center[1] + "\n";
        content += "             " + "\nZ: " +   Mathf.Abs((float)center[2]-ReadImage.imageList.Count)*NewProjectMenu.gapBetweenImages + "\n";
        content += "\nCell bounds: " + "\nX: " +  (float)min[0] + " - " + (float)max[0] + "\n";
        content += "             " + "\nY: " +  (float)min[1] + " - " + (float)max[1] + "\n";
        content += "             " + "\nZ: " +  Mathf.Abs((float)max[2]-ReadImage.imageList.Count)*NewProjectMenu.gapBetweenImages + " - " + Mathf.Abs((float)min[2]-ReadImage.imageList.Count)*NewProjectMenu.gapBetweenImages + "\n";
        content += "\nCell volume: " + volume/Mathf.Pow((NewProjectMenu.gapBetweenImages/NewProjectMenu.samplingRate), 2) + " cubic micrometers\n";
        cellContent.text = content;
    }
}
