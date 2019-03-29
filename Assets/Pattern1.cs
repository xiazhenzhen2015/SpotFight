using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Pattern1 : MonoBehaviour {


	public GameObject PointPrefab;
	public GameObject PrefabRoot;
	public Text PointCntText;
	public GameObject TimeBar;


	float totalTime = 0;
	float curTime = 0;

	// Use this for initialization
	void Start () {
	    //this is a test!!!!!
	}
	

	bool startTiming = false;
	void Update () {



		if (startTiming)
		{
			TimeBar.transform.localScale =new Vector2 (curTime / totalTime, 1);
			curTime-= Time.deltaTime;

			if(curTime <= 0)
			{
				TimeBar.transform.localScale =new Vector2 (0, 1);
				startTiming = false;
			}
		}

	}


	List<GameObject> SpotGroup = new List<GameObject>();
	List<GameData.Mission> MissionList = new List<GameData.Mission> ();
	public void StartGame()
	{
		MissionList = GameData.getMissonListByType (1);
		SpotGroup.Clear();
		MissionIndex = 0;
		pointCnt = 0;
		startMission(0);


	}

	int MissionIndex = 0;
	public void startMission(int MissionIndex)
	{
		this.MissionIndex = MissionIndex;

		if(MissionIndex >= MissionList.Count)
		{
			MissionIndex = MissionList.Count - 1;
		}


		GameData.Mission mission = MissionList[MissionIndex];

		totalTime = mission.MissionTime + curTime;
		curTime += mission.MissionTime;

		StartCoroutine (createPoint (mission.SpotCount));
	}

	int pointCnt = 0;
	IEnumerator createPoint(int cnt)
	{
		startTiming = true;

		for(int i = 0; i < cnt; i++)
		{
			System.Random rd = new System.Random ();

			Vector2 pos = new Vector2 ();
			pos.x = GameData.area_weith * ((float)rd.NextDouble () - 0.5f);
			pos.y = GameData.area_hight * ((float)rd.NextDouble () - 0.5f);

			GameObject obj = Instantiate (PointPrefab);
			obj.GetComponent<PointPrefab> ().initPointPrefab (1, 1, PrefabRoot, Vector2.zero, pos);
			obj.GetComponent<PointPrefab> ().born ();
			pointCnt++;
			PointCntText.text = pointCnt.ToString ();
			SpotGroup.Add (obj);
			yield return 0;
		}

		yield return 0;

	}

	public void removePoint(GameObject obj, int SpotType)
	{
		pointCnt--;
		PointCntText.text = pointCnt.ToString ();

		SpotGroup.Remove (obj);

		if (SpotType == 2)
		{

			System.Random rd = new System.Random ();

			Vector2 pos = new Vector2 ();
			pos.x = GameData.area_weith * ((float)rd.NextDouble () - 0.5f);
			pos.y = GameData.area_hight * ((float)rd.NextDouble () - 0.5f);

			GameObject obj2 = Instantiate (PointPrefab);
			obj2.GetComponent<PointPrefab> ().initPointPrefab (1, 1, PrefabRoot, obj.transform.localPosition, pos);
			obj2.GetComponent<PointPrefab> ().born ();
			pointCnt++;
			PointCntText.text = pointCnt.ToString ();
			SpotGroup.Add (obj);
		}


		if(pointCnt == 0)
		{
			MissionIndex++;
			startMission (MissionIndex);
		}

	}


}
