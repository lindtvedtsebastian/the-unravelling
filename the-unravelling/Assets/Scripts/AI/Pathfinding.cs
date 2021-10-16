using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;
using UnityEngine;

public class Pathfinding : MonoBehaviour {
	private const int STRAIGHT_COST = 10;
	private const int DIAGONAL_COST = 14;

	// Start is called before the first frame update
	void Start() { }

	// Update is called once per frame
	void Update() { }

	public Pathfinding() {
        float startTime = Time.realtimeSinceStartup;
        
        PathfindingJob pathfinding = new PathfindingJob {
            startPos = new int2(0, 0),
            endPos = new int2(21, 22),
            // nodeArray = BuildPathfindingGrid(new int2(21, 22)),
            nodeArray = BuildPathfindingGrid(new int2(254, 254)),
            gridSize = GameData.Get.world.worldSize
        };
        pathfinding.Run();
        Debug.Log("Time: " + ((Time.realtimeSinceStartup - startTime) * 1000f + "ms"));
    }


	private NativeArray<Node> BuildPathfindingGrid(int2 endPos) {
		int2 gridSize = new int2(GameData.Get.world.worldSize, GameData.Get.world.worldSize);
		NativeArray<Node> nodeArray = new NativeArray<Node>(gridSize.x * gridSize.y, Allocator.Persistent);

		for (int x = 0; x < gridSize.x; x++) {
			for (int y = 0; y < gridSize.y; y++) {
				Node node = new Node();
				node.x = x;
				node.y = y;
				node.index = CalculateIndex(x, y, gridSize.x);

				node.isWalkable = true;
				node.previousIndex = -1;

				node.additionalCost = GameData.Get.world.pathfindingMap[y, x];

				node.gCost = int.MaxValue;
				node.hCost = CalculateHeuristics(new int2(x, y), endPos);
				node.CalculateFCost();

				nodeArray[node.index] = node;
			}
		}

		return nodeArray;
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


    [BurstCompile]
	private struct PathfindingJob : IJob {
		public int2 startPos;
		public int2 endPos;
        [DeallocateOnJobCompletion]
		public NativeArray<Node> nodeArray;
		public int2 gridSize;
        

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
			NativeList<PathPart> path = BuildPath(nodeArray, endNode);

			if (path.Length > 0) {
				foreach (PathPart node in path) {
					// Debug.Log(node.x + ", " +node.y);
				}
			}
			else Debug.Log("Did not find a path!");

			path.Dispose();
			neighbourOffsetArray.Dispose();
			// nodeArray.Dispose();
			openList.Dispose();
			closedList.Dispose();
		}

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

		private bool IsPosInsideGrid(int2 gridPos, int2 gridSize) {
			return
				gridPos.x >= 0 &&
				gridPos.y >= 0 &&
				gridPos.x < gridSize.x &&
				gridPos.y < gridSize.y;
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

		private NativeList<PathPart> BuildPath(NativeArray<Node> nodeArray, Node endNode) {
			NativeList<PathPart> path = new NativeList<PathPart>(Allocator.Temp);
			if (endNode.previousIndex != -1) {

				path.Add(new PathPart(endNode.x,
					endNode.y,
					endNode.isDestroyable ? true : false));

				Node currentNode = endNode;

				while (currentNode.previousIndex != -1) {
					Node previousNode = nodeArray[currentNode.previousIndex];
					path.Add(new PathPart(
						previousNode.x,
						previousNode.y,
						previousNode.isDestroyable ? true : false));
					currentNode = previousNode;
				}
			}

			return path;
		}

	}

	private struct Node {
		public int x;
		public int y;

		public int index;

		public int gCost;
		public int hCost;
		public int fCost;

		public int additionalCost;

		public bool isWalkable;
		public bool isDestroyable;

		public int previousIndex;

		public void CalculateFCost() {
			fCost = gCost + hCost;
		}
	}

	private struct PathPart {
		public int x;
		public int y;

		public bool mustBeDestroyed;

		public PathPart(int x, int y, bool mustBeDestroyed) {
			this.x = x;
			this.y = y;
			this.mustBeDestroyed = mustBeDestroyed;
		}
	}
}
