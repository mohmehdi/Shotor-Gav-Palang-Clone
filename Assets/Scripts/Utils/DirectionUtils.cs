using System.Collections.Generic;
using UnityEngine;

public static class DirectionUtils
{
    readonly static Dictionary<Direction, Vector2> _directionMapping = new()
    {
        { Direction.Up, Vector2.up },
        { Direction.Down, Vector2.down },
        { Direction.Left, Vector2.left },
        { Direction.Right, Vector2.right }
    };
    readonly static Dictionary<Direction, Direction> _oppositeDirectionMapping = new()
    {
        { Direction.Up, Direction.Down },
        { Direction.Down, Direction.Up },
        { Direction.Left, Direction.Right },
        { Direction.Right, Direction.Left }
    };

    public static Vector2 GetDirectionVector(this Direction direction)
    {
        if (_directionMapping.TryGetValue(direction, out var vector))
            return vector;

        return Vector2.zero;
    }

    public static Direction GetOppositeDirection(this Direction direction)
    {
        if (_oppositeDirectionMapping.TryGetValue(direction, out var oppositeDirection))
            return oppositeDirection;

        Debug.LogError("Invalid Direction");
        return Direction.Up;
    }
}
