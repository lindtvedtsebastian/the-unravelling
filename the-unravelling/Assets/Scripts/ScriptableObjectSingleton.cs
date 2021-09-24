using UnityEngine;

public class ScriptableObjectSingleton<T> : ScriptableObject where T : ScriptableObjectSingleton<T> {
    private static T instance;
    public static T Get {
        get {
                if (instance == null) {
                    T[] assets = Resources.LoadAll<T>(""); // Load all assets of type T
                    if (assets == null || assets.Length < 1)
                        throw new System.Exception("Could not find any SO singleton instances in the resources folder");
                    else if (assets.Length > 1)
                        Debug.LogWarning("Multiple instances of the SO singleton was found in the resources folder");
                    instance = assets[0]; // If multiple instances, assign the first one
                }
                return instance; // Return the instance
            }
    }
}
