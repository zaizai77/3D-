using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OptionUI : MonoBehaviour
{
    public TextMeshProUGUI optionText;
    private Button thisButton;
    private DialogPiece currentPiece;
    private string nextPieceID;

    private void Awake()
    {
        thisButton = GetComponent<Button>();
        thisButton.onClick.AddListener(OnOptionClicked);
    }

    public void UpdateOption(DialogPiece piece,DialogOption option)
    {
        currentPiece = piece;
        optionText.text = option.text;
        nextPieceID = option.targetID;
    }

    public void OnOptionClicked()
    {
        if(nextPieceID == "")
        {
            DialogUI.Instance.dialogPanel.SetActive(false);
            return;
        } 
        else
        {
            DialogUI.Instance.UpdateMainDialog(DialogUI.Instance.currentData.dialogIndex[nextPieceID]);
        }
    }
}
