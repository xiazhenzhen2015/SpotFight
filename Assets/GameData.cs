using UnityEngine;
using System.IO;
using System.Xml;
using System.Collections;
using System.Collections.Generic;

public class GameData{

	public static float area_weith = 1800f;
	public static float area_hight = 950f;



	public struct Mission
	{
		public int MissionId;
		public int MissionType;
		public int MissionTime;
		public int SpotCount;
		public int MaxSpotCount;
	}


	public static List<Mission> MissionList = new List<Mission>();

	public void initMissonData()
	{
		Mission m = new Mission ();

	}

	public static List<Mission> getMissonListByType(int type)
	{
		List<Mission> List = new List<Mission>();
		for (int i = 0; i < MissionList.Count; i++)
		{
			if(MissionList[i].MissionType == type)
			{
				List.Add (MissionList[i]);
			}
		}

		return List;
	}


	public static void loadXML()
	{
		XmlDocument xml = new XmlDocument();
		XmlReaderSettings set = new XmlReaderSettings();
		set.IgnoreComments = true;//这个设置是忽略xml注释文档的影响。有时候注释会影响到xml的读取
		xml.Load(XmlReader.Create((Application.dataPath+"/StreamingAssets/Mission.xml"),set));
		//得到objects节点下的所有子节点
		XmlNodeList xmlNodeList = xml.SelectSingleNode("config").ChildNodes;

		//遍历所有子节点
		foreach(XmlElement xl1 in xmlNodeList)
		{
			Mission Mi=new Mission();

			Mi.MissionId = int.Parse(xl1.GetAttribute("MissionId"));
			Mi.MissionType = int.Parse(xl1.GetAttribute("MissionType"));
			Mi.MissionTime = int.Parse(xl1.GetAttribute("MissionTime"));
			Mi.SpotCount = int.Parse(xl1.GetAttribute("SpotCount"));
			Mi.MaxSpotCount = int.Parse(xl1.GetAttribute("MaxSpotCount"));


			MissionList.Add (Mi);

		}

	}
}
