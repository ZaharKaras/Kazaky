using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public class building : MonoBehaviour
    {
        public Renderer mainRenderer;

        public Color defauitColor;

    public float cellsize = 1f;
        public int weight;
        public int height;
        // Start is called before the first frame update
        void Start()
        {

        }

        public void setBuildColor(bool flag)
        {


            if (flag)
            {
                mainRenderer.material.color = Color.green;
            }
            else
            {
                mainRenderer.material.color = Color.red;
            }
        }

        public void setNormalColor()
        {
            mainRenderer.material.color = defauitColor;
        }

        private void OnDrawGizmos()
        {
            for (int x = 0; x < weight; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    if (y % 2 == x % 2)
                    {
                        Gizmos.color = new Color(1f, 0f, .5f, 0.2f);
                    }
                    else
                    {
                        Gizmos.color = new Color(.5f, 0f, 1f, 0.2f); ;
                    }
                    Gizmos.DrawCube(transform.position + new Vector3(x * cellsize, 0, y * cellsize), new Vector3(cellsize, .1f, cellsize));
                }
            }
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
