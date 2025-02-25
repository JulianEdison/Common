using System.Collections.Generic;
using UnityEngine;

namespace JulianEdisonArt
{
    /// <summary>
    /// A collection of functions for 2D Vector math and logic
    /// </summary>
    public class JuVector
    {

        static public bool DoLinesIntersect(Vector2 a, Vector2 b, Vector2 c, Vector2 d, out Vector2 intersection)
        {
            Vector2 diffAB = new(b.x - a.x, b.y - a.y);
            Vector2 diffCD = new(d.x - c.x, d.y - c.y);

            float p1, p2;
            p1 = (-diffAB.y * (a.x - c.x) + diffAB.x * (a.y - c.y)) / (-diffCD.x * diffAB.y + diffAB.x * diffCD.y);
            p2 = (diffCD.x * (a.y - c.y) - diffCD.y * (a.x - c.x)) / (-diffCD.x * diffAB.y + diffAB.x * diffCD.y);

            if (p1 >= 0 && p1 <= 1 && p2 >= 0 && p2 <= 1) {
                intersection = new Vector2(a.x + (p2 * diffAB.x), a.y + (p2 * diffAB.y));
                return true;
            }

            intersection = Vector2.positiveInfinity;
            return false;
        }

        static public Vector2Int GetBotLeft(Vector2Int pos1, Vector2Int pos2)
        {
            return new Vector2Int(pos1.x < pos2.x ? pos1.x : pos2.x, pos1.y < pos2.y ? pos1.y : pos2.y);
        }

        static public Vector2Int GetTopRight(Vector2Int pos1, Vector2Int pos2)
        {
            return new Vector2Int(pos1.x > pos2.x ? pos1.x : pos2.x, pos1.y > pos2.y ? pos1.y : pos2.y);
        }

        static public Vector2Int[] DrunkenWalk(Vector2Int startPos)
        {
            Vector2Int[] positions = new Vector2Int[JuRandom.Random(4, 16)];
            positions[0] = startPos;

            for (int n = 1; n < positions.Length; n++) {
                switch (JuRandom.Random(0, 4)) {
                    case 0:
                        startPos += Vector2Int.left;
                        positions[n] = startPos;
                        break;
                    case 1:
                        startPos += Vector2Int.up;
                        positions[n] = startPos;
                        break;
                    case 2:
                        startPos += Vector2Int.right;
                        positions[n] = startPos;
                        break;
                    case 3:
                        startPos += Vector2Int.down;
                        positions[n] = startPos;
                        break;
                }
            }
            return positions;
        }

        /// <summary>
        /// Returns the center position of a tile
        /// </summary>
        static public Vector2 ClampToGrid(Vector2 pos, float tileSize)
        {
            return new Vector3((int)(pos.x / tileSize) * tileSize, (int)(pos.y / tileSize) * tileSize) + new Vector3(tileSize, tileSize) / 2f;
        }

        static public Vector2Int AverageGridPos(List<Vector2Int> gridPositions)
        {
            Vector2Int total = default;

            foreach (Vector2Int pos in gridPositions) {
                total += pos;
            }

            return total / gridPositions.Count;
        }

        /// <summary>
        /// x, y: returns the closest postion of those provided. 
        /// z: returns the closest distance.
        /// </summary>
        static public Vector3 Closest(List<Vector2> positions, Vector2 to)
        {
            Vector3 closestPos = new(0, 0, Mathf.Infinity);

            foreach (Vector2 pos in positions) {
                float dist = Vector2.Distance(pos, to);
                if (dist < closestPos.z)
                    closestPos = new Vector3(pos.x, pos.y, dist);
            }

            return closestPos;
        }

        static public Vector2Int Closest(List<Vector2Int> positions, Vector2Int to)
        {
            Vector3 closestPos = new(0, 0, Mathf.Infinity);

            foreach (Vector2Int pos in positions) {
                float dist = Vector2Int.Distance(pos, to);
                if (dist < closestPos.z)
                    closestPos = new Vector3(pos.x, pos.y, dist);
            }

            return new Vector2Int((int)closestPos.x, (int)closestPos.y);
        }

        static public int ClosestIdx(List<Vector2> positions, Vector2 to)
        {
            Vector3 closestPos = new(0, 0, Mathf.Infinity);
            int closestIdx = 0;

            for (int i = 0; i < positions.Count; i++) {
                Vector2 pos = positions[i];
                float dist = Vector2.Distance(pos, to);
                if (dist < closestPos.z) {
                    closestPos = new Vector3(pos.x, pos.y, dist);
                    closestIdx = i;
                }
            }

            return closestIdx;
        }

    }
}
