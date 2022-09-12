using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buildingGrid : MonoBehaviour
{
    private int weight = 500;
    private int height = 500;
    private float cellsize = 1;
    private Camera mainCamera;
    private Grid buildingMap;

    public bool flag;
    public building[,] grid;
    building flyBuilding;
    // Start is called before the first frame update
    void Start()
    {

        buildingMap = new Grid(weight,height,cellsize,flag);

        grid = new building[weight, height];

        mainCamera = Camera.main;
    }

    public void startPlacingBuilding(building buildingPrefab)
    {
        if (flyBuilding != null)
        {
            Destroy(flyBuilding.gameObject);
        }

        flyBuilding = Instantiate(buildingPrefab);
    }




    // Update is called once per frame
    void Update()
    {
        if(flyBuilding != null)
        {
            bool available = true;

            var groundPlane = new Plane(Vector3.up, Vector3.zero);
            var ray = mainCamera.ScreenPointToRay(Input.mousePosition);

            if(groundPlane.Raycast(ray,out float position))
            {
                Vector3 worldPosition = ray.GetPoint(position);

                int x = Mathf.RoundToInt(worldPosition.x);
                int y = Mathf.RoundToInt(worldPosition.z);

                flyBuilding.transform.position = new Vector3(x, 0 , y) * cellsize + new Vector3(cellsize,0,cellsize) * .5f;

                if (x >= 0 && y >= 0 && x + flyBuilding.weight <= weight && y + flyBuilding.height <= height)
                {
                    bool empty = true; // проверить на доступность всей площади под постройку
                    for(int i = 0; i < flyBuilding.weight; i++)
                    {
                        for(int j = 0; j < flyBuilding.height; j++)
                        {
                            if(buildingMap.getValue(x + i, y + j) < 0)
                            {
                                empty = false;
                            }
                        }
                    }
                    if (empty)
                    {
                        available = true;
                    } else {
                        available = false;
                    }
                }
                else
                {
                    available = false;
                }

                flyBuilding.setBuildColor(available);

                if (Input.GetMouseButtonDown(0))
                {
                    if (available)
                    { 
                         Debug.Log("1)" + x + "," + y);
                         for (int i = 0; i < flyBuilding.weight; i++)
                         {
                           for (int j = 0; j < flyBuilding.height; j++)
                             {
                              Debug.Log(x + "," + y);
                             buildingMap.setValue(x + i, y + j, -1);
                            }
                        }
                        flyBuilding.setNormalColor();
                        flyBuilding = null;
                    }
                }
            }

            if (Input.GetMouseButtonDown(1))
            {
                flyBuilding.setNormalColor();
                Destroy(flyBuilding.gameObject);
                flyBuilding = null;
            }

        }
    }
}
