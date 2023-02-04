using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGeneration : MonoBehaviour
{
    public float Granularity = 0.25f;
    public Vector2 Dimension = new Vector2(200, 200);

    public int X_Direction = 1;
    public int Y_Direction = 1;

    public GameObject MapUnitObject;

    List<MapUnit> mapUnits;

    private void Awake() {
        mapUnits = new List<MapUnit>();
        MapUnit.Scale = Granularity;
    }

    private void Start() {
        GenerateMap();
    }

    private void GenerateMap() {
        Vector3 origin = transform.position;
        for(int x = 0; 0 < Dimension.x; x++) {
            for (int y = 0; 0 < Dimension.y; y++) {
                float x_coordinate = origin.x + Granularity * X_Direction;
                float y_coordinate = origin.z + Granularity * Y_Direction;
                GameObject mapUnitObject = Instantiate(MapUnitObject, new Vector3(x_coordinate, origin.y, y_coordinate), Quaternion.identity);
                MapUnit mapUnit = mapUnitObject.GetComponent<MapUnit>();
                mapUnit.MapX = x;
                mapUnit.MapY = y;
                mapUnits.Add(mapUnit);
            }
        }
    }
}
