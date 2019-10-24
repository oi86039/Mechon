using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

public class FilterNonStaticObjects : EditorWindow
{
    [MenuItem( "Custom/Select Non-Static" )]
    static void Init()
    {
        Object[] gameObjects = FindObjectsOfType( typeof ( GameObject ) );

        GameObject[] gameObjectArray;
        gameObjectArray = new GameObject[ gameObjects.Length ];

        int arrayPointer = 0;

        foreach ( GameObject gameObject in gameObjects )
        {
            StaticEditorFlags flags = GameObjectUtility.GetStaticEditorFlags( gameObject );

            if ( ( flags & StaticEditorFlags.ContributeGI ) == 0 )
            {
                gameObjectArray[ arrayPointer ] = gameObject;
                arrayPointer += 1;
            }
        }

        Selection.objects = gameObjectArray;

    }
}