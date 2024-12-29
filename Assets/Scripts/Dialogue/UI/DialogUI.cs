using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogUI : Singleton<DialogUI>
{
    [Header("Basic Elements")]
    public Image icon;
    public TextMeshProUGUI mainText;
    public Button nextButton;
    public GameObject dialogPanel;

    [Header("Data")]
    public DialogData_SO currentData;
    int currentIndex = 0; 

    public void UpdateDialogData(DialogData_SO data)
    {
        currentData = data;
        currentIndex = 0;
    }

    public void UpdateMainDialog(DialogPiece dialogPiece)
    {
        dialogPanel.SetActive(true);

        if(dialogPiece.image != null)
        {
            icon.enabled = true;
            icon.sprite = dialogPiece.image;
        } 
        else
        {
            icon.enabled = false;
        }

        mainText.text = "";
        mainText.text = dialogPiece.text;
    }
}
