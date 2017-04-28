using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowBall : MonoBehaviour {

	public GameObject ball;
	private Vector3 startPos;
	private Vector3 endPos;
	private float ballTime;
	private bool ballMove;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (ballMove) {
			ballTime += Time.deltaTime;
		}
		if (ballTime > 5) {
			ball.GetComponent<Rigidbody> ().velocity = Vector3.zero;
			ball.GetComponent<Rigidbody> ().angularVelocity = Vector3.zero;
			ball.GetComponent<Rigidbody> ().position = new Vector3 (0, (float)-4.5, 0);
			ballMove = false;
			ballTime = 0;
		}
	}

	private void OnGUI(){
		Event e = Event.current;
		Ray ray = Camera.main.ScreenPointToRay(e.mousePosition);
		if (e.type == EventType.MouseDown){
			startPos = ray.origin;
			endPos = ray.origin;
		}else if (e.type == EventType.MouseDrag){
			endPos = ray.origin;
		}else if (e.type == EventType.MouseUp){
			if (!ballMove){
				ball.GetComponent<Rigidbody>().AddForce(Vector3.Scale(new Vector3(endPos.x - startPos.x, endPos.y - startPos.y, (endPos.y - startPos.y) * -1), new Vector3(1, -1, 1)) * 50);
			}
			ballMove = true;
		}
	}

	private void OnCollisionEnter(Collision collision){
		if(collision.gameObject.tag == "enemy"){
			ball.GetComponent<Rigidbody> ().position = new Vector3 ((float)-4.3, (float)3, (float)0.8);
			Destroy(collision.gameObject);
		}
	}
}
