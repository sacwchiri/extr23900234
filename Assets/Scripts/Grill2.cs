using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Grill2 : MonoBehaviour 
{
	
	public GameObject gridContainer;
	public float speed;
	public float rateOfDecay = 0.01f;
	
	private List<Transform> orderedRow;
	private Transform selectedObject;
	private Transform[,] theGrid = new Transform[6,10];
	private Vector3 selectedPosition;
	
	private Vector3 startPosition;
	private Vector3 endPosition;
	private Vector3 difPosition;
	
	private bool activeMovement = false;
	
	private int axis = 0; //1 x, 2 y
	private float multipliyer;
	
	private float startTime;
	private float endTime;
	private float difTime;
	
	private GameObject transformHolder;
	
	private Ray rayHitButton;
	private RaycastHit objectHitRay;
	
	
	// Use this for initialization
	void Start () 
	{
		transformHolder = new GameObject("TransformHolder");
		setGrid();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(!activeMovement)
		{
			//Debug.Log("!Active Move");
			if(Input.GetMouseButtonDown(0))
			{
				//Debug.Log("Mouse Button down");
				rayHitButton = Camera.main.ScreenPointToRay(Input.mousePosition);	
				if(Physics.Raycast(rayHitButton,out objectHitRay))
				{
					//select active Object
					selectedObject = objectHitRay.transform;
					
					selectedPosition = selectedObject.position;
					selectedPosition.x += 4.5f;
					selectedPosition.z += 2.5f;
					
					transformHolder.transform.position = selectedObject.position;
					
					startPosition = selectedObject.position;
					startTime = Time.time;
					
					Debug.Log("sel Obj: " + selectedObject.position + 
							  "sel Arr Pos: " + selectedPosition + 
							  "start Pos: " + startPosition + 
							  "holder Pos: " + transformHolder.transform.position);
					
					StartCoroutine(calculateDirection());
					
				}
			}
			//cambiar button up
			
			
			else if(Input.GetMouseButtonUp(0))
			{
				
//				activeMovement = false;
			}
		}
		if(activeMovement)
		{
			if(Input.GetMouseButton(0))
				moveSelectedLine();
			else
			{
				clearLine();
			}
		}
		
	}
	void clearLine()
	{
		Vector3 clearThis;
		Vector3 moveFinal;
		float pos;
		switch(axis)
		{
		case 1:
			pos = Mathf.Round(transformHolder.transform.position.x);
			clearThis = transformHolder.transform.position;
			if(multipliyer > 0)
				clearThis.x = pos + 0.5f;
			if(multipliyer < 0)
				clearThis.x = pos + 0.5f;
				
			transformHolder.transform.position = clearThis;
			resetChildren();
			break;
		case 2:
			pos = Mathf.Round(transformHolder.transform.position.z);
			clearThis = transformHolder.transform.position;
			if(multipliyer > 0)
				clearThis.z = pos + 0.5f;
			if(multipliyer < 0)
				clearThis.z = pos + 0.5f;
				
			transformHolder.transform.position = clearThis;
			resetChildren();
			break;
		default:
			break;
		}
	}
	void resetChildren()
	{
		switch(axis)
		{
		case 1:
			for(int i = 0; i < orderedRow.Count; i++)
			{
				if(orderedRow[i].position.x > -5 && orderedRow[i].position.x < 5)
				{
					orderedRow[i].parent = gridContainer.transform;
				}
			}
			int lenght1 = transformHolder.transform.GetChildCount();
			for(int i = 0; i < lenght1; i++)
			{
				Destroy(transformHolder.transform.GetChild(i).gameObject);
			}
			break;
		case 2:
			for(int i = 0; i < orderedRow.Count; i++)
			{
				if(orderedRow[i].position.z > -3 && orderedRow[i].position.z < 3)
				{
					orderedRow[i].parent = gridContainer.transform;
				}
			}
			int lenght = transformHolder.transform.GetChildCount();
			for(int i = 0; i < lenght; i++)
			{
				Destroy(transformHolder.transform.GetChild(i).gameObject);
			}
			break;
		default:
			break;
		}	
		orderedRow.Clear();
		setGrid();
		activeMovement = false;
	}
	IEnumerator calculateDirection()
	{
		activeMovement = true;
		yield return new WaitForSeconds(0.15f);
		endPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		Debug.Log("End: " + endPosition);
		endTime = Time.time;
		
		difPosition = startPosition - endPosition;
		difTime = startTime - endTime;
		
		if(Mathf.Abs(difPosition.x) > Mathf.Abs(difPosition.z))
		{
			Debug.Log("Preping to lock Y");
			axis = 1;//X axis
			multipliyer = Mathf.Abs(difPosition.x)/difTime;
			multipliyer *= difPosition.x/Mathf.Abs(difPosition.x);
		}
		else
		{
			Debug.Log("Preping to lock X");
			axis = 2;//Y axis
			multipliyer = Mathf.Abs(difPosition.z)/difTime;
			multipliyer *= difPosition.z/Mathf.Abs(difPosition.z);
		}
		setTransformChildren();
	}
	
	void moveSelectedLine()
	{
		Vector3 mouseWorld;
		switch(axis)
		{
		case 1:
			//calculate where the mouse is
			mouseWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			mouseWorld.y = 2f;
			mouseWorld.z = transformHolder.transform.position.z;
			
			//calculate speed
			speed = Mathf.Abs(mouseWorld.x - transformHolder.transform.position.x);
			
			//go towards the mouse X
			if(multipliyer > 0)
			{
				if(transformHolder.transform.position.x >= startPosition.x + 0.4f ||
					mouseWorld.x >= startPosition.x)
					{
						if(transformHolder.transform.position.x <= 4.5f ||
							mouseWorld.x <= transformHolder.transform.position.x)
						{
							transformHolder.transform.position = Vector3.MoveTowards(
								transformHolder.transform.position,
								mouseWorld,
								Mathf.Abs(multipliyer) * speed * Time.deltaTime);
							checkAbsolutePosition();
						}
				}
			}
			if(multipliyer < 0)
			{
				if(transformHolder.transform.position.x <= startPosition.x - 0.4f ||
					mouseWorld.x <= startPosition.x)
					{
						if(transformHolder.transform.position.x >= -4.5f ||
							mouseWorld.x >= transformHolder.transform.position.x)
						{
							transformHolder.transform.position = Vector3.MoveTowards(
								transformHolder.transform.position,
								mouseWorld,
								Mathf.Abs(multipliyer) * speed * Time.deltaTime);
							checkAbsolutePosition();
						}
				}
			}
			
			break;
		case 2:
			//calculate where the mouse is
			mouseWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			mouseWorld.y = 2f;
			mouseWorld.x = transformHolder.transform.position.x;
			
			//calculate speed
			speed = Mathf.Abs(mouseWorld.z - transformHolder.transform.position.z);
			
			//go towards the mouse Y
			if(multipliyer > 0)
			{
				if(transformHolder.transform.position.z >= startPosition.z + 0.4f ||
					mouseWorld.z >= startPosition.z)
					{
						if(transformHolder.transform.position.z <= 2.5f ||
							mouseWorld.z <= transformHolder.transform.position.z)
						{
							transformHolder.transform.position = Vector3.MoveTowards(
								transformHolder.transform.position,
								mouseWorld,
								Mathf.Abs(multipliyer) * speed * Time.deltaTime);
						checkAbsolutePosition();
						}
				}
			}
			if(multipliyer < 0)
			{
				if(transformHolder.transform.position.z <= startPosition.z - 0.6f ||
					mouseWorld.z <= startPosition.z)
					{
						if(transformHolder.transform.position.z >= -2.5f ||
							mouseWorld.z >= transformHolder.transform.position.z)
						{
							transformHolder.transform.position = Vector3.MoveTowards(
								transformHolder.transform.position,
								mouseWorld,
								Mathf.Abs(multipliyer) * speed * Time.deltaTime);
						checkAbsolutePosition();
						}
				}
			}

			break;
		default:
			break;
		}
	}
	private Vector3 resetVector;
	void checkAbsolutePosition()
	{
		switch(axis)
		{
		case 1:
			if(multipliyer > 0)
			{
				if(transformHolder.transform.position.x > 4.5f)
				{
					resetVector = transformHolder.transform.position;
					resetVector.x = 4.5f;
					transformHolder.transform.position = resetVector;
				}
				else if(transformHolder.transform.position.x < startPosition.x)
				{
					transformHolder.transform.position = startPosition;
				}
			}
			if(multipliyer < 0)
			{
				if(transformHolder.transform.position.x < -4.5f)
				{
					resetVector = transformHolder.transform.position;
					resetVector.x = -4.5f;
					transformHolder.transform.position = resetVector;
				}
				else if(transformHolder.transform.position.x > startPosition.x)
				{
					transformHolder.transform.position = startPosition;
				}
			}
			break;
		case 2:
			if(multipliyer > 0)
			{
				if(transformHolder.transform.position.z > 2.5f)
				{
					resetVector = transformHolder.transform.position;
					resetVector.z = 2.5f;
					transformHolder.transform.position = resetVector;
				}
				else if(transformHolder.transform.position.z < startPosition.z)
				{
					transformHolder.transform.position = startPosition;
				}
			}
			if(multipliyer < 0)
			{
				if(transformHolder.transform.position.z < -2.5f)
				{
					resetVector = transformHolder.transform.position;
					resetVector.z = -2.5f;
					transformHolder.transform.position = resetVector;
				}
				else if(transformHolder.transform.position.z > startPosition.z)
				{
					transformHolder.transform.position = startPosition;
				}
			}
			break;
		default:
			break;
		}
			
	}
	
	void setTransformChildren()
	{
		GameObject clonedObject;
		Vector3 clonedPosition;
		
		switch(axis)
		{
		case 1://x axis moving
			if(multipliyer > 0)
			{
				for(int i = (int)selectedPosition.x; i < 10; i++)
				{
					//generate copy
					clonedObject = (GameObject)Instantiate(theGrid[(int)selectedPosition.z, i].gameObject);
					//Debug.Log(clonedObject.transform.position);
					//position copy
					clonedPosition = clonedObject.transform.position;
					clonedPosition.x -= 10;
					clonedObject.transform.position = clonedPosition;
					//ordered list
					orderedRow.Add(clonedObject.transform);
					//set the moving object children
					//Debug.Log("holder Pos: " + transformHolder.transform.position);
					clonedObject.transform.parent = transformHolder.transform;
				}
			}
			else if(multipliyer < 0)
			{
				for(int i = (int)selectedPosition.x; i >= 0; i--)
				{
					//generate copy
					clonedObject = (GameObject)Instantiate(theGrid[(int)selectedPosition.z, i].gameObject);
					//Debug.Log(clonedObject.transform.position);
					//position copy
					clonedPosition = clonedObject.transform.position;
					clonedPosition.x += 10;
					clonedObject.transform.position = clonedPosition;
					//ordered list
					orderedRow.Add(clonedObject.transform);
					//set the moving object children
					//Debug.Log("holder Pos: " + transformHolder.transform.position);
					clonedObject.transform.parent = transformHolder.transform;
				}
			}
			for(int i = 0; i < 10; i++)
			{
				orderedRow.Add(theGrid[(int)selectedPosition.z,i]);
				theGrid[(int)selectedPosition.z,i].parent = transformHolder.transform;
			}
			break;
		case 2:
			if(multipliyer > 0)
			{
				//Debug.Log("positive multipliyer Y");
				for(int i = (int)selectedPosition.z; i < 6; i++)
				{
					
					//generate copy
					clonedObject = (GameObject)Instantiate(theGrid[i,(int)selectedPosition.x].gameObject);
					//Debug.Log(clonedObject.transform.position);
					//position copy
					Debug.Log("grid: " + theGrid[i,(int)selectedPosition.x].transform.position);
					clonedPosition = clonedObject.transform.position;
					clonedPosition.z -= 6;
					clonedObject.transform.position = clonedPosition;
					//ordered list
					orderedRow.Add(clonedObject.transform);
					//set the moving object children
					//Debug.Log("holder Pos: " + transformHolder.transform.position);
					clonedObject.transform.parent = transformHolder.transform;
				}
			}
			else if(multipliyer < 0)
			{
				for(int i = (int)selectedPosition.z; i >= 0; i--)
				{
					//generate copy
					clonedObject = (GameObject)Instantiate(theGrid[i,(int)selectedPosition.x].gameObject);
					//Debug.Log(clonedObject.transform.position);
					//position copy
					clonedPosition = clonedObject.transform.position;
					clonedPosition.z += 6;
					clonedObject.transform.position = clonedPosition;
					//ordered list
					orderedRow.Add(clonedObject.transform);
					//set the moving object children
					//Debug.Log("holder Pos: " + transformHolder.transform.position);
					clonedObject.transform.parent = transformHolder.transform;
				}
			}
			for(int i = 0; i < 6; i++)
			{
				orderedRow.Add(theGrid[i,(int)selectedPosition.x]);
				theGrid[i,(int)selectedPosition.x].parent = transformHolder.transform;
			}
			break;
		default:
			break;
		}
	}
	
	void setGrid()
	{
		Debug.Log("Set Grid");
		float x,z;
		orderedRow = new List<Transform>();
		foreach(Transform tra in gridContainer.transform)
		{
			//set the values as a zero base position
			x = tra.position.x + 4.5f; //half the size of the grid - half the size of the block
			z = tra.position.z + 2.5f;
			
			//adding it to our internal manager
			theGrid[(int)z,(int)x] = tra;
		}
	}	
}
