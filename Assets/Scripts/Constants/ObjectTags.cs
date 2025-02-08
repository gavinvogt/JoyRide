public class ObjectTags
{
    public readonly static string OBSTACLE_BLOCKING = "ObstacleBlocking";
    public readonly static string OBSTACLE_NON_BLOCKING = "ObstacleNonBlocking";
    public readonly static string INDESTRUCTABLE_OBSTACLE = "IndestructableObstacle";
    public readonly static string INDESTRUCTABLE_OBSTACLE_NON_BLOCKING = "IndestructableObstacleNonBlocking";
    public readonly static string BOUNDARY = "Boundary";
    public readonly static string BOOSTER = "Booster";

    public static bool IsObstacle(string tag)
    {
        return tag == OBSTACLE_BLOCKING || tag == OBSTACLE_NON_BLOCKING || tag == INDESTRUCTABLE_OBSTACLE || tag == INDESTRUCTABLE_OBSTACLE_NON_BLOCKING;
    }

    public static bool IsBlockingObstacle(string tag)
    {
        return tag == OBSTACLE_BLOCKING || tag == INDESTRUCTABLE_OBSTACLE;
    }

    public static bool IsDestructableObstacle(string tag)
    {
        return tag == OBSTACLE_BLOCKING || tag == OBSTACLE_NON_BLOCKING;
    }

    public static bool ShouldKillPlayer(string tag)
    {
        return tag == OBSTACLE_BLOCKING || tag == BOUNDARY || tag == INDESTRUCTABLE_OBSTACLE;
    }

    public static bool ShouldSpeedUp(string tag)
    {
        return tag == OBSTACLE_BLOCKING || tag == BOOSTER || tag == OBSTACLE_NON_BLOCKING;
    }
}
