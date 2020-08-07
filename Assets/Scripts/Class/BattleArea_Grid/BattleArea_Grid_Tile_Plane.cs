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
    public void LightOn(Color color)
    {
        sr.color = color;
        LightOn();
    }
    public void LightOff()
    {
        sr.enabled = false;
    }
    public void LightOff(Color color)
    {
        sr.color = color;
        LightOff();
    }
    public bool IsLight()
    {
        return sr.enabled;
    }
}
