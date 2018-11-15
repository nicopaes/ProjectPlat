﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HidePlayer : MonoBehaviour {

	[SerializeField]
	public bool _playerPresence;
	[SerializeField]
	private bool _playerHidden = false;
	private Transform playerTranform;
	[SerializeField]
	private bool _fromTheRight;
	private Vector2 positionToGo;


	private void OnEnable()
    {
        PlayerComponent.ActionButton += Action;
    }
    private void OnDisable()
    {
        PlayerComponent.ActionButton -= Action;
    }
	void Update()
	{
		//se o player está escondido, atualiza o lado para qual o player deseja sair da caixa, baseado no input corrente
        //se não houver input, mantem a saida baseada no lado que ele entrou originalmente
        if(_playerHidden)
        {
            float xInput = playerTranform.GetComponent<PlayerComponent>().GetDirectionalInput().x;
            float tol = 0.3f;
            if(Mathf.Abs(xInput) >= tol)
            {
                _fromTheRight = xInput > 0 ? false : true;
                //equivalente a 
                //_fromTheRight = !(xInput > 0);
                //mas achei mais claro assim
            }
        }
	}
	void Action()
	{
        if(!this.GetComponent<PushObject>().holdingBox){
            if (_playerPresence && !_playerHidden)
            {
                positionToGo = new Vector2(this.transform.position.x - playerTranform.position.x, playerTranform.position.y);
                if (positionToGo.x < 0)
                {
                    _fromTheRight = true;
                }
                if (playerTranform.gameObject.layer != LayerMask.NameToLayer("Hide"))
                {
                    playerTranform.GetComponent<Collider2D>().enabled = false;
                    //em vez de desligar o componente, habilita o freeze
                    //playerTranform.GetComponent<PlayerComponent>().BlockPlayerMovement(true);
                    playerTranform.GetComponent<PlayerComponent>().enabled = false;
                    playerTranform.GetComponent<Controller2D>().enabled = false;
                    playerTranform.GetComponent<PlayerComponent>().anim.SetBool("hide", true);
                    playerTranform.gameObject.layer = LayerMask.NameToLayer("Hide");
                    playerTranform.position = new Vector3(this.transform.position.x, playerTranform.position.y, playerTranform.position.z);

                    _playerHidden = true;
                }

            }
            else if (_playerHidden)
            {
                GetComponent<Rigidbody2D>().velocity = Vector2.zero;

                playerTranform.GetComponent<Collider2D>().enabled = true;
                //em vez de religar o componente, desliga o block
                //playerTranform.GetComponent<PlayerComponent>().BlockPlayerMovement(false);
                playerTranform.GetComponent<PlayerComponent>().enabled = true;
                playerTranform.GetComponent<Controller2D>().enabled = true;
                playerTranform.gameObject.layer = LayerMask.NameToLayer("Player");
                playerTranform.GetComponent<PlayerComponent>().anim.SetBool("hide", false);
                playerTranform.gameObject.GetComponent<PlayerComponent>().anim.SetBool("onPush", false);                
                if (!_fromTheRight)
                {
                    playerTranform.position = new Vector3(this.transform.position.x, playerTranform.position.y, playerTranform.position.z)
                    +Vector3.right * 3.2f;
                }
                else
                {
                    playerTranform.position = new Vector3(this.transform.position.x, playerTranform.position.y, playerTranform.position.z)
                    +Vector3.left * 3.2f;
                }
                _playerHidden = false;
                _fromTheRight = false;
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            playerTranform = collision.transform;
            _playerPresence = true;
            collision.gameObject.GetComponent<PlayerComponent>().nearBox = this.gameObject;
            collision.gameObject.GetComponent<PlayerComponent>().anim.SetBool("onPush", true);
        }
    }
}
