using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterCard : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text_proficiencies_weapon, text_proficiencies_hacking, text_proficiencies_movement;
    [SerializeField] TextMeshProUGUI text_trait, text_className;
    [SerializeField] Image img_class, img_trait;
    public void SetDisplay(Character character)
    {
        text_className.text = character.characterClass;

        text_proficiencies_weapon.text = "Weapon: " + (character.proficiencies.weaponSpreadMod + 5);
        text_proficiencies_hacking.text = "Hacking: " + character.proficiencies.hackSpeedMod;
        text_proficiencies_movement.text = "Movement: " + character.proficiencies.moveMod;

        switch (character.characterClass)
        {
            case "CAPTAIN":
                img_class.sprite = MenuManager.instance.classUI.classImages[1];
                break;
            case "ENGINEER":
                img_class.sprite = MenuManager.instance.classUI.classImages[3];
                break;
            case "DOCTOR":
                img_class.sprite = MenuManager.instance.classUI.classImages[2];
                break;
            case "NAVIGATOR":
                img_class.sprite = MenuManager.instance.classUI.classImages[4];
                break;
            case "CREWMATE":
                img_class.sprite = MenuManager.instance.classUI.classImages[0];
                break;
            default:
                break;
        }

        //text_trait.text = character.trait.name;
        //img_trait.sprite = character.trait.icon;
    }
}
