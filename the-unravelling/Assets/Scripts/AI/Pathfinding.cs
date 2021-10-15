using Unity.Collections;
using Unity.Mathematics;
using UnityEngine;

public class Pathfinding : MonoBehaviour
{
    private const int STRAIGHT_COST = 10;
    private const int DIAGONAL_COST = 14;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void FindPath(int2 startPos, int2 endPos) {
        int2 gridSize = new int2(GameData.Get.world.worldSize,GameData.Get.world.worldSize);

        NativeArray<Node> nodeArray = new NativeArray<Node>(gridSize.x * gridSize.y, Allocator.Temp);

        for (int x = 0; x < gridSize.x; x++) {
            for (int y = 0; y < gridSize.y; y++) {
                Node node = new Node();
                node.x = x;
                node.y = y;
                node.index = CalculateIndex(x, y, gridSize.x);

                node.isWalkable = true;
                node.previousIndex = -1;

                node.gCost = int.MaxValue;
                node.hCost = CalculateHeuristics(new int2(x, y), endPos);
                node.CalculateFCost();

                nodeArray[node.index] = node;
            }
        }

        Node startNode = nodeArray[CalculateIndex(startPos.x, startPos.y, gridSize.x)];

        startNode.gCost = 0;
        startNode.CalculateFCost();
        nodeArray[startNode.index] = startNode;

        NativeList<int> openList = new NativeList<int>(Allocator.Temp);
        NativeList<int> closedList = new NativeList<int>(Allocator.Temp);

        nodeArray.Dispose();
        openList.Dispose();
        closedList.Dispose();
    }

    private int CalculateIndex(int x, int y, int gridWidth) {
        return x + y * gridWidth;
    }

    private int CalculateHeuristics(int2 startPos, int2 endPos) {
        int distanceX = math.abs(startPos.x - endPos.x);
        int distanceY = math.abs(startPos.y - endPos.y);
        int remaining = math.abs(distanceX - distanceY);
        return DIAGONAL_COST * math.min(distanceX, distanceY) + STRAIGHT_COST * remaining;
    }


    private struct Node {
        public int x;
        public int y;

        public int index;

        public int gCost;
        public int hCost;
        public int fCost;

        public bool isWalkable;

        public int previousIndex;

        public void CalculateFCost() {
            fCost = gCost + hCost;
        }
    }
}
