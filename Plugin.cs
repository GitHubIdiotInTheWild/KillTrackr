using BepInEx;
using BepInEx.Unity.IL2CPP;
using HarmonyLib;
using UnityEngine;
using Il2CppInterop.Runtime.Injection;
using System.Collections.Generic;

[BepInPlugin("com.hp.killtrackr", "KillTrackr", "1.0.0")]
public class KillTrackrPlugin : BasePlugin {
    public static Harmony harmony = new Harmony("com.hp.killtrackr");

    public override void Load() {
        ClassInjector.RegisterTypeInIl2Cpp<KillFeedComponent>();
        AddComponent<KillFeedComponent>();
        harmony.PatchAll();
    }
}

// stores kill events
public static class KillFeed {
    public static List<string> entries = new List<string>();
    public static List<float> timestamps = new List<float>();
    public static float displayTime = 5f; // how long each entry shows

    public static void AddKill(string killer, string victim, string room) {
        entries.Add(killer + " killed " + victim + " in " + room);
        timestamps.Add(Time.time);
    }
}

[HarmonyPatch(typeof(PlayerControl), nameof(PlayerControl.MurderPlayer))]
public class MurderPlayerPatch {
    public static void Postfix(PlayerControl __instance, PlayerControl target) {
        string killer = __instance.Data.PlayerName;
        string victim = target.Data.PlayerName;
string room = "Unknown";
foreach (var shipRoom in ShipStatus.Instance.AllRooms) {
    if (shipRoom.roomArea != null && shipRoom.roomArea.OverlapPoint(target.transform.position)) {
        room = shipRoom.RoomId.ToString();
        break;
    }
}
        KillFeed.AddKill(killer, victim, room);
    }
}

public class KillFeedComponent : MonoBehaviour {
    void OnGUI() {
        float now = Time.time;
        float y = 300f;

        for (int i = KillFeed.entries.Count - 1; i >= 0; i--) {
            float age = now - KillFeed.timestamps[i];
            if (age > KillFeed.displayTime) continue;

            float alpha = 1f - (age / KillFeed.displayTime);
            GUI.color = new Color(1f, 1f, 1f, alpha);
            GUI.Box(new Rect(Screen.width / 2 - 150f, y, 300f, 25f), "");
            
            GUIStyle style = new GUIStyle();
            style.normal.textColor = new Color(1f, 0.3f, 0.3f, alpha);
            style.fontSize = 13;
            style.alignment = TextAnchor.MiddleCenter;
            GUI.Label(new Rect(Screen.width / 2 - 150f, y, 300f, 25f), 
                KillFeed.entries[i], style);
            y -= 30f;
        }
        GUI.color = Color.white;
    }
}