using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleArea_Grid_Tile_Plane : MonoBehaviour
{
    SpriteRenderer sr;
    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }
    public void LightOn()
    {
        sr.enabled = true;
    }
    public void LightOff()
    {
        sr.enabled = false;
    }
    public bool IsLight()
    {
        return sr.enabled;
    }
}
