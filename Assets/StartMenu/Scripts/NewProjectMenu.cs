
using System.Collections;
using FileBrowserPackage.Windows;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class NewProjectMenu : MonoBehaviour
{
    public TMP_InputField pixelWidthInput, pixelHeightInput, micronWidthInput, micronHeightInput, gapBetweenImagesInput, samplingRateInput, pathInput;
    public TextMeshProUGUI warning;
    public GameObject overView;
    public Button newProjectButton;

    public static string path;
    public static float pixelWidth, pixelHeight, micronWidth, micronHeight, gapBetweenImages, samplingRate; //Example: 100f, 100f, 69.53125f, 69.53125f, 2f

    public void OnClick_ChooseFolder()
    {
        path = new FileBrowser().OpenFolderBrowser();
        pathInput.text = path;
    }

    public void OnClick_Render()
    {
        UpdateVariables();
        if (pixelWidth != 0 && pixelHeight != 0 && micronWidth != 0 && micronHeight != 0 && gapBetweenImages != 0 && samplingRate !=0 && path != null)
        {
            warning.text = "";
            CellGenerator.generateCells();
            CanvasManager.ChangeCanvas(CanvasType.APPLICATION_GUI, gameObject);
        }
        else 
        {
            warning.text = "**All fields must be filled before rendering!**";
        }
        updateOverviewData();

        newProjectButton.interactable = false;
        newProjectButton.transform.Find("Text").GetComponent<TextMeshProUGUI>().color = new Color(0.36f, 0.36f, 0.36f, 1);            
    }

    void updateOverviewData()
    {
        TextMeshProUGUI overviewContent = overView.transform.Find("Content").GetComponent<TextMeshProUGUI>();
        string content = "";
        content += "Number of cells: " + ReadImage.cellNumbers.Count + "\n\n";
        content += "FOV dimensions (micrometers):\n\n";
        content += "X: " + (NewProjectMenu.micronHeight) + "\n\n";
        content += "Y: " + (NewProjectMenu.micronWidth) + "\n\n";
        content += "Z: " + ReadImage.imageList.Count*NewProjectMenu.gapBetweenImages + "\n\n";
        content += "Number of slides: Z: " + ReadImage.imageList.Count;
        overviewContent.text = content;
    }

    public void OnClick_Back()
    {
        CanvasManager.ChangeCanvas(CanvasType.START_MENU, gameObject);
    }

    void UpdateVariables()
    {
        float samplingRateValue;
        float.TryParse(pixelWidthInput.text, out pixelWidth);
        float.TryParse(pixelHeightInput.text, out pixelHeight);
        float.TryParse(micronWidthInput.text, out micronWidth);
        float.TryParse(micronHeightInput.text, out micronHeight);
        float.TryParse(gapBetweenImagesInput.text, out gapBetweenImages);
        float.TryParse(samplingRateInput.text, out samplingRateValue); 
        samplingRate = gapBetweenImages/samplingRateValue;
        path = pathInput.text;  
    }
}
