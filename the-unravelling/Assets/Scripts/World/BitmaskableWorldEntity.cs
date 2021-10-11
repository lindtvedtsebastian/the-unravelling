using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


[CreateAssetMenu(fileName = "Bitmaskable World Entity" ,menuName = "Scriptable Objects/World/Bitmaskable World Entity")]
public class BitmaskableWorldEntity : WorldEntity
{
    private bool checkNorth;
    private bool checkWest;
    private bool checkEast;
    private bool checkSouth;
    private bool northExists;
    private bool westExists;
    private bool eastExists;
    private bool southExists;

    /// <summary>
    /// Determines what sprite should be at this given location [x,y]
    /// </summary>
    /// <param name="y">The tile's position on the y axis</param>
    /// <param name="x">The tile's position on the x axis</param>
    /// <returns>The sprite that should be at this world position</returns>
    public override TileBase SetSprite(int y, int x) {
        return this.sprites[bitmaskToSpriteIndex[calculateBitmask(y,x)]];
    }

    /// <summary>
    /// Calculates the bitmask for the tile at position [x,y]
    /// </summary>
    /// <param name="y">The tile's position on the y axis</param>
    /// <param name="x">The tile's position on the x axis</param>
    /// <returns>The bitmask (int value) at the tile's position</returns>
    public int calculateBitmask(int y, int x) {
        resetBooleans();
        identifyDirections(y, x);
        int bitmask = calculateCardinals(y, x);
        bitmask += calculateCorners(y, x);
        return bitmask;
    }

    /// <summary>
    /// Determines which directions are relevant for this tile's calculations
    /// </summary>
    /// <param name="y">The tile's position on the y axis</param>
    /// <param name="x">The tile's position on the x axis</param>
    private void identifyDirections(int y, int x) {
        checkNorth = y - 1 >= 0;
        checkWest = x - 1 >= 0;
        checkEast = x + 1 < GameData.Get.world.worldSize;
        checkSouth = y + 1 < GameData.Get.world.worldSize;
    }


    /// <summary>
    /// Calculates the bitmask for this tile's cardinals
    /// </summary>
    /// <param name="y">The tile's position on the y axis</param>
    /// <param name="x">The tile's position on the x axis</param>
    /// <returns>The calculated bitmask sum for the cardinal directions</returns>
    public int calculateCardinals(int y, int x) {
        int bitmask = 0;
        if (checkNorth && BitmaskPredicate(y-1,x,this.id)) {
            bitmask += GameData.N;
            northExists = true;
        }
        if (checkWest && BitmaskPredicate(y,x-1,this.id)) {
            bitmask += GameData.W;
            westExists = true;
        }
        if (checkEast && BitmaskPredicate(y,x+1,this.id)) {
            bitmask += GameData.E;
            eastExists = true;
        }
        if (checkSouth && BitmaskPredicate(y+1,x,this.id)) {
            bitmask += GameData.S;
            southExists = true;
        }
        return bitmask;
    }

    /// <summary>
    /// Calculates the bitmask for this tile's corners
    /// </summary>
    /// <param name="y">The tile's position on the y axis</param>
    /// <param name="x">The tile's position on the y axis</param>
    /// <returns>The calculated bitmask sum for the corners</returns>
    public int calculateCorners(int y, int x) {
        int bitmask = 0;
        
        if ((checkNorth && checkWest && BitmaskPredicate(y-1, x-1,this.id))
            && northExists && westExists) {
            bitmask += GameData.NW;
        }
        if ((checkNorth && checkEast && BitmaskPredicate(y-1, x+1,this.id))
            && northExists && eastExists) {
            bitmask += GameData.NE;
        }
        if ((checkSouth && checkWest && BitmaskPredicate(y+1, x-1,this.id))
            && southExists && westExists) {
            bitmask += GameData.SW;
        }
        if ((checkSouth && checkEast && BitmaskPredicate(y+1, x+1,this.id))
            && southExists && eastExists) {
            bitmask += GameData.SE;
        }
        return bitmask;
    }

    /// <summary>
    /// Checks if the tile at the provided x and y axis is of the provided id or not
    /// </summary>
    /// <param name="y">The position of the tile to be checked in the y axis</param>
    /// <param name="x">The position of the tile to be checked in the x axis</param>
    /// <param name="id">The entity ID to be checked against the position in the world</param>
    /// <returns>Wheter or not the tile at pos [x,y] is of the provided entity ID</returns>
    public bool IsWorldPosTile(int y, int x, int id) {
        return GameData.Get.world.map[y, x] == id;
    }

    /// <summary>
    /// The base function for determining if a tile should be part of the bitmask or not
    /// <summary>
    /// <param name="y">The position of the tile to be checked in the y axis</param>
    /// <param name="x">The position of the tile to be checked in the x axis</param>
    /// <param name="id">The id of the tile having its mask calculated</param>
    /// <returns>Wheter or not the tile that was checked is part of the bitmask or not</returns>
    public virtual bool BitmaskPredicate(int y, int x, int id) {
        return IsWorldPosTile(y, x, id);
    }

    /// <summary>
    /// Resets all the booleans used for the bitmask calculation 
    /// </summary>
    private void resetBooleans() {
        checkNorth = false;
        checkWest = false;
        checkEast = false;
        checkSouth = false;
        northExists = false;
        westExists = false;
        eastExists = false;
        southExists = false;
    }


    private Dictionary<int, int> bitmaskToSpriteIndex = new Dictionary<int,int>()
        {
            {0,0},
            {2,1},
            {8,2},
            {10,3},
            {11,4},
            {16,5},
            {18,6},
            {22,7},
            {24,8},
            {26,9},
            {27,10},
            {30,11},
            {31,12},
            {64,13},
            {66,14},
            {72,15},
            {74,16},
            {75,17},
            {80,18},
            {82,19},
            {86,20},
            {88,21},
            {90,22},
            {91,23},
            {94,24},
            {95,25},
            {104,26},
            {106,27},
            {107,28},
            {120,29},
            {122,30},
            {123,31},
            {126,32},
            {127,33},
            {208,34},
            {210,35},
            {214,36},
            {216,37},
            {218,38},
            {219,39},
            {222,40},
            {223,41},
            {248,42},
            {250,43},
            {251,44},
            {254,45},
            {255,46}
        };

}
