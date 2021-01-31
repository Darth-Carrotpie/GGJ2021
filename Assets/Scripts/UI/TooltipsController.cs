using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TooltipsController : MonoBehaviour {
    public List<GameObject> tooltips;

    public float tooltipStayLength = 5f;

    public float tooltipPauseLength = 2f;

    float timeCounter;
    int currentTooltip;

    void Update() {
        timeCounter += Time.deltaTime;

        if (currentTooltip >= tooltips.Count)
            Destroy(gameObject);

        if (timeCounter > (tooltipStayLength + tooltipPauseLength) * currentTooltip) {
            tooltips[currentTooltip].SetActive(true);
        }
        if (timeCounter > (tooltipStayLength + tooltipPauseLength) * currentTooltip + tooltipStayLength) {
            tooltips[currentTooltip].SetActive(false);
            currentTooltip++;
        }
    }
}