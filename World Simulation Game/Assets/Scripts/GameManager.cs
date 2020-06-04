using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;

public class GameManager : MonoBehaviour
{
    public static int3 int3Max = new int3(int.MaxValue, int.MaxValue, int.MaxValue);
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            int3 clickPos = MouseToWorld();
            if(!clickPos.Equals(int3Max))
            {
                Block block = WorldManager.GetBlock(clickPos.x, clickPos.y, clickPos.z);
                Debug.Log("Type: " + block.type + " pos: " + block.position);
            }
        }
    }

    public int3 MouseToWorld()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit))
        {
            int x = Mathf.RoundToInt(hit.point.x);
            int y = Mathf.FloorToInt(hit.point.y) < 0 ? 0 : Mathf.FloorToInt(hit.point.y);
            int z = Mathf.RoundToInt(hit.point.z);
            return new int3(x, y, z);
        }
        else
        {
            return int3Max;
        }
    }
}
