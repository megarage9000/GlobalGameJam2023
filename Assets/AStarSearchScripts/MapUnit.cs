using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapUnit
{
    public static float Scale = 1.0f;
    public static LayerMask ObstacleLayer;
    public static LayerMask FloorMask;
    public static float CeilingHeight;

    private Vector2Int _mapPosition;
    public Vector2Int MapPosition => _mapPosition;

    GameObject debugObject = null;
    
    // X -> Horizontal, Y -> Vertical
    public int MapX {
        get => _mapPosition.x;
        set => _mapPosition.x = value;
    }
    
    public int MapY {
        get => _mapPosition.y;  
        set => _mapPosition.y = value;
    }

    public Vector3 _position;

    public Vector3 Position => _position;

    public float x {
        get => _position.x;
        set => _position.x = value;
    }

    public float y {
        get => _position.y;
        set => _position.y = value;
    }

    public float z {
        get => _position.z;
        set => _position.z = value;
    }

    public bool IsObstacle = false;

    public MapUnit(int mapX, int mapY, Vector3 worldPosition) {
        _mapPosition = new Vector2Int(mapX, mapY);
        _position = worldPosition; 
    }

    public bool IsEqualTo(MapUnit other) {
        return other.MapX == MapX && other.MapY == MapY;
    }

    public bool IsWideEnough(float radius) {
        Collider[] colliders = new Collider[1];
        return Physics.OverlapSphereNonAlloc(Position, radius, colliders, ObstacleLayer) == 0;
    }

    public void SetYPosition(GameObject instantiable) {
        RaycastHit hit;
        if(Physics.Raycast(Position + Vector3.up * CeilingHeight, Vector3.down, out hit, CeilingHeight, FloorMask)) {
            _position.y = hit.point.y;
        }
        // debugObject = GameObject.Instantiate(instantiable, Position, Quaternion.identity);
    }

    public void CheckOverlappingObstacle(GameObject instantiable) {
        /*RaycastHit[] hits = new RaycastHit[1];
        IsObstacle = Physics.BoxCastNonAlloc(Position + Vector3.up * CeilingHeight, Vector3.one * Scale, Vector3.down, hits, Quaternion.identity, CeilingHeight) != 0;
        */
        RaycastHit hit;
        IsObstacle = Physics.Raycast(Position + Vector3.up * CeilingHeight, Vector3.down, out hit, CeilingHeight);
        if (IsObstacle) {
            IsObstacle = ObstacleLayer == (ObstacleLayer | (1 << hit.collider.gameObject.layer));
        }
       
    }
}
