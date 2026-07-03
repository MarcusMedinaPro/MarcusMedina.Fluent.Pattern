# MarcusMedina.Fluent.Pattern

[![NuGet](https://img.shields.io/nuget/v/MarcusMedina.Fluent.Pattern.svg?style=for-the-badge&logo=nuget)](https://www.nuget.org/packages/MarcusMedina.Fluent.Pattern/)
[![NuGet Downloads](https://img.shields.io/nuget/dt/MarcusMedina.Fluent.Pattern.svg?style=for-the-badge&logo=nuget)](https://www.nuget.org/packages/MarcusMedina.Fluent.Pattern/)
[![C#](https://img.shields.io/badge/C%23-14.0-239120?style=for-the-badge&logo=csharp&logoColor=white)](#)
[![.NET](https://img.shields.io/badge/.NET-10.0+-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)](https://dotnet.microsoft.com/)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg?style=for-the-badge)](https://opensource.org/licenses/MIT)
[![Open Source](https://raw.githubusercontent.com/MarcusMedinaPro/MarcusMedina.Fluent.Pattern/main/assets/open-source.svg)](https://opensource.org)
[![Build](https://img.shields.io/github/actions/workflow/status/MarcusMedinaPro/MarcusMedina.Fluent.Pattern/release.yml?branch=main&label=Build&style=for-the-badge&logo=github)](https://github.com/MarcusMedinaPro/MarcusMedina.Fluent.Pattern/actions)
[![Signed](https://img.shields.io/badge/Signed-Sigstore-green?style=for-the-badge&logo=linux)](https://docs.sigstore.dev)
[![Wiki](https://img.shields.io/badge/docs-wiki-blue?style=for-the-badge&logo=github)](https://github.com/MarcusMedinaPro/MarcusMedina.Fluent.Pattern/wiki)

**Fluent string pattern matching and analysis for .NET 10+**

I detta fall ville jag förenkla användandet av mönsterigenkänning i C#. Strängmönster, regex, wildcards — samlat i ett Fluent API så man slipper tänka på implementationen varje gång.

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

## Built with Human + AI Collaboration

This library was written by **Marcus Medina** together with **Claude Code** (Anthropic) — not through "vibe coding" where you just describe and accept, but through genuine collaboration: planning together, reviewing each other's decisions, pushing back when something felt wrong, and iterating until the result felt right.

The goal was always to write code worth reading and code worth using — the kind a student can open, understand, and learn from, and the kind any programmer can drop into real, professional work without wanting to rewrite it from scratch. AI was a partner in that process, not a shortcut around it.

If you're curious about this way of working, the source code and git history are open. Every decision has a reason behind it.

## Made for Curious Minds

This library was built with students in mind — not as a black box to copy and paste, but as a real-world example of how clean, purposeful code is written and shared. At the same time, it's built to be genuinely useful in professional projects too — for any developer who's tired of writing the same code over and over.

Whether you're discovering C# for the first time, need a reliable helper for your school project, want a dependable building block for production work, or are simply trying to fall in love with writing code — you're exactly who this was made for.

The source is open. Read it, fork it, break it, improve it. That's the whole point.

---

## Package Integrity

All releases are signed with [cosign](https://docs.sigstore.dev) (Sigstore keyless signing).

To verify a downloaded package, download both the `.nupkg` and its `.sigstore.json` bundle from the [GitHub Release](https://github.com/MarcusMedinaPro/MarcusMedina.Fluent.Pattern/releases), then run:

```bash
cosign verify-blob <package.nupkg> \
  --bundle <package.nupkg.sigstore.json> \
  --certificate-identity-regexp "https://github.com/MarcusMedinaPro/.*/release.yml" \
  --certificate-oidc-issuer https://token.actions.githubusercontent.com
```

Expected output: `Verified OK`

---

## Related Projects

- [MarcusMedina.Fluent.Data](https://github.com/MarcusMedinaPro/MarcusMedina.Fluent.Data) — CSV, JSON, XML extensions
- [MarcusMedina.Fluent.Data.Sql](https://github.com/MarcusMedinaPro/MarcusMedina.Fluent.Data.Sql) — SQL query builder
- [MarcusMedina.Maths.Algebra](https://github.com/MarcusMedinaPro/MarcusMedina.Maths.Algebra) — Algebraic expressions and symbolic math
