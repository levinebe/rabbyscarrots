using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EnemyController : MonoBehaviour {

	public Transform target;
	private NavMeshAgent navComponent;
	private float attackDistance;
	private Vector3 startPos;
	private Animation anim;
	bool playerInSight;

	public Text stateText;
	
	enum State
	{
		IDLE,
		MOVE,
		ATTACK,
		RETREAT
	}
	private State state;

	void Start () {
		target =  GameObject.FindGameObjectWithTag("Player").transform;
		navComponent = this.gameObject.GetComponent<NavMeshAgent> ();
		anim = this.gameObject.GetComponent<Animation> ();
		attackDistance = navComponent.stoppingDistance;
		state = State.IDLE;
		startPos = transform.position;
		playerInSight = false;
	}
	
	void Update () {
		float distToTarget = Vector3.Distance (target.position, transform.position);
		float distToStart = Vector3.Distance (startPos, transform.position);

		switch (state) {
			case State.IDLE:
				anim.CrossFade ("Idle");
				break;
			case State.MOVE:
				anim.CrossFade ("Walk");
				navComponent.SetDestination (target.position);
				break;
			case State.ATTACK:
				anim.CrossFade ("Lumbering");
				break;
			case State.RETREAT:
				anim.CrossFade ("Walk");
				navComponent.SetDestination (startPos);
				break;
			default:
				break;
		}

		if (playerInSight) {
			ChangeState (State.MOVE);
			if (distToTarget <= attackDistance) {
				ChangeState (State.ATTACK);
			}
		} else if (distToStart < 2) {
			ChangeState (State.IDLE);
		} else {
			ChangeState (State.RETREAT);
		}
	}

	void OnTriggerEnter(Collider other) 
	{
		playerInSight = true;
	}

	void OnTriggerExit(Collider other) 
	{
		playerInSight = false;
	}


	void ChangeState(State newState) {
		state = newState;
		stateText.text = "ENEMY STATE: " + state.ToString ();
	}


}
