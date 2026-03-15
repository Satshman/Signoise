using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ManaUi : MonoBehaviour
{
    [SerializeField] private TMP_Text mana;

    public void UpdateManaText(int currentMana)
    {
        mana.text=currentMana.ToString();
    }
}
