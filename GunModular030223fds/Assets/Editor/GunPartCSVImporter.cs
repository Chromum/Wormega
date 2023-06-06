using UnityEngine;
using UnityEditor;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Networking;

public class GunPartCSVImporter : EditorWindow
{
    private TextAsset GuncsvFile;
    private string GunfileName;
    private TextAsset ItemcsvFile;
    private string ItemfileName;
    private TextAsset EnemyStatsFile;
    private string EnemyStatsfileName;
    private TextAsset bossWaveFile;
    private string bossWaveFileName;
    private Database db;

    [MenuItem("Window/Gun Part CSV Importer")]
    public static void ShowWindow()
    {
        GunPartCSVImporter window = GetWindow<GunPartCSVImporter>("Gun Part CSV Importer");
    }

    private void OnGUI()
    {
        GUILayout.Label("Import Gun Parts from CSV", EditorStyles.boldLabel);

        GuncsvFile = EditorGUILayout.ObjectField("Gun CSV File", GuncsvFile, typeof(TextAsset), false) as TextAsset;
        GunfileName = GuncsvFile ? GuncsvFile.name : "";

        ItemcsvFile = EditorGUILayout.ObjectField("Upgrades CSV File", ItemcsvFile, typeof(TextAsset), false) as TextAsset;
        ItemfileName = ItemcsvFile ? ItemcsvFile.name : "";
        
        EnemyStatsFile = EditorGUILayout.ObjectField("Enemy Stat CSV File", EnemyStatsFile, typeof(TextAsset), false) as TextAsset;
        EnemyStatsfileName = EnemyStatsFile ? EnemyStatsFile.name : "";
        
        bossWaveFile = EditorGUILayout.ObjectField("Boss Wave CSV File", bossWaveFile, typeof(TextAsset), false) as TextAsset;
        bossWaveFileName = bossWaveFile ? bossWaveFile.name : "";
        
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
            LoadEnemyStats();
            LoadBossWave();
            
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
    }
    
    private void LoadPlayerItems()
    {

        
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

                bool d = false;
                string path = "Assets/PlayerUpgrades/" + name + ".asset";
                Item gunPart = AssetDatabase.LoadAssetAtPath<Item>(path);
                if (gunPart == null)
                {
                    d = true;
                    gunPart = new Item();
                }


                gunPart.Name = name;
                gunPart.Description = bio;
                gunPart.ItemStats = new Stats()
                {
                    Health = health,
                    Speed = speed,
                    JumpHeight = jumpHeight,
                    GrappleCooldown = grappleCooldown,
                    AbilityCooldown = abilityCooldown,
                    Strength = strength,
                    Stamina = stamina
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

               

                
                if (d)
                {
                    System.IO.Directory.CreateDirectory(System.IO.Path.GetDirectoryName(path));
                    AssetDatabase.CreateAsset(gunPart, path);
                }
                else      
                    EditorUtility.SetDirty(gunPart);

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

                string path = "Assets/GunParts/" + type + "/" + name + ".asset";
                bool d = false;
                GunPart gunPart = AssetDatabase.LoadAssetAtPath<GunPart>(path);
                UnityEngine.Debug.Log(gunPart.Bio);
                if (gunPart == null)
                {
                    d = true;
                    gunPart = new GunPart();
                }

                switch (type)
                {
                    case "Barrel":
                        ((Barrel)gunPart).ShotMode = (ShotMode)System.Enum.Parse(typeof(ShotMode), data[7]);
                        break;
                }

                string Bio = data[8];
                int Debug = int.Parse(data[9]);
                UnityEngine.Debug.Log(Debug);
                if (Debug == 0)
                    gunPart.Debug = false;
                else
                    gunPart.Debug = true;

                switch(type)
                {
                    case "Barrel":
                        ((Barrel)gunPart).barrelType = (BarrelType)System.Enum.Parse(typeof(BarrelType), data[10]);
                        gunPart.Type = Typwe.Barrel;
                        break;
                    case "Magazine":
                        gunPart.Type = Typwe.Mag;
                        break;
                    case "Sight":
                        gunPart.Type = Typwe.Sight;
                        break;
                    case "Module":
                        gunPart.Type = Typwe.Module;
                        break;
                    case "Grip":
                        gunPart.Type = Typwe.Grip;
                        break;

                }


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
                if (d)
                {
                    
                    System.IO.Directory.CreateDirectory(System.IO.Path.GetDirectoryName(path));
                    AssetDatabase.CreateAsset(gunPart, path);
                }
                else
                {
                    EditorUtility.SetDirty(gunPart);
                }
            }
            catch {

            }
            

            
        }

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        db.FillInDatabase();
            
    }

    private void LoadEnemyStats()
    {
         string[] lines = EnemyStatsFile.text.Split('\n');

        for (int i = 1; i < lines.Length; i++)
        {
            try {
                string[] data = lines[i].Split(',');

                string name = data[0];
                float Health = float.Parse(data[1]);
                float minDamage = float.Parse(data[2]);
                float maxDamage = float.Parse(data[3]);
                float cooldown = float.Parse(data[4]);



                string path = "Assets/EnemyStats/" + name + ".asset";
                bool d = false;
                EnemyStats gunPart = AssetDatabase.LoadAssetAtPath<EnemyStats>(path);
                if (gunPart == null)
                {
                    d = true;
                    gunPart = new EnemyStats();
                }

              
                
                
           

                gunPart.name = name;
                gunPart.Health = Health;
                gunPart.minAttackDamage = minDamage;
                gunPart.maxAttackDamage = maxDamage;
                gunPart.attackCooldown = cooldown;
                
                if (d)
                {
                    System.IO.Directory.CreateDirectory(System.IO.Path.GetDirectoryName(path));
                    AssetDatabase.CreateAsset(gunPart, path);
                }
                else
                {
                    EditorUtility.SetDirty(gunPart);
                }
            }
            catch {

            }
        }

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        db.FillInDatabase();
            
    }

    private void LoadBossWave()
    {
        string[] lines = bossWaveFile.text.Split('\n');

        for (int i = 1; i < lines.Length; i++)
        {
            try {
                string[] data = lines[i].Split(',');

                string name = data[0];
                float Health = float.Parse(data[1]);
                int enemyiesToSpawn = int.Parse(data[2]);
     



                string path = "Assets/BossWaves/" + name + ".asset";
                bool d = false;
                BossWave gunPart = AssetDatabase.LoadAssetAtPath<BossWave>(path);
                if (gunPart == null)
                {
                    d = true;
                    gunPart = new BossWave();
                }

              
                
                
           

                gunPart.name = name;
                gunPart.bossHealth = Health;
                gunPart.enemysToSpawn = enemyiesToSpawn;

                
                if (d)
                {
                    System.IO.Directory.CreateDirectory(System.IO.Path.GetDirectoryName(path));
                    AssetDatabase.CreateAsset(gunPart, path);
                }
                else
                {
                    EditorUtility.SetDirty(gunPart);
                }
            }
            catch {

            }
        }

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        db.FillInDatabase();
            
    }
    


}
