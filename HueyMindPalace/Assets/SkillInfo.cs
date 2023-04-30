using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class SkillInfo : MonoBehaviour
{

    public float maxCooldown;
    public float currentCooldown;
    public int manaCost;
    public int maxUses;
    public int currentUses;
    Image CooldownFill;
    bool isOnCooldown;
    public TextMeshProUGUI useText;
    public TextMeshProUGUI CooldownText;
    public GameObject cooldownObject;
    private bool onCooldown;
    public UnityEvent SkillPreview;
    private CombatManager combat;
    public Character playercharacter;
    public bool infiniteUses;
    static bool ispreviewingSkill;
    public Sprite SkillIcon;
    public Image skilliconObject;
    private Image iconColo;
    Color iconColor;
    public string Title;
    public TextMeshProUGUI Manatext;

    [TextArea(15, 20)]
    public string ToolTip;

    // Start is called before the first frame update
    void Start()
    {
        currentUses = maxUses;
        currentCooldown = maxCooldown;
        isOnCooldown = false;
        useText.text = maxUses.ToString();
        cooldownObject.SetActive(false);
        onCooldown = false;
        CooldownFill = cooldownObject.GetComponent<Image>();
        combat = GameObject.FindGameObjectWithTag("GameManager").GetComponent<CombatManager>();
        if (playercharacter == null)
        {
            // if no player is specified, default to current player
            playercharacter = combat.currentPlayer;
        }
        playercharacter.skillInfos.Add(this);
        ispreviewingSkill = false;
        iconColo = GetComponent<Image>();
        iconColor = iconColo.color;
        SkillIcon = skilliconObject.sprite;
        Manatext.text = manaCost.ToString();
        CooldownText.text = maxCooldown.ToString();
    }

    private void FixedUpdate()
    {

        if (infiniteUses == false)
        {
            useText.text = currentUses.ToString();
        }else if(infiniteUses == true){
            useText.text = "∞";
        }
    }


    void StartCooldown()
    {
        if (maxCooldown != 0)
        {
            cooldownObject.SetActive(true);
            onCooldown = true;
            CooldownFill.fillAmount = currentCooldown/maxCooldown;
            
        }
    }

    public void tickCooldown()
    {
        //INSERT END TURN STUFF
        if (onCooldown == true)
        {
            currentCooldown -= 1;
            CooldownText.text = currentCooldown.ToString();
        }

        if (currentCooldown == 0)
        {
            onCooldown = false;
            cooldownObject.SetActive(false);
            currentCooldown = maxCooldown;
            CooldownText.text = currentCooldown.ToString();
        }
    }



    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCooldown();
        }
    }

    public void ActivateSkillPreview()
    {
        if (CanUseSkill())
        {
            ispreviewingSkill = true;
            SkillPreview.Invoke();

        }

    }

    public bool CanUseSkill()
    {
        return onCooldown != true && ispreviewingSkill == false && playercharacter.currMp >= manaCost && (currentUses > 0 || infiniteUses == true);
    }

    public void endskillPreview(bool Canceled)
    {
        if(Canceled == true)
        {
            ispreviewingSkill = false;
        }
        else if(Canceled == false)
        {
            ispreviewingSkill = false;
            StartCooldown();
            if(infiniteUses == false)
            {
                currentUses -= 1;

            }
            playercharacter.PayMP(manaCost);
        }


    }


    public void HoverOver()
    {
        GameObject.FindGameObjectWithTag("GameManager").GetComponent<UiManager>().updateToolTip(ToolTip,maxUses.ToString(),maxCooldown.ToString(),SkillIcon,iconColor,Title,manaCost,infiniteUses);
    }

    public void hoverOut()
    {
        GameObject.FindGameObjectWithTag("GameManager").GetComponent<UiManager>().closeMenu();

    }




}
