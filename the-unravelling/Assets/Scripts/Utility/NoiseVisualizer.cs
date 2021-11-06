using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoiseVisualizer : MonoBehaviour {
	public int mapSize;
	public int seed;
	[Range(1,500)]
	public float scale;
	[Range(1,5)]
	public int octaves;
	[Range(0,5)]
	public float persistance;
	[Range(0,5)]
	public float lacunarity;
	[Range(1,10)]
	public float startFrequency;
	public Vector2 offset;

    public Renderer texRenderer;

    public bool autoUpdate;

    public void visualizeNoise() {
		float[,] noise = MapGenerator.generateNoiseMap(mapSize,seed,scale,octaves,persistance,
													   lacunarity,startFrequency,offset);
        Texture2D tex = new Texture2D(mapSize, mapSize);
        Color[] colors = new Color[mapSize * mapSize];
        for (int y = 0; y < mapSize; y++) {
            for (int x = 0; x < mapSize; x++){
                colors[y * mapSize + x] = Color.Lerp(Color.black, Color.white, noise[x, y]);
            }
        }
        tex.SetPixels(colors);
        tex.Apply();

        texRenderer.sharedMaterial.mainTexture = tex;
        texRenderer.transform.localScale = new Vector3(mapSize, 1, mapSize);
    }
}
