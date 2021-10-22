using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour {
    [SerializeField] private Image bar;
    [SerializeField] private Canvas canvas;

    public delegate float GetHealthPercentage();

    public GetHealthPercentage Health;

    private void Awake() {
        canvas.worldCamera = Camera.current;
    }

    private void Update() {
        if (Health == null) return;

        bar.fillAmount = Health();
    }
}