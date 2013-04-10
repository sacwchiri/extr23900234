using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Cell : MonoBehaviour 
{	
	public CellType myType;
	
	#region a* variables
	public ushort h_heuristicValue = 0;
	public ushort g_moveValue = 0;
	public ushort f_totalCost = 0;
	
	public Cell parent;
	public Cell top,bottom,left,right;
	
	#endregion
	
	#region initial cell identification
	public enum CellType
	{
		Acid,
		Desert,
		Ice,
		Lava,
		Radio,
		Salt
	};
	// Use this for initialization
	void Start () 
	{
		myType = (CellType)Enum.Parse(typeof(CellType),transform.renderer.material.name.Split(' ')[0]);
	}
	#endregion
	
	
}
