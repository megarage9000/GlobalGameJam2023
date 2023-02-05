using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class AStarSearch : MonoBehaviour
{
    public GameObject pathObject;
    public MapGeneration map;
    public float pathWidth = 1.0f;
    bool isDone = false;

    Dictionary<Vector2Int, AStarUnit> open_list;
    Dictionary<Vector2Int, AStarUnit> closed_list;

    AStarUnit start_node;
    AStarUnit end_node;
    AStarUnit current;

    AStarUnit prev_end_node;
    AStarUnit prev_start_node;

    List<GameObject> pathObjects;

    private void Awake() {
        open_list = new Dictionary<Vector2Int, AStarUnit>();
        closed_list = new Dictionary<Vector2Int, AStarUnit>();
        pathObjects = new List<GameObject>();

        start_node = null;
        end_node = null;
    }

    public List<Vector3> BeginSearch(Vector3 start_pos, Vector3 end_pos) {

        List<Vector3> nodes = new List<Vector3>();

        isDone = false;
        open_list.Clear();
        closed_list.Clear();

        MapUnit start_map_unit = map.GetMapUnitFromWorld(start_pos);
        MapUnit end_map_unit = map.GetMapUnitFromWorld(end_pos);

        if (start_map_unit == null || start_map_unit.IsObstacle == true) {
            start_node = prev_start_node;
        }
        else {
            start_node = new AStarUnit(start_map_unit, 0f, 0f, 0f);
            prev_start_node = start_node;
        }
        if(end_map_unit == null || end_map_unit.IsObstacle == true) {
            end_node = prev_end_node;
        }
        else {
            end_node = new AStarUnit(end_map_unit, 0f, 0f, 0f);
            prev_end_node = end_node;
        }
        current = start_node;
        while (!isDone) {
            Search();
        }
        return OutputPath();
    }

    public List<Vector3> OutputPath() {
        List<Vector3> path = new List<Vector3>();
        if (isDone) {
            foreach (GameObject go in pathObjects) {
                Destroy(go);
            }
            pathObjects.Clear();

            AStarUnit path_node = current;
            while(path_node != null && !path_node.IsEqualTo(start_node)) {
                GameObject path_object = Instantiate(pathObject, path_node.MapLocation.Position, Quaternion.identity);
                pathObjects.Add(path_object);
                path.Insert(0, path_node.MapLocation.Position);
                path_node = path_node.Parent;
            }
        }
        return path;
    }
    /*
     * Basic A * Search
     * 
     * At current node
     * 1. If end condition met, finish search
     * 2. Else get neighbors
     * 2a. Check if neighbor is
     *  - not wall
     *  - not out of bounds
     *  - not in closed list
     * 2b. Calculate G, H, F for each neigbour
     * 2c. Update/Add neighbor as node for A* Search to open list. Update the neighbor to also have its parent set to current node
     * 3. Once all neighbors are done, go through all nodes in open list and find lowest F value. Label that lowest_node
     * 4. Remove lowest_node from open list and add to closed list
     * 5. Repeat 1 - 4 with lowest_node being the current node
     */

    public void Search() {
        if(start_node == null || end_node == null) {
            isDone = true;
            return;
        }
                
        if (current.IsEqualTo(end_node)) {
            isDone = true;
            return;
        }
        List<MapUnit> neighbors = map.GetNeighbours(current.MapLocation);
        foreach(MapUnit neighbor in neighbors) {
            if (closed_list.ContainsKey(neighbor.MapPosition)) continue;
            // if (!neighbor.IsWideEnough(pathWidth)) continue;

            // Calculate g,f,h
            float g = current.G + Vector2.Distance(current.MapLocation.MapPosition, neighbor.MapPosition);
            float h = Vector2.Distance(neighbor.MapPosition, end_node.MapLocation.MapPosition);
            float f = g + h;

            // Add or update the node at given location
            if (open_list.TryGetValue(neighbor.MapPosition, out AStarUnit open_node)) {
                open_node.G = g;
                open_node.H = h;
                open_node.F = f;
            }
            else {
                open_list.Add(
                    neighbor.MapPosition,
                    new AStarUnit(neighbor, g, h, f, current));
            }
        }
        if(open_list.Count == 0) {
            isDone = true;
            return;
        }

        AStarUnit lowest_node = open_list.Values.OrderBy(x => x.F).ToList()[0];
        open_list.Remove(lowest_node.MapLocation.MapPosition);
        closed_list.Add(lowest_node.MapLocation.MapPosition, lowest_node);
        current = lowest_node;
    }
}
