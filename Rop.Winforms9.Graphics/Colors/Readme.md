# Rop.Drawing.Colors

Features
--------

Rop.Drawing.Colors is a group of structs and helpers to work with GDI+ colors


Structs
-------

```csharp
/// Colors in Float Values
public struct ColorF;
/// Colors in HSL Values
public struct ColorHSL;
```

`Color` helper
-------

Helper class relative to Colors

```csharp
public static Color DeltaHSL(this Color c, float deltah = 0, float deltas = 1, float deltal = 1);
public static ColorF Contrast(this ColorF c, float cont = 0.25f);
public static Color Contrast(this Color c, float cont = 0.25f);
public static ColorF DeltaHSL(this ColorF c, float deltah = 0, float deltas = 1, float deltal = 1);
public static Color MixColors(this Color c, Color n, float a = 0.5f);
public static bool IsTOrEmpty(this Color c);
public static Color IfTOrEmpty(this Color c, Color def);
public static double ColorDistance(Color e1, Color e2);
public static ColorF Solid(this ColorF a);
public static ColorF PreMult(this ColorF a);
public static ColorF Blend(ColorF a, ColorF b, Func<float, float, float> operation, bool premult = true);
public static ColorF BlendMix(ColorF a, ColorF b, Func<float, float, float> operation, float weightb = 1, bool premult = true);
public static ColorF Blend(ColorF a, ColorF b, Func<float, float, float, float> operation);
public static ColorF Multiply(ColorF a, ColorF b, float weightb = 1);
public static ColorF Screen(ColorF a, ColorF b, float weightb = 1);
public static ColorF Normal(ColorF a, ColorF b);
public static ColorF Add(ColorF a, ColorF b, float weightb = 1);
public static ColorF Sub(ColorF a, ColorF b, float weightb = 1);
public static ColorF Mix(ColorF a, ColorF b, float weightb = 1);
public static ColorHSL Blend(ColorHSL a, ColorHSL b, Func<float, float, float> operationH, Func<float, float, float> operationS, Func<float, float, float> operationL);
public static ColorHSL BlendMix(ColorHSL a, ColorHSL b, Func<float, float, float> operationH, Func<float, float, float> operationS, Func<float, float, float> operationL, float weightb = 1);
public static ColorHSL Hue(ColorHSL a, ColorHSL b, float weightb = 1);
public static ColorHSL Saturation(ColorHSL a, ColorHSL b, float weightb = 1);
public static ColorHSL Luminosity(ColorHSL a, ColorHSL b, float weightb = 1);
public static ColorF Hue(ColorF a, ColorF b, float weightb = 1);
public static ColorF Saturation(ColorF a, ColorF b, float weightb = 1);
public static ColorF Luminosity(ColorF a, ColorF b, float weightb = 1);
public static Color Hue(Color a, Color b, float weightb = 1);
public static Color Saturation(Color a, Color b, float weightb = 1);
public static Color Luminosity(Color a, Color b, float weightb = 1);
```


 ------
 (C)2022 Ramón Ordiales Plaza
