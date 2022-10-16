using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "Create Game Type Setting", order = 1)]
public class GameTypeSettings : ScriptableObject
{
    public List<GameType> types;

    public GameType GetTypeData(GameTypeEnum id)
    {
        foreach (var gameType in types.Where(gameType => gameType.typeId == id))
        {
            return gameType;
        }
        return new GameType();
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
    Grass = 1 << 0,
    Fire = 1 << 1,
    Water = 1 << 2,
}