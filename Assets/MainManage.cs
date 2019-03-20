using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class MainManage : MonoBehaviour {


	public GameObject Pattern1;
	public GameObject Pattern2;

	void Awake()
	{
		Application.targetFrameRate = 60;
	}

	void Start () {
		GameData.loadXML ();

	}

	void OnGUI()
	{
		if(GUI.Button(new Rect(100, 100, 100, 30), "模式1"))
		{
			Pattern2.SetActive (true);
			Pattern2.GetComponent<Pattern2> ().startGame ();
		}
	}

}
