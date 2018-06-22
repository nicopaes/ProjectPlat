using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// This class will manage all the text tha appears in the bubble text
public class ManageText : MonoBehaviour
{
    public RectTransform BubbleBox;
    public Text BubbleText;

    public float OffSetWidth;
    public float OffSetHeight;

    public float MaxTextWidth;

    [Header("Dialog Documents")]
    public TextAsset[] DialogTexts;

   
    [System.Serializable]
    public struct Characters
    {
        public GameObject character;
        public string codeInDocument;
    }

    [Header("Characters")]
    public Characters[] TalkableCharacters;

    [HideInInspector]
    public int currentText = 0;              // o texto atual que está lendo
    [HideInInspector]
    public int currentDialogLine = 0;        // a frase no texto que se está lendo
    [HideInInspector]
    public bool endedBubble;
    [HideInInspector]
    public bool endedDialog;

    private string newText;
    private int totalTextWidth = 0;
    private int totalTextHeight = 0;

    private DialogControl dialogController;
    
    private List<GameObject> currentSpeakers = new List<GameObject>();       // adiciona conforme ve escrito no texto o nome do character
    private List<string> currentLines = new List<string>();                  // adiciona conforme ve escrito no texto o nome do character


    // falta ajeitar a funcao presentBubbleText pra não bugar e fazer um jeito de triggar a função, also textar com mais de um script

    void Start()
    {
        //
        FindSpeaker(DialogTexts[currentText]);
        dialogController = this.GetComponent<DialogControl>();
        dialogController.showSpeechBallon(currentSpeakers[currentDialogLine]);
        endedBubble = true; // nem isso
        endedDialog = true; // isso não deve acontecer aqui

    }

    private void Update() {

       if (endedBubble)
       {
            if (currentDialogLine < currentLines.Count && endedDialog)
            {
                dialogController.showSpeechBallon(currentSpeakers[currentDialogLine]);
                presentBubbleText();
                currentDialogLine += 1;
            } else
            {
                endedDialog = false;
                currentDialogLine = 0;
                currentText += 1;
                currentSpeakers.Clear();
                currentLines.Clear();
            }
       }

    }

    // adjust de bubble with the text
    private void presentBubbleText()
    {
        if (currentLines[currentDialogLine].Length != 0)
        {
            print(currentLines[currentDialogLine]);
            endedBubble = false;

            CalculateWidthOfMessage(currentLines[currentDialogLine]);

            BubbleText.text = newText;
            totalTextHeight = (int)(BubbleText.preferredHeight);

            if (totalTextWidth < MaxTextWidth)
            {
                BubbleBox.sizeDelta = new Vector2(totalTextWidth + OffSetWidth, totalTextHeight + OffSetHeight);
            }
            else
            {
                BubbleBox.sizeDelta = new Vector2(MaxTextWidth + OffSetWidth, totalTextHeight + OffSetHeight);
            }
        }
    }

    // Organize the speakers and their lines in two arrays
    private void FindSpeaker(TextAsset text)
    {
        string[] linesWithCharacters;

        char[] arr = text.text.ToCharArray();
        List<char> dialogLine = new List<char>();
        bool willSearchdialog = false;
        for (int i=0;i<arr.Length;i++)
        {
            char currentLetter = arr[i];

            if (currentLetter == ' ')
            {
                if (!willSearchdialog)
                {
                    dialogLine.Add(currentLetter);
                }

            } else
            {
                if (willSearchdialog)
                {
                    willSearchdialog = false;
                }
                dialogLine.Add(currentLetter);
            }

            // get the speaker
            if (currentLetter == ':')
            {
                dialogLine.Remove(currentLetter);

                // Check if not the first speaker
                if (dialogLine.Contains('\n'))
                {
                    dialogLine.Remove('\n');
                    dialogLine.RemoveAt(0);
                } 

                foreach (Characters speaker in TalkableCharacters)
                {
                    if (speaker.codeInDocument == new string(dialogLine.ToArray()))
                    {
                        currentSpeakers.Add(speaker.character);
                        break;
                    }
                }

                dialogLine.Clear();
                willSearchdialog = true;
            }

            // get the line
            if (currentLetter == ';')
            { 
                dialogLine.Remove(currentLetter);
                currentLines.Add(new string(dialogLine.ToArray()));

                dialogLine.Clear();
            }

        }


        }

    // Todo -- separar por palavra e não letra, se uma palavra for maior que todo o texto, separar a palavra

    // Calculates the Width of the text
    private void CalculateWidthOfMessage(string message)
    {
        Font myFont = BubbleText.font;  //chatText is my Text component
        CharacterInfo characterInfo = new CharacterInfo();
        int lineText = 0;

        List<char> lst = new List<char>();

        char[] arr = message.ToCharArray();
        foreach (char c in arr) { lst.Add(c); }

        for (int i = 0; i < lst.Count; i++)
        {
            myFont.GetCharacterInfo(lst[i], out characterInfo, BubbleText.fontSize);
            totalTextWidth += characterInfo.advance;
            lineText += characterInfo.advance;

            // Don't let the text pass it's maximum
            if (lineText > MaxTextWidth)
            {
                lineText = 0;
                lst.Insert(i, '\n');
            }
        }

        string txt = new string(lst.ToArray());
        newText = txt;
    }
}
