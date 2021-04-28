using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CustomisationSet : Status
{
    #region Variables
    [Header("Character Name")]
    public string charName;
    [Header("Charcter Class")]
    public CharacterClass characterClass = CharacterClass.Barbarian;
    public string[] selectedClass = new string[8];
    public int selectedClassIndex = 0;
    [System.Serializable]
    public struct Stats
    {
        public string baseStatsName;
        public int baseStats;
        public int tempStats;
    }
    [System.Serializable]
    public struct PointUI
    {

        public Status.StatBlock stat;
        public Text NameDisplay;
        public GameObject plusButton;
        public GameObject minusButton;
    };
    public PointUI[] pointSystem;
    public Text pointsText;
    public void TextUpdate()
    {
        pointsText.text = "Points: " + statPoints;
    }

    public Stats[] characterStats;
    [Header("Dropdown Menu")]
    public bool showDropdown;
    public Vector2 scrollpos;
    public string classButton = "";
    public int statPoints = 10;
    [Header("Texture Lists")]
    public List<Texture2D> skin = new List<Texture2D>();
    public List<Texture2D> eyes = new List<Texture2D>();
    public List<Texture2D> mouth = new List<Texture2D>();
    public List<Texture2D> hair = new List<Texture2D>();
    public List<Texture2D> clothes = new List<Texture2D>();
    public List<Texture2D> armour = new List<Texture2D>();
    [Header("Current Texture Index")]
    public int skinIndex;
    public int eyesIndex, mouthIndex, hairIndex, clothesIndex, armourIndex;
    [Header("Renderer")]
    public Renderer charcterRenderer;
    [Header("Max amount of textures per type")]
    public int skinMax;
    public int eyesMax, mouthMax, hairMax, clothesMax, armourMax;
    [Header("Mat Name")]
    public string[] matName = new string[6];

    #endregion
    private void Start()
    {
        matName = new string[] {"Skin","Eyes","Mouth","Hair","Clothes","Armour"};
        

        selectedClass = new string[] { "Barbarian", "Warlock", "Ranger", "Priest", "Gunslinger", "Monk", "Bard" };
        ChoosenClass(0);
        for (int i = 0; i < skinMax; i++)
        {
            Texture2D tempTexture = Resources.Load("Character/Skin_" + i) as Texture2D;
            skin.Add(tempTexture);
        }
        for (int i = 0; i < eyesMax; i++)
        {
            Texture2D tempTexture = Resources.Load("Character/Eyes_" + i) as Texture2D;
            eyes.Add(tempTexture);
        }
        for (int i = 0; i < mouthMax; i++)
        {
            Texture2D tempTexture = Resources.Load("Character/Mouth_" + i) as Texture2D;
            mouth.Add(tempTexture);
        }
        for (int i = 0; i < hairMax; i++)
        {
            Texture2D tempTexture = Resources.Load("Character/Hair_" + i) as Texture2D;
            hair.Add(tempTexture);
        }
        for (int i = 0; i < clothesMax; i++)
        {
            Texture2D tempTexture = Resources.Load("Character/Clothes_" + i) as Texture2D;
            clothes.Add(tempTexture);
        }
        for (int i = 0; i < armourMax; i++)
        {
            Texture2D tempTexture = Resources.Load("Character/Armour_" + i) as Texture2D;
            armour.Add(tempTexture);
        }
        #region Point System
        ChoosenClass(0);
        TextUpdate();
        for (int i = 0; i < pointSystem.Length; i++)
        {
            pointSystem[i].NameDisplay.text = pointSystem[i].stat.characterName + ": " + (pointSystem[i].stat.value + pointSystem[i].stat.tempValue);
            pointSystem[i].minusButton.SetActive(false);
        }
        #endregion
    }
    public void SetTexture(string type, int dir)
    {
        int index = 0, max = 0, matIndex = 0;
        Texture2D[] textures = new Texture2D[0];
        switch (type)
        {
            case "Skin":
                index = skinIndex;
                max = skinMax;
                textures = skin.ToArray();
                matIndex = 1;
                break;
            case "Eyes":
                index = eyesIndex;
                max = eyesMax;
                textures = eyes.ToArray();
                matIndex = 2;
                break;
            case "Mouth":
                index = mouthIndex;
                max = mouthMax;
                textures = mouth.ToArray();
                matIndex = 3;
                break;
            case "Hair":
                index = hairIndex;
                max = hairMax;
                textures = hair.ToArray();
                matIndex = 4;
                break;
            case "Clothes":
                index = clothesIndex;
                max = clothesMax;
                textures = clothes.ToArray();
                matIndex = 5;
                break;
            case "Armour":
                index = armourIndex;
                max = armourMax;
                textures = armour.ToArray();
                matIndex = 6;
                break;
        }
        index += dir;
        if (index < 0)
        {
            index = max - 1;
        }
        if (index > max - 1)
        {
            index = 0;
        }
        Material[] mat = charcterRenderer.materials;
        mat[matIndex].mainTexture = textures[index];
        charcterRenderer.materials = mat;
        switch (type)
        {
            case "Skin":
                skinIndex = index;
                break;
            case "Eyes":
                eyesIndex = index;
                break;
            case "Mouth":
                mouthIndex = index;
                break;
            case "Hair":
                hairIndex = index;
                break;
            case "Clothes":
                clothesIndex = index;
                break;
            case "Armour":
                armourIndex = index;
                break;
        }
    }
    public void ChoosenClass(int classIndex)
    {
        switch (classIndex)
        {
            case 0:
                //strength
                characterStats[0].baseStats = 7;
                //Dexterity
                characterStats[1].baseStats = 5;
                //Intelligence
                characterStats[2].baseStats = 3;
                //Agility
                characterStats[3].baseStats = 4;
                //Wisdom
                characterStats[4].baseStats = 2;
                //Charisma
                characterStats[5].baseStats = 5;


                characterClass = CharacterClass.Barbarian;
                break;
            case 1:
                //strength
                characterStats[0].baseStats = 3;
                //Dexterity
                characterStats[1].baseStats = 5;
                //Intelligence
                characterStats[2].baseStats = 8;
                //Agility
                characterStats[3].baseStats = 4;
                //Wisdom
                characterStats[4].baseStats = 7;
                //Charisma
                characterStats[5].baseStats = 6;

                characterClass = CharacterClass.Warlock;
                break;
            case 2:
                //strength
                characterStats[0].baseStats = 4;
                //Dexterity
                characterStats[1].baseStats = 6;
                //Intelligence
                characterStats[2].baseStats = 6;
                //Agility
                characterStats[3].baseStats = 8;
                //Wisdom
                characterStats[4].baseStats = 6;
                //Charisma
                characterStats[5].baseStats = 8;

                characterClass = CharacterClass.Ranger;
                break;
            case 3:
                //strength
                characterStats[0].baseStats = 2;
                //Dexterity
                characterStats[1].baseStats = 5;
                //Intelligence
                characterStats[2].baseStats = 6;
                //Agility
                characterStats[3].baseStats = 6;
                //Wisdom
                characterStats[4].baseStats = 7;
                //Charisma
                characterStats[5].baseStats = 8;

                characterClass = CharacterClass.Priest;
                break;
            case 4:
                //strength
                characterStats[0].baseStats = 4;
                //Dexterity
                characterStats[1].baseStats = 7;
                //Intelligence
                characterStats[2].baseStats = 6;
                //Agility
                characterStats[3].baseStats = 8;
                //Wisdom
                characterStats[4].baseStats = 5;
                //Charisma
                characterStats[5].baseStats = 4;

                characterClass = CharacterClass.Gunslinger;
                break;
            case 5:
                //strength
                characterStats[0].baseStats = 1;
                //Dexterity
                characterStats[1].baseStats = 3;
                //Intelligence
                characterStats[2].baseStats = 7;
                //Agility
                characterStats[3].baseStats = 5;
                //Wisdom
                characterStats[4].baseStats = 9;
                //Charisma
                characterStats[5].baseStats = 4;

                characterClass = CharacterClass.Monk;
                break;
            case 6:
                //strength
                characterStats[0].baseStats = 1;
                //Dexterity
                characterStats[1].baseStats = 5;
                //Intelligence
                characterStats[2].baseStats = 7;
                //Agility
                characterStats[3].baseStats = 7;
                //Wisdom
                characterStats[4].baseStats = 7;
                //Charisma
                characterStats[5].baseStats = 9;

                characterClass = CharacterClass.Bard;
                break;

        }
        for (int i = 0; i < pointSystem.Length; i++)
        {
            pointSystem[i].stat.tempValue = 0;
            statPoints = 10;
            pointSystem[i].NameDisplay.text = characterStats[i].baseStatsName + ": " + (characterStats[i].baseStats + characterStats[i].tempStats);
            pointSystem[i].minusButton.SetActive(false);
            pointSystem[i].plusButton.SetActive(true);
        }
    }
    public void SaveCharacter()
    {
        PlayerPrefs.SetInt("SkinIndex", skinIndex);
        PlayerPrefs.SetInt("HairIndex", hairIndex);
        PlayerPrefs.SetInt("EyesIndex", eyesIndex);
        PlayerPrefs.SetInt("MouthIndex", mouthIndex);
        PlayerPrefs.SetInt("ClothesIndex", clothesIndex);
        PlayerPrefs.SetInt("ArmourIndex", armourIndex);

        PlayerPrefs.SetString("CharacterName", charName);

        for (int i = 0; i < characterStats.Length; i++)
        {
            PlayerPrefs.SetInt(characterStats[i].baseStatsName, characterStats[i].baseStats + characterStats[i].tempStats);

        }
        PlayerPrefs.SetString("CharacterClass", selectedClass[selectedClassIndex]);
    }
    //private void OnGUI()
    //{

    //    #region GUI Value Setup
    //    //16:9
    //    Vector2 screen = new Vector2(Screen.width / 16, Screen.height / 9);

    //    float left = 0.25f * screen.x;
    //    float mid = 0.75f * screen.x;
    //    float right = 2.25f * screen.x;

    //    float x = 0.5f * screen.x;
    //    float y = 0.5f * screen.x;
    //    float label = 1.5f * screen.x;
    //    #endregion
    //    #region Customisation Textures
    //    for (int i = 0; i < matName.Length; i++)
    //    {
    //        if (GUI.Button(new Rect(left, y + i * y, x, y), "<"))
    //        {
    //            SetTexture(matName[i], -1);
    //        }
    //        GUI.Box(new Rect(mid, y + i * y, label, y), matName[i]);
    //        if (GUI.Button(new Rect(right, y + i * y, x, y), ">"))
    //        {
    //            SetTexture(matName[i], 1);
    //        }
    //    }
    //    #endregion
    //    #region Choose Class
    //    float classX = 12.75f * screen.x;
    //    float h = 0;
    //    if (GUI.Button(new Rect(classX, y + h * y, 4 * x, y), classButton))
    //    {
    //        showDropdown = !showDropdown;
    //    }
    //    if (showDropdown)
    //    {
    //        scrollpos = GUI.BeginScrollView(new Rect(classX, y + h * y, 4 * x, 4 * y), scrollpos, new Rect(0, 0, 0, selectedClass.Length * y), false, true);

    //        for (int i = 0; i < selectedClass.Length; i++)
    //        {
    //            if (GUI.Button(new Rect(0, y + i * y, 3 * x, y), selectedClass[i]))
    //            {
    //                ChoosenClass(i);
    //                classButton = selectedClass[i];
    //                showDropdown = false;
    //            }
    //        }
    //        GUI.EndScrollView();
    //    }
    //    #endregion
    //    #region Set Stats
    //    GUI.Box(new Rect(classX, 6 * y, 4 * x, y), "Points " + statPoints);
    //    for (int i = 0; i < characterStats.Length; i++)
    //    {
    //        if (statPoints > 0)
    //        {
    //            if (GUI.Button(new Rect(classX - x, 7 * y + i * y, x, y), "+"))
    //            {
    //                statPoints--;
    //                characterStats[i].tempStats++;
    //            }
    //        }
    //        GUI.Box(new Rect(classX, 7 * y + i * y, 4 * x, y), characterStats[i].baseStatsName + ": " + (characterStats[i].baseStats + characterStats[i].tempStats));
    //        if (statPoints < 10 && characterStats[i].tempStats > 0)
    //        {
    //            if (GUI.Button(new Rect(classX + 4 * x, 7 * y + i * y, x, y), "-"))
    //            {
    //                statPoints++;
    //                characterStats[i].tempStats--;
    //            }
    //        }
    //    }
    //    #endregion
    //    charName = GUI.TextField(new Rect(left, 7 * y, 5 * x, y), charName, 32);
    //    if (GUI.Button(new Rect(left, 8 * y, 5 * x, y), "Save And Play"))
    //    {
    //        SaveCharacter();
    //        SceneManager.LoadScene(2);
    //    }
    //}
    public void BackArray(string type)
    {
        SetTexture(type, -1);
    }
    public void ForwardArray(string type)
    {
        SetTexture(type, 1);
    }
    public void SetName(string characterName)
    {
        charName = characterName;
    }
    public void SetPointspos(int i)
    {
        //change the values
        statPoints--;
        characterStats[i].tempStats++;
        //if we have no pints hid the pos button
        if (statPoints <= 0)
        {
            for (int button = 0; button < pointSystem.Length; button++)
            {
                pointSystem[button].plusButton.SetActive(false);
            }
        }
        if (pointSystem[i].minusButton.activeSelf == false)
        {
            pointSystem[i].minusButton.SetActive(true);
        }
        pointSystem[i].NameDisplay.text = characterStats[i].baseStatsName + ": " + (characterStats[i].baseStats + characterStats[i].tempStats);
        TextUpdate();
    }
    public void SetPointNeg(int i)
    {
        statPoints++;
        characterStats[i].tempStats--;
        if (pointSystem[i].stat.tempValue <= 0)
        {
            pointSystem[i].minusButton.SetActive(false);
        }
        if (pointSystem[i].plusButton.activeSelf == false)
        {
            for (int button = 0; button < pointSystem.Length; button++)
            {
                pointSystem[button].plusButton.SetActive(true);
            }
        }
        pointSystem[i].NameDisplay.text = characterStats[i].baseStatsName + ": " + (characterStats[i].baseStats + characterStats[i].tempStats);
        TextUpdate();
    }
}
public enum CharacterClass
{
    Barbarian,
    Warlock,
    Ranger,
    Priest,
    Gunslinger,
    Monk,
    Bard
}
