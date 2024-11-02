/*******************************************************************
* COPYRIGHT       : 2024
* PROJECT         : Space SHMUP Exercise
* FILE NAME       : Main.cs
* DESCRIPTION     : Short Description of script.
*                    
* REVISION HISTORY:
* Date 			Author    		        Comments
* ---------------------------------------------------------------------------
* 2000/01/01		Developer's Name    		 Created <short comment of changes>
* 
*
/******************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Enable the loading and reloading of scenes
//using Random = UnityEngine.Random;

public class Main : MonoBehaviour {
    static private Main S;// A private singleton for main
    static private Dictionary<eWeaponType, WeaponDefinition> WEAP_DICT;

    [Header("Inscribed")]
    public bool spawnEnemies = true;
    public GameObject[] prefabEnemies;//array of enemies

    public float enemySpawnPerSecond = 0.5f;// spawn rate

    public float enemyInsetDefault = 1.5f;// Inset from the sides

    public float gameRestartDelay = 2;
    public GameObject prefabPowerUp;
    public WeaponDefinition[] WeaponDefinition;

    public eWeaponType[] powerUpFrequency = new eWeaponType[] {
                            eWeaponType.blaster, eWeaponType.blaster,
                            eWeaponType.spread, eWeaponType.shield};
    private BoundsCheck bndCheck;

    void Awake() {// Awake is called once at instantiation
        S = this;

        bndCheck = GetComponent<BoundsCheck>();

        Invoke(nameof(SpawnEnemy), 1f / enemySpawnPerSecond);

        WEAP_DICT = new Dictionary<eWeaponType, WeaponDefinition>();
        foreach (WeaponDefinition def in WeaponDefinition){
            WEAP_DICT[def.type] = def;
        }
    }

    private void SpawnEnemy() {

        if (!spawnEnemies){
            Invoke(nameof(SpawnEnemy), 1f / enemySpawnPerSecond);
            return;
        }
        int ndx = Random.Range(0, prefabEnemies.Length);

        GameObject go = Instantiate<GameObject>(prefabEnemies[ndx]);

        float enemyInset = enemyInsetDefault;

        if (go.GetComponent<BoundsCheck>() != null ){

            enemyInset = Mathf.Abs(go.GetComponent<BoundsCheck>().radius);
        }

        Vector3 pos = Vector3.zero;
        float xMin = -bndCheck.camWidth + enemyInset;
        float xMax =  bndCheck.camWidth - enemyInset;
        pos.x = Random.Range(xMin, xMax);
        pos.y = bndCheck.camHeight + enemyInset;
        go.transform.position = pos;

        Invoke(nameof(SpawnEnemy), 1f / enemySpawnPerSecond);
    }

    void DelayedRestart(){
        Invoke(nameof(Restart), gameRestartDelay);
    }

    void Restart(){
        SceneManager.LoadSceneAsync(2);
    }

    static public void HERO_DIED(){
        S.DelayedRestart();
    }

    static public WeaponDefinition GET_WEAPON_DEFINITION(eWeaponType wt) {
        if (WEAP_DICT.ContainsKey(wt)) {
            return (WEAP_DICT[wt]);
        }
        return (new WeaponDefinition());
    }

    /// <summary>
    /// Called by an Enemy ship whenever it is destroyed. It sometimes creates
    /// a PowerUp in place of the destroyed ship.
    /// </summary>
    /// <param name="e">The Enemy that was destroyed</param>
    static public void SHIP_DESTROYED(Enemy e) {
    // Potentially generate a PowerUp
    if (Random.value <= e.powerUpDropChance) {  // Underlined red for now
        
        // Choose a PowerUp from the possibilities in powerUpFrequency
        int ndx = Random.Range(0, S.powerUpFrequency.Length);  // d
        eWeaponType pUpType = S.powerUpFrequency[ndx];

        // Spawn a PowerUp
        GameObject go = Instantiate<GameObject>(S.prefabPowerUp);
        PowerUp pUp = go.GetComponent<PowerUp>();

        // Set it to the proper WeaponType
        pUp.SetType(pUpType);  // e

        // Set it to the position of the destroyed ship
        pUp.transform.position = e.transform.position;
    }
}
}
