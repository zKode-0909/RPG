using UnityEngine;

public static class SimulationUtils
{
    public static bool InRange(Vector2 pos1, Vector2 pos2, float range) {
        return (pos1 - pos2).sqrMagnitude <= range * range;
    }
}
