using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Cell : MonoBehaviour 
{	
	public CellType myType;
	
	#region a* variables
	public int h_heuristicValue = 0;
	public int g_moveValue = 0;
	public int f_totalCost = 0;
	
	public Cell Parent = null;
	public Cell top = null;
	public Cell bottom = null;
	public Cell left = null;
	public Cell right =null;
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
	
	public void CalculateFValue()
	{
		f_totalCost = g_moveValue + h_heuristicValue;
	}
}
