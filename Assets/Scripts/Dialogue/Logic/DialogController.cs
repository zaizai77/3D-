using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogController : MonoBehaviour
{
    public DialogData_SO currentData;
    bool canTalk = false;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") && currentData != null)
        {
            Debug.Log("wanjia ");
            canTalk = true;
        }   
    }

    private void Update()
    {
        if(canTalk && Input.GetMouseButtonDown(1))
        {
            OpenDialog();
        }
    }

    void OpenDialog()
    {
        //打开UI面板
        //传输对话内容信息
        DialogUI.Instance.UpdateDialogData(currentData);
        DialogUI.Instance.UpdateMainDialog(currentData.dialogPieces[0]);
    }
}
