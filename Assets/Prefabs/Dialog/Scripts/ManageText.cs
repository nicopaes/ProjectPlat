using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

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

    private PersistentInfo pi;

    private string dialogIDString;

    [Range(0.0f, 1.0f)]
    public float SFXVolume;
    public AudioSource Template;
    public AudioClip NextLine;
    public Transform AudioListener;


    [System.Serializable]
    public struct Characters
    {
        public GameObject character;
        public string codeInDocument;
    }

    [Header("Characters")]
    public Characters[] TalkableCharacters;

    [HideInInspector]
    public int currentText = 0;              
    [HideInInspector]
    public int currentDialogLine = 0;    
    [HideInInspector]
    public bool endedBubble;
    [HideInInspector]
    public bool DialogHasNotEnded;

    private bool _playerMovementBlocked;

    private string newText;
    private int totalTextWidth = 0;
    private int totalTextHeight = 0;

    private DialogControl dialogController;
    
    private List<GameObject> currentSpeakers = new List<GameObject>(); 
    private List<string> currentLines = new List<string>();

    private PlayerComponent _player;    

    [Header("Função chamada no inicio do diálogo:")]
    public UnityEvent Event_inicio;

    [Header("Função chamada ao término do diálogo:")]
    public UnityEvent Event;

    private playerKeyBindings pKeys;

    void Start()
    {
        dialogController = this.GetComponent<DialogControl>();
        _player = GameObject.FindObjectOfType<PlayerComponent>();
        _playerMovementBlocked = false;
        pi = GameObject.FindObjectOfType<PersistentInfo>();
        dialogIDString = SceneManager.GetActiveScene().name + this.gameObject.ToString();
        pKeys = GameObject.FindObjectOfType<InputController>().GetPKeys();
    }


    private void Update() {

       //se ainda não acabou este dialogo e ele já ta registrado, permite pular o diálogo
       
       if(DialogHasNotEnded && Input.GetKeyDown(pKeys.jumpKey.ToLower()) && pi.Registry.Contains(dialogIDString))
       {
           Debug.LogWarning("if1");
           //pular o diálogo:
           //seta linha atual para última:
           currentDialogLine = currentLines.Count;
           //"aperta E": apertar E => callback(CheckCommand) => Exit()
           this.gameObject.GetComponent<Begin>().OnDialogEnd();

       }
       
       if (endedBubble)
       {
            Debug.LogWarning("if (endedBubble)");
            if (currentDialogLine < currentLines.Count && DialogHasNotEnded)
            {
                Debug.LogWarning("if (currentDialogLine < currentLines.Count && DialogHasNotEnded)");
                if (currentLines[currentDialogLine].Length != 0)
                {
                    Debug.LogWarning("if (currentLines[currentDialogLine].Length != 0)");
                    dialogController.showSpeechBallon(currentSpeakers[currentDialogLine]);
                    presentBubbleText();
                    currentDialogLine += 1;
                    totalTextWidth = 0;

                    //Fazer  barulho
                    //PlayClipAt(NextLine, AudioListener.position);


                }

            } else
            {
                Debug.LogWarning("else");
                //se, quando acabou o diálogo, eu ainda não tiver desbloqueado o movimento, desbloqueio
                //além disso, aciono o evento Event
                if(_playerMovementBlocked)
                {
                    _player.BlockPlayerMovement(false);
                    _playerMovementBlocked = false;
                    Event.Invoke();
                }

                //quando acabou o diálogo, registra que esse diálogo já foi visto, se ainda não tiver registrado
                if(!pi.Registry.Contains(dialogIDString))
                {
                    pi.Registry.Add(dialogIDString);
                }
                
                DialogHasNotEnded = false;
                currentDialogLine = 0;
                totalTextWidth = 0;
                currentSpeakers.Clear();
                currentLines.Clear();
            }
       }

    }

    public void startDialog(int number)
    {
        currentText = number - 1;
        FindSpeaker(DialogTexts[currentText]);
        //Debug.Log(currentSpeakers[0].name);

        //Debug.Log(currentSpeakers[1].name);
        endedBubble = true; 
        DialogHasNotEnded = true; 

        //no início de qualquer dialogo, bloqueia movimento do player
        _player.BlockPlayerMovement(true);
        _playerMovementBlocked = true;

        //aciona evento no inicio do dialogo
        Event_inicio.Invoke();

    }


    // adjust de bubble with the text
    private void presentBubbleText()
    {
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

    // Organize the speakers and their lines in two arrays
    private void FindSpeaker(TextAsset text)
    {
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
                string textSpeaker = new string(dialogLine.ToArray());

                // Check if not the first speaker
                if (dialogLine.Contains('\n'))
                {
                    textSpeaker = Regex.Replace(textSpeaker, "[^\\w\\.]", "");

                    //dialogLine.Remove('\n');
                    //dialogLine.RemoveAt(0);
                }

                foreach (Characters speaker in TalkableCharacters)
                {
                    if (speaker.codeInDocument == textSpeaker)
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
        // voltar até o primeiro espaço e jogar a palavra pra baixo

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

               // print("entrei");
                //print(MaxTextWidth);
                //print(lineText);
                int j = i-1;
                if (lst[i-1] != ' ')
                {

                    for (; j >= 0; j--)
                    {
                        if(lst[j] == ' ')
                        {
                            break;
                        }
                    }

                    if(j == 0)
                    {
                        j = i-1;
                        lst.Insert(j, '-');
                    }

                } 

                lineText = 0;
                lst.Insert(j, '\n');
            }
        }

        string txt = new string(lst.ToArray());
        newText = txt;
    }


    public AudioSource PlayClipAt(AudioClip clip, Vector3 pos)
    {
        GameObject tempGO = new GameObject("TempAudio"); // create the temp object
        tempGO.transform.position = pos; // set its position
        AudioSource aSource = tempGO.AddComponent<AudioSource>(); // add an audio source
        aSource.clip = clip; // define the clip


        aSource.outputAudioMixerGroup = Template.outputAudioMixerGroup;
        aSource.volume = SFXVolume;


        aSource.Play(); // start the sound
        Destroy(tempGO, clip.length); // destroy object after clip duration
        return aSource; // return the AudioSource reference
    }


}
