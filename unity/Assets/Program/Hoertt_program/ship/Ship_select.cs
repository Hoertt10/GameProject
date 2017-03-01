using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Ship_select : MonoBehaviour
{

    public int radio = 0;

    [HideInInspector]
    public int item = 5;

    public static int _item;

    public float[] floatFieldLeft = new float[15];

    public static List<float> _floatFieldLeft = new List<float>();


    public float[] floatFieldRight = new float[15];

    public int[] Qusnum = new int[15];

    public static List<int> _Qusnum = new List<int>();

    public int[] Rannum = new int[15];

    public static List<int> _Rannum = new List<int>();


    public static List<float> _floatFieldRight = new List<float>();
    

    [CustomEditor(typeof(Ship_select))]
    public class Ship_select_Editor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            Ship_select edit = target as Ship_select;

            GUILayout.Space(10);

            EditorGUILayout.BeginHorizontal();

            edit.radio = GUILayout.Toolbar(edit.radio, new string[] { "Level1", "Level2", "Level3" });

            EditorGUILayout.EndHorizontal();

            GUILayout.Space(15);

            EditorGUILayout.BeginHorizontal();

            if (GUILayout.Button("X", GUILayout.Width(30)))
            {
                edit.item = 0;
            }

            GUILayout.Space(15);
            GUILayout.Label("QusTime");
            GUILayout.Space(10);
            GUILayout.Label("ShipSpeed");
            GUILayout.Space(10);
            GUILayout.Label("Qusnum");
            GUILayout.Space(10);
            GUILayout.Label("Rannum");


            EditorGUILayout.EndHorizontal();

            for (int i = 0; i < edit.item; i++)
            {

                EditorGUILayout.BeginHorizontal();

                if (GUILayout.Button("Re", GUILayout.Width(30), GUILayout.Height(20)))
                {

                    edit.floatFieldLeft[i] = 0;

                    edit.floatFieldRight[i] = 0;

                    edit.Qusnum[i] = 0;

                    edit.Rannum[i] = 0;
                }

                GUILayout.Label(i + ":");

                edit.floatFieldLeft[i] = EditorGUILayout.FloatField(edit.floatFieldLeft[i]);

                edit.floatFieldRight[i] = EditorGUILayout.FloatField(edit.floatFieldRight[i]);

                edit.Qusnum[i] = EditorGUILayout.IntField(edit.Qusnum[i]);

                edit.Rannum[i] = EditorGUILayout.IntField(edit.Rannum[i]);


                EditorGUILayout.EndHorizontal();
            }

            EditorGUILayout.BeginHorizontal();


            if (GUILayout.Button("+", GUILayout.Width(23)))
                edit.item++;

            if (GUILayout.Button("-", GUILayout.Width(23)))
                edit.item--;
            EditorGUILayout.EndHorizontal();

        }
    }
    // Use this for initialization
    void Start()
    {
        switch (radio)
        {
            case 0:
                gameObject.AddComponent<Ship_1>();
                break;
            case 1:
                gameObject.AddComponent<Ship_2>();
                break;
            case 2:
                gameObject.AddComponent<Ship_3>();
                break;
        }
        _item = item;

        _floatFieldLeft.AddRange(floatFieldLeft);

        _floatFieldRight.AddRange(floatFieldRight);

        _Qusnum.AddRange(Qusnum);

        _Rannum.AddRange(Rannum);

    }

    public void Compare ()
    {
        Ship_1.WantCompare();
    }
    // Update is called once per frame
    void Update()
    {

    } 
}
