using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(TwitchIRC))]
public class TwitchBot : MonoBehaviour {
	public List<GameObject> free_tiles;
	private List<GameObject> _used_tiles;

	private TwitchIRC _irc;
	private Dictionary<string, int> _users;

	void OnChatMsgRecieved(string msg) {
		var msgIndex = msg.IndexOf("PRIVMSG #", System.StringComparison.Ordinal);
		var msgString = msg.Substring(msgIndex + _irc.channelName.Length + 11);
		var user = msg.Substring(1, msg.IndexOf('!') - 1);

		ParseMessage(msgString, user);
	}

	// Use this for initialization
	void Start() {
		_irc = this.GetComponent<TwitchIRC>();
		_irc.messageRecievedEvent.AddListener(OnChatMsgRecieved);

		_users = new Dictionary<string, int>();
		_used_tiles = new List<GameObject>();
	}

	// Update is called once per frame
	void Update() {

	}

	GameObject GetTile(string username) {
		if (_users.ContainsKey(username)) {
			var tile_id = _users[username];
			return _used_tiles[tile_id];
		}
		return null;
	}

	bool ParseTileCommand(char command, string username) {
		if (_users.ContainsKey(username)) {
			var tile = GetTile(username);
			var offset = 1.5f;
			switch (command) {
				case 'w':
					tile.transform.position += new Vector3(0, offset, 0);
					break;
				case 'a':
					tile.transform.position += new Vector3(-offset, 0, 0);
					break;
				case 's': 
					tile.transform.position += new Vector3(0, -offset, 0);
					break;
				case 'd': 
					tile.transform.position += new Vector3(offset, 0, 0);
					break;
				case 'e':
					break;
				case 'q':
					break;
				case 'z':
					break;
				case 'x':
					break;
				case 'c':
					break;
				case 't':
					break;
				case 'm':
					break;
				default:
					return false;
			}
			return true;
		}
		return false;
	}

	void ParseMessage(string cmd, string username) {
		if (cmd.StartsWith("!", System.StringComparison.CurrentCulture)) {
			cmd = cmd.TrimStart('!');
			if (cmd == "join") {
				JoinUser(username);
			} else if (cmd == "leave") {
				LeaveUser(username);
			} else {
				for (int i = 0; i < cmd.Length; ++i) {
					var c = cmd[i];
					Debug.Log(c);
					ParseTileCommand(c, username);
				}
			}
		}
	}

	void JoinUser(string username) {
		if (!_users.ContainsKey(username)) {
			Debug.Log("joining " + username);
			var tile_id = Random.Range(0, free_tiles.Count - 1);
			var tile = free_tiles[tile_id];
			free_tiles.RemoveAt(tile_id);

			var text = tile.GetComponentInChildren<Text>() as Text;
			text.enabled = true;
			text.text = username;

			_users.Add(username, _used_tiles.Count);
			_used_tiles.Add(tile);
		}
	}

	void LeaveUser(string username) {
		if (_users.ContainsKey(username)) {
			var tile_id = _users[username];
			var tile = _used_tiles[tile_id];
			_used_tiles.RemoveAt(tile_id);
			free_tiles.Add(tile);

			var text = tile.GetComponentInChildren<Text>() as Text;
			text.enabled = false;

			_users.Remove(username);
		}
	}
}
