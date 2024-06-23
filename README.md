# zahlwort2num.NET

:de: :de: :de:
A small but useful package for converting German numerals (including ordinal numbers) written as strings into numbers.

To put it differently: _It allows reverse text normalization for numbers_.

This package is a .NET adaptation of the original Python package [zahlwort2num](https://pypi.org/project/zahlwort2num/) by [Wortmeister HQ](https://github.com/Wortmeister-HQ). If you are looking for a Python version, you might want to check out the original repository or the complementary library [num2words](https://github.com/savoirfairelinux/num2words).

:crying_cat_face: _Currently, it doesn't support the Swiss variant. TBD_ :switzerland:

# Installation

Add the `Zahlwort2Num` package to your .NET project using NuGet Package Manager:

```
dotnet add package Zahlwort2Num
```

# Usage

### Definition:

```csharp
using Zahlwort2Num;
```

### Few examples:

```csharp
Console.WriteLine(ZahlConverter.Convert("Zweihundertfünfundzwanzig")); // => 225
Console.WriteLine(ZahlConverter.Convert("neunte")); // => "9."
Console.WriteLine(ZahlConverter.Convert("minus siebenhundert Millionen achtundsiebzig")); // => -700000078
```

_Or even stuff like:_ :see_no_evil:

```csharp
Console.WriteLine(ZahlConverter.Convert("sechshundertdreiundfünfzigtausendfünfhunderteinundzwanzig")); // => 653521
```

# Development
Before doing anything, make sure to restore the dependencies by running:

```bash
dotnet restore
```

Make sure tests are passing:

```bash
dotnet test
```

# Thanks 
- @Wortmeister-HQ for addressing the problem and creating the original Python package
- ...and to anyone who uses this package ;-)
