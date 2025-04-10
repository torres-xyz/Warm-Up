using System;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    public static event EventHandler CoinCaught;

    [SerializeField] private float movementSpeed;
    private NavMeshAgent navMeshAgent;

    Camera mainCam;

    const string COIN_TAG = "Coin";
    const string CHEST_TAG = "Chest";


    // Start is called before the first frame update
    void Start()
    {
        mainCam = Camera.main;
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SetDestinationToMousePosition();
        }

        transform.LookAt(navMeshAgent.steeringTarget);
    }

    private void OnTriggerEnter(Collider collider)
    {
        string collisionTag = collider.gameObject.tag;

        if (collisionTag == COIN_TAG)
        {
            TouchedCoin(collider.gameObject);
        }
    }

    private void TouchedCoin(GameObject coin)
    {
        CoinCaught?.Invoke(this, EventArgs.Empty);
        Destroy(coin); //Manage this better later
    }

    private void SetDestinationToMousePosition()
    {
        Ray ray = mainCam.ScreenPointToRay(Input.mousePosition);

        Debug.Log($"on mouse down");

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            navMeshAgent.SetDestination(hit.point);
            Debug.Log($"On Mouse down hit");
        }
    }
}
