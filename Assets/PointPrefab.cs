using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PointPrefab : MonoBehaviour {

	public int bornFrameCnt = 10;
	public int dieFrameCnt = 10;

	public  Text number;
	// Use this for initialization
	void Start () {
		GetComponent<Button> ().onClick.AddListener (delegate() {
			Onclick();
		});
	}


	void Onclick()
	{
		if (GameType == 1)
		{
			GetComponentInParent<Pattern1> ().removePoint (gameObject, SpotType);
		} else if (GameType == 2)
		{
			GetComponentInParent<Pattern2> ().removeSpot (gameObject);
		}


		StartCoroutine (die());
	}

	int SpotType;
	int GameType;
	GameObject parentNode;
	Vector2 startPos;
	Vector2 endPos;
	public void initPointPrefab(int SpotType, int GameType, GameObject parentNode, Vector2 startPos, Vector2 endPos)
	{
		this.SpotType = SpotType;
		this.GameType = GameType;
		this.parentNode = parentNode;
		this.startPos = startPos;
		this.endPos = endPos;
	}


	public void born()
	{
		transform.parent = parentNode.transform;
		transform.localScale = Vector2.one;

		if(SpotType == 1)
		{
			GetComponent<Image> ().color = new Color (0,0,0);
		}else if(SpotType == 2)
		{
			GetComponent<Image> ().color = new Color (255,0,0);
		}


		StartCoroutine (move(startPos, endPos));
	}

	public IEnumerator die()
	{
		GetComponent<Image> ().raycastTarget = false;

		float n = 1 / (float)dieFrameCnt;


		for (int i = 1; i <= dieFrameCnt; i++)
		{
			transform.localScale = new Vector2 (0.5f + i * n, 0.5f + i * n);
			GetComponent<Image> ().color = new Color (0, 0, 0, 255 - i * 255 * n);
			yield return 0;
		}
		Destroy (gameObject);
		yield return 0;

	}

	IEnumerator move(Vector2 startPos, Vector2 endPos)
	{
		GetComponent<Button> ().enabled = false;

		float distance_x = (endPos.x - startPos.x) / bornFrameCnt;
		float distance_y = (endPos.y - startPos.y) / bornFrameCnt;

		transform.localPosition = startPos;
		for(int i = 0; i < 10; i ++)
		{
			yield return 0;
		}


		for(int i = 1; i <= bornFrameCnt; i ++)
		{
			transform.localPosition = new Vector2 (startPos.x + distance_x * i, startPos.x + distance_y * i);
			yield return 0;
		}

		yield return 0;

		GetComponent<Button> ().enabled = true;
	}

}
