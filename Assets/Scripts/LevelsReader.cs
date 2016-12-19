using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public static class LevelsReader  {


    public static Rootobject ranksDeserialize;
    private static string jsonString;
    private const string LevelsConfig = "levels.json";

//#if UNITY_IPHONE
//               public static string LevelsConfigPath = Application.dataPath + "/Raw";
//#endif

//#if UNITY_ANDROID
//    public static string LevelsConfigPath = "jar:file://" + Application.dataPath + "!/assets";
//#endif

//#if UNITY_STANDALONE
//            public static string LevelsConfigPath = "file:///" + Application.dataPath + "/StreamingAssets";
//#endif


        //Для теста
    public static string LevelsConfigPath = "file:///" + Application.dataPath + "/StreamingAssets";

    public static IEnumerator JsonReader()
    {
        WWW www = new WWW(LevelsConfigPath + "/" + LevelsConfig);
        yield return www;

        if (www.error == null)
        {
            jsonString = www.text;
        }
        else
        {
            Debug.Log(LevelsConfigPath + "/" + LevelsConfig);
            Debug.LogError("ERROR: " + www.error);
        }


        ParseJson();


    }

    static void ParseJson()
    {
        if (jsonString != null)
        {
            ranksDeserialize = JsonConvert.DeserializeObject<Rootobject>(jsonString);
            int currentLevel = LevelManager.instance.currentLevel;
          //  Debug.Log(jsonString);

            for (int i = 0; i < ranksDeserialize.levels[currentLevel].levelStruct.Length; i++)
            {
                for (int j = 0; j < ranksDeserialize.levels[currentLevel].levelStruct[i].Length; j++)
                {
                    //  Debug.Log(ranksDeserialize.levels[0].levelStruct[i][j]);
                    LevelCreator.instance.CreateBrickObject(ranksDeserialize.levels[currentLevel].levelStruct[i][j], ranksDeserialize.levels[currentLevel].levelStruct[i].Length, ranksDeserialize.levels[currentLevel].levelStruct.Length, j, i);
                }

            }
            GameMaster.instance.gameStarted = true;

        }
    }

}
