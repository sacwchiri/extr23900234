using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class characterManager : MonoBehaviour {

	public List<Player> activeCharacters;
	
	public void checkForDamage()
	{
		Ray checkRay = new Ray(Vector3.zero,Vector3.zero);
		RaycastHit checkedCell;
		
		Cell tempCellType;
		PlayerMovement playerTypeTemp;
		
		foreach(Player p in activeCharacters)
		{
			checkRay = new Ray(p.transform.position, Vector3.down);
			playerTypeTemp = p.transform.GetComponent<PlayerMovement>();
			
			if(playerTypeTemp == null)
			{
				Debug.Log("An error ocured while applying PlayerMovement");
			}
			else if(Physics.Raycast(checkRay,out checkedCell))
			{
				tempCellType = checkedCell.transform.GetComponent<Cell>();
				if(playerTypeTemp.playerType != tempCellType.myType)
				{
					p.wrongCell = true;
				}
				else
				{
					p.wrongCell = false;
				}
			}
		}
	}
	
}
