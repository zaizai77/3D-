using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkButton : MonoBehaviour
{
    private GameObject tipsButton;
    [Header("对话提示")]
    public GameObject dialogBox;

    private void OnTriggerEnter(Collider other)
    {
        tipsButton = other.transform.Find("dialogTip").gameObject;
        tipsButton.SetActive(true);
    }

    private void OnTriggerExit(Collider other)
    {
        tipsButton.SetActive(false);
        dialogBox.SetActive(false);
    }

    private void Update()
    {
        if (tipsButton != null && tipsButton.activeSelf && Input.GetKeyDown(KeyCode.R))
        {
            dialogBox.SetActive(true);
        }
    }
}
