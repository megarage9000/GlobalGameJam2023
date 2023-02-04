using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapUnit : MonoBehaviour
{

    public static float Scale = 1.0f;

    private Vector2 _mapPosition;
    public Vector2 MapPosition => _mapPosition;
    
    // X -> Horizontal, Y -> Vertical
    public float MapX {
        get => _mapPosition.x;
        set => _mapPosition.x = value;
    }
    
    public float MapY {
        get => _mapPosition.y;  
        set => _mapPosition.y = value;
    } 

    public bool IsObstacle = false;

    private void Awake() {
        transform.localScale = new Vector3(Scale, Scale, Scale);
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

    private void OnTriggerEnter(Collider other) {
        if(other.tag.Equals("Obstacle")) {
            IsObstacle = true;
            enabled = false;
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.tag.Equals("Obstacle")) {
            IsObstacle = false;
            enabled = true;
        }
    }
}
