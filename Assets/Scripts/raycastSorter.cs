using UnityEngine;
using System;
using System.Collections;

public class raycastSorter : IComparable<raycastSorter> {

	public float distance;
	public Collider collider;
	public RaycastHit hit;
	
	public raycastSorter(RaycastHit hit)
	{
		distance = hit.distance;
		collider = hit.collider;
		this.hit = hit;
	}
	public int CompareTo(raycastSorter other)
	{
		return distance.CompareTo(other.distance);
	}
}
