using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class UnitMoverUI : MonoBehaviour
{
    private Vector3 _mousePos;
    private List<HexCell> _selectedPath = new();
    private HashSet<HexCell> _selectedCells = new();
    private bool _isCoroutineActive;

    public void StartSelectPath(Unit u)
    {
        if (_isCoroutineActive == false && u.State != UnitState.Moving)
        {
            StartCoroutine(SelectPath(u));
            _isCoroutineActive = true;
        }
    }

    private IEnumerator SelectPath(Unit origin)
    {
        Axial originPos = origin.Position;
        HexCell destination = null;
        while (Input.GetMouseButton(0))
        {
            _mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            destination = HexMap.GetCell(HexMapUtil.GetCellAxialFromPosition(_mousePos));
            List<Node> path = Pathfinding.FindPath(HexMap.GetCell(originPos), destination);
            _selectedPath = path.Select(c => (HexCell)c).ToList();
            _selectedCells.UnionWith(_selectedPath);
            UpdateCellColors();
            yield return new WaitForEndOfFrame();
        }
        if (destination != null)
        {
            origin.MoveTo(destination);
        }
        _isCoroutineActive = false;
    }

    private void UpdateCellColors()
    {
        HashSet<HexCell> unselectedCells = new(_selectedCells);
        foreach (HexCell c in _selectedCells)
        {
            if (_selectedPath.Contains(c))
            {
                c.SetColor(Color.red);
                unselectedCells.Remove(c);
            }
        }
        unselectedCells.ExceptWith(_selectedPath);
        foreach (HexCell c in unselectedCells)
        {
            c.ResetColor();
        }
    }
}
