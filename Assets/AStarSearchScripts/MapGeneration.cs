using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class MapGeneration : MonoBehaviour
{
    public float Granularity = 0.25f;
    public Vector2Int Dimension = new Vector2Int(100, 100);

    public int X_Direction = 1;
    public int Y_Direction = 1;

    public GameObject MapUnitObject;

    public GameObject inner;
    public GameObject outer;

    GameObject tracker = null;

    MapUnit[,] mapUnits;
    private void Awake() {
        mapUnits = new MapUnit[Dimension.x, Dimension.y];
        MapUnit.Scale = Granularity;
    }

    private void Start() {
        GenerateMap();
    }

    private void Update() {
        isInMap(inner.transform.position);
        isInMap(outer.transform.position);
    }

    private void GenerateMap() {
        Vector3 origin = transform.position;
        for(int x = 0; x < Dimension.x; x++) {
            for (int y = 0; y < Dimension.y; y++) {
                Vector3 worldPosition = new Vector3(
                    origin.x + (x * Granularity * X_Direction),
                    origin.y,
                    origin.z + (y * Granularity * Y_Direction)
                );
                MapUnit mapUnit = new MapUnit(x, y, worldPosition);
                mapUnits[x, y] = mapUnit;
            }
        }

        Instantiate(MapUnitObject, mapUnits[Dimension.x - 1, Dimension.y - 1].Position, Quaternion.identity);
        Instantiate(MapUnitObject, mapUnits[0, 0].Position, Quaternion.identity);

    }

    public bool isInMap(Vector3 other_position) {
        other_position = other_position - transform.position;
        float x = other_position.x / (Granularity * X_Direction);
        float y = other_position.z / (Granularity * Y_Direction);

        if(x > 0 && y > 0 && x < Dimension.x && y < Dimension.y) {
            int pos_x = Mathf.FloorToInt(x);
            int pos_y = Mathf.FloorToInt(y);
            if(tracker == null) {
                tracker = Instantiate(MapUnitObject, mapUnits[pos_x, pos_y].Position, Quaternion.identity);
            }
            else {
                tracker.transform.position = mapUnits[pos_x, pos_y].Position;
            }
            
            return true;
        }
        return false;
    }
}
