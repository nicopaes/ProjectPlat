using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;

public class CameraSelection : MonoBehaviour {

    public GameObject ThisCamera;

    public Animator LookAhead;
    public GameObject AlternativeAnim;
    public bool IsAnimated;

    private Animator tcAnim;
    private Animator altAnim;

    private string tcName;
    private string altName;

    private playerKeyBindings pKeys;

    private PersistentInfo pi;
    
    private StateMachineBehaviour[] smb;
    private StateMachineBehaviour[] altsmb;

    private CinemachineBrain cmb;
    private CinemachineVirtualCamera previousCamera;

    private bool _alreadyPlayedInThisLife;

    void Start()
    {
        if(ThisCamera)
        {
            tcAnim = ThisCamera.GetComponent<Animator>();
            if(tcAnim)
            {
                tcName = SceneManager.GetActiveScene().name + tcAnim.ToString();
            }
        }
        if(AlternativeAnim)
        {
            altAnim = AlternativeAnim.GetComponent<Animator>();
            if(altAnim)
            {
                altName = SceneManager.GetActiveScene().name + altAnim.ToString();
            }
        }
        
        pi = GameObject.FindObjectOfType<PersistentInfo>();
        pKeys = GameObject.FindObjectOfType<InputController>().GetPKeys();
        cmb = GameObject.FindObjectOfType<CinemachineBrain>();
        _alreadyPlayedInThisLife = false;
    }

    void Update()
    {
        //habilita pular a animação se o jogador já a tiver visto antes:
        //estou contando com curto circuito pra não avaliar todo frama registry.contains, o que poderia ser relativamente lento
        //não deve acontecer de o .Contains ser avaliado muitas vezes, já que ou tc.enabled == true e potencialmente cai no loop, 
        //ou tc.enable == false e curto circuito
        Debug.Log(tcAnim);
        if(tcAnim && tcAnim.enabled && Input.GetKeyDown(pKeys.jumpKey.ToLower()) && pi.Registry.Contains(tcName))
        {
            tcAnim.enabled = false;
            //dá prioridade à camera anterior
            previousCamera.Priority = 50;
            previousCamera.MoveToTopOfPrioritySubqueue();
        }

        //o mesmo vale para o outro animator
        if(altAnim && altAnim.enabled && Input.GetKeyDown(pKeys.jumpKey.ToLower()) && pi.Registry.Contains(altName))
        {
            altAnim.enabled = false;
            //dá prioridade à camera anterior
            previousCamera.Priority = 50;
            previousCamera.MoveToTopOfPrioritySubqueue();
        }

        //ps: aqui botei que a tecla para pular a animação é a mesma para pular com a Isis (no pun intended)
        

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.tag == "Player" && !_alreadyPlayedInThisLife)
        {
            _alreadyPlayedInThisLife = true;
            Debug.LogWarning("Selected");

            previousCamera = null;
            if(cmb.ActiveVirtualCamera != null)
            {
                previousCamera =  cmb.ActiveVirtualCamera.VirtualCameraGameObject.GetComponent<CinemachineVirtualCamera>();
                Debug.LogWarning(previousCamera);
            }

            ThisCamera.GetComponent<Cinemachine.CinemachineVirtualCamera>().Priority = 50;
            ThisCamera.GetComponent<Cinemachine.CinemachineVirtualCamera>().MoveToTopOfPrioritySubqueue();


            if (IsAnimated)
            {
                //se tem o animator, e essa animação não está registrada como já tocada, 
                if (tcAnim != null)
                {
                    tcAnim.enabled = true;
                    Debug.Log("existe um animator na minha camera, ou seja, ela faz um look ahead");

                    
                    StartCoroutine(MovementBlocker(tcAnim));

                }
                
                if(AlternativeAnim)
                {
                    string animName = SceneManager.GetActiveScene().name + altAnim.ToString();
                    //se tem o animator, e essa animação não está registrada como já tocada,
                    if (altAnim != null)
                    {
                        altAnim.enabled = true;
                        Debug.Log("existe um animator na minha camera, ou seja, ela faz um look ahead");
                        
                        StartCoroutine(MovementBlocker(altAnim));
                    }
                }
            }
        }
    }

    IEnumerator  MovementBlocker(Animator anim)
    {
        GameObject.FindObjectOfType<PlayerComponent>().BlockPlayerMovement(true);
        while(true)
        {
            if(!anim.GetCurrentAnimatorStateInfo(0).IsTag("End") && anim.enabled)
            {
                GameObject.FindObjectOfType<PlayerComponent>().BlockPlayerMovement(true);
                yield return new WaitForSeconds(0.1f);
            }
            else
            {
                Debug.LogWarning("end coroutine");
                GameObject.FindObjectOfType<PlayerComponent>().BlockPlayerMovement(false);

                //registra que eu já toquei essa animação, se já não estiver registrado
                string name = SceneManager.GetActiveScene().name + anim.ToString();
                if(!pi.Registry.Contains(name))
                {
                    Debug.LogWarning("added" + name);
                    pi.Registry.Add(name);
                }

                //disable animator?

                yield break;
            }
        }
        
    }

}
