using UnityEngine;
using UnityEngine.Serialization;


public class BuildingHandler : MonoBehaviour {

    [SerializeField, FormerlySerializedAs("city")]  private City City;
    [SerializeField, FormerlySerializedAs("uiController")] private UIController UiController;
    [SerializeField, FormerlySerializedAs("buildings")] private Building[] Buildings;
    [SerializeField, FormerlySerializedAs("board")] private Board Board;
    private Building _selectedBuilding;

    // Update is called once per frame
    private void Update () 
    {
        if (Input.GetMouseButton(0) && Input.GetKey(KeyCode.LeftShift) && _selectedBuilding != null)
        {
            InteractWithBoard(0);
        }
		else if (Input.GetMouseButtonDown(0) && _selectedBuilding != null)
        {
            InteractWithBoard(0);
        }

        if (Input.GetMouseButtonDown(1))
        {
            InteractWithBoard(1);
        }
    }

    private void InteractWithBoard(int action)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            Vector3 gridPosition = Board.CalculateGridPosition(hit.point);
            if (!UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
            {
                if (action == 0 && Board.CheckForBuildingAtPosition(gridPosition) == null)
                {
                    if (City.Cash >= _selectedBuilding.cost)
                    {
                        City.DepositCash(-_selectedBuilding.cost);
                        UiController.UpdateCityData();
                        City.buildingCounts[_selectedBuilding.id]++;
                        Board.AddBuilding(_selectedBuilding, gridPosition);
                    }
                }
                else if (action == 1 && Board.CheckForBuildingAtPosition(gridPosition) != null)
                {
                    
                    City.DepositCash(Board.CheckForBuildingAtPosition(gridPosition).cost/2);
                    Board.RemoveBuilding(gridPosition);
                    UiController.UpdateCityData();
                }
            }
        }
    }

    public void EnableBuilder(int building)
    {
        _selectedBuilding = Buildings[building];
        Debug.Log("Selected building: " + _selectedBuilding.buildingName);
    }
}
