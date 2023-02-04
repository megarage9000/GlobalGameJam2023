using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapUnit
{
    public static float Scale = 1.0f;
    public static LayerMask ObstacleLayer;

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

    public override bool Equals(object other) {
        if(other == null || GetType().Equals(other.GetType()) == false) {
            return false;
        } 
        else {
            MapUnit otherMapUnit = other as MapUnit;
            return
                otherMapUnit.MapY == otherMapUnit.MapY &&
                otherMapUnit.MapX == otherMapUnit.MapX;
        }
    }

    public override int GetHashCode() {
        return base.GetHashCode();
    }

    public void CheckIfObstacle(GameObject instantiable) {
        float radius = Scale;
        Vector3 direction = Vector3.up;
        Vector3 position = Position - new Vector3(0f, 10 * Scale, 0f);
        LayerMask layer_mask = ObstacleLayer;
        RaycastHit hit;
        if(Physics.SphereCast(position, radius, direction, out hit, Mathf.Infinity, layer_mask)) {
            Debug.Log($"Obstacle at Map Position {MapPosition}");
            GameObject.Instantiate(instantiable, Position, Quaternion.identity);
        }
    }

    public void CheckOverlappingObstacle(GameObject instantiable) {
        Collider[] colliders = new Collider[1];
        /*        if(Physics.OverlapBoxNonAlloc(Position, new Vector3(Scale, 2 * Scale, Scale), colliders, Quaternion.identity, ObstacleLayer) != 0) {
        *//*            if (debugObject) {
                        GameObject.Destroy(debugObject);
                    }
                    debugObject = GameObject.Instantiate(instantiable, Position + Vector3.down, Quaternion.identity);
                    debugObject.transform.localScale = Vector3.one * Scale;*//*
                    IsObstacle = true;
                }
                else {
                    if(debugObject != null) {
        *//*                GameObject.Destroy(debugObject);*//*
                        IsObstacle = false;
                    }

                }*/
        IsObstacle = Physics.OverlapBoxNonAlloc(Position, new Vector3(Scale, 2 * Scale, Scale), colliders, Quaternion.identity, ObstacleLayer) != 0;
        if (IsObstacle) {
            if (debugObject) {
                GameObject.Destroy(debugObject);
            }
            debugObject = GameObject.Instantiate(instantiable, Position + Vector3.down, Quaternion.identity);
            debugObject.transform.localScale = Vector3.one * Scale;
        }
        else {
            if (debugObject != null) {
                GameObject.Destroy(debugObject);
            }
        }
    }
}
