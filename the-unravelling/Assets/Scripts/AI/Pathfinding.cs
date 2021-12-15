using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;
using UnityEngine;

/// <summary>
/// A part of a pathfinding path 
/// </summary>
public struct PathPart {
    public int x;
    public int y;

    public PathPart(int x, int y) {
        this.x = x;
        this.y = y;
    }
}

/// <summary>
/// A class for calculating pathfinding in the Unravelling world grid 
/// </summary>
public class Pathfinding {
	private World _world;

	private const int STRAIGHT_COST = 10;
	private const int DIAGONAL_COST = 14;

	public Pathfinding(int2 startPos, int2 endPos, NativeList<PathPart> resultPath) {
		_world = GameObject.FindGameObjectWithTag("WorldManager").GetComponent<WorldManager>().world;
        float startTime = Time.realtimeSinceStartup;
		
        PathfindingJob pathfinding = new PathfindingJob {
                startPos = startPos,
                endPos = endPos,
                nodeArray = BuildPathfindingGrid(endPos),
                gridSize = _world.size,
				resultPath = resultPath
            };
		
        JobHandle handle = pathfinding.Schedule();
        handle.Complete();
		
        // Debug.Log("Time: " + ((Time.realtimeSinceStartup - startTime) * 1000f + "ms"));
    }


    /// <summary>
    /// Builds a grid to be consumed by the pathfinding job 
    /// </summary>
    /// <param name="endPos">The end position in the grid, used for calculating heuristics</param>
    /// <returns>A grid ready to be used for pathfinding</returns>
    private NativeArray<Node> BuildPathfindingGrid(int2 endPos) {
		int2 gridSize = new int2(_world.size, _world.size);
		NativeArray<Node> nodeArray = new NativeArray<Node>(gridSize.x * gridSize.y, Allocator.Persistent);

		for (int y = 0; y < gridSize.y; y++) {
			for (int x = 0; x < gridSize.x; x++) {
				Node node = new Node();
				node.x = x;
				node.y = y;
				node.index = CalculateIndex(x, y, gridSize.x);

                node.isWalkable = true;
				node.previousIndex = -1;

				node.additionalCost = _world.entities[y][x] > 0 ? 999 : 0;
				//node.additionalCost = _world.entities[y][x];

                node.gCost = int.MaxValue;
				node.hCost = CalculateHeuristics(new int2(x, y), endPos);
				node.CalculateFCost();

				nodeArray[node.index] = node;
			}
		}

		return nodeArray;
	}


    /// <summary>
    /// Calculates the index of this node in the nodeArray
    /// </summary>
    /// <param name="x">The x coordinate</param>
    /// <param name="y">The y coordinate</param>
    /// <param name="gridWidth">The width of the grid</param>
    /// <returns>The calculated index</returns>
    static public int CalculateIndex(int x, int y, int gridWidth) {
		return x + y * gridWidth;
	}

    /// <summary>
    /// Calculates the heuristics for the given startPos in relation to the endPos
    /// </summary>
    /// <param name="startPos">The start position</param>
    /// <param name="endPos">The end position</param>
    /// <returns>The calculated heuristics</returns>
    static public int CalculateHeuristics(int2 startPos, int2 endPos) {
		int distanceX = math.abs(startPos.x - endPos.x);
		int distanceY = math.abs(startPos.y - endPos.y);
		int remaining = math.abs(distanceX - distanceY);
		return DIAGONAL_COST * math.min(distanceX, distanceY) + STRAIGHT_COST * remaining;
	}


    [BurstCompile]
	private struct PathfindingJob : IJob {
		public int2 startPos;
		public int2 endPos;
        [DeallocateOnJobCompletion]
		public NativeArray<Node> nodeArray;
		public int2 gridSize;

        public NativeList<PathPart> resultPath;


        public void Execute() {
			int endNodeIndex = CalculateIndex(endPos.x, endPos.y, gridSize.x);
			NativeArray<int2> neighbourOffsetArray = CreateNeighbourOffsetArray();

			Node startNode = nodeArray[CalculateIndex(startPos.x, startPos.y, gridSize.x)];
			startNode.gCost = 0;
			startNode.CalculateFCost();
			nodeArray[startNode.index] = startNode;

			NativeList<int> openList = new NativeList<int>(Allocator.Temp);
			NativeList<int> closedList = new NativeList<int>(Allocator.Temp);

			openList.Add(startNode.index);

			while (openList.Length > 0) { // While there still is nodes to be checked
				int currentNodeIndex = GetLowestCostNodeIndex(openList, nodeArray);
				Node currentNode = nodeArray[currentNodeIndex];

				if (currentNodeIndex == endNodeIndex) {
					break; // Pathfinding successful
				}

				for (int i = 0; i < openList.Length; i++) {
					if (openList[i] == currentNodeIndex) {
						openList.RemoveAtSwapBack(i);
						break;
					}
				}

				closedList.Add(currentNodeIndex);

				for (int i = 0; i < neighbourOffsetArray.Length; i++) {
					int2 neighbourOffset = neighbourOffsetArray[i];
					int2 neighbourPos = new int2(currentNode.x + neighbourOffset.x, currentNode.y + neighbourOffset.y);

					if (!IsPosInsideGrid(neighbourPos, gridSize)) {
						continue; // Not a valid position
					}

					int neighbourNodeIndex = CalculateIndex(neighbourPos.x, neighbourPos.y, gridSize.x);

					if (closedList.Contains(neighbourNodeIndex)) {
						continue; // Node has already been searched
					}

					Node neighbourNode = nodeArray[neighbourNodeIndex];
					if (!neighbourNode.isWalkable) {
						continue; // Node not walkable
					}

					int2 currentNodePos = new int2(currentNode.x, currentNode.y);
					int tentativeGCost = currentNode.gCost + CalculateHeuristics(currentNodePos, neighbourPos);
					if (tentativeGCost < neighbourNode.gCost) {
						neighbourNode.previousIndex = currentNodeIndex;
						neighbourNode.gCost = tentativeGCost + neighbourNode.additionalCost;
						neighbourNode.CalculateFCost();
						nodeArray[neighbourNodeIndex] = neighbourNode;

						if (!openList.Contains(neighbourNode.index)) {
							openList.Add(neighbourNode.index);
						}
					}
				}

			}

			Node endNode = nodeArray[endNodeIndex];
			BuildPath(nodeArray, endNode);

			neighbourOffsetArray.Dispose();
			openList.Dispose();
			closedList.Dispose();
		}

        /// <summary>
        /// Creates an array of neighbouring offsets
        /// </summary>
        /// <returns>Returns the neighbour offset array</returns>
        private NativeArray<int2> CreateNeighbourOffsetArray() {
            NativeArray<int2> neighbourOffsets = new NativeArray<int2>(8, Allocator.Temp);

            neighbourOffsets[0] = new int2(-1, 0); // West
            neighbourOffsets[1] = new int2(+1, 0); // East
            neighbourOffsets[2] = new int2(0, +1); // South
            neighbourOffsets[3] = new int2(0, -1); // North
            neighbourOffsets[4] = new int2(-1, -1); // NorthWest
            neighbourOffsets[5] = new int2(-1, +1); // SouthWest
            neighbourOffsets[6] = new int2(+1, -1); // NorthEast
            neighbourOffsets[7] = new int2(+1, +1); // SouthEast              

            return neighbourOffsets;
        }

        /// <summary>
        /// Determines if a position is inside the grid or not 
        /// </summary>
        /// <param name="gridPos">The position in the grid</param>
        /// <param name="gridSize">The size of the grid</param>
        /// <returns>Wheter or not the pos is inside the grid</returns>
        private bool IsPosInsideGrid(int2 gridPos, int2 gridSize) {
			return
				gridPos.x >= 0 &&
				gridPos.y >= 0 &&
				gridPos.x < gridSize.x &&
				gridPos.y < gridSize.y;
		}

        /// <summary>
        /// Finds the node with the lowest cost in the nodeArray 
        /// </summary>
        /// <param name="openList">The list of open nodes</param>
        /// <param name="nodeArray">The complete nodeArray</param>
        /// <returns>The node with the lowest cost</returns>
        private int GetLowestCostNodeIndex(NativeList<int> openList, NativeArray<Node> nodeArray) {
			Node currentLowestNode = nodeArray[openList[0]];
			for (int i = 0; i < openList.Length; i++) {
				Node nodeToBeChecked = nodeArray[openList[i]];
				if (nodeToBeChecked.fCost < currentLowestNode.fCost) {
					currentLowestNode = nodeToBeChecked;
				}
			}

			return currentLowestNode.index;
		}

        /// <summary>
        /// Builds a path using the previousIndex variables of each node 
        /// </summary>
        /// <param name="nodeArray">The grid</param>
        /// <param name="endNode">The last node (start of this calculation)</param>
        private void BuildPath(NativeArray<Node> nodeArray, Node endNode) {
            if (endNode.previousIndex != -1) {
				resultPath.Add(new PathPart(endNode.x,
					endNode.y));

				Node currentNode = endNode;

				while (currentNode.previousIndex != -1) {
					Node previousNode = nodeArray[currentNode.previousIndex];
					resultPath.Add(new PathPart(
						previousNode.x,
						previousNode.y));
					currentNode = previousNode;
				}
			}
		}

	}

    /// <summary>
    /// All data related to a node 
    /// </summary>
    private struct Node {
		public int x;
		public int y;

		public int index;

		public int gCost;
		public int hCost;
		public int fCost;

		public int additionalCost;

		public bool isWalkable;

		public int previousIndex;

		public void CalculateFCost() {
			fCost = gCost + hCost;
		}
	}

}
