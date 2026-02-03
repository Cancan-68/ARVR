using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private static UIManager _instance;
    public static UIManager Instance
    {
        get
        {
            if (_instance == null)
            {
                print("UIManager is null");
            }
            return _instance;
        }
    }

    private void Awake()
    {
        _instance = this;
    }

    public TextMeshProUGUI ObjectiveText;
    public TextMeshProUGUI PressEText;
    [SerializeField] public TextMeshProUGUI NoExitText;
    public List<Page> pages;
    public GameObject exit;

    public Transform player;
    public Slider signalSlider;
    public Slider staminaSlider;
    public Image staminaColor;
    public float minBar = 5f;
    public float maxBar = 500f;
    // private SC_FPSController player_class;
    public int totalPages = 10;

    public void UpdateObjectiveText()
    {
        int pages = VRPlayerManager.Instance != null ? VRPlayerManager.Instance._pages : 0;
        // int pages = 0;
        if (pages >= totalPages)
        {
            ObjectiveText.text = "Objective:\nGet to the exit...";
        }
        else
        {
            ObjectiveText.text = "Objective:\nFind out...\n" + pages + "/" + totalPages + " pages collected";
        }
    }

    public void EnablePressE()
    {
        PressEText.enabled = true;
    }
    public void DisablePressE()
    {
        PressEText.enabled = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        DisablePressE();
        NoExitText.enabled = false;
        maxBar = 500f;
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
        }
        else
        {
            Debug.LogError("Player not found! Make sure to tag your XR Origin/Camera as 'Player'");
        }
        // player_class = GameObject.Find("FPSPlayer").GetComponent<SC_FPSController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (pages.Count > 0)
        {
            if (player != null)
                updateBarFill(closestDist(closestPage()));
        }
        else
        {
            if (player != null && exit != null)
                updateBarFill(Vector3.Distance(player.position, exit.transform.position));
        }
        updateStaminaBar();
        updateColor();
    }

    public Page closestPage() {
        Page closest = pages[0];
        float minDist = Mathf.Infinity;
        if (player == null) return closest;
        Vector3 playerPosition = player.position;

        foreach(Page page in pages) {
            float distanceToPlayer = Vector3.Distance(page.transform.position, playerPosition);
            if (distanceToPlayer < minDist) {
                minDist = distanceToPlayer;
                closest = page;
            }
        }
        return closest;
    }

    public float closestDist(Page page) {
        if (player == null) return Mathf.Infinity;
        float distanceToTarget = Vector3.Distance(player.position, page.transform.position);
        return distanceToTarget;
    }

    private void updateBarFill(float distance) {
        signalSlider.value = maxBar - distance;
    }

    private void updateStaminaBar()
    {
        if (VRPlayerManager.Instance == null) return;

        float stamina = VRPlayerManager.Instance.getStamina();
        staminaSlider.value = stamina < 0.0f ? 0.0f : stamina;
        // Debug.Log(stamina);
    }
    private void updateColor()
    {
        if (VRPlayerManager.Instance == null) return;

        bool canRun = VRPlayerManager.Instance.getCanRun();
        if (canRun)
        {
            staminaColor.color = Color.yellow;
        }
        else
        {
            staminaColor.color = Color.red;
        }
    }
}
