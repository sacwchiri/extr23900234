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
	private Cell selectedCellItem;
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
			//Debug.Log("No Cell Selected");
			if(Input.GetMouseButtonDown(0))
			{
				Debug.Log("Cells Selecting");
				
				//StartCoroutine(axisManager());
				
				identifierRay = new Ray(
					new Vector3(-5.5f, 2.0f, 2.5f),
					Vector3.right);
//				identifierRay = Camera.main.ScreenPointToRay(Input.mousePosition);
				Debug.DrawRay(identifierRay.origin,identifierRay.direction,Color.yellow,10f);
				if(Physics.Raycast(identifierRay,out rayItem))
				{
					RaycastHit[] temp = Physics.RaycastAll(identifierRay,10f);
					Debug.Log(temp.Length);
//					cell = rayItem.transform;
//					
//					selectedCellItem = cell.GetComponent<Cell>();
//					SelectedCellPosition = selectedCellItem.pos;
//					
//					index.x = SelectedCellPosition.x + 4.5f;
//					index.y = SelectedCellPosition.y + 2.5f;
//					
//					selectionTime = Time.time;
				}
			}
		}
		else
		{
//			if(axis != 0)
//			{
//				updatePosition();
//				if(Input.GetMouseButtonUp(0))
//				{
//					
//					selectedCell = false;
//				}
//			}
		}
	}
	
	void updatePosition()
	{
		try
		{
			Vector3 currentMousePosition;
			Vector3 move2Vector;
			
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
						move2Vector = new Vector3(currentMousePosition.x + (i-index.x), 
								currentMousePosition.y, 
								currentMousePosition.z);
						
						currentMovingCell.position = Vector3.MoveTowards(currentMovingCell.position,
							move2Vector,
							Time.deltaTime * 5f);
					}
					
				}
				else if(axis < 0)
				{
					currentMousePosition.x = SelectedCellPosition.x;
					for(int i = 0; i < 6; i++)
					{
						currentMovingCell = Grid[i,(int)index.x];
						if(currentMovingCell == null)
						{
							
							Debug.Log ("NULL BREAK 3: " + Grid.Length	);
							Debug.Log ("Position 3: ind.y - " + (int)index.y + " - i - " + i);
							Debug.DebugBreak();
						}
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
			Debug.Break();
			Debug.Log("ERROR: " + e.StackTrace);
			//printGrid();
		}
	}
	
	void printGrid()
	{
		foreach(Transform tgrid in Grid)
		{
		 	Debug.Log(tgrid.position);
		}
		
	}
	
	void setGrid()
	{
		//Debug.Log("Set Grid");
		Grid = new Transform[6,10];
		float x,z;
		int count = 0;
		//orderedRow = new List<Transform>();
		foreach(Transform tra in gridContainer.transform)
		{
			//set the values as a zero base position
			x = tra.position.x + 4.5f; //half the size of the grid - half the size of the block
			z = tra.position.z + 2.5f;
			//Debug.Log(x + ", " + z);
			//adding it to our internal manager
			if(Grid[Mathf.RoundToInt(z),Mathf.RoundToInt(x)] == null)
				Grid[Mathf.RoundToInt(z),Mathf.RoundToInt(x)] = tra;
			else
				Debug.Log("overwriting cell on load");
			count++;
		}
		Debug.Log(count);
	}
	IEnumerator axisManager()
	{
		selectedCell = true;
		float timeDiff;
		Vector3 posDiff;
		
		yield return new WaitForSeconds(0.1f);
		
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
