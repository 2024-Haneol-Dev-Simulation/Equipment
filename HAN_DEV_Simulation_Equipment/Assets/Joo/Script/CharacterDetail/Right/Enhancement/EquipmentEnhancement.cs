using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngineInternal;

public class EquipmentEnhancement : UIElement
{
    [SerializeField] private EquipmentSystem equipmentSystem;
    [SerializeField] private PlayerData playerData;
    [SerializeField] private EquipmentManager equipmentManager;
    [SerializeField] private EnhancementEquipmentList enhancementEquipmentList;

    [SerializeField] private TMP_Text EnhancementCountText;
    [SerializeField] private TMP_Text EnhancementCountTempEffectText;
    [SerializeField] private TMP_Text EnhancementExpText;
    [SerializeField] private TMP_Text EnhancementTempExpText;
    [SerializeField] private Image EnhancementExpBar;
    [SerializeField] private Image EnhancementTempExpBar;

    [SerializeField] private TMP_Text MainOptionTypeText;
    [SerializeField] private TMP_Text MainOptionValueText;
    [SerializeField] private MainOptionEffect mainOptionEffect;

    [SerializeField] private EnhancementAdditionalOption additionalOption;
    [SerializeField] private GameObject newAdditionalOptionElement;
    [SerializeField] private TMP_Text newAdditionalOptionElementText;

    [SerializeField] private TMP_Text EnhancementMaterialCountText;

    [SerializeField] private GameObject MaxEnhancementObject;
    [SerializeField] private GameObject EnhancementObject;

    [SerializeField] private Button EnforceButton;

    [SerializeField] private List<GameObject> EnhancementMaterialButtonList;
    private List<MyEquipment> enhancementMaterialList;

    public int tempEnhancementExperience;
    private void Start()
    {
        EnforceButton.onClick.AddListener(() =>
        {
            EnforceCalculate();
        });
    }
    public void Init()
    {
        MyEquipment equipment = equipmentSystem.GetEquipment(equipmentSystem.SelectEquipmentID);

        EnhancementCountText.text = $"+{equipment.enhancement}";

        

        
        EnhancementTempExpBar.fillAmount = 0;

        MainOptionTypeText.text = equipment.mainOption.GetOptionTypeString();
        MainOptionValueText.text = equipment.mainOption.GetOptionValueString();

        additionalOption.Init(equipment.additionalOption);

        if(CheackMax())
        {
            MaxEnhancementObject.SetActive(true);
            EnhancementObject.SetActive(false);

            EnhancementExpText.text = string.Empty;
            EnhancementExpBar.fillAmount = 0;
        }
        else
        {
            MaxEnhancementObject.SetActive(false);
            EnhancementObject.SetActive(true);

            float n_exp = equipmentManager.GetEquipmentNeedExperience(equipment.equipmentClass, equipment.enhancement);
            EnhancementExpText.text = $"{equipment.enhancementExperience}/{n_exp}";
            EnhancementExpBar.fillAmount = equipment.enhancementExperience / n_exp;

            for (int i = 0; i < EnhancementMaterialButtonList.Count; i++)
            {
                EnhancementMaterialButtonList[i].GetComponent<Button>().onClick.AddListener(() =>
                {
                    enhancementEquipmentList.Show(ShowType.Fade);
                });
            }

            enhancementMaterialList = new List<MyEquipment>();
            InitEnhancementMaterialButton();
        }
        


        
        
        //
        newAdditionalOptionElement.SetActive(false);
        EnhancementTempExpText.gameObject.SetActive(false);
        EnhancementTempExpBar.gameObject.SetActive(false);


        

        if (equipment.equipmentClass == EquipmentClass.Rare)
            GetComponent<BackgroundImage>().ChangeImage(new Color32(72, 56, 113, 255));
        else
            GetComponent<BackgroundImage>().ChangeImage(new Color32(145, 107, 60, 255));
    }
    private void EnforceCalculate()
    {
        MyEquipment equipment = equipmentSystem.GetEquipment(equipmentSystem.SelectEquipmentID);
        List<int> list = equipmentManager.GetEquipmentNeedExperience(equipment.equipmentClass);

        int t = tempEnhancementExperience + equipment.enhancementExperience;
        int i;
        int count = 0;
        for (i = equipment.enhancement; i < list.Count; i++)
        {
            if (t - list[i] < 0 || list[i] == 0)
                break;
            t -= list[i];

            count++;
        }

        if (equipment.enhancement != i)
        {
            int add = (i / 4) - (equipment.enhancement / 4);
            if (add > 0)
                equipmentManager.AddAdditionalOption(equipment, add, (index) =>
                {
                    additionalOption.ChangeAdditionalOption(index);
                });

            equipment.enhancement = i;

            equipment.mainOption.value = equipmentManager.GetMainOption(equipment.mainOption.optionType, equipment.enhancement);
        }
        
        equipment.enhancementExperience = t;

        
        EnhancementTempExpBar.gameObject.SetActive(false);
        EnhancementCountTempEffectText.gameObject.SetActive(false);
        EnhancementTempExpText.gameObject.SetActive(false);
        mainOptionEffect.OffEffect(equipment.mainOption.GetOptionValueString());
        newAdditionalOptionElement.SetActive(false);
        EnhancementMaterialCountText.text = "장비 강화 소모 (0/15)";

        tempEnhancementExperience = 0;

        if(!CheackMax())
        {
            float value = equipment.enhancementExperience / (float)list[i];
            StartCoroutine(ExpBar(count, value));
        }

        

        for(int j = 0; j < enhancementMaterialList.Count;j++)
        {
            playerData.RemoveEquipment(enhancementMaterialList[j].UID);
        }

        Init();
    }
    private int TempCalculate()
    {
        MyEquipment equipment = equipmentSystem.GetEquipment(equipmentSystem.SelectEquipmentID);
        List<int> list = equipmentManager.GetEquipmentNeedExperience(equipment.equipmentClass);

        int t = tempEnhancementExperience;
        
        int count = 0;
        for (int i = equipment.enhancement; i < list.Count; i++)
        {
            t -= list[i];
            if (t < 0 || list[i] == 0)
                break;

            count++;
        }

        return count;
    }
    public int EnhancementMaterialListCount()
    {
        return enhancementMaterialList.Count;
    }
    public void AddEnhancementMaterial(MyEquipment equipment)
    {
        if (enhancementMaterialList.Count >= 15)
            return;

        int temp = TempCalculate();

        if (CheackMax())
            return;




        enhancementMaterialList.Add(equipment);
        EnhancementMaterialCountText.text = $"장비 강화 소모 ({enhancementMaterialList.Count}/15)";

        SetEnhancementExperience(GetEnhancementExperience(equipment));

        InitEnhancementMaterialButton();
    }
    public bool CheackMax()
    {
        int temp = TempCalculate();
        MyEquipment selectEquipment = equipmentSystem.GetEquipment(equipmentSystem.SelectEquipmentID);
        if (((selectEquipment.equipmentClass == EquipmentClass.Legend) && ((selectEquipment.enhancement + temp) == 20)) ||
            (selectEquipment.equipmentClass == EquipmentClass.Rare) && ((selectEquipment.enhancement + temp) == 16))
        {
            Debug.Log("MAX");
            return true;
        }
        return false;
    }
    public bool ContainEnhancementMaterial(string uid)
    {
        for (int i = 0; i < enhancementMaterialList.Count; i++)
        {
            if (uid == enhancementMaterialList[i].UID)
                return true;
        }

        return false;
    }
    public void RemoveEnhancementMaterial(string uid)
    {
        for (int i = 0; i < enhancementMaterialList.Count; i++)
        {
            if (uid == enhancementMaterialList[i].UID)
            {
                SetEnhancementExperience(-GetEnhancementExperience(enhancementMaterialList[i]));
                enhancementMaterialList.RemoveAt(i);
                InitEnhancementMaterialButton();
                break;
            }    
        }
        EnhancementMaterialCountText.text = $"장비 강화 소모 ({enhancementMaterialList.Count}/15)";
    }
    private void InitEnhancementMaterialButton()
    {
        //foreach(var button in EnhancementMaterialButtonList)
        //{
        //    button.GetComponent<ElementEuqipmentButton>().Clear();
        //}

        for (int i = 0;i< EnhancementMaterialButtonList.Count;i++)
        {
            int idnex = i;
            EnhancementMaterialButtonList[idnex].GetComponent<ElementEuqipmentButton>().Clear();

            if(idnex < enhancementMaterialList.Count)
            {
                EnhancementMaterialButtonList[idnex].GetComponent<ElementEuqipmentButton>().InitButton(
                enhancementMaterialList[idnex],
                () =>
                {
                    enhancementEquipmentList.Show(ShowType.Fade);
                });
            }
            else
            {
                EnhancementMaterialButtonList[i].GetComponent<Button>().onClick.AddListener(() =>
                {
                    enhancementEquipmentList.Show(ShowType.Fade);
                });
            }

            
        }
    }
    private int GetEnhancementExperience(MyEquipment equipment)
    {
        if (equipment.equipmentClass == EquipmentClass.Legend)
            return (equipment.enhancement + 1) * 3780;
        else
            return (equipment.enhancement + 1) * 2520;
    }
    public void SetEnhancementExperience(int value)
    {
        MyEquipment equipment = equipmentSystem.GetEquipment(equipmentSystem.SelectEquipmentID);
        tempEnhancementExperience += value;

        if(tempEnhancementExperience > 0)
        {
            float n_exp = equipmentManager.GetEquipmentNeedExperience(equipment.equipmentClass, equipment.enhancement);

            EnhancementTempExpBar.gameObject.SetActive(true);
            EnhancementTempExpBar.fillAmount = equipment.enhancementExperience + tempEnhancementExperience / n_exp;


            EnhancementTempExpText.gameObject.SetActive(true);
            EnhancementTempExpText.text = $"+{tempEnhancementExperience}";

           
        }
        else
        {
            newAdditionalOptionElement.SetActive(false);
            EnhancementTempExpText.gameObject.SetActive(false);
            EnhancementTempExpBar.gameObject.SetActive(false);
           
        }
        

        int calc = TempCalculate();
        if (calc != 0)
        {
            EnhancementCountTempEffectText.gameObject.SetActive(true);
            EnhancementCountTempEffectText.gameObject.transform.localScale = new Vector3(1.25f, 1.25f, 1.25f);
            EnhancementCountTempEffectText.gameObject.transform.DOScale(Vector3.one, 0.3f);
            EnhancementCountTempEffectText.text = $"+{calc}";

            Option temp = new Option();
            temp.optionType = equipment.mainOption.optionType;
            temp.value = equipmentManager.GetMainOption(temp.optionType, equipment.enhancement + TempCalculate());

            mainOptionEffect.OnEffect(equipment.mainOption.GetOptionValueString(), temp.GetOptionValueString());

            int add = ((equipment.enhancement + calc) / 4) - (equipment.enhancement / 4);
            if (add > 0)
            {
                newAdditionalOptionElement.SetActive(true);
                newAdditionalOptionElementText.text = $"◆ 새로운 부가 속성 {add}개 증가";
            }
            else
            {
                newAdditionalOptionElement.SetActive(false);
            }
        }
        else
        {
            EnhancementCountTempEffectText.gameObject.SetActive(false);
            mainOptionEffect.OffEffect(equipment.mainOption.GetOptionValueString());
        }
    }


    public override void Hide(HideType hideType, Complete complete = null)
    {
        gameObject.SetActive(false);
    }

    public override void Show(ShowType showType, Complete complete = null)
    {
        tempEnhancementExperience = 0;

        Init();
        gameObject.SetActive(true);
        mainOptionEffect.ImmediatelyOffEffect();
        if (showType == ShowType.FadeAndMove)
        {
            Vector3 pos = gameObject.GetComponent<RectTransform>().anchoredPosition;
            gameObject.GetComponent<CanvasGroup>().alpha = 0;
            gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, pos.y, pos.z);
            gameObject.GetComponent<CanvasGroup>().DOFade(1, 0.3f);
            gameObject.GetComponent<RectTransform>().DOAnchorPos(pos, 0.3f);
        }
        else if (showType == ShowType.Fade)
        {
            gameObject.GetComponent<CanvasGroup>().alpha = 0;
            gameObject.GetComponent<CanvasGroup>().DOFade(1, 0.3f);
        }
    }
    
    IEnumerator ExpBar(int maxCount,float value)
    {
        float time = 0.3f/ maxCount;

        for(int i = 0; i < maxCount; i++)
        {
            EnhancementExpBar.DOKill();
            EnhancementExpBar.DOFillAmount(1, time);
            yield return new WaitForSeconds(time);
            EnhancementExpBar.fillAmount = 0;
        }
        EnhancementExpBar.DOFillAmount(value, time);
    }
}
