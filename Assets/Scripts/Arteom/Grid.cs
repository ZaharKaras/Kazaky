using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// карта работает сейчас только в плоскоти x,0,z. Y пока не анстраивал да и думаю это нам не нужно
// строительство работате в том же формате
public class Grid
{
    private int width;
    private int hight;
    private float cellsize;
    private int[,] gridArray;
    private TextMesh[,] debugTextArray;
    private bool flagF;

    public Grid (int width, int hight, float cellsize, bool flag = true)
    {
        flagF = flag;

        this.width = width;
        this.hight = hight;
        this.cellsize = cellsize;

        gridArray = new int[width, hight];
        debugTextArray = new TextMesh[width, hight];

        for (int x= 0; x < gridArray.GetLength(0); x++)
        {
            for(int y = 0; y < gridArray.GetLength(1); y++)
            {

                if (flag)
                {
                    debugTextArray[x, y] = AUtils.CreateWorldText(gridArray[x, y].ToString(), null, GetWorlPosition(x, y) + new Vector3(cellsize, 0, cellsize) * .5f, 10, Color.yellow, TextAnchor.MiddleCenter); // числа в ячейках
                }

                RaycastHit upHit;
                Debug.DrawLine(GetWorlPosition(x, y) + new Vector3(0.5f, 0, 0.5f), GetWorlPosition(x, y) + new Vector3(0.5f, 0, 0.5f) + Vector3.up * 3f, Color.red, 100f);
                if (Physics.Raycast(new Vector3(x,-1,y) * cellsize + new Vector3(0.5f,0,0.5f),Vector3.up, out upHit, 10f))
                {
                    Debug.Log(upHit.point.x + ", " + upHit.point.z);
                    gridArray[x, y] = -1;
                    if (flag)
                    {
                        debugTextArray[x, y].color = Color.red;
                        debugTextArray[x, y].text = gridArray[x, y].ToString();
                    }
                } else
                {   
                    gridArray[x, y] = 0;
                }


                if (flag)
                {
                    Debug.DrawLine(GetWorlPosition(x, y), GetWorlPosition(x, y + 1), Color.yellow, 100f); // Это сетка
                    Debug.DrawLine(GetWorlPosition(x, y), GetWorlPosition(x + 1, y), Color.yellow, 100f);
                }
            }
        }

        if (flag)
        {
            Debug.DrawLine(GetWorlPosition(gridArray.GetLength(0), 0), GetWorlPosition(gridArray.GetLength(0), gridArray.GetLength(1)), Color.yellow, 100f);
            Debug.DrawLine(GetWorlPosition(0, gridArray.GetLength(1)), GetWorlPosition(gridArray.GetLength(0), gridArray.GetLength(1)), Color.yellow, 100f);

        }   }

    private Vector3 GetWorlPosition(int x, int y)
    {
        return new Vector3(x, 0, y) * cellsize;
    }

    public int getValue(int x, int y)
    {
        return gridArray[x, y];
    }

    public void setValue(int x, int y, int value)
    {
        if (x > 0 && y > 0 && x < gridArray.GetLength(0) && y < gridArray.GetLength(1))
        {
            gridArray[x, y] = value;
            if (flagF)
            {
                debugTextArray[x, y].text = value.ToString();

                if (gridArray[x, y] == -1) debugTextArray[x, y].color = Color.red;
            }
        }
    }


}
