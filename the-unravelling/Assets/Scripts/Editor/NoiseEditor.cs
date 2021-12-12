using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor (typeof (NoiseVisualizer))]
public class NoiseEditor : Editor {
    public override void OnInspectorGUI() {
        NoiseVisualizer noiseViz = (NoiseVisualizer)target;

        if (DrawDefaultInspector()) {
            if (noiseViz.autoUpdate) {
                noiseViz.visualizeNoise();
            }
        }

        if (GUILayout.Button("Generate")) {
            noiseViz.visualizeNoise();
        }

    }
}
