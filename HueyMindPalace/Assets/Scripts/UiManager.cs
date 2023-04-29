using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class UiManager : MonoBehaviour
{
    public GameObject tooltip;
    public TextMeshProUGUI tooltipText;
    public TextMeshProUGUI uses;
    public TextMeshProUGUI cooldown;
    public TextMeshProUGUI CurrentManaText;
    public Image skillIcon;
    public Image SkillColor;
    public TextMeshProUGUI manaToolTip;
    Animator tooltipAnimation;
    public TextMeshProUGUI TitleText;
    public Image ManaFill;
    Color iconColor;


    // Start is called before the first frame update
    void Start()
    {
        tooltip.SetActive(false);
        tooltipAnimation = tooltip.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void updateManatext(float maxMana, float currMana)
    {
       
        CurrentManaText.text = "MP: " + currMana.ToString() + "/" + maxMana.ToString();
        ManaFill.fillAmount = currMana / maxMana;
        

    }
    public void updateToolTip(string tooltipToSet, string usestoset, string cooldowntoset, Sprite skillIconImport, Color iconColor, string TitleTextImport,int manaCost,bool infiniteuses)
    {
        tooltip.SetActive(true);
        tooltipText.text = tooltipToSet;
        if(infiniteuses == false)
        {
            uses.text = usestoset;
        }else if(infiniteuses == true)
        {
            uses.text = "∞";
        }
        
        cooldown.text = cooldowntoset;
        skillIcon.sprite = skillIconImport;
        SkillColor.color = iconColor;
        tooltipAnimation.Play("TooltipOpen");
        TitleText.text = TitleTextImport;
        manaToolTip.text = manaCost.ToString();
    }


    public void closeMenu()
    {
        tooltipAnimation.Play("ToolTipClose");
    }



}
