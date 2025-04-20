using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RedarSpawnerScript : MonoBehaviour
{
    [SerializeField] private GameObject prefabToSpawn;
    [SerializeField] private GameObject noResourceMarker;
    [SerializeField] private Camera mainCamera;       

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !IsPointerOverUI()) 
        {
            if (ScoreManagerScript.Instance.Podrska > 1)
            {
                Vector3 mousePosition = Input.mousePosition;
                Vector3 worldPosition = mainCamera.ScreenToWorldPoint(mousePosition);
                worldPosition.z = 0f;
                Instantiate(prefabToSpawn, worldPosition, Quaternion.identity);
                ScoreManagerScript.Instance.Podrska-=2;
            }
            else
            {
                Vector3 mousePosition = Input.mousePosition;
                Vector3 worldPosition = mainCamera.ScreenToWorldPoint(mousePosition);
                worldPosition.z = 0f;
                Instantiate(noResourceMarker, worldPosition, Quaternion.identity);
            }
        }
    }

    bool IsPointerOverUI()
    {
        return EventSystem.current != null && EventSystem.current.IsPointerOverGameObject();
    }

}
