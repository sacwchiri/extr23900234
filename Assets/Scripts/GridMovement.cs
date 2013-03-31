using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class GridMovement: MonoBehaviour {
	
	public LayerMask lm;
	
	#region Grid Elements
	public Transform gridContainer;
	public Transform emiters;
	private Transform[,] Grid = new Transform[6,10];
	private List<Transform> OrderedGrid;
	#endregion
	
	#region cell selection
	private bool selectedCell = false;
	private Vector3 selectedCellPosition;
	private float selectionTime;
	private float finalTime;
	private Vector3 mouseDirection;
	#endregion
	
	#region Snap to Grid
	bool snappingToGrid = false;
	#endregion
	
	#region Grid Movement
	private Vector2 index;
	private Transform currentMovingCell;
	private RaycastHit[] movingCells;
	private float direction; //positive one dir - negative the other
	private int axis;
	#endregion
	
	
	// Use this for initialization
	void Start () {
		//setGrid();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(!selectedCell)
		{
			//Debug.Log("No Cell Selected");
			if(Input.GetMouseButtonDown(0))
			{
				selectionTime = Time.time;
				
				//check mouse click position
				selectedCellPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
				selectedCellPosition.y = 2f; //set in the grid plane
				
				//define Index for emiters as 0 based
				index.x = Mathf.Floor(selectedCellPosition.x) + 5;
				index.y = Mathf.Floor(selectedCellPosition.z) + 3;
				
				StartCoroutine(axisManager());
			}
		}
		else
		{
			if(axis != 0)
			{
				if(!snappingToGrid)
					UpdatePosition();
				else
					SnapToGrid();
			}
			if(Input.GetMouseButtonUp(0))
			{
				//selectedCell = false;
				snappingToGrid = true;
			}
		}
	}
	void SnapToGrid()
	{
		Vector3 tester = movingCells[0].transform.position;
		Vector3 move2Position;
		
		tester.x = (int)(tester.x*1000)%1000;
		tester.z = (int)(tester.z*1000)%1000;
		
		if(Mathf.Abs(tester.x) == 500 && Mathf.Abs(tester.z) == 500)
		{
			//Debug.Log("snapping Done");
			snappingToGrid = false;
			selectedCell = false;
			axis = 0;
			return;
		}
		
		switch(axis)
		{
		case -1:
			for(int i = 0; i < 6; i++)
			{
				currentMovingCell = movingCells[i].transform;
				move2Position = currentMovingCell.position;
				
				move2Position.z = Mathf.Floor(move2Position.z)+0.5f;
				move2Position.z = Mathf.Round(move2Position.z*2)/2;
				
				currentMovingCell.position = Vector3.MoveTowards(currentMovingCell.position,
					move2Position,
					Time.deltaTime *5.0f);
			}
			break;
		case 1:
			
			for(int i = 0; i < 10; i++)
			{
				currentMovingCell = movingCells[i].transform;
				move2Position = currentMovingCell.position;
				
				move2Position.x = Mathf.Floor(move2Position.x)+0.5f;
				move2Position.x = Mathf.Round(move2Position.x*2)/2;
				
				currentMovingCell.position = Vector3.MoveTowards(currentMovingCell.position,
					move2Position,
					Time.deltaTime *5.0f);
			}
			
			break;
		default:
			break;
		}
		
		
	}
	void UpdatePosition()
	{
		Vector3 currentMousePosition;
		Vector3 move2Vector;
		
		currentMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		currentMousePosition.y = 2f;
		
		switch(axis)
		{
		case -1:
			currentMousePosition.x = Mathf.Floor(selectedCellPosition.x) + 0.5f;
			for(int i = 0; i < 6; i++)
			{
				currentMovingCell = movingCells[i].transform;
				
				move2Vector = new Vector3(currentMousePosition.x, 
						currentMousePosition.y, 
						currentMousePosition.z - (i));
				
				currentMovingCell.position = Vector3.MoveTowards(currentMovingCell.position,
					move2Vector,
					Time.deltaTime * 5f);
			}
			break;
		case 1:
			currentMousePosition.z = Mathf.Floor(selectedCellPosition.z) + 0.5f;

			for(int i = 0; i< 10; i++)
			{
				currentMovingCell = movingCells[i].transform;
				move2Vector = new Vector3(currentMousePosition.x + (i-index.x), 
								currentMousePosition.y, 
								currentMousePosition.z);
				
				currentMovingCell.position = Vector3.MoveTowards(currentMovingCell.position,
							move2Vector,
							Time.deltaTime * 5f);
				
			}
			break;
		default:
			break;
		}
	}
	
	IEnumerator axisManager()
	{
		selectedCell = true;
		//movingCells = null;
		axis = 0;
		
		float timeDiff;
		Vector3 posDiff;
		
		Vector3 origin, destination;
		
		yield return new WaitForSeconds(0.1f);
		
		mouseDirection = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		finalTime = Time.time;
		timeDiff = selectionTime - finalTime;
		
		posDiff = selectedCellPosition - mouseDirection;
		
		if(Mathf.Abs(posDiff.x) > Mathf.Abs(posDiff.z))
		{
			Debug.Log("X axis");
			axis = 1;
			direction = Mathf.Abs(posDiff.x)/timeDiff;
			direction *= posDiff.x/Mathf.Abs(posDiff.x);
			
			origin = emiters.FindChild(axis.ToString()).FindChild(index.y.ToString()).transform.position;
			destination = Vector3.right;
			
			movingCells = Physics.RaycastAll(origin, destination, 30f);
			List<raycastSorter> rh = new List<raycastSorter>();
			foreach(RaycastHit h in movingCells)
			{
				rh.Add(new raycastSorter(h));
			}
			
			rh.Sort();
			
			for(int i = 0; i < movingCells.Length; i++)
			{
				movingCells[i] = rh[i].hit;
			}
		} 
		else
		{
			Debug.Log("Y axis");
			axis = -1;
			direction = Mathf.Abs(posDiff.z)/timeDiff;
			direction *= posDiff.z/Mathf.Abs(posDiff.z);
			
			origin = emiters.FindChild(axis.ToString()).FindChild(index.x.ToString()).transform.position;
			destination = Vector3.back;
			movingCells = Physics.RaycastAll(origin,destination,10f,lm.value);
			List<raycastSorter> rh = new List<raycastSorter>();
			foreach(RaycastHit h in movingCells)
			{
				rh.Add(new raycastSorter(h));
			}
			
			rh.Sort();
			
			for(int i = 0; i < movingCells.Length; i++)
			{
				movingCells[i] = rh[i].hit;
			}
		}	
	}
	public void updateMovingCells()
	{
		Vector3 origin, destination;
		List<raycastSorter> rh = new List<raycastSorter>();
		switch(axis)
		{
		case 1:
			rh.Clear();
			origin = emiters.FindChild(axis.ToString()).FindChild(index.y.ToString()).transform.position;
			destination = Vector3.right;
			
			movingCells = Physics.RaycastAll(origin, destination, 15f,lm.value);
			foreach(RaycastHit h in movingCells)
			{
				rh.Add(new raycastSorter(h));
			}
			
			rh.Sort();
			
			for(int i = 0; i < movingCells.Length; i++)
			{
				movingCells[i] = rh[i].hit;
			}
			break;
		case -1:
			rh.Clear();
			origin = emiters.FindChild(axis.ToString()).FindChild(index.x.ToString()).transform.position;
			destination = Vector3.back;
			
			movingCells = Physics.RaycastAll(origin,destination,10f,lm.value);
			foreach(RaycastHit h in movingCells)
			{
				rh.Add(new raycastSorter(h));
			}
			
			rh.Sort();
			
			for(int i = 0; i < movingCells.Length; i++)
			{
				movingCells[i] = rh[i].hit;
			}
			break;
		default:
			break;
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
}
