using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowBall : MonoBehaviour {

	public GameObject ball;
	public GameObject laser;
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
			ball.GetComponent<Rigidbody> ().useGravity = true;
			ballTime += Time.deltaTime;
		}
		if (ballTime > 1) {
			ball.GetComponent<Rigidbody> ().velocity = Vector3.zero;
			ball.GetComponent<Rigidbody> ().angularVelocity = Vector3.zero;
			ball.GetComponent<Rigidbody> ().position = new Vector3 (0, (float)-4.5, -15);
			//ball.GetComponent<Rigidbody> ().position 
			//	= new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, 0);
			ballMove = false;
			ball.GetComponent<Rigidbody> ().useGravity = false;
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
				ball.GetComponent<Rigidbody>().AddForce(Vector3.Scale(new Vector3(endPos.x - startPos.x, endPos.y - startPos.y, (endPos.y - startPos.y) * -1), new Vector3(1, -1, 1)) * 100);
			}
			ballMove = true;
		}
	}

	private void OnCollisionEnter(Collision collision){
		if(collision.gameObject.tag == "enemy"){
			ball.GetComponent<Rigidbody> ().isKinematic = true;
			ball.GetComponent<Rigidbody> ().position = new Vector3 ((float)-4, (float)2, (float)-10);
			Instantiate(laser.gameObject, new Vector3 ((float)-4, (float)2, (float)-10), Quaternion.identity);
			Destroy(collision.gameObject);
		}
	}
}
