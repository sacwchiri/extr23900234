using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class GridMovement: MonoBehaviour 
{	
	public characterManager 	playerChecks;
	public GridCellFiller 		gridFill;
	public exitController		playerExit;
	private aStar 				astarHolder;
	private float 				runTime;
	
	#region sound
	public AudioSource foundPath;
	public AudioSource notFoundPath;
	
	#endregion
	
	#region Grid Elements
	public Transform 			gridContainer;
	public Transform 			emiters;
	private List<Transform> 	OrderedGrid;
	#endregion
	
	#region cell selection
	private bool 				selectedCell = false;
	private Vector3 			selectedCellPosition;
	private float 				selectionTime;
	private float 				finalTime;
	private Vector3 			mouseDirection;
	public bool 				findingPath = false;
	public bool 				movingPlayer = false;
	public bool 				goingFinish = false;
	#endregion
	
	#region Snap to Grid
	bool 						snappingToGrid = false;
	#endregion
	
	#region Grid Movement
	private Vector2 			index;
	private Transform 			currentMovingCell;
	private RaycastHit[] 		movingCells;
	private float 				direction; //positive one dir - negative the other
	private int 				axis;
	#endregion
	
	#region player Movement
	private PlayerMovement unitMovement;
	#endregion
	
	// Update is called once per frame
	void Update () 
	{
		if(!selectedCell && !findingPath && !movingPlayer)
		{
			//Debug.Log("No Cell Selected");
			if(Input.GetMouseButtonDown(0))
			{
				Ray identifier = Camera.main.ScreenPointToRay(Input.mousePosition);
				RaycastHit hitter = new RaycastHit();
				
				if(Physics.Raycast(identifier,out hitter))
				{
					if(hitter.transform.tag == "Player")
					{
						Player playEr;
						playEr = hitter.transform.GetComponent<Player>();
						
//						Debug.Log(playEr.transform.GetComponent<PlayerMovement
						
						if(!playEr.wrongCell)
						{
							Ray r2 = new Ray(hitter.transform.position,Vector3.down);
							RaycastHit d2;
							if(Physics.Raycast(r2,out d2))
							{
								Cell cellHolder = d2.transform.GetComponent<Cell>();
								astarHolder = new aStar();
								astarHolder.startNode = cellHolder;
								unitMovement = hitter.transform.GetComponent<PlayerMovement>();
								Debug.Log(unitMovement.playerType);
								findingPath = true;
							}
						}
					}
					else if(hitter.transform.tag == "Cell" || hitter.transform.tag == "Finish")
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
			}
		}
		else if(selectedCell)
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
				playerChecks.checkForDamage();
			}
		}
		else if(findingPath)
		{
			if(Input.GetMouseButtonDown(0))
			{
				Ray identifier = Camera.main.ScreenPointToRay(Input.mousePosition);
				RaycastHit hitter = new RaycastHit();
				
				if(Physics.Raycast(identifier,out hitter))
				{	
//					Debug.Log("beginning a*");
					if(hitter.transform.tag == "Cell")
					{
						astarHolder.targetNode = hitter.transform.GetComponent<Cell>();
						gridFill.fillCells(hitter.transform);
						movingPlayer = true;
						findingPath = false;
						StartCoroutine(playerManager());
					}
					else if(hitter.transform.tag == "Finish")
					{
						Ray r2 = new Ray(hitter.transform.position,Vector3.down);
						RaycastHit d2;
						if(Physics.Raycast(r2,out d2))
						{
							astarHolder.targetNode = d2.transform.GetComponent<Cell>();
							gridFill.fillCells(d2.transform);
							movingPlayer = true;
							goingFinish = true;
							findingPath = false;
							StartCoroutine(playerManager());
						}
					}
				}
//				else
//				{
//					Debug.DrawRay(identifier.origin,identifier.direction*30f,Color.red,20f);
//					Debug.Log("mouse: " + Input.mousePosition);
//					Debug.Log("ray o:" + identifier.origin);
//					Debug.Log("ray d:" + identifier.direction);
//				}
			}
		}
	}
	private IEnumerator playerManager()
	{
		Player currentPlayer;
		runTime = Time.time;
		yield return StartCoroutine(astarHolder.pathfinding());
		runTime = Time.time - runTime;
		Debug.Log(runTime);
		
		if(astarHolder.foundTarget)
		{
			foundPath.Play();
			yield return StartCoroutine(astarHolder.traceback());
			currentPlayer = unitMovement.transform.GetComponent<Player>();
			currentPlayer.walking = true;
			yield return StartCoroutine(unitMovement.movePlayer(astarHolder.path));
			if(goingFinish)
			{
				yield return StartCoroutine(playerExit.exitSecuence(currentPlayer,unitMovement));
				goingFinish = false;
			}
			currentPlayer.walking = false;
		}
		else if(astarHolder.stop)
		{
			notFoundPath.Play();
//			Debug.Log("astar Stopped from inside");
		}
		movingPlayer = false;
		
	}
	void ClearOverBuffer()
	{
		foreach(RaycastHit r in movingCells)
		{
			if(r.transform.position.x < -4.6 || 
				r.transform.position.x > 4.6 || 
				r.transform.position.z < -2.6 ||
				r.transform.position.z > 2.6)
			{
				Destroy(r.transform.gameObject);
			}
		}
	}
	void SnapToGrid()
	{
		Vector3 tester;
		Vector3 move2Position;
		
		try
		{	
			tester = movingCells[0].transform.position;
		
		tester.x = (int)(tester.x*1000)%1000;
		tester.z = (int)(tester.z*1000)%1000;
		
		if(Mathf.Abs(tester.x) == 500 && Mathf.Abs(tester.z) == 500)
		{
			//Debug.Log("snapping Done");
			//Clear leftOverCells
			ClearOverBuffer();
			snappingToGrid = false;
			selectedCell = false;
			axis = 0;
			return;
		}
		
		switch(axis)
		{
		case -1:
			for(int i = 0; i < movingCells.Length; i++)
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
			
			for(int i = 0; i < movingCells.Length; i++)
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
		catch(Exception e)
		{
			Debug.Log("Error in snapping to grid: " + e.Message);
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
//			Debug.Log(index.y + "length: " + movingCells.Length);
			currentMousePosition.x = Mathf.Floor(selectedCellPosition.x) + 0.5f;
			for(int i = 0; i < movingCells.Length; i++)
			{
				currentMovingCell = movingCells[i].transform;
				
				move2Vector = new Vector3(currentMousePosition.x, 
						currentMousePosition.y, 
						currentMousePosition.z + (i-6));
				
				currentMovingCell.position = Vector3.MoveTowards(currentMovingCell.position,
					move2Vector,
					Time.deltaTime * 5f);
			}
			break;
		case 1:
			currentMousePosition.z = Mathf.Floor(selectedCellPosition.z) + 0.5f;

			for(int i = 0; i< movingCells.Length; i++)
			{
				currentMovingCell = movingCells[i].transform;
				move2Vector = new Vector3(currentMousePosition.x + (i-10f), 
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
//			Debug.Log("X axis");
			axis = 1;
			direction = Mathf.Abs(posDiff.x)/timeDiff;
			direction *= posDiff.x/Mathf.Abs(posDiff.x);
			
			origin = emiters.FindChild(axis.ToString()).FindChild(index.y.ToString()).transform.position;
			destination = Vector3.right;
			
			movingCells = Physics.RaycastAll(origin, destination);
			List<raycastSorter> rh = new List<raycastSorter>();
			
			foreach(RaycastHit h in movingCells)
			{
				rh.Add(new raycastSorter(h));
			}
			
			rh.Sort();
			
			//find the last position to insert the cloned objects
			Vector3 insertPos = rh[rh.Count-1].collider.transform.position;
			for(int i = 0; i <= index.x; i++)
			{
				insertPos.x += 1f;
				Instantiate(rh[i].collider.transform.gameObject,
					insertPos,
					rh[i].collider.transform.rotation);
			}
			insertPos = rh[0].collider.transform.position;
			for(int i = rh.Count-1; i >= index.x; i--)
			{
				insertPos.x -= 1f;
				Instantiate(rh[i].collider.transform.gameObject,
					insertPos,
					rh[i].collider.transform.rotation);
			}
			//after here we need to recast the raycast for the purpouse of getting the new rows
			rh.Clear();
			movingCells = Physics.RaycastAll(origin, destination);
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
//			Debug.Log("Y axis");
			axis = -1;
			direction = Mathf.Abs(posDiff.z)/timeDiff;
			direction *= posDiff.z/Mathf.Abs(posDiff.z);
			
			origin = emiters.FindChild(axis.ToString()).FindChild(index.x.ToString()).transform.position;
			destination = Vector3.forward;
			movingCells = Physics.RaycastAll(origin,destination);
			
			List<raycastSorter> rh = new List<raycastSorter>();
			foreach(RaycastHit h in movingCells)
			{
				rh.Add(new raycastSorter(h));
			}
			
			rh.Sort();
			
			Vector3 insertPos = rh[rh.Count-1].collider.transform.position;
			for(int i = 0; i <= index.y; i++)
			{
				insertPos.z += 1f;
				Instantiate(rh[i].collider.transform.gameObject,
					insertPos,
					rh[i].collider.transform.rotation);
			}
			
			insertPos = rh[0].collider.transform.position;
			for(int i = rh.Count-1; i >= index.y; i--)
			{
				insertPos.z -= 1f;
				Instantiate(rh[i].collider.transform.gameObject,
					insertPos,
					rh[i].collider.transform.rotation);
			}
			//after here we need to recast the raycast for the purpouse of getting the new rows
			rh.Clear();
			
			movingCells = Physics.RaycastAll(origin, destination);
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
}
