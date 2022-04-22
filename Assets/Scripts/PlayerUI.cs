using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerUI : MonoBehaviour
{
    public TextMeshProUGUI LevelText;
    public TextMeshProUGUI InventoryText;
    public TextMeshProUGUI InteractText;
    public Image HealthBarFill;
    public Image XPBarFill;

    private Player _player;

    private void Awake()
    {
        _player = FindObjectOfType<Player>();
    }

    public void UpdateLevelText() => LevelText.text = "Lvl\n" + _player.CurLevel.ToString();

    public void UpdateHealthBar() => HealthBarFill.fillAmount = (float)_player.CurHP / (float)_player.MaxHP;
    public void UpdateXPBar() => XPBarFill.fillAmount = (float)_player.CurXp / (float)_player.XpToNextLevel;

    public void SetInteractText(Vector3 pos, string text)
    {
        InteractText.gameObject.SetActive(true);
        InteractText.text = text;

        InteractText.transform.position = Camera.main.WorldToScreenPoint(pos + Vector3.up);
    }

    public void DisableInteractText()
    {
        if (InteractText.gameObject.activeInHierarchy)
        {
            InteractText.gameObject.SetActive(false);
        }
    }
}
