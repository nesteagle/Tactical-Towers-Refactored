public class Building : Structure
{
    override protected void Remove()
    {
        BuildingMap.RemoveBuilding(Position);
        GameMap.RemoveAttackableStructure(Position);
        Destroy(gameObject); // TODO: check alternatives
    }
}
