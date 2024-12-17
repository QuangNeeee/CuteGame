using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class DayCycle : MonoBehaviour
{
    [SerializeField] private Gradient lightColor;
    [SerializeField] private GameObject light;
    [SerializeField] private GameObject boss;
    public GameObject spotLight;

    private int days;
    private int Days => days;
    public float time = 50;
    private bool canChangeDay = true;
    public delegate void OnDayChanged();
    public OnDayChanged DayChanged;

    private void Update()
    {
        if (time > 500)
        {
            time = 0;
        }

        if ((int)time == 170)
        {
            spotLight.SetActive(true);
            boss.SetActive(true);
        }
        if ((int)time == 250 && canChangeDay)
        {
            canChangeDay = false;
            DayChanged();
            days++;
        }

        if ((int)time == 251)
        {
            canChangeDay = true;
        }
        if ((int)time == 330)
        {
            spotLight.SetActive(false);
            boss.SetActive(false);
        }

        time += Time.deltaTime;
        light.GetComponent<Light2D>().color = lightColor.Evaluate(time * 0.002f);
    }
}
