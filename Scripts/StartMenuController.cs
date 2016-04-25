using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class StartMenuController : MonoBehaviour {
	
	public void PlayGame() {
		Application.LoadLevel("MainGame");
	}

	public void ExitGame() {
		Application.Quit ();
	}
}
