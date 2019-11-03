using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerPlacement : MonoBehaviour
{
    [System.Serializable]
    public class Tower
    {
        public string towerName;
        public GameObject tower;
        public GameObject hologram;
        public Button selectButton;
        public int cost;
    }

    public List<Tower> towers; // Allows class to be viewed in the inspector

    PlayerResources pr;

    // public Dropdown list;

    Tower selectedTower;
    GameObject currentHologram;

    //public Color canPlace;
    //public Color cannotPlace;
     

    [Header("Raycasts")]
    public float rayDistance = 1000f;
    public LayerMask hitLayers;
    public QueryTriggerInteraction triggerInteraction;

    private int currentIndex; // Current prefab selected

    void DrawRay(Ray ray)
    {
        Gizmos.DrawLine(ray.origin, ray.origin + ray.direction * 1000f);
    }
    
    // Use this for initialization
    void OnDrawGizmos()
    {
        Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        Ray playerRay = new Ray(transform.position, transform.forward);
        //float angle = Vector3.Angle(mouseRay.direction, playerRay.direction);
        //print(angle);
        Gizmos.color = Color.white;
        DrawRay(mouseRay);
        Gizmos.color = Color.red;
        DrawRay(playerRay);
    }

    private void Start()
    {
        pr = GetComponent<PlayerResources>();
        AssignButtons();
        SelectTower(towers[0]);
    }
    
    void AssignButtons()
    {
        foreach (Tower t in towers)
        {
            t.selectButton.onClick.AddListener(() => SelectTower(t));
            t.selectButton.GetComponent<Text>().text = t.towerName;
            print(t + "is assigned to button " + t.selectButton.name);
        }
        

        // list.onValueChanged.AddListener(delegate { SelectTower(towers[list]); });
        //list.onValueChanged.AddListener(delegate { SelectTower(towers})

    }

    // Update is called once per frame
    void Update()
    {
        currentHologram.SetActive(false);
        Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition); // Create ray from mouse position on Camera
        RaycastHit hit;
        if (Physics.Raycast(mouseRay, out hit, rayDistance, hitLayers, triggerInteraction)) // Perform Raycast
        {
            //Debug.Log("kablah");
            Placeable p = hit.transform.GetComponent<Placeable>(); // Try getting Placeable script
            if (p && p.isAvailable) // If it is a placeable AND it's available (no tower spawned)
            {
                bool canPurchase;
                if (pr.currency >= selectedTower.cost)
                {
                    canPurchase = true;
                }
                else
                {
                    canPurchase = false;
                }

                currentHologram.SetActive(true);
                currentHologram.transform.position = p.GetPivotPoint(); // Set position of hologram to pivot point (if any)

                if (canPurchase)
                {
                    //currentHologram.GetComponent<Material>().color = canPlace;

                    // If Left mouse is down 
                    if (Input.GetMouseButtonDown(0))
                    {
                        GameObject tower = Instantiate(selectedTower.tower); // Instantiates tower
                        tower.transform.position = p.GetPivotPoint(); // Position to placeable
                        p.isAvailable = false; // The Tile is no longer available
                        pr.currency -= selectedTower.cost; // Subtracts appropriate cost from available currency
                    }
                }
                else
                {
                    //currentHologram.GetComponent<Material>().color = cannotPlace;
                }
            }
        }
    }

    public void SelectTower(Tower towerToSelect)
    {
        if (currentHologram != null)
        {
            Destroy(currentHologram.gameObject);
        }
        currentHologram = Instantiate(towerToSelect.hologram);
        print("Tower selected changed from " + selectedTower + " to " + towerToSelect + ".");
        selectedTower = towerToSelect;
    }
}