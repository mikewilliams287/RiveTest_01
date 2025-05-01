using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
using Rive;
using Rive.Components;
using System.Reflection;
using Unity.VisualScripting;


public class RiveWidgetController : MonoBehaviour
{

    [SerializeField] private RiveUICustomData customData; // Reference to the custom data script
    private RiveWidget riveWidget; // Reference to the Rive widget component


    // String properties for the small buttons and center box text.
    private ViewModelInstanceStringProperty smallBtn_01_stringProperty;
    private ViewModelInstanceStringProperty smallBtn_02_stringProperty;
    private ViewModelInstanceStringProperty smallBtn_03_stringProperty;
    private ViewModelInstanceStringProperty centerBoxText_stringProperty = null;

    // Boolean properties for the buttons.
    private ViewModelInstanceBooleanProperty smallBtn_01_boolProperty;
    private ViewModelInstanceBooleanProperty smallBtn_02_boolProperty;
    private ViewModelInstanceBooleanProperty smallBtn_03_boolProperty;
    // Track previous value of bool for each button
    private bool previousSmallBtn_01_Value = false;
    private bool previousSmallBtn_02_Value = false;
    private bool previousSmallBtn_03_Value = false;

    // Number properties for weapon stats
    private ViewModelInstanceNumberProperty weaponPower_numberProperty;
    private ViewModelInstanceNumberProperty weaponFireRate_numberProperty;


    private void Awake()
    {
        // Automatically set the reference for riveWidget
        if (riveWidget == null)
        {
            riveWidget = GetComponent<RiveWidget>();
            if (riveWidget == null)
            {
                Debug.LogError("RiveWidget component not found on the same GameObject.");
            }
        }
    }
    private void OnEnable()
    {
        riveWidget.OnWidgetStatusChanged += HandleWidgetStatusChanged;
    }

    private void OnDisable()
    {
        riveWidget.OnWidgetStatusChanged -= HandleWidgetStatusChanged;
    }

    private void HandleWidgetStatusChanged()
    {
        // Check if the widget is loaded before accessing the view model instance
        if (riveWidget.Status == WidgetStatus.Loaded)
        {
            ViewModelInstance viewModelInstance = riveWidget.StateMachine.ViewModelInstance;


            //// STRING PROPERTIES ////

            // Small Button 1 string property
            smallBtn_01_stringProperty = viewModelInstance.GetStringProperty("SmallBtn_01_Text");
            if (customData != null && customData.weaponNames.Count > 0)
            {
                smallBtn_01_stringProperty.Value = customData.weaponNames[0]; // Set the value to the first index of the list

            }
            else
            {
                Debug.LogWarning("RiveUICustomData is null or list is empty.");
            }


            // Small Button 2 string property
            smallBtn_02_stringProperty = viewModelInstance.GetStringProperty("SmallBtn_02_Text");
            if (customData != null && customData.weaponNames.Count > 1)
            {
                smallBtn_02_stringProperty.Value = customData.weaponNames[1]; // Set the value to the second index of the list
            }
            else
            {
                Debug.LogWarning("RiveUICustomData is null or list is empty.");
            }

            // Small Button 3 string property
            smallBtn_03_stringProperty = viewModelInstance.GetStringProperty("SmallBtn_03_Text");
            if (customData != null && customData.weaponNames.Count > 2)
            {
                smallBtn_03_stringProperty.Value = customData.weaponNames[2]; // Set the value to the third index of the list
            }
            else
            {
                Debug.LogWarning("RiveUICustomData is null or list is empty.");
            }



            // Center Box string property
            centerBoxText_stringProperty = viewModelInstance.GetStringProperty("CenterBoxText");


            // BOOL PROPERTIES //

            // Small Button 1 boolean property
            smallBtn_01_boolProperty = viewModelInstance.GetBooleanProperty("Btn_01_pressed");
            Debug.Log($"Button 1 Bool value: {smallBtn_01_boolProperty.Value}");
            smallBtn_01_boolProperty.OnValueChanged += (newValue) =>
            {
                if (!previousSmallBtn_01_Value && newValue) SetDescriptionText(newValue, 0); // Only call if it changes from false to true

                previousSmallBtn_01_Value = newValue; // Update the previous value
            };


            // Small Button 2 boolean property
            smallBtn_02_boolProperty = viewModelInstance.GetBooleanProperty("Btn_02_pressed");
            Debug.Log($"Button 2 Bool value: {smallBtn_02_boolProperty.Value}");
            smallBtn_02_boolProperty.OnValueChanged += (newValue) =>
            {
                if (!previousSmallBtn_02_Value && newValue) SetDescriptionText(newValue, 1);

                previousSmallBtn_02_Value = newValue;
            };


            // Small Button 3 boolean property
            smallBtn_03_boolProperty = viewModelInstance.GetBooleanProperty("Btn_03_pressed");
            Debug.Log($"Button 3 Bool value: {smallBtn_03_boolProperty.Value}");
            smallBtn_03_boolProperty.OnValueChanged += (newValue) =>
            {
                if (!previousSmallBtn_03_Value && newValue) SetDescriptionText(newValue, 2);

                previousSmallBtn_03_Value = newValue;
            };


            // NUMBER PROPERTIES
            weaponPower_numberProperty = viewModelInstance.GetNumberProperty("Power_num");
            Debug.Log($"Weapon power is: {weaponPower_numberProperty.Value}");
            weaponPower_numberProperty.Value = 0;

            weaponFireRate_numberProperty = viewModelInstance.GetNumberProperty("Rate_num");
            Debug.Log($"Weapon Fire Rate is: {weaponFireRate_numberProperty.Value}");
            weaponFireRate_numberProperty.Value = 0;
        }
    }


    private void SetDescriptionText(bool newValue, int index)
    {
        if (newValue)
        {
            //Debug.Log($"Button 1: {smallBtn_01_boolProperty.Value}, Button 2: {smallBtn_02_boolProperty.Value}, Button 3: {smallBtn_03_boolProperty.Value}");

            if (customData != null && customData.weaponDescriptions.Count > index)
            {
                centerBoxText_stringProperty.Value = customData.weaponDescriptions[index];
                weaponPower_numberProperty.Value = customData.weaponPowers[index];
                weaponFireRate_numberProperty.Value = customData.weaponFireRates[index];
                Debug.Log($"Description index is: {index} and the value is: {customData.weaponDescriptions[index]}");
            }
            else
            {
                Debug.LogWarning("RiveUICustomData is null or weaponDescriptions list does not contain the specified index.");
            }
        }
        else
        {
            // Reset the center box text if the button is not pressed
            centerBoxText_stringProperty.Value = "Select a weapon to see its description.";
            weaponPower_numberProperty.Value = 0;
            weaponFireRate_numberProperty.Value = 0;
            Debug.Log($"Description index is: {index} and the value is: {centerBoxText_stringProperty.Value}");
        }
    }

    private void OnDestroy()
    {
        // Remove propery listeners
        if (smallBtn_01_boolProperty != null) smallBtn_01_boolProperty.OnValueChanged -= (newValue) => SetDescriptionText(newValue, 0);

        if (smallBtn_02_boolProperty != null) smallBtn_02_boolProperty.OnValueChanged -= (newValue) => SetDescriptionText(newValue, 1);

        if (smallBtn_03_boolProperty != null) smallBtn_03_boolProperty.OnValueChanged -= (newValue) => SetDescriptionText(newValue, 2);

    }


}
