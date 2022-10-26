using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.AI;

public class LevelBuilder : MonoBehaviour
{
    [SerializeField] private GameObject WallsParent;
    [SerializeField] private int WallDensity;
    [SerializeField] WallPositioner wallPositionerScript;
    [SerializeField] Material wallMaterial;
    // Start is called before the first frame update




    private void Start()
    {
        BuildLevel();
    }

    [ContextMenu("Build Level")]
    void BuildLevel()
    {
        DeleteLevel();
        for (int i = 0; i < WallDensity; i++)
        {
            GameObject wall = GameObject.CreatePrimitive(PrimitiveType.Cube);
            wall.transform.parent = WallsParent.transform;          
            wall.gameObject.AddComponent<WallPositioner>();
            wall.gameObject.tag = "GeneratedWall";
            wall.GetComponent<WallPositioner>().RandomisePosition();           
            wall.AddComponent<BoxCollider>().isTrigger = true;
            wall.GetComponent<Renderer>().material = wallMaterial;


            GameObjectUtility.SetStaticEditorFlags(wall, StaticEditorFlags.NavigationStatic);
            

        }

        NavMeshBuilder.ClearAllNavMeshes();       
        NavMeshBuilder.BuildNavMesh();
    }

    
    void DeleteLevel()
    {
        int childrenNumber = WallsParent.transform.childCount;

        for (int i = childrenNumber - 1; i >= 0; i--)
        {
            DestroyImmediate(WallsParent.transform.GetChild(i).gameObject);
        }

    }


    
}
