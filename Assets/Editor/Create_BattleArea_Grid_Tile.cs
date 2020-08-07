using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Create_BattleArea_Grid_Tile
{
    [MenuItem("GameObject/Custom Edit/Creat_BattleArea_Grid_Tile",false,0)]
    static void CreateTile()
    {
        //此处定义生成地图的大小
        const int WIDTH = 40;
        const int HEIGHT = 40;
        //创建一个BattleArea_Grid空物体用于管理层级
        GameObject grid = new GameObject("BattleArea_Grid");
        grid.AddComponent(typeof(BattleArea_Grid));
        //获取Asset中的prefab资源
        Object prefabAsset = Resources.Load("Prefabs/BattleArea_Grid_Tile");
        //循环创建响应大小的网格
        GameObject prefabObject;
        for (int y = 0; y < HEIGHT; y++)
        {
            for (int x = 0; x < WIDTH; x++)
            {
                prefabObject = (GameObject)PrefabUtility.InstantiatePrefab(prefabAsset);
                prefabObject.transform.parent = grid.transform;
                if (y % 2 == 0)
                {
                    prefabObject.transform.position = new Vector3(x, 0, 0.86602540378f * y);
                }
                else
                {
                    prefabObject.transform.position = new Vector3(x - 0.5f, 0, 0.86602540378f*y);
                }
                prefabObject.name = string.Format("BattleArea_Grid_Tile_{0}_{1}", x, y);
            }
        }
    }
}

