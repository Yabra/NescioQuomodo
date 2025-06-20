using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextIndicator : MonoBehaviour
{
    [SerializeField] private GameController gameController;
    [SerializeField] private float timeToAnimate;

    TextMeshProUGUI text;
    Animator animator;

    int startValue;
    Queue<int> changesQueue = new Queue<int>();
    bool changeInProgress;
    int currentChange;
    float timer;

    private void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        animator = GetComponent<Animator>();
    }

    private void Awake()
    {
        changesQueue.Clear();
    }

    void Update()
    {
        if (!changeInProgress && changesQueue.Count != 0)
        {
            currentChange = changesQueue.Dequeue();
            changeInProgress = true;
            timer = 0;
        }

        if (changeInProgress)
        {
            animator.SetInteger("change", currentChange);
            timer += Time.deltaTime;
            text.text = ((int)
                (startValue + currentChange * Math.Min(1, timer / timeToAnimate))
                ).ToString();

            if (timer > timeToAnimate)
            {
                startValue += currentChange;
                changeInProgress = false;
            }
        }

        else
        {
            animator.SetInteger("change", 0);
        }
    }

    public void AddValue(int value)
    {
        changesQueue.Enqueue(value);
    }

    public void SetBaseValue(int value)
    {
        if (text == null)
        {
            text = GetComponent<TextMeshProUGUI>();
        }

        startValue = value;
        changesQueue.Clear();
        changeInProgress = false;
        text.text = startValue.ToString();
    }
}
