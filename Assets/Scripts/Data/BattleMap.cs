using UnityEngine;
using System.Collections;

public class BattleMap {

    public int battleMapID { get; private set; }

    public string prefabName { get; private set; }

    public float fogStartDistance { get; private set; }

    public float fogEndDistance { get; private set; }

    public int fogDensity { get; private set; }

    public int fogColorR { get; private set; }

    public int fogColorG { get; private set; }

    public int fogColorB { get; private set; }

    public int sound { get; private set; }

    public int ambientR { get; private set; }

    public int ambientG { get; private set; }

    public int ambientB { get; private set; }

    public float DirectLight { get; private set; }

    public int DirectLightR { get; private set; }

    public int DirectLightG { get; private set; }

    public int DirectLightB { get; private set; }

    public float DarkLight { get; private set; }

    public int DarkLightR { get; private set; }

    public int DarkLightG { get; private set; }

    public int DarkLightB { get; private set; }

    public float BackLight { get; private set; }

    public int BackLightR { get; private set; }

    public int BackLightG { get; private set; }

    public int BackLightB { get; private set; }

    public int lightMapNum { get; private set; }

    public BattleMap(int newBattleMapID, string newPrefabName, int newFogStartDistance, int newFogEndDistance,
        int newFogDensity, int newFogColorR, int newFogColorG, int newFogColorB, int newSound,
        int newAmbientR, int newAmbientG, int newAmbientB, float newDirectLight, int newDirectLightR, int newDirectLightG, int newDirectLightB
        , float newDarkLight, int newDarkLightR, int newDarkLightG, int newDarkLightB
        , float newBackLight, int newBackLightR, int newBackLightG, int newBackLightB, int newLightMapNum)
    {
        this.battleMapID = newBattleMapID;
        this.prefabName = newPrefabName;
        this.fogStartDistance = newFogStartDistance / 10f;
        this.fogEndDistance = newFogEndDistance / 10f;
        this.fogDensity = newFogDensity;
        this.fogColorR = newFogColorR;
        this.fogColorG = newFogColorG;
        this.fogColorB = newFogColorB;
        this.sound = newSound;
        this.ambientR = newAmbientR;
        this.ambientG = newAmbientG;
        this.ambientB = newAmbientB;

        this.DirectLight = newDirectLight / 100f;
        this.DirectLightR = newDirectLightR;
        this.DirectLightG = newDirectLightG;
        this.DirectLightB = newDirectLightB;

        this.DarkLight = newDarkLight / 100f;
        this.DarkLightR = newDarkLightR;
        this.DarkLightG = newDarkLightG;
        this.DarkLightB = newDarkLightB;

        this.BackLight = newBackLight / 100f;
        this.BackLightR = newBackLightR;
        this.BackLightG = newBackLightG;
        this.BackLightB = newBackLightB;

        this.lightMapNum = newLightMapNum;
    }
}
