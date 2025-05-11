public class Building : Structure
{
    override protected void Remove()
    {
        BuildingMap.RemoveBuilding(Position);
        GameMap.RemoveAttackable(this);
        Destroy(this.gameObject); // TODO: check alternatives
    }
}
