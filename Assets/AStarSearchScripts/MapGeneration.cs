using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class MapGeneration : MonoBehaviour
{
    public float Granularity = 0.25f;
    public LayerMask ObstacleLayer;
    public Vector2Int Dimension = new Vector2Int(100, 100);

    public int X_Direction = 1;
    public int Y_Direction = 1;

    public GameObject MapUnitObject;
    public GameObject ObstacleDetector;

    public GameObject inner;
    public GameObject outer;

    GameObject tracker = null;
    GameObject tracker2 = null;
    GameObject tracker3 = null;
    GameObject tracker4 = null;


    MapUnit[,] mapUnits;
    private void Awake() {
        mapUnits = new MapUnit[Dimension.x, Dimension.y];
        MapUnit.Scale = Granularity;
        MapUnit.ObstacleLayer = ObstacleLayer;
    }

    private void Start() {
        GenerateMap();
    }

    private void Update() {
        isInMap(inner.transform.position);
        isInMap(outer.transform.position);
    }

    private void GenerateMap() {
        for(int x = 0; x < Dimension.x; x++) {
            for (int y = 0; y < Dimension.y; y++) {
                Vector3 world_position = MapToWorldCoordinates(x, y);
                MapUnit mapUnit = new MapUnit(x, y, world_position);
                mapUnits[x, y] = mapUnit;
                mapUnit.CheckOverlappingObstacle(ObstacleDetector);
            }
        }

        SpawnTracker(mapUnits[Dimension.x - 1, Dimension.y - 1].Position);
        SpawnTracker(mapUnits[0, 0].Position);

    }


    

    public Vector2Int WorldToMapCoordinates(Vector3 world_position) {
        world_position = world_position - transform.position;
        int x = Mathf.FloorToInt(world_position.x / (Granularity * X_Direction));
        int y = Mathf.FloorToInt(world_position.z / (Granularity * Y_Direction));
        return new Vector2Int(x, y);
    }

    public Vector2Int[] WorldToAreaMapCoordinates(Vector3 world_position) {
        
        world_position = world_position - transform.position;
        float x = world_position.x / (Granularity * X_Direction);
        float y = world_position.z / (Granularity * Y_Direction);

        return new Vector2Int[4] {
            new Vector2Int(Mathf.FloorToInt(x), Mathf.FloorToInt(y)),
            new Vector2Int(Mathf.CeilToInt(x), Mathf.FloorToInt(y)),
            new Vector2Int(Mathf.FloorToInt(x), Mathf.CeilToInt(y)),
            new Vector2Int(Mathf.CeilToInt(x), Mathf.CeilToInt(y)),
        };
    }

    public Vector3 MapToWorldCoordinates(int x, int y) {
        return MapToWorldCoordinates(new Vector2Int(x, y));
    }

    public Vector3 MapToWorldCoordinates(Vector2Int map_position) {
        Vector3 origin = transform.position;
        return new Vector3(
            origin.x + (map_position.x * Granularity * X_Direction),
            origin.y,
            origin.z + (map_position.y * Granularity * Y_Direction)
        );
    }

    public GameObject SpawnTracker(Vector3 position) {
        GameObject trackerObj = Instantiate(MapUnitObject, position, Quaternion.identity);
        trackerObj.transform.localScale = Vector3.one * Granularity;
        trackerObj.transform.position -= Vector3.one;
        return trackerObj;
    }

    public bool isInMap(Vector3 other_position) {
        Vector2Int map_position = WorldToMapCoordinates(other_position);
        int x = map_position.x;
        int y = map_position.y;
        if(x > 0 && y > 0 && x < Dimension.x && y < Dimension.y) {
            Vector2Int[] a = WorldToAreaMapCoordinates(other_position);
            if(tracker == null) {
                tracker = SpawnTracker(mapUnits[a[0].x, a[0].y].Position);
            }
            else {
                tracker.transform.position = mapUnits[a[0].x, a[0].y].Position;
                tracker.transform.position += Vector3.down;
            }

            if(tracker2 == null) {
                tracker2 = SpawnTracker(mapUnits[a[1].x, a[1].y].Position);
            }
            else {
                tracker2.transform.position = mapUnits[a[1].x, a[1].y].Position;
                tracker2.transform.position += Vector3.down;
            }

            if (tracker3 == null) {
                tracker3 = SpawnTracker(mapUnits[a[2].x, a[2].y].Position);
                tracker3.transform.position += Vector3.down;
            }
            else {
                tracker3.transform.position = mapUnits[a[2].x, a[3].y].Position;
                tracker4.transform.position += Vector3.down;
            }

            if (tracker4 == null) {
                tracker4 = SpawnTracker(mapUnits[a[3].x, a[3].y].Position);
            }
            else {
                tracker4.transform.position = mapUnits[a[3].x, a[3].y].Position;
                tracker4.transform.position += Vector3.down;
            }
            return true;
        }
        return false;
    }
}
