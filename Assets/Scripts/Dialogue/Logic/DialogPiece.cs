using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogPiece
{
    public string ID;
    public Sprite image;
    [TextArea]
    public string text;
    public List<DialogOption> dialogOptions = new List<DialogOption>();
}
