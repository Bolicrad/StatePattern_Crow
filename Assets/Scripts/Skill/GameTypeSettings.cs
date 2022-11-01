using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "Create Game Type Setting", order = 1)]
public class GameTypeSettings : ScriptableObject
{
    public List<GameType> types;
    public List<GameType> GetTypeData(GameTypeEnum id)
    {
        return types.Where(gameType => id.HasFlag(gameType.typeId)).ToList();
    }

    public GameType GetType(GameTypeEnum id)
    {
        GameType returnType = default;
        foreach (var type in types.Where(type => type.typeId == id))
        {
            returnType = type;
        }
        return returnType;
    }
}

[Serializable]
public struct GameType
{
    public GameTypeEnum typeId;
    public Sprite typeIcon;
    public GameTypeEnum resistList;
    public GameTypeEnum weakList;
    public GameTypeEnum noEffectList;
}

[Flags]
public enum GameTypeEnum
{
    None = 0,
    Normal = 1 << 0,
    Fire = 1 << 1,
    Water = 1 << 2,
    Electric = 1 << 3,
    Grass = 1 << 4,
    Ground = 1 << 5,
}