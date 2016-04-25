using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(Animator))]
public class PlayerController : MonoBehaviour {

	private Animator animator;
	private CharacterController controller;

	private int hashHit = Animator.StringToHash("Base Layer.Hit");
	private int hashDead = Animator.StringToHash("Base Layer.Dead");
	private int hashWalk = Animator.StringToHash("Base Layer.Walk");
	private int hashJump = Animator.StringToHash("Base Layer.Jump");
	private int hashPick = Animator.StringToHash("Base Layer.Pick");
	private int hashPunch = Animator.StringToHash("Base Layer.Punch");

	public Text countText;
	private int count;

	public AudioClip footstepClip;
	private AudioSource footstepSource;

	public AudioClip carrotClip;
	private AudioSource carrotSource;
	
	void Start () {
		animator = GetComponent<Animator>();
		controller = GetComponent<CharacterController>();

		footstepSource = gameObject.AddComponent<AudioSource>();
		footstepSource.clip = footstepClip;
		footstepSource.loop = true;
		footstepSource.volume = 0.15f;

		carrotSource = gameObject.AddComponent<AudioSource>();
		carrotSource.clip = carrotClip;
		carrotSource.volume = 0.25f;

		count = 0;
		SetCountText ();
	}

	void Update () {
		if (Input.GetKeyDown("space")) {
			animator.Play(hashJump);
		}

		if (Input.GetMouseButtonDown (0)) {
			animator.Play(hashPunch);
		}
		
		float v = Input.GetAxis ("Vertical");
		float h = Input.GetAxis ("Horizontal");
		bool move = (v != 0.0f || h != 0.0f);
		
		animator.speed = move ? 2.0f : 1.0f;
		animator.SetFloat ("Speed", move ? 1.0f : 0.0f);

		if (footstepSource.isPlaying && animator.GetCurrentAnimatorStateInfo (0).IsName ("Base Layer.Jump")) {
				footstepSource.Stop();
		}
		else if (Input.GetButtonDown( "Horizontal" ) || Input.GetButtonDown( "Vertical" )) {
			if (!footstepSource.isPlaying) {
				footstepSource.Play();
			}
		}
		else if ( !Input.GetButton( "Horizontal" ) && !Input.GetButton( "Vertical" ) && footstepSource.isPlaying )
			footstepSource.Stop();
	}

	void OnTriggerEnter(Collider other) 
	{
		if (other.gameObject.CompareTag ("Pick Up")) 
		{
			other.gameObject.SetActive (false);
			count = count + 1;
		    SetCountText ();
			carrotSource.Play ();
		}
	}
	
	void SetCountText ()
	{
		countText.text = count.ToString ();
	}
}