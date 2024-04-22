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

        //text_proficiencies_weapon.text = character.prof.profWeapon.ToString();
        //text_proficiencies_hacking.text = character.prof.profHacking.ToString();
        //text_proficiencies_movement.text = character.prof.profMovement.ToString();

        //text_trait.text = character.trait.name;
        //img_trait.sprite = character.trait.icon;
    }
}
