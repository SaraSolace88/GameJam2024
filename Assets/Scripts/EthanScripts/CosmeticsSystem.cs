using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CosmeticsSystem : MonoBehaviour
{
    [Header("Lists of Sprites")]
    [SerializeField] private List<Sprite> HeadSprites;
    [SerializeField] private List<Sprite> EyeSprites;
    [SerializeField] private List<Color> SkinColors;
    [SerializeField] private List<Sprite> ClothsSprites;
    [SerializeField] private List<Sprite> MicSprites;
    [SerializeField] private List<Sprite> LightSprites;
    [SerializeField] private List<Sprite> StageSprites;

    [Header("Lists of Ints")]
    public TextMeshProUGUI Headnumb;
    public TextMeshProUGUI Eyenumb;
    public TextMeshProUGUI Skinnumb;
    public TextMeshProUGUI Clothsnumb;
    public TextMeshProUGUI Micnumb;
    public TextMeshProUGUI Lightnumb;
    public TextMeshProUGUI Stagenumb;

    [Header("List of GameObjects")]
    [SerializeField] private GameObject PlayerSprite;
    [SerializeField] private GameObject StageSprite;
    [SerializeField] private GameObject MicSprite;
    [SerializeField] private GameObject HeadSprite;
    [SerializeField] private GameObject ClothsSprite;
    [SerializeField] private GameObject LightSprite;

    public bool BLevel;

    private int SelHead;
    private int SelEye;
    private int SelSkin;
    private int SelCloths;
    private int SelMic;
    private int SelLight;
    private int SelStage;

    private void Awake()
    {
        if (PlayerPrefs.HasKey("HeadNumb"))
        {
            SelHead = PlayerPrefs.GetInt("HeadNumb");
        }
        HeadSprite.GetComponent<SpriteRenderer>().sprite = HeadSprites[SelHead];


        if (PlayerPrefs.HasKey("SkinNumb"))
        {
            SelSkin = PlayerPrefs.GetInt("SkinNumb");
        }
        PlayerSprite.GetComponent<SpriteRenderer>().color = SkinColors[SelSkin];

        if (PlayerPrefs.HasKey("ClothsNumb"))
        {
            SelCloths = PlayerPrefs.GetInt("ClothsNumb");
        }
        ClothsSprite.GetComponent<SpriteRenderer>().sprite = ClothsSprites[SelCloths];

        if (PlayerPrefs.HasKey("MicNumb"))
        {
            SelMic = PlayerPrefs.GetInt("MicNumb");
        }
        MicSprite.GetComponent<SpriteRenderer>().sprite = MicSprites[SelMic];

        if (PlayerPrefs.HasKey("LightNumb"))
        {
            SelLight = PlayerPrefs.GetInt("LightNumb");
        }
        LightSprite.GetComponent<SpriteRenderer>().sprite = LightSprites[SelLight];


        if (PlayerPrefs.HasKey("StageNumb"))
        {
            SelStage = PlayerPrefs.GetInt("StageNumb");
        }
        StageSprite.GetComponent<SpriteRenderer>().sprite = StageSprites[SelStage];
    }

    private void OnGUI()
    {
        if (!BLevel)
        {
            Headnumb.text = (SelHead + 1).ToString();
            Eyenumb.text = (SelEye + 1).ToString();
            Skinnumb.text = (SelSkin + 1).ToString();
            Clothsnumb.text = (SelCloths + 1).ToString();
            Micnumb.text = (SelMic + 1).ToString();
            Lightnumb.text = (SelLight + 1).ToString();
            Stagenumb.text = (SelStage + 1).ToString();
        }
    }

    public void LeftHead()
    {
        SelHead--;
        if (SelHead < 0)
        {
            SelHead = 0;
        }
        HeadSprite.GetComponent<SpriteRenderer>().sprite = HeadSprites[SelHead];
        PlayerPrefs.SetInt("HeadNumb", SelHead);
    }
    public void RightHead()
    {
        SelHead++;
        if (SelHead > HeadSprites.Count - 1)
        {
            SelHead = HeadSprites.Count - 1;
        }
        HeadSprite.GetComponent<SpriteRenderer>().sprite = HeadSprites[SelHead];
        PlayerPrefs.SetInt("HeadNumb", SelHead);
    }
    public void LeftEye()
    {
        SelEye--;
        if (SelEye < 0)
        {
            SelEye = 0;
        }
    }
    public void RightEye()
    {
        SelEye++;
        if (SelEye > EyeSprites.Count - 1)
        {
            SelEye = EyeSprites.Count - 1;
        }
    }

    public void LeftSkin()
    {
        SelSkin--;
        if (SelSkin < 0)
        {
            SelSkin = 0;
        }
        PlayerSprite.GetComponent<SpriteRenderer>().color = SkinColors[SelSkin];
        PlayerPrefs.SetInt("SkinNumb", SelSkin);
    }
    public void RightSkin()
    {
        SelSkin++;
        if (SelSkin > SkinColors.Count - 1)
        {
            SelSkin = SkinColors.Count - 1;
        }
        PlayerSprite.GetComponent<SpriteRenderer>().color = SkinColors[SelSkin];
        PlayerPrefs.SetInt("SkinNumb", SelSkin);
    }
    public void LeftCloth()
    {
        SelCloths--;
        if (SelCloths < 0)
        {
            SelCloths = 0;
        }
        ClothsSprite.GetComponent<SpriteRenderer>().sprite = ClothsSprites[SelCloths];
        PlayerPrefs.SetInt("ClothsNumb", SelCloths);
    }
    public void RightCloth()
    {
        SelCloths++;
        if (SelCloths > ClothsSprites.Count - 1)
        {
            SelCloths = ClothsSprites.Count - 1;
        }
        ClothsSprite.GetComponent<SpriteRenderer>().sprite = ClothsSprites[SelCloths];
        PlayerPrefs.SetInt("ClothsNumb", SelCloths);
    }
    public void LeftMic()
    {
        SelMic--;
        if (SelMic < 0)
        {
            SelMic = 0;
        }
        MicSprite.GetComponent<SpriteRenderer>().sprite = MicSprites[SelMic];
        PlayerPrefs.SetInt("MicNumb", SelMic);
    }
    public void RightMic()
    {
        SelMic++;
        if (SelMic > MicSprites.Count - 1)
        {
            SelMic = MicSprites.Count - 1;
        }
        MicSprite.GetComponent<SpriteRenderer>().sprite = MicSprites[SelMic];
        PlayerPrefs.SetInt("MicNumb", SelMic);
    }
    public void LeftLight()
    {
        SelLight--;
        if (SelLight < 0)
        {
            SelLight = 0;
        }
        LightSprite.GetComponent<SpriteRenderer>().sprite = LightSprites[SelLight];
        PlayerPrefs.SetInt("LightNumb", SelLight);
    }
    public void RightLight()
    {
        SelLight++;
        if (SelLight > LightSprites.Count - 1)
        {
            SelLight = LightSprites.Count - 1;
        }
                LightSprite.GetComponent<SpriteRenderer>().sprite = LightSprites[SelLight];
        PlayerPrefs.SetInt("LightNumb", SelLight);
    }
    public void LeftStage()
    {
        SelStage--;
        if (SelStage < 0)
        {
            SelStage = 0;
        }
        StageSprite.GetComponent<SpriteRenderer>().sprite = StageSprites[SelStage];
        PlayerPrefs.SetInt("StageNumb", SelStage);
    }
    public void RightStage()
    {
        SelStage++;
        if (SelStage > StageSprites.Count - 1)
        {
            SelStage = StageSprites.Count - 1;
        }
        StageSprite.GetComponent<SpriteRenderer>().sprite = StageSprites[SelStage];
        PlayerPrefs.SetInt("StageNumb", SelStage);
    }
}
