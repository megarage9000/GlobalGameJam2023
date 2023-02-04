using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapUnit
{
    public static float Scale = 1.0f;

    private Vector2Int _mapPosition;
    public Vector2Int MapPosition => _mapPosition;
    
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
}
