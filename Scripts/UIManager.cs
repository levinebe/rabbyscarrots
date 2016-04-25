using UnityEngine;
using System.Collections;

public class UIManager : MonoBehaviour {

	public GameObject pauseMenu;
	public Camera cam;
	private AudioSource backgroundSong;
	private float backgroundSongVolume;

	void Start () {
		backgroundSong = cam.GetComponent<AudioSource> ();
		backgroundSongVolume = backgroundSong.volume;
	}

	void Update () {
		ScanForKeyStroke ();
	}

	void ScanForKeyStroke()
	{
		if (Input.GetKeyDown ("escape") || Input.GetKeyDown ("p")) {
			TogglePauseMenu ();
		}
	}
	
	public void TogglePauseMenu ()
	{
		if (pauseMenu.activeSelf) {	// resume game
			pauseMenu.SetActive (false);
			Time.timeScale = 1.0f;
			backgroundSong.volume = backgroundSongVolume;
		}
		else {	// pause game
			pauseMenu.SetActive (true);
			Time.timeScale = 0.0f;
			backgroundSong.volume = .075f;
		}
	}

	public void QuitGame () {
		Application.Quit ();
	}

}
