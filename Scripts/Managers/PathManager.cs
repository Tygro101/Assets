using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PathManager  {
    private static PathManager Instance;
    private PathObject[,] matrix;
    public PathManager()
    {
        matrix = new PathObject[2, 2];
       
        matrix[0, 0] = new PathObject(null, 1);
        matrix[0, 1] = new PathObject(null, 2);
        matrix[1, 0] = new PathObject(null, 3);
        matrix[1, 1] = new PathObject(null, 4);

        matrix[0, 0].Neighbors.Push(matrix[0, 1]);
        matrix[0, 0].Neighbors.Push(matrix[1, 0]);

        matrix[1, 0].Neighbors.Push(matrix[0, 0]);
        matrix[1, 0].Neighbors.Push(matrix[1, 1]);
                               
        matrix[0, 1].Neighbors.Push(matrix[0, 0]);
        matrix[0, 1].Neighbors.Push(matrix[1, 1]);
                               
        matrix[1, 1].Neighbors.Push(matrix[0, 1]);
        matrix[1, 1].Neighbors.Push(matrix[1, 0]);
        var res = CalculatePath(matrix[0, 1], matrix[1, 1], new List<PathObject>());
    }

    public static PathManager GetInstance() {
        if (Instance == null)
        {
            Instance = new PathManager();
            
        }
        return Instance;
    }

    public void AssignToMatrix(PathObject _PathObject)
    {

    }

    public List<PathObject> CalculatePath(PathObject start, PathObject end, List<PathObject> path)
    {
        var currentPath = new List<PathObject>(path);
        currentPath.Add(start);
        if (start.Id == end.Id)
        {
            return currentPath;
        }
        var neighbors = new Stack<PathObject>(start.Neighbors);

        List<PathObject> calcPath = null;
        while (neighbors.Count > 0)
        {
            List<PathObject> tempPath = null;
            var pathObject = neighbors.Pop();
            if(!checkIfPathContains(pathObject.Id, currentPath))
            {
                tempPath = CalculatePath(pathObject, end, currentPath);
            }
            if(tempPath != null)
            {
                if (calcPath == null || calcPath.Count > tempPath.Count) calcPath = tempPath;
            }            
        }
        return calcPath;
    }

    private void dummyPath()
    {
        
    }

    private bool checkIfPathContains(int Id, List<PathObject> path)
    {
        foreach(var pathO in path){
            if (pathO.Id == Id)
                return true;
        }
        return false;
    }
}

[System.Serializable]
public class PathObject {
    public GameObject _GameObject;
    public int Id;
    public Stack<PathObject> Neighbors;

    public PathObject(GameObject _GameObject, int Id)
    {
        this._GameObject = _GameObject;
        this.Id = Id;
        Neighbors = new Stack<PathObject>();
    }
}