using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerIndicator : MonoBehaviour {

	public Color[] colors;
	public SpriteRenderer spriteRenderer;
	public Text indicatorText;

	// Use this for initialization
	void Start () {
		spriteRenderer.color = colors[Random.Range (0, colors.Length)];
	}

	public void SetPlayer(int index){
		indicatorText.text = (index + 1).ToString ();
	}
	
	// Update is called once per frame
	void Update () {
		transform.eulerAngles=Vector3.zero;
	}
}
