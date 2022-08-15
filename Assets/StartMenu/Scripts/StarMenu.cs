using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarMenu : MonoBehaviour
{
    public void OnClick_NewProject()
    {
        CanvasManager.ChangeCanvas(CanvasType.NEW_PROJECT_MENU, gameObject);
    }

    public void OnClick_Continue()
    {
        CanvasManager.ChangeCanvas(CanvasType.APPLICATION_GUI, gameObject);
    }

    public void OnClick_Quit()
    {
        Application.Quit();
    }
}
