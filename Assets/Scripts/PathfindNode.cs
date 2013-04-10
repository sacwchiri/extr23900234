using UnityEngine;

public class PathfindNode : MonoBehaviour
{

    #region Fields

    public int h_heuristicValue = 0;
    public int g_movementCost = 0;
    public int f_totalCost = 0;
    public PathfindNode parentNode = null;
    public PathfindNode north = null;
    public PathfindNode east = null;
    public PathfindNode south = null;
    public PathfindNode west = null;
    public GameObject arrow = null;     // Unrelated to the A*. Used to visually show the parent node.

    #endregion

    #region Properties

    public int FValue
    {
        get { return f_totalCost; }
    }

    public int HValue
    {
        get { return h_heuristicValue; }
        set { h_heuristicValue = value; }
    }

    public int GValue
    {
        get { return g_movementCost; }
        set { g_movementCost = value; }
    }

    public PathfindNode North
    {
        get { return north; }
    }

    public PathfindNode East
    {
        get { return east; }
    }

    public PathfindNode South
    {
        get { return south; }
    }

    public PathfindNode West
    {
        get { return west; }
    }

    public PathfindNode Parent
    {
        get { return parentNode; }
        set
        {
            parentNode = value;
            if (parentNode != null)     //Unrelated to the A*. Used to visually show the parent node.
                UpdateArrowToParent();
        }
    }

    #endregion

    #region Methods

    public void Awake()
    {
        DetectAdjacentNodes();
    }

    public void UpdateArrowToParent()   // Unrelated to the A*. Used to visually show the parent node.
    {
        if (parentNode == null)
            arrow.active = false;
        else
        {
            arrow.active = true;
            arrow.transform.LookAt(parentNode.transform);
            arrow.transform.rotation = Quaternion.Euler(0f, arrow.transform.rotation.eulerAngles.y, 0f);
        }
    }

    private void DetectAdjacentNodes()
    {
        RaycastHit hitInfo;

        if (Physics.Raycast(this.transform.position, this.transform.forward, out hitInfo) == true)
        {
            north = hitInfo.collider.GetComponent<PathfindNode>();
        }

        if (Physics.Raycast(this.transform.position, this.transform.right, out hitInfo) == true)
        {
            east = hitInfo.collider.GetComponent<PathfindNode>();
        }

        if (Physics.Raycast(this.transform.position, -this.transform.forward, out hitInfo) == true)
        {
            south = hitInfo.collider.GetComponent<PathfindNode>();
        }

        if (Physics.Raycast(this.transform.position, -this.transform.right, out hitInfo) == true)
        {
            west = hitInfo.collider.GetComponent<PathfindNode>();
        }
    }   // Automatically finds the adjacent nodes.

    public void CalculateFValue()
    {
        f_totalCost = GValue + HValue;
    }

    #endregion
}

