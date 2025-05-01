using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rive;
using Rive.Components;

public class RiveButtonListener : MonoBehaviour
{
    [SerializeField] private RiveWidget m_riveWidget; // Reference to the Rive widget component

    void OnEnable()
    {
        m_riveWidget.OnWidgetStatusChanged += OnWidgetStatusChanged;
    }

    private void OnWidgetStatusChanged()
    {
        // Wait for the Rive Widget to load before accessing the state machine.
        if (m_riveWidget.Status == WidgetStatus.Loaded)
        {
            StateMachine m_stateMachine = m_riveWidget.StateMachine;

            SMIBool someBool = m_stateMachine.GetBool("small 1 pressed");
            if (someBool == null) return;
            Debug.Log(someBool.Value);
            //someBool.Value = !someBool.Value;
            //Debug.Log(someBool.Value);
        }
    }


    void OnDisable()
    {
        m_riveWidget.OnWidgetStatusChanged -= OnWidgetStatusChanged;
    }


}
