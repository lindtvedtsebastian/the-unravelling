public static class JaggedArrayUtility
{
	/// <summary>
    /// Creates a NxM jagged int array 
    /// </summary>
    /// <param name="width">The width of the array</param>
    /// <param name="height">The height of the array</param>
    /// <returns>The new jagged 2d array</returns>
    public static T[][] createJagged2dArray<T>(int width, int height) {
        T[][] array = new T[height][];
        for (int i = 0; i < array.Length; i++) {
            array[i] = new T[width];
        }
        return array;
    }

}
