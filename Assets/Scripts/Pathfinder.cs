using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Pathfinder : MonoBehaviour
{

    #region Fields

    private List<PathfindNode> openList = new List<PathfindNode>();     // Nodes that have not been evaluated.
    private List<PathfindNode> closedList = new List<PathfindNode>();   // Nodes that have been evaluated.
    private PathfindNode checkingNode = null;                           // This is the starting node or the lowest F value node off the open list
    public PathfindNode firstNodeInGrid = null;                         // First node on the grid, most upper left corner.
    public PathfindNode startNode = null;                               // Position to trace a path from.
    public PathfindNode targetNode = null;                              // Position that needs to be found.
    public bool foundTarget = false;                                    // Has the target node been found?
    public int baseMovementCost = 10;                                   // Base movement cost for horizontal & vertical movement.

    //Used for manual control with right clicking or auto run with left click
    private int step = 0;
    private int currentStep = 0;
    private bool autoRun = false;

    #endregion

    #region Methods

    public void Start()
    {
        CalculateAllHeuristics(firstNodeInGrid);    // Automatically calculates the manhattan distance heuristic.
        checkingNode = startNode;
        checkingNode.renderer.material = Instantiate(Resources.Load("Materials/mat_tile_checking")) as Material;
    }

    public void Update()
    {
        //Automatic step through control
        if (Input.GetMouseButtonDown(0) == true)
        {
            autoRun = true;
        }

        //Manual step through control
        if (Input.GetMouseButtonDown(1) == true && foundTarget == false && autoRun == false)
        {
            if (currentStep < 2)
            {
                currentStep += 1;
                step = currentStep;
            }
            else
            {
                currentStep = 1;
                step = 1;
            }
        }

        //Do while the target has not been found
        if (foundTarget == false)
        {
            FindPath();
        }

        //Trace the path if the target has been found
        if (foundTarget == true)
        {
            TraceBackPath();
        }
    }

    private void FindPath()
    {
        if (foundTarget == false)
        {     
            if (step == 1 || autoRun == true)
            {
                //Check the north node
                if (checkingNode.North != null)
                    DetermineNodeValues(checkingNode, checkingNode.North);
                //Check the east node
                if (checkingNode.East != null)
                    DetermineNodeValues(checkingNode, checkingNode.East);
                //Check the south node
                if (checkingNode.South != null)
                    DetermineNodeValues(checkingNode, checkingNode.South);
                //Check the west node
                if (checkingNode.West != null)
                    DetermineNodeValues(checkingNode, checkingNode.West);

                step = 0;
            }

            if (step == 2 || autoRun == true)
            {
                if (foundTarget == false)
                {
                    //Once done checking add to the closed list and remove from the open list
                    AddToClosedList(checkingNode);
                    RemoveFromOpenList(checkingNode);

                    //Get the next node with the smallest F value
                    checkingNode = GetSmallestFValueNode();
                    checkingNode.renderer.material = Instantiate(Resources.Load("Materials/mat_tile_checking")) as Material;
                }
                step = 0;
            }
        }
    }

    private void DetermineNodeValues(PathfindNode currentNode, PathfindNode testing)
    {
        //Dont work on null nodes
        if (testing == null)
            return;

        //Check to see if the node is the target
        if (testing == targetNode)
        {
            targetNode.Parent = currentNode;
            foundTarget = true;
            return;
        }

        //Ignore Walls
        if (testing.gameObject.tag == "Wall")
            return;

        //While the node has not already been tested
        if (closedList.Contains(testing) == false)
        {
            //Check to see if the node is already on the open list
            if (openList.Contains(testing) == true)
            {
                //Get a Gcost to move from this node to the testing node
                int newGCost = currentNode.GValue + baseMovementCost;

                //If the G cost is better then change the nodes parent and update its costs.
                if (newGCost < testing.GValue)
                {
                    testing.Parent = currentNode;
                    testing.GValue = newGCost;
                    testing.CalculateFValue();
                }
            }
            else
            {
                //Set the testing nodes parent to the current location, calculate its costs, and add it to the open list
                testing.Parent = currentNode;
                testing.GValue = currentNode.GValue + baseMovementCost;
                testing.CalculateFValue();
                AddToOpenList(testing);
                testing.renderer.material = Instantiate(Resources.Load("Materials/mat_tile_open")) as Material;
            }
        }
    }

    private void AddToOpenList(PathfindNode node)
    {
        openList.Add(node);
    }

    private void AddToClosedList(PathfindNode currentNode)
    {
        currentNode.renderer.material = Instantiate(Resources.Load("Materials/mat_tile_closed")) as Material;
        closedList.Add(currentNode);
    }

    private void RemoveFromOpenList(PathfindNode currentNode)
    {
        openList.Remove(currentNode);
    }

    private PathfindNode GetSmallestFValueNode()
    {
        float smallest = float.MaxValue;
        PathfindNode smallestNode = null;
        foreach (PathfindNode node in openList)
        {
            if (node.FValue < smallest)
            {
                smallest = node.FValue;
                smallestNode = node;
            }
        }
        return smallestNode;
    }

    private void TraceBackPath()
    {
        PathfindNode node = targetNode;
        do
        {
            node.renderer.material = Instantiate(Resources.Load("Materials/mat_tile_path")) as Material;
            node = node.Parent;
        } while (node != null);
    }

    private void CalculateManhattanDistance(PathfindNode currentNode, PathfindNode targetNode)
    {
        int x1 = Mathf.FloorToInt(currentNode.transform.position.x);
        int x2 = Mathf.FloorToInt(targetNode.transform.position.x);
        int y1 = Mathf.FloorToInt(currentNode.transform.position.z);
        int y2 = Mathf.FloorToInt(targetNode.transform.position.z);
        float h = (Mathf.Abs(x1 - x2) + Mathf.Abs(y1 - y2));
        currentNode.HValue = (int)h;
    }

    private void CalculateAllHeuristics(PathfindNode start)
    {
        PathfindNode rowStart = start;
        PathfindNode currentNode = rowStart;
        while (rowStart != null)
        {
            while (currentNode != null)
            {
                CalculateManhattanDistance(currentNode, targetNode);
                currentNode = currentNode.East;
            }
            rowStart = rowStart.South;
            currentNode = rowStart;
        }
    }

    #endregion
}
