using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ApplicationGui : MonoBehaviour
{
    public Gradient gradientDepth;
    public Gradient gradientVolume;
    public Button continueButton;
    public void OnClick_Back()
    {
        continueButton.interactable = true;
        continueButton.transform.Find("Text").GetComponent<TextMeshProUGUI>().color = new Color(0.24f, 0.09f, 0.38f, 1);
        CanvasManager.ChangeCanvas(CanvasType.START_MENU, gameObject);
    }

    public void OnClick_ColorDifferently()
    {
        foreach(GameObject cell in CellGenerator.cellsList)
        {
              Color color = new Color(
                Random.Range(0f, 1f), 
                Random.Range(0f, 1f), 
                Random.Range(0f, 1f)
            );
            cell.GetComponent<MeshRenderer>().material.color = color;
        }
    }

    public void OnClick_ColorDepth()
    {
        float minZ = 100f; 
        float maxZ = -1f;
        
        foreach (GameObject cell in CellGenerator.cellsList)
        {
            if((float)cell.GetComponent<MeshFilter>().mesh.bounds.min[2] < minZ)
            {
                minZ = (float)cell.GetComponent<MeshFilter>().mesh.bounds.min[2];
            }
            if((float)cell.GetComponent<MeshFilter>().mesh.bounds.max[2] > maxZ)
            {
                maxZ = (float)cell.GetComponent<MeshFilter>().mesh.bounds.max[2];
            }
        }

        foreach(GameObject cell in CellGenerator.cellsList)
        {
            Color color = gradientDepth.Evaluate((cell.GetComponent<MeshFilter>().mesh.bounds.center[2]-minZ)/(maxZ - minZ));
            cell.GetComponent<MeshRenderer>().material.color = color;
        }
    }

    public void OnClick_ColorVolume()
    {
        float minVolume = 10000f; 
        float maxVolume = -1f;
        
        foreach (GameObject cell in CellGenerator.cellsList)
        {
            if(cell.GetComponent<Cell>().volume < minVolume)
            {
                minVolume = cell.GetComponent<Cell>().volume;
            }
            if(cell.GetComponent<Cell>().volume > maxVolume)
            {
                maxVolume = cell.GetComponent<Cell>().volume;
            }
        }

        foreach(GameObject cell in CellGenerator.cellsList)
        {
            Color color = gradientVolume.Evaluate((cell.GetComponent<Cell>().volume)/(maxVolume - minVolume));
            cell.GetComponent<MeshRenderer>().material.color = color;
        }
    }

    public void OnClick_ColorDefault()
    {
        foreach(GameObject cell in CellGenerator.cellsList)
        {
            cell.GetComponent<MeshRenderer>().material.color = new Color(0.73f, 0.6f, 0.8f, 1);
        }
    }


}
