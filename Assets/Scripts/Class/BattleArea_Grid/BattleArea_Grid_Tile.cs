using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleArea_Grid_Tile : MonoBehaviour
{
    [Header("Quote")]
    public BattleArea_Grid_Tile_Plane plane;
    public BattleArea_Grid_Tile_Frame frame;
    public BattleArea_Grid_Tile_GridSign gridSign;
    public BattleArea_Grid_Tile_MeshCollider meshCollider;

    [Header("State")]
    public bool walkable;

    [Header("Info")]
    public BattleAreaCoordinate coordinate;

    [Header("Object")]
    public BaseInteractableObject objectOnIt;

    private void Start()
    {
        string[] strings = this.name.Split('_');
        int x = int.Parse(strings[strings.Length-2]);
        int y = int.Parse(strings[strings.Length-1]);
        coordinate = new BattleAreaCoordinate(x, y);
    }

    private void OnDrawGizmos()
    {
        string[] strings = this.name.Split('_');
        int x = int.Parse(strings[strings.Length - 2]);
        int y = int.Parse(strings[strings.Length - 1]);
        UnityEditor.Handles.Label(transform.position, string.Format("({0},{1})", x, y));
    }
}
