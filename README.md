# MarcusMedina.Fluent.Pattern

[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)
[![.NET](https://img.shields.io/badge/.NET-10.0-purple.svg)](https://dotnet.microsoft.com/download)
[![NuGet](https://img.shields.io/badge/NuGet-1.0.0-blue.svg)](https://www.nuget.org/packages/MarcusMedina.Fluent.Pattern/)
[![Tests](https://img.shields.io/badge/tests-191%20passed-brightgreen)]()

**Fluent string pattern matching and analysis for .NET 10+**

SQL-inspired patterns (`LIKE`, `IN`, `BETWEEN`), fuzzy matching (Levenshtein, Jaro-Winkler, Jaccard), string type detection (email, URL, IP, MAC, GUID, JSON, XML and more), and utility methods (palindromes, anagrams, pangrams, phonetic keys).

---

## Features

- ✅ **SQL-style** — `IsLike`, `IsNotLike`, `In`, `NotIn`, `Between`
- ✅ **Shorthand** — `LikeContains`, `LikeStartsWith`, `LikeEndsWith`
- ✅ **Fuzzy matching** — `LevenshteinDistance`, `SimilarityTo`, `IsSimilarTo`
- ✅ **Advanced distance** — `DamerauLevenshtein`, `Hamming`, `Jaccard`, `JaroWinkler`
- ✅ **String analysis** — `IsPalindrome`, `IsAnagramOf`, `IsPangram`, `LongestCommonSubstring`
- ✅ **Validation** — `IsEmail`, `IsUrl`, `IsIpAddress`
- ✅ **Type detection** — `DetectStringType()` returns a `StringType` enum
- ✅ **Phonetic matching** — `PhoneticKey()` (Soundex-inspired)
- ✅ **Zero dependencies** — Pure .NET, no external packages

---

## Installation

```bash
dotnet add package MarcusMedina.Fluent.Pattern
```

**Requirements:** .NET 10.0+, C# 14.0+

---

## Quick Start

```csharp
using MarcusMedina.Fluent.Pattern;

// SQL-like pattern matching
"hello world".IsLike("hello%");       // true
"hello world".IsNotLike("goodbye%");  // true

// Shorthand
"hello world".LikeContains("world");    // true
"hello world".LikeStartsWith("he");     // true
"hello world".LikeEndsWith("ld");       // true

// Set membership
"apple".In("apple", "banana", "cherry");      // true
"grape".NotIn("apple", "banana");             // true
"m".Between("a", "z");                        // true

// Fuzzy matching
"hello".SimilarityTo("hallo");                // ~0.8
"hello".IsSimilarTo("hallo");                 // true (default threshold 0.7)
"Kalle".LevenshteinDistance("Kålle");         // 1
"teh".DamerauLevenshteinDistance("the");      // 1
"karolin".HammingDistance("kathrin");         // 3
"hello".JaccardSimilarity("hallo");           // ~0.33
"Marcus".JaroWinklerSimilarity("Markus");     // ~0.88
"abcdef".LongestCommonSubstring("zbcdf");     // "bcd"

// Validation
"hej@example.com".IsEmail();                  // true
"https://example.com".IsUrl();                // true
"192.168.1.1".IsIpAddress();                  // true

// Type detection
"hej@example.com".DetectStringType();         // StringType.Email
"192.168.1.1".DetectStringType();             // StringType.IpAddress
"{0000-0000...}".DetectStringType();          // StringType.Guid
"42".DetectStringType();                      // StringType.Number

// String analysis
"anna".IsPalindrome();                        // true
"listen".IsAnagramOf("silent");               // true
"The quick brown fox jumps over the lazy dog".IsPangram();  // true
"hello".PhoneticKey();                        // Soundex-style key
```

---

## API Overview

### Pattern Matching

| Method | Description |
|--------|-------------|
| `IsLike(pattern)` | SQL `LIKE` with `%` and `_` wildcards |
| `IsNotLike(pattern)` | Negation of `IsLike` |
| `LikeContains(value)` | `LIKE '%value%'` shorthand |
| `LikeStartsWith(value)` | `LIKE 'value%'` shorthand |
| `LikeEndsWith(value)` | `LIKE '%value'` shorthand |
| `In(values)` | SQL `IN` against a list |
| `NotIn(values)` | SQL `NOT IN` negation |
| `Between(min, max)` | Range comparison |

All accept optional `caseSensitive` parameter (default `false`).

### Fuzzy Matching

| Method | Description |
|--------|-------------|
| `LevenshteinDistance(other)` | Edit distance (insert/delete/substitute) |
| `DamerauLevenshteinDistance(other)` | Levenshtein + transpositions |
| `HammingDistance(other)` | Position-by-position diff (same length only) |
| `JaccardSimilarity(other, nGramSize)` | N-gram set similarity |
| `JaroWinklerSimilarity(other)` | Optimized for short strings (names) |
| `SimilarityTo(other)` | Normalized 0.0–1.0 (based on Levenshtein) |
| `IsSimilarTo(other, threshold)` | `SimilarityTo` >= threshold (default 0.7) |
| `LongestCommonSubstring(other)` | Longest shared substring |

### Validation & Detection

| Method | Description |
|--------|-------------|
| `IsEmail()` | Basic email format validation |
| `IsUrl()` | http/https/ftp URL validation |
| `IsIpAddress()` | IPv4 and IPv6 validation |
| `DetectStringType()` | Returns `StringType` enum |

### Text Analysis

| Method | Description |
|--------|-------------|
| `IsPalindrome()` | Same forwards and backwards |
| `IsAnagramOf(other)` | Same characters, different order |
| `IsPangram()` | Contains all letters of the alphabet |
| `PhoneticKey()` | Soundex-inspired phonetic encoding |

---

## `StringType` Enum

| Value | Detects |
|-------|---------|
| `PlainText` | Unrecognized format |
| `Email` | `user@domain.tld` |
| `Url` | `http://`, `https://`, `ftp://` |
| `FilePath` | Unix paths (`/...`) |
| `WindowsPath` | `C:\...`, `\\server\...` |
| `IpAddress` | IPv4/IPv6 |
| `SubnetMask` | `255.255.255.0` style |
| `MacAddress` | `AA:BB:CC:DD:EE:FF` |
| `Json` | `{...}` or `[...]` |
| `Xml` | `<...>` |
| `Number` | Integers and decimals |
| `Guid` | UUIDs with/without dashes/braces |
| `Sql` | `SELECT`, `INSERT`, `UPDATE`, etc. |

---

## Testing

```bash
dotnet test --configuration Release
```

Tests: **191 passed** — covering pattern matching, fuzzy distance, validation, detection, string analysis, null/empty edge cases, and all StringType variants.

---

## License

MIT — see [LICENSE](LICENSE) for details.

---

## Related Projects

- [MarcusMedina.Fluent.Data](https://github.com/MarcusMedinaPro/MarcusMedina.Fluent.Data) — CSV, JSON, XML extensions
- [MarcusMedina.Fluent.Data.Sql](https://github.com/MarcusMedinaPro/MarcusMedina.Fluent.Data.Sql) — SQL query builder
- [MarcusMedina.Maths.Algebra](https://github.com/MarcusMedinaPro/MarcusMedina.Maths.Algebra) — Algebraic expressions and symbolic math
