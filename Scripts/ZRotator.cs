﻿using UnityEngine;
using System.Collections;

public class ZRotator : MonoBehaviour {
	
	// Update is called once per frame
	void Update () {
		transform.Rotate(new Vector3(0,50,0) * Time.deltaTime);
	}
}
