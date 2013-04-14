using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class aStar
{	
	private SortedDictionary<int,List<Cell>> openList   = new SortedDictionary<int, List<Cell>>();
	private SortedDictionary<int,List<Cell>> closedList = new SortedDictionary<int, List<Cell>>();
	
	public List<Transform> path = new List<Transform>();
	
    public Cell startNode = null;
    public Cell targetNode = null;
	private Cell checkingNode = null; 
	
	public int baseMovementCost = 10;
	private float timeTaken;
	
    public bool foundTarget = false;
    public bool stop = false;

	public IEnumerator pathfinding()
	{
		foundTarget = false;
		stop = false;
		
		openList.Clear();
		closedList.Clear();
		
		startNode.Parent = null;
		checkingNode = startNode;
		
		timeTaken = Time.realtimeSinceStartup;
		if(startNode.myType != targetNode.myType)
		{
//			Debug.Log("Not the same types");
			stop = true;
		}
		
		while(!foundTarget && !stop)
		{
			findPath();
			yield return null;
		}
		if(foundTarget)
		{
			timeTaken = Time.realtimeSinceStartup - timeTaken;	
//			Debug.Log("A star done: " + timeTaken);
//			yield return StartCoroutine(traceback());
		}
		if(stop)
		{
			timeTaken = Time.realtimeSinceStartup - timeTaken;	
//			Debug.Log("A star Stopped: " + timeTaken);
		}
	}
	
	public IEnumerator traceback()
	{
		Cell cellEnumarator;
		
		cellEnumarator = targetNode;
		
		while(cellEnumarator != null)
		{
//			Debug.Log("cell: " + cellEnumarator.transform.position);
			path.Add(cellEnumarator.transform);
			cellEnumarator = cellEnumarator.Parent;
			yield return null;
		}
//		Debug.Log("Done traceback....");
	}
	
	private void findPath()
	{
		if(checkingNode == null)
		{
			stop = true;
			return;
		}
		if(!foundTarget)
		{
			//Check the north node
            if (checkingNode.top != null)
                DetermineNodeValues(checkingNode, checkingNode.top);
            //Check the east node
            if (checkingNode.right != null)
                DetermineNodeValues(checkingNode, checkingNode.right);
            //Check the south node
            if (checkingNode.bottom != null)
                DetermineNodeValues(checkingNode, checkingNode.bottom);
            //Check the west node
            if (checkingNode.left != null)
                DetermineNodeValues(checkingNode, checkingNode.left);
			
			AddToClosedList(checkingNode);
            RemoveFromOpenList(checkingNode);

            //Get the next node with the smallest F value
            checkingNode = GetSmallestFValueNode();
		}
	}
	
	private void DetermineNodeValues(Cell currentNode, Cell testingNode)
	{
		
		if(testingNode == null)
			return;
		if(testingNode == targetNode)
		{
//			Debug.Log("I Can Seee youuu");
			targetNode.Parent = currentNode;
            foundTarget = true;
            return;
		}
		
		 if (testingNode.myType != startNode.myType)
		{
            return;
		}
		
		if(!containsCell(closedList, testingNode))
		{
			if(containsCell(openList, testingNode))
			{
				int newGCost = currentNode.g_moveValue + baseMovementCost;

                //If the G cost is better then change the nodes parent and update its costs.
                if (newGCost < testingNode.g_moveValue)
                {
                    testingNode.Parent = currentNode;
                    testingNode.g_moveValue = newGCost;
                    testingNode.CalculateFValue();
                }
			}
			else
			{
				testingNode.Parent = currentNode;
                testingNode.g_moveValue = currentNode.g_moveValue + baseMovementCost;
                testingNode.CalculateFValue();
                AddToOpenList(testingNode);
			}
		}
	}
	
	private bool containsCell(SortedDictionary<int,List<Cell>> matchDic, Cell checking)
	{
		List<Cell> tempHolder;
		if(matchDic.TryGetValue(checking.f_totalCost, out tempHolder))
		{
			return tempHolder.Contains(checking);
		}
		else
			return false;
	}

	private Cell GetSmallestFValueNode()
	{
		List<Cell> tempHolder;
		IEnumerator hold = openList.Keys.GetEnumerator();
		
		if(hold.MoveNext())
		{
			if(openList.TryGetValue(int.Parse(hold.Current.ToString()) ,out tempHolder))
			{
				return tempHolder[0];
			}
			else
			{
//				Debug.Log("ERROR ERROR: " + tempHolder);
				return null;
			}
		}
		else
		{
//			Debug.Log("ERROR 2 ERROR: ");
			return null;
		}
	}
	
	private void AddToClosedList(Cell Node)
	{
		List<Cell> tempNodes;
		if(closedList.TryGetValue(Node.f_totalCost,out tempNodes))
		{
			tempNodes.Add(Node);
		}
		else
		{
			tempNodes = new List<Cell>();
			tempNodes.Add(Node);
			closedList.Add(Node.f_totalCost, tempNodes);
		}
	}
	private void AddToOpenList(Cell Node)
	{
//		Debug.Log("Adding to open List");
		List<Cell> tempNodes;
		if(openList.TryGetValue(Node.f_totalCost,out tempNodes))
		{
			tempNodes.Add(Node);
		}
		else
		{
//			Debug.Log("new node: " + Node.myType);
			tempNodes = new List<Cell>();
			tempNodes.Add(Node);
			openList.Add(Node.f_totalCost, tempNodes);
		}
	}
	
	private void RemoveFromOpenList(Cell Node)
	{
		List<Cell> tempNodes;

		if(openList.TryGetValue(Node.f_totalCost,out tempNodes))
		{
			tempNodes.Remove(Node);
			if(tempNodes.Count == 0)
			{
				openList.Remove(Node.f_totalCost);
			}
		}
	}
}
