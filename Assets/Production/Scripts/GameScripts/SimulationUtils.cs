using UnityEngine;

public class SimulationUtils
{
    public float CheckDistanceFromHuman(Vector2 SimPosition, Vector2 HumanPosition)
    {
        Vector2 toTarget = HumanPosition - SimPosition;

        return toTarget.magnitude;




    }
}
