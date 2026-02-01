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
    public SC_FPSController player;
    public Slider signalSlider;
    public Slider staminaSlider;
    public Image staminaColor;
    public float minBar = 5f;
    public float maxBar = 500f;
    private SC_FPSController player_class;
    public int totalPages = 10;

    public void UpdateObjectiveText()
    {
        int pages = SC_FPSController.Instance._pages;
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
        player_class = GameObject.Find("FPSPlayer").GetComponent<SC_FPSController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (pages.Count > 0)
            updateBarFill(closestDist(closestPage()));      
        else
            updateBarFill(Vector3.Distance(player.transform.position, exit.transform.position));
        updateStaminaBar();
        updateColor();
    }

    public Page closestPage() {
        Page closest = pages[0];
        float minDist = Mathf.Infinity;
        Vector3 playerPosition = player.transform.position;

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
        float distanceToTarget = Vector3.Distance(player.transform.position, page.transform.position);
        return distanceToTarget;
    }

    private void updateBarFill(float distance) {
        signalSlider.value = maxBar - distance;
    }

    private void updateStaminaBar()
    {
        float stamina = player_class.getStamina();
        staminaSlider.value = stamina < 0.0f ? 0.0f : stamina;
        Debug.Log(stamina);
    }
    private void updateColor()
    {
        bool canRun = player_class.getCanRun();
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
