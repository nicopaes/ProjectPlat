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

    public TextAsset[] DialogTexts;

    [System.Serializable]
    public struct Characters
    {
        public GameObject character;
        public string code;
    }
    public Characters[] TalkableCharacters;

    private string newText;
    private int totalTextWidth = 0;
    private int totalTextHeight = 0;

    private DialogControl dialogController;

    private int currentDialog = 0;              // o texto atual que está lendo
    private int currentDialogLine = 0;          // a frase no texto que se está lendo
    private GameObject[] currentSpeakers;       // adiciona conforme ve escrito no texto o nome do character

    // criar um script em um gameObject fora do jogo que recebe todos os documentos de texto do jogo, ele libera pra ler um documento de cada vez, que é triguerado pelo player
    // ele vai pegar as falas e separar ela em dois arrays, um é o de falas, na ordem que elas aparecem e outro são com os personagens, repetindo mesmo, na ordem que eles falam
    // SÓ QUE antes de colocar os personagens que falam ele precisa achar o personagem que está falando, como? -- com uma lista enorme de todos os personagens que falam? tags?
    // ae ele vai liberar as falas aos poucos conforme a pessoa aperta o teclado e vai acionar a acao do item do balão pra trocar e ir até o target da fala

        // falta fazer a função que entende as pessoas que falam e as falas, 

    void Start()
    {
        //

        dialogController = this.GetComponent<DialogControl>();
        //dialogController.showSpeechBallon(currentSpeakers[currentDialog]);        -- é assim que tem que ser
        dialogController.showSpeechBallon(TalkableCharacters[currentDialogLine].character);
        BubbleText.text = DialogTexts[currentDialog].text;

    }

    private void Update() {

       if (totalTextWidth == 0)
        {
            newText = CalculateWidthOfMessage(BubbleText.text);
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

    // Todo -- separar por palavra e não letra

    // Calculates the Width of the text
    private string CalculateWidthOfMessage(string message)
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
                lst.Insert(i ,'\n');
            }
        }
        string txt = new string(lst.ToArray());
        newText = txt;

        return newText;

    }
}
