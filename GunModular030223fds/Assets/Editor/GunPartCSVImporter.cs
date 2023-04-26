using UnityEngine;
using UnityEditor;
using System.IO;
using System.Collections.Generic;
using System.Linq;

public class GunPartCSVImporter : EditorWindow
{
    private TextAsset GuncsvFile;
    private string GunfileName;
    private TextAsset ItemcsvFile;
    private string ItemfileName;
    private Database db;

    [MenuItem("Window/Gun Part CSV Importer")]
    public static void ShowWindow()
    {
        GetWindow<GunPartCSVImporter>("Gun Part CSV Importer");
    }

    private void OnGUI()
    {
        GUILayout.Label("Import Gun Parts from CSV", EditorStyles.boldLabel);

        GuncsvFile = EditorGUILayout.ObjectField("Gun CSV File", GuncsvFile, typeof(TextAsset), false) as TextAsset;
        GunfileName = GuncsvFile ? GuncsvFile.name : "";

        ItemcsvFile = EditorGUILayout.ObjectField("Gun CSV File", ItemcsvFile, typeof(TextAsset), false) as TextAsset;
        ItemfileName = ItemcsvFile ? ItemcsvFile.name : "";
        
        db = EditorGUILayout.ObjectField("Database", db, typeof(Database), false) as Database;


        if (GUILayout.Button("Import"))
        {
            if (GuncsvFile == null)
            {
                Debug.LogError("Please select a CSV file to import");
                return;
            }

            LoadGunParts();
            LoadPlayerItems();
            
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
    }
    
    private void LoadPlayerItems()
    {
        if(Directory.Exists("Assets/PlayerUpgrades"))
            Directory.Delete("Assets/PlayerUpgrades",true);
        
        string[] lines = ItemcsvFile.text.Split('\n');

        for (int i = 1; i < lines.Length; i++)
        {
            try {
                string[] data = lines[i].Split(',');

                string name = data[0];
                string bio = data[1];
                string rarity = data[2];

                float health = float.Parse(data[3]);
                float speed = float.Parse(data[4]);
                float jumpHeight = float.Parse(data[5]);
                float grappleCooldown = float.Parse(data[6]);
                float abilityCooldown = float.Parse(data[7]);
                float strength = float.Parse(data[8]);
                float stamina = float.Parse(data[9]);


                Item gunPart = new Item
                {
                    ItemStats = new Stats()
                    {
                        Health = health,
                        Speed = speed,
                        JumpHeight = jumpHeight,
                        GrappleCooldown = grappleCooldown,
                        AbilityCooldown = abilityCooldown,
                        Strength = strength,
                        Stamina = stamina
                    }
                };


                int Debug = int.Parse(data[10]);
                if (Debug == 0)
                    gunPart.Debug = false;
                else
                    gunPart.Debug = true;

                

                switch (rarity)
                {
                    case "Common":
                        gunPart.Rareity = Rareity.Common;
                        break;
                    case "Rare":
                        gunPart.Rareity = Rareity.Rare;
                        break;
                    case "Legendary":
                        gunPart.Rareity = Rareity.Legendary;
                        break;
                    case
                        "Mythic":
                        gunPart.Rareity = Rareity.Mythic;
                        break;
                }

               
                string path = "Assets/PlayerUpgrades/" + name + ".asset";
                System.IO.Directory.CreateDirectory(System.IO.Path.GetDirectoryName(path));
                AssetDatabase.CreateAsset(gunPart, path);
            }
            catch {

            }
            

            
        }

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        db.FillInDatabase();
    }

    private void LoadGunParts()
    {
        if(Directory.Exists("Assets/GunParts"))
            Directory.Delete("Assets/GunParts",true);
        
        string[] lines = GuncsvFile.text.Split('\n');

        for (int i = 1; i < lines.Length; i++)
        {
            try {
                string[] data = lines[i].Split(',');

                string name = data[0];
                string type = data[1];
                string rarity = data[2];

                float damage = float.Parse(data[3]);
                float fireRate = float.Parse(data[4]);
                int magSize = int.Parse(data[5]);
                float accuracy = float.Parse(data[6]);

                GunPart gunPart;

                switch (type)
                {
                    case "Barrel":
                        gunPart = ScriptableObject.CreateInstance<Barrel>();
                        ((Barrel)gunPart).ShotMode = (ShotMode)System.Enum.Parse(typeof(ShotMode), data[7]);
                        break;

                    case "Module":
                        gunPart = ScriptableObject.CreateInstance<Module>();
                        break;

                    default:
                        gunPart = ScriptableObject.CreateInstance<GunPart>();
                        break;
                }

                string Bio = data[8];
                int Debug = int.Parse(data[9]);
                UnityEngine.Debug.Log(Debug);
                if (Debug == 0)
                    gunPart.Debug = false;
                else
                    gunPart.Debug = true;

                if (type == "Barrel")
                    ((Barrel)gunPart).barrelType = (BarrelType)System.Enum.Parse(typeof(BarrelType), data[10]);


                gunPart.name = name;
                gunPart.Damage = damage;
                gunPart.FireRate = fireRate;
                gunPart.MagSize = magSize;
                gunPart.Accuracy = accuracy;

                switch (rarity)
                {
                    case "Common":
                        gunPart.Rareity = Rareity.Common;
                        break;
                    case "Rare":
                        gunPart.Rareity = Rareity.Rare;
                        break;
                    case "Legendary":
                        gunPart.Rareity = Rareity.Legendary;
                        break;
                    case
                        "Mythic":
                        gunPart.Rareity = Rareity.Mythic;
                        break;
                }

                gunPart.Name = name;
                gunPart.Bio = Bio;
                string path = "Assets/GunParts/" + type + "/" + name + ".asset";
                System.IO.Directory.CreateDirectory(System.IO.Path.GetDirectoryName(path));
                AssetDatabase.CreateAsset(gunPart, path);
            }
            catch {

            }
            

            
        }

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        
    }
}
