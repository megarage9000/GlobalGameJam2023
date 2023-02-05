using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AStarUnit 
{
    private MapUnit mapLocation;
    public MapUnit MapLocation => mapLocation;
    public AStarUnit Parent;
    public float G;
    public float H;
    public float F;

    public AStarUnit(MapUnit mapLocation, float g, float h, float f, AStarUnit parent = null) {
        this.mapLocation = mapLocation;
        G = g;
        H = h;
        F = f;
        Parent = parent;
    }

    public bool IsEqualTo(AStarUnit other) {
        return other.MapLocation.IsEqualTo(mapLocation);
    }

    public override bool Equals(object other) {
        if(other != null && other is MapUnit otherMapUnit) {
            return otherMapUnit.MapPosition.Equals(mapLocation);
        }
        else {
            return false;
        }
    }

    public override int GetHashCode() {
        return base.GetHashCode();
    }
}
