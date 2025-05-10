using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitMover : MonoBehaviour
{
    private Unit _unit;
    private const float Speed = 1.5f;
    private const float RestTime = 0.1f;

    private void Awake()
    {
        _unit = GetComponent<Unit>();
    }

    private IEnumerator MoveCoroutine(HexCell c)
    {
        List<Node> path = Pathfinding.FindPath(HexMap.GetCell(_unit.Position), c);
        IEnumerator<Node> pathEnumerator = path.GetEnumerator();
        pathEnumerator.MoveNext(); // skips the first element
        while (pathEnumerator.MoveNext())
        {
            HexCell dest = (HexCell)pathEnumerator.Current;
            yield return StartCoroutine(MoveStep(dest));
        }
        _unit.State = UnitState.Rest;
    }

    public void MoveTo(HexCell c)
    {
        _unit.State = UnitState.Moving;
        StartCoroutine(MoveCoroutine(c));
    }

    private IEnumerator MoveStep(HexCell to)
    {
        Vector3 originPos = _unit.transform.position;
        Vector3 destPos = new(to.transform.position.x, to.transform.position.y);
        yield return new WaitForSeconds(RestTime);
        for (float i = 0; i < 1; i += Time.deltaTime * Speed)
        {
            _unit.transform.position = Vector3.Lerp(originPos, destPos, i);
            yield return new WaitForEndOfFrame();
        }
        _unit.transform.position = destPos;
        _unit.Position = to.Position;
    }
}
