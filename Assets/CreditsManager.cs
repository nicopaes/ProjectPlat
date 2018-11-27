using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsManager : MonoBehaviour
{

    public int SceneNumber = 0;
    public GameObject CreditsSong;


    // Use this for initialization
    public void GoBack()
    {

        //limpa registro, impedindo que o próximo jogador possa pular cutscenes e diálogos:
        var pi = GameObject.FindObjectOfType<PersistentInfo>();
        if(pi != null) pi.Registry.Clear();
        
        //CreditsSong.SetActive(true);
        SceneManager.LoadScene(SceneNumber);

    }

}
