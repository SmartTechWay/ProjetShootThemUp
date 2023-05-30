using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

/* Ce script Unity permet d'afficher la valeur d'un curseur (Slider) dans un champ de texte (TextMeshProUGUI).
 * Ainsi, lorsque vous bougez le curseur, le texte change pour afficher la nouvelle valeur du curseur.
 */

public class TextSlider : MonoBehaviour
{
    public TextMeshProUGUI numberText;

    private Slider slider;

     void Start()
     {
        slider = GetComponent<Slider>();
        SetNumberText(slider.value);
     }

    public void SetNumberText(float value)
    {
        numberText.text = value.ToString();
    }
}
