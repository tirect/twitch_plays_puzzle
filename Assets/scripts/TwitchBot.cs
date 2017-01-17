using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TwitchIRC))]
public class TwitchBot : MonoBehaviour {

	private TwitchIRC IRC;

	void OnChatMsgRecieved(string msg){
		Debug.Log(msg);
	}

	// Use this for initialization
	void Start () {
		IRC = this.GetComponent<TwitchIRC>();
		IRC.messageRecievedEvent.AddListener(OnChatMsgRecieved);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
