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

        //CreditsSong.SetActive(true);
        SceneManager.LoadScene(SceneNumber);

    }

}
