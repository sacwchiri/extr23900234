using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerMovement : MonoBehaviour
{
	public Cell.CellType playerType;
	private Transform checkablePos;
	private Vector3 pos;
	
	public IEnumerator movePlayer(List<Transform> path)
	{
		Vector3 tempPos = Vector3.zero;
		pos = path[path.Count-1].position;
//		pos;
		
		for(int i = path.Count-2; i >= 0; i--)
		{
			tempPos = new Vector3(path[i].position.x,
				transform.position.y,
				path[i].position.z);
			while(pos != path[i].position)
			{
				transform.position = Vector3.MoveTowards(
										transform.position,
										tempPos,
										3 * Time.deltaTime);
				
				pos = transform.position;
				pos.y = path[i].position.y;
//				checkablePos.position = pos;
				yield return null;
			}
			yield return null;
		}
	}
	public IEnumerator movePlayer(Transform destiny)
	{
		while(transform.position != destiny.position)
		{
			transform.position = Vector3.MoveTowards(
								transform.position,
								destiny.position,
								3 * Time.deltaTime);
			yield return null;
		}
	}
}
