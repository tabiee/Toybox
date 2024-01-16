using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AI;

public class GatherControls : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject cam;
    [SerializeField] private CreatureUI ui;
    [SerializeField] private SceneStateManager sceneState;

    [SerializeField] private float pullForce;
    [SerializeField] private float dispenseForce;
    [SerializeField] private float distanceFactor;

    private Vector3 pull;
    private Rigidbody rb;
    private Collectable ai;

    [SerializeField] private float raycastDistance = 10f;
    [SerializeField] private float raycastForce;
    [SerializeField] private LayerMask layerMask;

    private float dispenseCD = 0.5f;
    private float dispenseAllow = 0f;
    private void OnTriggerStay(Collider other)
    {
        if (Input.GetMouseButton(1))
        {
        if (other == null) return;

            if (other.GetComponent<Rigidbody>() && other.tag != "Player")
            {
                rb = other.GetComponent<Rigidbody>();
                ai = other.GetComponent<Collectable>();

                pull = transform.position - other.transform.position;
                ai.isPickedUp = true;

                float distance = pull.magnitude;
                float factorPull = pullForce / (1 - distance / distanceFactor);

                if (distance > 0.001f)
                {
                    rb.AddForce(pull.normalized * factorPull * Time.deltaTime);
                }
            }
        }
    }

    private void Update()
    {
        DebugStuff();

        if (Input.GetMouseButton(1))
        {
            Siphon();
        }
        else if (Input.GetMouseButton(0))
        {
            string creatureString = ui.stringForCreature;
            int creatureValue = LevelData.GetCreatureValue(creatureString);

            if (creatureValue > 0 && Time.time > dispenseAllow)
            {
                dispenseAllow = Time.time + dispenseCD;

                creatureValue--;
                LevelData.SetCreatureValue(creatureString, creatureValue);

                CreaturePrefabs myScriptableObject = Resources.Load<CreaturePrefabs>(creatureString);
                Vector3 spawnPoint = player.transform.position + player.transform.forward * 2 + Vector3.up;

                GameObject spawnedCreature = Instantiate(myScriptableObject.prefab, spawnPoint, Quaternion.identity);

                foreach (Transform childTransform in spawnedCreature.transform)
                {

                    Rigidbody creatureRB = childTransform.GetComponent<Rigidbody>();

                    if (creatureRB != null)
                    {
                        creatureRB.velocity = player.transform.forward * dispenseForce;
                        break;
                    }
                }
            }
        }
    }
    private void Siphon()
    {
        //y ray = new Ray(cam.transform.position, cam.transform.forward);
        //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        RaycastHit hit;
        Ray ray = new Ray(cam.transform.position, cam.transform.TransformDirection(Vector3.forward));

        if (Physics.Raycast(ray, out hit, raycastDistance, ~layerMask))
        {
            if (hit.transform.gameObject.TryGetComponent(out Rigidbody rbody))
            {
                Vector3 forceDirection = cam.transform.position - hit.transform.position;
                rbody.AddForce(forceDirection.normalized * raycastForce, ForceMode.Force);
            }
        //Debug.Log("Raycast hit: " + hit.collider.name + " at position: " + hit.point + " with normal: " + hit.normal);
        }
       Debug.DrawRay(ray.origin, ray.direction * raycastDistance, Color.red, 0.1f);
    }
    void DebugStuff()
    {

        if (Input.GetKeyDown(KeyCode.T))
        {
            Debug.Log($"current data is {LevelData.levelToLoad} and {LevelData.zoneToLoad}");
            Debug.Log($"greens are {LevelData.greenCreature}");
            Debug.Log($"yellows are {LevelData.yellowCreature}");
            Debug.Log($"reds are {LevelData.redCreature}");
        }

        if (Input.GetKeyDown(KeyCode.F1))
        {
            sceneState.SaveSceneState(SceneManager.GetActiveScene().name);
            Invoke("QuitToUI", 0.1f);
        }
    }
    void QuitToUI()
    {
            SceneManager.LoadScene("BattleUI");
            Cursor.lockState = CursorLockMode.None;
    }
}
