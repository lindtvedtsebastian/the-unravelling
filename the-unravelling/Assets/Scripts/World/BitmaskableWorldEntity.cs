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

    private Dictionary<int, int> bitmaskToSpriteIndex = new Dictionary<int,int>()
        {
            {0,45},
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
            {31,11},
            {64,12},
            {66,13},
            {72,14},
            {74,15},
            {75,16},
            {80,17},
            {82,18},
            {86,19},
            {88,20},
            {90,21},
            {91,22},
            {94,23},
            {95,24},
            {104,25},
            {106,26},
            {107,27},
            {120,28},
            {122,29},
            {123,30},
            {126,31},
            {127,32},
            {208,33},
            {210,34},
            {214,35},
            {216,36},
            {218,37},
            {219,38},
            {222,39},
            {223,40},
            {248,41},
            {250,42},
            {251,43},
            {254,44},
            {255,0}
        };

    public TileBase SetSprite(int x, int y) {
        // Debug.Log(calculateBitmask(x, y));
        return this.sprites[bitmaskToSpriteIndex[calculateBitmask(x,y)]];
    }

    public int calculateBitmask(int x, int y) {
        resetBooleans();
        identifyDirections(x, y);
        int bitmask = calculateCardinals(x, y);
        bitmask += calculateCorners(x, y);
        return bitmask;
    }

    private void identifyDirections(int x, int y) {
        checkNorth = y - 1 > 0;
        checkWest = x - 1 > 0;
        checkEast = x + 1 < WorldData.Get.worldSize;
        checkSouth = y + 1 < WorldData.Get.worldSize;
    }

    
    public int calculateCardinals(int x, int y) {
        int bitmask = 0;

        if (checkNorth && WorldData.Get.map[x,y-1] != this.id) {
            bitmask += WorldData.N;
            northExists = true;
        }
        if (checkWest && WorldData.Get.map[x-1,y] != this.id) {
            bitmask += WorldData.W;
            westExists = true;
        }
        if (checkEast && WorldData.Get.map[x+1,y] != this.id) {
            bitmask += WorldData.E;
            eastExists = true;
        }
        if (checkSouth && WorldData.Get.map[x,y+1] != this.id) {
            bitmask += WorldData.S;
            southExists = true;
        }
        // Debug.Log("Cardinal: " + bitmask);
        return bitmask;
    }

    public int calculateCorners(int x, int y) {
        int bitmask = 0;
        
        if ((checkNorth && checkWest && WorldData.Get.map[x-1, y-1] != this.id)
            && northExists && westExists) {
            bitmask += WorldData.NW;
        }
        if ((checkNorth && checkEast && WorldData.Get.map[x+1, y-1] != this.id)
            && northExists && eastExists) {
            bitmask += WorldData.NE;
        }
        // Debug.Log("South: " + southExists + ", West: " + westExists);
        if ((checkSouth && checkWest && WorldData.Get.map[x-1, y+1] != this.id)
            && southExists && westExists) {
            bitmask += WorldData.SW;
        }
        if ((checkSouth && checkEast && WorldData.Get.map[x+1, y+1] != this.id)
            && southExists && eastExists) {
            bitmask += WorldData.SE;
        }
        // Debug.Log("Corner: " + bitmask);
        return bitmask;
    }

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
}
