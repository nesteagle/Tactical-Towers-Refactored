using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickHandler : MonoBehaviour
{
    private Camera _mainCamera;
    [SerializeField] private HexMap _map;
    [SerializeField] private BuildingPlacer _buildingPlacer;

    [SerializeField] private LayerMask _unitLayer;

    private void Awake()
    {
        _mainCamera = Camera.main;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 clickPos = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
            clickPos.z = 0;

            RaycastHit2D hit = Physics2D.Raycast(clickPos, Vector2.zero, _unitLayer);

            if (hit.collider != null)
            {
                // unit clicked
                Debug.Log("Unit clicked: " + hit.collider.gameObject.name);
            }
            else
            {
                Axial axial = HexMapUtil.GetCellAxialFromPosition(clickPos);
                HexCell cell = HexMap.GetCell(axial);
                if (cell != null)
                {
                    // cell clicked
                    _buildingPlacer.PlaceBuilding(cell);
                }
            }
        }
    }
}
