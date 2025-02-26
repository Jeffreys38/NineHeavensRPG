using System;
using UnityEngine;

public enum RealmStage
{
    Early,
    Mid,
    Late
}

public enum RealmTier
{
    Mortal,         // 범인 (Phàm Nhân)
    QiRefining,     // 기연 (Luyện Khí)
    Foundation,     // 축기 (Trúc Cơ)
    CoreFormation,  // 결단 (Kết Đan)
    NascentSoul,    // 원영 (Nguyên Anh)
    SoulFormation,  // 화신 (Hóa Thần)
    GreatAscension, // 연허 (Đại Thừa)
    ImmortalEmperor // 선제 (Tiên Đế)
}

[Serializable]
public class RealmLevel
{
    public RealmStage stage;
    public int requiredExp;
}

[CreateAssetMenu(fileName = "New Realm Data", menuName = "Game Data/Realm Data")]
public class RealmData : ScriptableObject
{
    public RealmTier realmTier;
    public RealmLevel[] levels;
}