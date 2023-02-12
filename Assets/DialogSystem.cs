using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogSystem : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI dialogsTextObject;
    [SerializeField] private TextAsset sampleDialogsCSV;
    public List<Dialog> dialogs = new List<Dialog>();
    Coroutine DialogSystemCoroutine;

    public static DialogSystem instance;
    private void Awake()
    {
        //Singleton Declaration
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }

        //Information Extraction
        ExtractDialogsFromCSV();
    }

    private void Start()
    {
        OpenDialogSystem();
    }

    void ExtractDialogsFromCSV()
    {
        string[] lines = sampleDialogsCSV.text.Split("\n"[0]);
        for (int i = 0; i < lines.Length; i++)
        {
            List<string> row = new List<string>();
            bool inQuotes = false;
            string currentValue = "";
            foreach (char c in lines[i])
            {
                if (c == '"')
                {
                    inQuotes = !inQuotes;
                }
                else if (c == ',' && !inQuotes)
                {
                    row.Add(currentValue);
                    currentValue = "";
                }
                else
                {
                    currentValue += c;
                }
            }
            row.Add(currentValue);
            var dialog = new Dialog(row);
            dialogs.Add(dialog);
        }
    }

    IEnumerator ShowDialogs()
    {
        //just to get sure that we start from zero thats why we set default value to -1
        int startDialog = PlayerPrefs.GetInt("lastPlayedDialogIndex", 1) - 1;
        for (int i = startDialog; i < dialogs.Count; i++)
        {
            SetDialog(dialogs[i]);
            yield return new WaitForSeconds(dialogs[0].DialogDuration);
        }
    }

    public void SetDialog(Dialog dialog)
    {
        dialogsTextObject.text = $"{dialog.Sayer}: {dialog.DialogText}";
        PlayerPrefs.SetInt("lastPlayedDialogIndex", dialog.DialogIndex);
    }

    public void OpenDialogSystem()
    {
        DialogSystemCoroutine = StartCoroutine(ShowDialogs());
        print("dialog system opened");

    }

    public void CloseDialogSystem()
    {
        StopCoroutine(DialogSystemCoroutine);
        print("dialog system closed");
    }
}
