using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GridMovement: MonoBehaviour {
	
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
	private RaycastHit[] movingCells;
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
				else;
			}
			if(Input.GetMouseButtonUp(0))
			{
				//selectedCell = false;
				snappingToGrid = true;
			}
		}
	}
	
	void UpdatePosition()
	{
		switch(axis)
		{
		case -1:
			
			break;
		case 1:
			break;
		default:
			break;
		}
	}
	
	IEnumerator axisManager()
	{
		selectedCell = true;
		movingCells = null;
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
			movingCells = Physics.RaycastAll(origin,destination,11f);
		} 
		else
		{
			Debug.Log("Y axis");
			axis = -1;
			direction = Mathf.Abs(posDiff.z)/timeDiff;
			direction *= posDiff.z/Mathf.Abs(posDiff.z);
			
			origin = emiters.FindChild(axis.ToString()).FindChild(index.x.ToString()).transform.position;
			destination = Vector3.back;
			movingCells = Physics.RaycastAll(origin,destination,6f);
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
