using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandGrid : MonoBehaviour
{
    public int size;
    public LandCell[,] cells;


    [SerializeField] private GameObject dirtPrefab;
    void Start()
    {
        cells = new LandCell[size, size];
        for(int i = 0;i < size; i++)
        {
            for(int j = 0; j< size;j++)
            {
                LandCell cell = gameObject.AddComponent<LandCell>();
                cell.isTaken = false;
                cells[i, j] = cell;
            }
        }


        for (int i = 0; i < size; i++) //y
        {
            for (int j = 0; j < size; j++) // x
            {
                Vector3 dirtPos = new Vector3(j, 0, i);
                GameObject cell_GO = Instantiate(dirtPrefab, dirtPos, Quaternion.identity);
                cell_GO.transform.parent = transform;
            }
        }

    }

    public Vector2 GetRecomendateEmptyAreaPos()
    {
        Vector2 center = new Vector2((int)size / 2, (int)size / 2);
        Vector2 cellPos = center;
        print("cellPos" + cellPos);
        while(cells[(int)cellPos.x, (int)cellPos.y].isTaken)
        {
            int randDirection = (int)Random.Range(0, 4);
            switch (randDirection)
            {
                case 0: cellPos.x++; 
                    break;
                case 1:
                    cellPos.y++;
                    break;
                case 2:
                    cellPos.x--;
                    break;
                case 3:
                    cellPos.y--;
                    break;
            }
            cellPos.x = Mathf.Clamp(cellPos.x, 0, size);
            cellPos.y = Mathf.Clamp(cellPos.y, 0, size);
            print("while cellPos" + cellPos);
        }
        return cellPos;
    }

    public void SetCellTaken(Vector2 pos)
    {
        cells[(int)pos.x, (int)pos.y].isTaken = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
