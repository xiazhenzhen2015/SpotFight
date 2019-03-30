using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Pattern2 : MonoBehaviour {

	public GameObject SpotPrefab;
	public GameObject PrefabRoot;
	public Text SpeedText;

	public GameObject bar;


	void Start () {
	    //Testgit!!!!!!!!!!!
	}
	

	bool isStart = false;
	float interval = 1.2f;

	float time = 0;
	void Update () {

		if (isStart)
		{
			time += Time.deltaTime;
			if (time >= interval)
			{
				createPoint ();
				if (interval > 0.512f)
				{
					interval *= 0.99f;
				} else
				{
					interval *= 0.995f;
				}

				time = 0;
			}
		}
	}

	List<GameObject> SpotGroup = new List<GameObject>();
	public void startGame()
	{
		SpotGroup.Clear ();

		isStart = true;

	}

	int spotCount = 0;
	void createPoint()
	{

		System.Random rd = new System.Random ();

		Vector2 pos = new Vector2 ();
		pos.x = GameData.area_weith * ((float)rd.NextDouble () - 0.5f);
		pos.y = GameData.area_hight * ((float)rd.NextDouble () - 0.5f);

		GameObject obj = Instantiate (SpotPrefab);
		obj.GetComponent<PointPrefab> ().initPointPrefab (1, 2, PrefabRoot, Vector2.zero, pos);
		obj.GetComponent<PointPrefab> ().born ();
		spotCount++;
		SpotGroup.Add (obj);

		SpeedText.text = string.Format( "{0:N3} ", 1 / interval) + " 个/秒";
			
		if(spotCount > 10)
		{
			isStart = false;
		}

		bar.transform.localScale =new Vector3((float)spotCount / 10, 1, 1);
	}

	public void removeSpot(GameObject obj)
	{
		spotCount--;
		SpotGroup.Remove (obj);
		bar.transform.localScale =new Vector3((float)spotCount / 10, 1, 1);
	}

}
