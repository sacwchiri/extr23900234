using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GridMovement : MonoBehaviour {
	
	#region Grid Elements
	public Transform gridContainer;
	private Transform[,] Grid = new Transform[6,10];
	private List<Transform> OrderedGrid;
	#endregion
	
	#region cell selection
	private bool selectedCell = false;
	private Vector3 SelectedCellPosition;
	private float selectionTime;
	private float finalTime;
	private Vector3 mouseDirection;
	#endregion
	
	#region Detection Mouse
	private Ray identifierRay;
	private RaycastHit rayItem;
	
	private Transform cell;
	#endregion
	
	#region Grid Movement
	private Vector2 index;
	private Transform currentMovingCell;
	private float direction; //positive one dir - negative the other
	private int axis;
	#endregion
	
	
	// Use this for initialization
	void Start () {
		setGrid();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(!selectedCell)
		{
			Debug.Log("No Cell Selected");
			if(Input.GetMouseButtonDown(0))
			{
				Debug.Log("Cell Selecting");
				StartCoroutine(axisManager());
				identifierRay = Camera.main.ScreenPointToRay(Input.mousePosition);
				if(Physics.Raycast(identifierRay,out rayItem))
				{
					cell = rayItem.transform;
					SelectedCellPosition = cell.position;
					
					index.x = SelectedCellPosition.x + 4.5f;
					index.y = SelectedCellPosition.z + 2.5f;
					
					selectionTime = Time.time;
				}
			}
		}
		else
		{
			updatePosition();
			if(Input.GetMouseButtonUp(0))
			{
				selectedCell = false;
			}
		}
	}
	
	void updatePosition()
	{
		try
		{
			Vector3 currentMousePosition;
			
			currentMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			currentMousePosition.y = 2.0f;
			
			if(axis != 0)
			{
				if(axis > 0)
				{
					currentMousePosition.z = SelectedCellPosition.z;
					for(int i = 0; i < 10; i++)
					{
						currentMovingCell = Grid[(int)index.y,i];
						currentMovingCell.position = Vector3.MoveTowards(currentMovingCell.position,
							new Vector3(currentMousePosition.x + (i-index.x), 
								currentMousePosition.y, 
								currentMousePosition.z),
							Time.deltaTime * 5f);
					}
					
				}
				else if(axis < 0)
				{
					currentMousePosition.x = SelectedCellPosition.x;
					for(int i = 0; i < 6; i++)
					{
						currentMovingCell = Grid[i,(int)index.x];
						currentMovingCell.position = Vector3.MoveTowards(currentMovingCell.position,
							new Vector3(currentMousePosition.x, 
								currentMousePosition.y, 
								currentMousePosition.z + (i-index.y)),
							Time.deltaTime * 5f);
					}
				}
			}
		}
		catch(System.Exception e)
		{
			Debug.Log("ERROR");
			Debug.Break();
		}
	}
	
	void setGrid()
	{
		//Debug.Log("Set Grid");
		float x,z;
		//orderedRow = new List<Transform>();
		foreach(Transform tra in gridContainer.transform)
		{
			//set the values as a zero base position
			x = tra.position.x + 4.5f; //half the size of the grid - half the size of the block
			z = tra.position.z + 2.5f;
			
			//adding it to our internal manager
			Grid[(int)z,(int)x] = tra;
		}
	}
	IEnumerator axisManager()
	{
		selectedCell = true;
		float timeDiff;
		Vector3 posDiff;
		
		yield return new WaitForSeconds(0.15f);
		
		mouseDirection = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		finalTime = Time.time;
		timeDiff = selectionTime - finalTime;
		
		posDiff = SelectedCellPosition - mouseDirection;
		
		if(Mathf.Abs(posDiff.x) > Mathf.Abs(posDiff.z))
		{
			Debug.Log("X axis");
			axis = 1;
			direction = Mathf.Abs(posDiff.x)/timeDiff;
			direction *= posDiff.x/Mathf.Abs(posDiff.x);
		}
		else
		{
			Debug.Log("Y axis");
			axis = -1;
			direction = Mathf.Abs(posDiff.z)/timeDiff;
			direction *= posDiff.z/Mathf.Abs(posDiff.z);
		}
		
	}
}
