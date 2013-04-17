using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class GridCellFiller : MonoBehaviour {

	public Transform emiters;
	public Transform TargetCell;
	
	public void fillCells(Transform target)
	{
		Transform[] 	xEmiters; //holds the emiters for x Axis
		Transform[] 	yEmiters; //holds the emiters for y Axis
		Transform 		childrenHolder; //Temp holder of the parent transform
		
		TargetCell = target;
		
		//getting the transform for the emitters (gameObject Transforms)
		childrenHolder = emiters.FindChild("1");
		yEmiters = childrenHolder.GetComponentsInChildren<Transform>();
		
		childrenHolder = emiters.FindChild("-1");
		xEmiters = childrenHolder.GetComponentsInChildren<Transform>();
		
		// Loop through all the emitters in the x axis to shoot a raycast so we can
		// add the cvalid info to the cells
		foreach(Transform xem in xEmiters)
		{
			orderAndSetCells(Physics.RaycastAll(xem.position,Vector3.forward),true);
		}
		foreach(Transform yem in yEmiters)
		{
			orderAndSetCells(Physics.RaycastAll(yem.position,Vector3.right),false);
		}
		
	}//end fillCells
	
	
	/// <summary>
	/// Will follow the raycast collisions to fill a cell parameter in all of them
	/// </summary>
	/// <param name='hitters'>
	/// Hitters contains the collisions of hit from a raycastHit
	/// </param>
	/// <param name='direction'>
	/// Direction. True if going forward, false if going right
	/// </param>
	private void orderAndSetCells(RaycastHit[] hitters, bool direction)
	{
		
		Cell kvpCell;
		//Its a sorted list that orders the hits based on the distance
		//this list is overWritten constantly
		SortedList<float,Cell> hitList = new SortedList<float, Cell> ();
		
		//Add all RaycastHit to a sorted list based on the hit distance
		foreach(RaycastHit hit in hitters)
		{
			hitList.Add(hit.distance,hit.transform.GetComponent<Cell>());	
		}
		
		//Go throught all the keys (in order) grabbing the cells and
		//setting up the corresponding info
		foreach(float k in hitList.Keys)
		{
			kvpCell = hitList[k];
			try
			{
				if(direction)
					kvpCell.top = hitList[k+1];
				else
					kvpCell.right = hitList[k+1];
			}
			//we catch the exception for those keys that are over the edges
			catch(KeyNotFoundException)
			{
				if(direction)
					kvpCell.top = null;
				else
					kvpCell.right = null;
			}
			try
			{
				if(direction)
					kvpCell.bottom = hitList[k-1];
				else
					kvpCell.left = hitList[k-1];
			}
			catch(KeyNotFoundException)
			{
				if(direction)
					kvpCell.bottom = null;
				else
					kvpCell.left = null;
			}
			if(kvpCell!=null)
			{
				//calculate heuristics
				Vector3 tempH = kvpCell.transform.position - TargetCell.position;
				kvpCell.h_heuristicValue = (int)(Mathf.Abs(tempH.x)+Mathf.Abs(tempH.z));
			}
		}//end ForEach
	}//end orderSetCells
}
