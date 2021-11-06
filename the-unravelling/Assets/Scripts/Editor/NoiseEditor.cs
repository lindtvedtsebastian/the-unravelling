using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor (typeof (NoiseVisualizer))]
public class NoiseEditor : Editor {
    public override void OnInspectorGUI() {
        NoiseVisualizer noiseViz = (NoiseVisualizer)target;

        DrawDefaultInspector();

        if (GUILayout.Button("Generate")) {
            noiseViz.visualizeNoise();
        }
    }
}
