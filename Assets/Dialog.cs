using System.Collections.Generic;

[System.Serializable]
public class Dialog
{
    private string sayer, dialogText;
    private int dialogIndex, dialogDuration;

    public string Sayer {
        get { return sayer; }
        set { sayer = value; }
    }

    public string DialogText
    {
        get { return dialogText; }
        set { dialogText = value; }
    }

    public int DialogIndex
    {
        get { return dialogIndex; }
        set { dialogIndex = value; }
    }

    public int DialogDuration
    {
        get { return dialogDuration; }
        set { dialogDuration = value; }
    }

    //Constructor
    public Dialog(List<string> dialogInfoLine)
    {
        CreateNewDialog(dialogInfoLine);
    }

    public void CreateNewDialog(List<string> dialogInfoLine)
    {
        DialogIndex = int.Parse(dialogInfoLine[0]);
        Sayer = dialogInfoLine[1];
        DialogText = dialogInfoLine[2];
        DialogDuration = int.Parse(dialogInfoLine[3]);
    }

}
