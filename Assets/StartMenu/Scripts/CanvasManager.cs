using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CanvasManager
 {
    public static bool IsInitialised { get; private set; }
    public static GameObject startMenu, newProjectMenu, applicationGui;
    public static void Initialise() 
    {
        GameObject canvas = GameObject.Find("Canvas");
        startMenu = canvas.transform.Find("StartMenu").gameObject;
        newProjectMenu = canvas.transform.Find("NewProjectMenu").gameObject;
        applicationGui = canvas.transform.Find("ApplicationGui").gameObject;
        IsInitialised = true;
    }

    public static void ChangeCanvas(CanvasType _canvasType, GameObject _callingMenu)
    {
        if (!IsInitialised)
            Initialise();
        switch ( _canvasType)
        {
            case CanvasType.START_MENU:
                startMenu.SetActive(true);
                break;
            case CanvasType.NEW_PROJECT_MENU:
                newProjectMenu.SetActive(true);
                break;
            case CanvasType.APPLICATION_GUI:
                applicationGui.SetActive(true);
                break;
        }

        _callingMenu.SetActive(false);
    }
 }
