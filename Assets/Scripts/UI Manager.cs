using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("UI Elements")]
    public TextMeshProUGUI ObjectiveText;
    public TextMeshProUGUI PressEText;
    [SerializeField] public TextMeshProUGUI NoExitText;
    public Slider signalSlider;
    public Slider staminaSlider;
    public Image staminaColor;

    [Header("References")]
    public SC_FPSController player; // À assigner via l'Inspector ou au Spawn
    public GameObject exit;
    public List<Page> pages = new List<Page>();

    [Header("Settings")]
    public float minBar = 5f;
    public float maxBar = 500f;
    public int totalPages = 10;

    // Start is called before the first frame update
    void Start()
    {
        DisablePressE();
        if (NoExitText != null) NoExitText.enabled = false;
        maxBar = 500f;

        // Si le player n'est pas assigné dans l'inspector, on essaie de le trouver
        if (player == null)
        {
            GameObject playerObj = GameObject.Find("FPSPlayer");
            if (playerObj != null)
                player = playerObj.GetComponent<SC_FPSController>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null) return;

        if (pages.Count > 0)
        {
            Page targetPage = closestPage();
            if (targetPage != null)
                updateBarFill(Vector3.Distance(player.transform.position, targetPage.transform.position));
        }
        else if (exit != null)
        {
            updateBarFill(Vector3.Distance(player.transform.position, exit.transform.position));
        }

        updateStaminaBar();
        updateColor();
    }

    public void UpdateObjectiveText(int currentPages)
    {
        if (currentPages >= totalPages)
        {
            ObjectiveText.text = "Objective:\nGet to the exit...";
        }
        else
        {
            ObjectiveText.text = $"Objective:\nFind out...\n{currentPages}/{totalPages} pages collected";
        }
    }

    public void EnablePressE() => PressEText.enabled = true;
    public void DisablePressE() => PressEText.enabled = false;

    public Page closestPage()
    {
        if (pages.Count == 0) return null;

        Page closest = pages[0];
        float minDist = Mathf.Infinity;
        Vector3 playerPosition = player.transform.position;

        foreach (Page page in pages)
        {
            if (page == null) continue;
            float distanceToPlayer = Vector3.Distance(page.transform.position, playerPosition);
            if (distanceToPlayer < minDist)
            {
                minDist = distanceToPlayer;
                closest = page;
            }
        }
        return closest;
    }

    private void updateBarFill(float distance)
    {
        signalSlider.value = maxBar - distance;
    }

    private void updateStaminaBar()
    {
        float stamina = player.getStamina();
        staminaSlider.value = stamina < 0.0f ? 0.0f : stamina;
    }

    private void updateColor()
    {
        bool canRun = player.getCanRun();
        staminaColor.color = canRun ? Color.yellow : Color.red;
    }
}