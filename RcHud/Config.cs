﻿using BepInEx.Configuration;

namespace RcHud;

#nullable disable

public enum StaminaColorMode { Vanilla, Static, AllRed, AllDark, RedDark }

public static class Config {
    private static ConfigEntry<bool>
        refreshFistOnPunch,
        refreshFistOnSwitch,
        refreshGunOnSwitch,
        refreshOnMusic,
        refreshOnBossBar,
        hiVisOverheal,
        persistentHp;

    public static bool RefreshFistOnPunch => refreshFistOnPunch.Value;
    public static bool RefreshFistOnSwitch => refreshFistOnSwitch.Value;
    public static bool RefreshGunOnSwitch => refreshGunOnSwitch.Value;
    public static bool RefreshIconsOnBattleMusic => refreshOnMusic.Value;
    public static bool RefreshIconsOnBossHealthBar => refreshOnBossBar.Value;
    public static bool HiVisOverheal => hiVisOverheal.Value;
    public static bool PersistentHp => persistentHp.Value;

    private static ConfigEntry<StaminaColorMode> staminaColorMode;
    public static StaminaColorMode StaminaColorMode => staminaColorMode.Value;

    private static ConfigEntry<float>
        iconFade,
        wheelFade,
        fistScale,
        gunScale,
        fistOffset,
        gunOffset;

    public static float IconFadeTime => iconFade.Value;
    public static float WheelFadeTime => wheelFade.Value;

    public static float FistIconScale => fistScale.Value;
    public static float GunIconScale => gunScale.Value;

    // the game's UI is scaled for 720p but my default values were set for 1080p before the addition of aim assist support
    public static float FistIconOffset => fistOffset.Value * 2 / 3;
    public static float GunIconOffset => gunOffset.Value * 2 / 3;

    public static void Init(ConfigFile cfg) {
        refreshFistOnPunch = cfg.Bind("Refresh", "Punch", true);
        refreshFistOnSwitch = cfg.Bind("Refresh", "FistSwitch", true);
        refreshGunOnSwitch = cfg.Bind("Refresh", "GunSwitch", true);
        refreshOnMusic = cfg.Bind("Refresh", "BattleMusic", true);
        refreshOnBossBar = cfg.Bind("Refresh", "BossHealthBar", true);

        string[] scmDescriptions = [
            "Vanilla: vanilla behavior (first segment red while charging)",
            "Static: all segments blue, all the time",
            "AllRed: all segments red while charging",
            "AllDark: all segments dark blue while charging",
            "RedDark: first segment red while charging, other segments dark blue",
            "(the actual colors reflect game color settings)",
        ];

        staminaColorMode = cfg.Bind("Tweaks", "StaminaColorMode", StaminaColorMode.AllRed, string.Join("\n", scmDescriptions));
        hiVisOverheal = cfg.Bind("Tweaks", "HiVisOverheal", true, "Display overheal as a dark green ring with a different thickness");
        persistentHp = cfg.Bind("Tweaks", "PersistentHp", true, "Prevent HP wheel from fading if damaged or overhealed");

        iconFade = cfg.Bind("FadeTime", "WeaponIcons", 4.0f, AcceptableRange(0f, 30f));
        wheelFade = cfg.Bind("FadeTime", "RailcannonMeter", 5.0f, AcceptableRange(0f, 30f));

        fistScale = cfg.Bind("IconScale", "Fist", 1.25f, AcceptableRange(0f, 10f));
        gunScale = cfg.Bind("IconScale", "Gun", 0.1f, AcceptableRange(0f, 10f));

        fistOffset = cfg.Bind("IconOffset", "Fist", 50f, AcceptableRange(0f, 1000f));
        gunOffset = cfg.Bind("IconOffset", "Gun", 45f, AcceptableRange(0f, 1000f));
    }

    private static ConfigDescription AcceptableRange<T>(T min, T max) where T : System.IComparable {
        return new("", new AcceptableValueRange<T>(min, max));
    }
}
