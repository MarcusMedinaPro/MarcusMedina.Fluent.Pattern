using System.Text.RegularExpressions;

namespace MarcusMedina.Fluent.Pattern;

/// <summary>Typ av upptäckt strängformat.</summary>
public enum StringType
{
    /// <summary>Vanlig text, inget specifikt format.</summary>
    PlainText,
    /// <summary>E-postadress.</summary>
    Email,
    /// <summary>Webbadress (http/https/ftp).</summary>
    Url,
    /// <summary>Unix/Linux-filsökväg.</summary>
    FilePath,
    /// <summary>Windows-filsökväg (inkl. UNC).</summary>
    WindowsPath,
    /// <summary>IP-adress (IPv4/IPv6).</summary>
    IpAddress,
    /// <summary>Subnet mask (IPv4).</summary>
    SubnetMask,
    /// <summary>MAC-adress.</summary>
    MacAddress,
    /// <summary>JSON-sträng.</summary>
    Json,
    /// <summary>XML-sträng.</summary>
    Xml,
    /// <summary>Heltal eller decimaltal.</summary>
    Number,
    /// <summary>GUID/UUID.</summary>
    Guid,
    /// <summary>SQL-fråga (SELECT/INSERT/UPDATE/DELETE).</summary>
    Sql
}

/// <summary>
/// SQL-inspirerade mönstermetoder för strängar — LIKE, IN, BETWEEN utan att vara SQL.
/// Inspirationskälla för studerande att bygga egna hjälpklasser.
/// </summary>
public static partial class StringPatternExtensions
{
    /// <summary>
    /// SQL LIKE-mönstermatchning. % matchar valfri sekvens, _ matchar ett tecken.
    /// </summary>
    /// <param name="value">Strängen att testa.</param>
    /// <param name="pattern">LIKE-mönster (t.ex. "hello%", "%world", "h%d").</param>
    /// <param name="caseSensitive">Om true, görs skiftlägeskänslig matchning.</param>
    /// <example>
    /// "hello world".IsLike("hello%") → true
    /// </example>
    public static bool IsLike(this string value, string pattern, bool caseSensitive = false)
    {
        ArgumentNullException.ThrowIfNull(value);
        ArgumentNullException.ThrowIfNull(pattern);

        var regexPattern = $"^{Regex.Escape(pattern).Replace("%", ".*").Replace("_", ".")}$";
        var options = caseSensitive ? RegexOptions.None : RegexOptions.IgnoreCase;
        return Regex.IsMatch(value, regexPattern, options);
    }

    /// <summary>
    /// Negation av <see cref="IsLike"/>.
    /// </summary>
    /// <param name="value">Strängen att testa.</param>
    /// <param name="pattern">LIKE-mönster (t.ex. "hello%", "%world", "h%d").</param>
    /// <param name="caseSensitive">Om true, görs skiftlägeskänslig matchning.</param>
    /// <example>
    /// "hello world".IsNotLike("goodbye%") → true
    /// </example>
    public static bool IsNotLike(this string value, string pattern, bool caseSensitive = false)
    {
        return !value.IsLike(pattern, caseSensitive);
    }


    /// <summary>
    /// Kontrollerar om en sträng finns i en given samling.
    /// </summary>
    /// <param name="value">Strängen att testa.</param>
    /// <param name="values">Samlingen att söka i.</param>
    /// <param name="caseSensitive">Om true, görs skiftlägeskänslig matchning.</param>
    /// <example>
    /// "hello".In(["hi", "hello", "hej"]) → true
    /// </example>
    public static bool In(this string value, IEnumerable<string> values, bool caseSensitive = false)
    {
        ArgumentNullException.ThrowIfNull(value);
        ArgumentNullException.ThrowIfNull(values);

        var comparison = caseSensitive ? StringComparison.Ordinal : StringComparison.OrdinalIgnoreCase;
        return values.Any(v => string.Equals(value, v, comparison));
    }

    /// <summary>
    /// Kontrollerar om en sträng INTE finns i en given samling.
    /// </summary>
    /// <param name="value">Strängen att testa.</param>
    /// <param name="values">Samlingen att söka i.</param>
    /// <param name="caseSensitive">Om true, görs skiftlägeskänslig matchning.</param>
    /// <example>
    /// "xyz".NotIn(["hi", "hello", "hej"]) → true
    /// </example>
    public static bool NotIn(this string value, IEnumerable<string> values, bool caseSensitive = false)
    {
        return !value.In(values, caseSensitive);
    }

    /// <summary>
    /// Kontrollerar om en sträng innehåller en sökterm, med stöd för case-alternativ.
    /// Undviker krock med string.Contains() genom unikt namn.
    /// </summary>
    /// <param name="value">Strängen att söka i.</param>
    /// <param name="searchTerm">Termen att söka efter.</param>
    /// <param name="caseSensitive">Om true, görs skiftlägeskänslig sökning.</param>
    /// <example>
    /// "hello world".LikeContains("WORLD") → true
    /// </example>
    public static bool LikeContains(this string value, string searchTerm, bool caseSensitive = false)
    {
        ArgumentNullException.ThrowIfNull(value);
        ArgumentNullException.ThrowIfNull(searchTerm);

        var comparison = caseSensitive ? StringComparison.Ordinal : StringComparison.OrdinalIgnoreCase;
        return value.Contains(searchTerm, comparison);
    }

    /// <summary>
    /// Kontrollerar om en sträng börjar med ett prefix, med stöd för case-alternativ.
    /// Undviker krock med string.StartsWith() genom unikt namn.
    /// </summary>
    /// <param name="value">Strängen att testa.</param>
    /// <param name="prefix">Prefixet att söka efter.</param>
    /// <param name="caseSensitive">Om true, görs skiftlägeskänslig matchning.</param>
    /// <example>
    /// "hello world".LikeStartsWith("HELLO") → true
    /// </example>
    public static bool LikeStartsWith(this string value, string prefix, bool caseSensitive = false)
    {
        ArgumentNullException.ThrowIfNull(value);
        ArgumentNullException.ThrowIfNull(prefix);

        var comparison = caseSensitive ? StringComparison.Ordinal : StringComparison.OrdinalIgnoreCase;
        return value.StartsWith(prefix, comparison);
    }

    /// <summary>
    /// Kontrollerar om en sträng slutar med ett suffix, med stöd för case-alternativ.
    /// Undviker krock med string.EndsWith() genom unikt namn.
    /// </summary>
    /// <param name="value">Strängen att testa.</param>
    /// <param name="suffix">Suffixet att söka efter.</param>
    /// <param name="caseSensitive">Om true, görs skiftlägeskänslig matchning.</param>
    /// <example>
    /// "hello world".LikeEndsWith("WORLD") → true
    /// </example>
    public static bool LikeEndsWith(this string value, string suffix, bool caseSensitive = false)
    {
        ArgumentNullException.ThrowIfNull(value);
        ArgumentNullException.ThrowIfNull(suffix);

        var comparison = caseSensitive ? StringComparison.Ordinal : StringComparison.OrdinalIgnoreCase;
        return value.EndsWith(suffix, comparison);
    }

    /// <summary>
    /// Kontrollerar om en sträng ligger inom ett alfabetiskt intervall.
    /// </summary>
    /// <param name="value">Strängen att testa.</param>
    /// <param name="start">Startvärde (inklusive).</param>
    /// <param name="end">Slutvärde (inklusive).</param>
    /// <param name="caseSensitive">Om true, görs skiftlägeskänslig jämförelse.</param>
    /// <example>
    /// "banana".Between("apple", "cherry") → true
    /// </example>
    public static bool Between(this string value, string start, string end, bool caseSensitive = false)
    {
        ArgumentNullException.ThrowIfNull(value);
        ArgumentNullException.ThrowIfNull(start);
        ArgumentNullException.ThrowIfNull(end);

        var comparison = caseSensitive ? StringComparison.Ordinal : StringComparison.OrdinalIgnoreCase;
        return string.Compare(value, start, comparison) >= 0
            && string.Compare(value, end, comparison) <= 0;
    }

    /// <summary>
    /// Beräknar Levenshtein-avståndet mellan två strängar — antal
    /// teckenändringar (insert, delete, replace) som krävs för att
    /// omvandla den ena till den andra.
    /// </summary>
    /// <param name="value">Den första strängen.</param>
    /// <param name="other">Den andra strängen.</param>
    /// <param name="caseSensitive">Om false (default) ignoreras skiftläge.</param>
    /// <example>
    /// "Kalle".LevenshteinDistance("Kålle") → 1
    /// "hat".LevenshteinDistance("cat") → 1
    /// </example>
    public static int LevenshteinDistance(this string value, string other, bool caseSensitive = false)
    {
        ArgumentNullException.ThrowIfNull(value);
        ArgumentNullException.ThrowIfNull(other);

        if (!caseSensitive)
        {
            value = value.ToLowerInvariant();
            other = other.ToLowerInvariant();
        }

        var n = value.Length;
        var m = other.Length;
        var d = new int[n + 1, m + 1];

        for (int i = 0; i <= n; i++) d[i, 0] = i;
        for (int j = 0; j <= m; j++) d[0, j] = j;

        for (int i = 1; i <= n; i++)
        {
            for (int j = 1; j <= m; j++)
            {
                var cost = value[i - 1] == other[j - 1] ? 0 : 1;
                d[i, j] = Math.Min(
                    Math.Min(d[i - 1, j] + 1, d[i, j - 1] + 1),
                    d[i - 1, j - 1] + cost);
            }
        }

        return d[n, m];
    }

    /// <summary>
    /// Beräknar likhet mellan 0.0 och 1.0 baserat på Levenshtein-avstånd.
    /// 1.0 = identiska, 0.0 = helt olika.
    /// </summary>
    /// <param name="value">Den första strängen.</param>
    /// <param name="other">Den andra strängen.</param>
    /// <param name="caseSensitive">Om false (default) ignoreras skiftläge.</param>
    /// <example>
    /// "Kalle".SimilarityTo("Kålle") → 0.8
    /// "abc".SimilarityTo("xyz") → 0.0
    /// </example>
    public static double SimilarityTo(this string value, string other, bool caseSensitive = false)
    {
        ArgumentNullException.ThrowIfNull(value);
        ArgumentNullException.ThrowIfNull(other);

        var dist = value.LevenshteinDistance(other, caseSensitive);
        var maxLen = Math.Max(value.Length, other.Length);
        if (maxLen == 0) return 1.0;
        return 1.0 - (double)dist / maxLen;
    }

    /// <summary>
    /// Kontrollerar om två strängar är lika nog enligt ett tröskelvärde
    /// (default 0.7 = 70% likhet).
    /// </summary>
    /// <param name="value">Den första strängen.</param>
    /// <param name="other">Den andra strängen.</param>
    /// <param name="threshold">Tröskel 0.0–1.0 (default 0.7).</param>
    /// <param name="caseSensitive">Om false (default) ignoreras skiftläge.</param>
    /// <example>
    /// "Kalle".IsSimilarTo("Kålle") → true
    /// "Kalle".IsSimilarTo("Anna") → false
    /// </example>
    public static bool IsSimilarTo(this string value, string other, double threshold = 0.7, bool caseSensitive = false)
    {
        ArgumentNullException.ThrowIfNull(value);
        ArgumentNullException.ThrowIfNull(other);

        return value.SimilarityTo(other, caseSensitive) >= threshold;
    }

    /// <summary>
    /// Beräknar Damerau–Levenshtein-avståndet — som Levenshtein men
    /// transpositioner (omkastning av två intilliggande tecken) kostar 1.
    /// </summary>
    /// <example>
    /// "teh".DamerauLevenshteinDistance("the") → 1
    /// </example>
    public static int DamerauLevenshteinDistance(this string value, string other, bool caseSensitive = false)
    {
        ArgumentNullException.ThrowIfNull(value);
        ArgumentNullException.ThrowIfNull(other);

        if (!caseSensitive)
        {
            value = value.ToLowerInvariant();
            other = other.ToLowerInvariant();
        }

        var n = value.Length;
        var m = other.Length;
        var d = new int[n + 1, m + 1];

        for (int i = 0; i <= n; i++) d[i, 0] = i;
        for (int j = 0; j <= m; j++) d[0, j] = j;

        for (int i = 1; i <= n; i++)
        {
            for (int j = 1; j <= m; j++)
            {
                var cost = value[i - 1] == other[j - 1] ? 0 : 1;
                d[i, j] = Math.Min(
                    Math.Min(d[i - 1, j] + 1, d[i, j - 1] + 1),
                    d[i - 1, j - 1] + cost);

                if (i > 1 && j > 1
                    && value[i - 1] == other[j - 2]
                    && value[i - 2] == other[j - 1])
                {
                    d[i, j] = Math.Min(d[i, j], d[i - 2, j - 2] + cost);
                }
            }
        }

        return d[n, m];
    }

    /// <summary>
    /// Hamming-avstånd: antal positioner där tecknen skiljer sig.
    /// Fungerar bara på strängar av samma längd.
    /// </summary>
    /// <exception cref="ArgumentException">Om strängarna har olika längd.</exception>
    /// <example>
    /// "karolin".HammingDistance("kathrin") → 3
    /// </example>
    public static int HammingDistance(this string value, string other, bool caseSensitive = false)
    {
        ArgumentNullException.ThrowIfNull(value);
        ArgumentNullException.ThrowIfNull(other);

        if (value.Length != other.Length)
            throw new ArgumentException("Strings must have the same length for Hamming distance.");

        if (!caseSensitive)
        {
            value = value.ToLowerInvariant();
            other = other.ToLowerInvariant();
        }

        return value.Zip(other, (a, b) => a == b ? 0 : 1).Sum();
    }

    /// <summary>
    /// Jaccard-likhet baserad på n-gram (delsträngar av längd n).
    /// 1.0 = samma uppsättning n-gram, 0.0 = inga gemensamma.
    /// </summary>
    /// <param name="value">Den första strängen.</param>
    /// <param name="other">Den andra strängen.</param>
    /// <param name="nGramSize">Storlek på n-gram (default 2).</param>
    /// <param name="caseSensitive">Om false (default) ignoreras skiftläge.</param>
    /// <example>
    /// "hello".JaccardSimilarity("hallo") → ~0.33
    /// </example>
    public static double JaccardSimilarity(this string value, string other, int nGramSize = 2, bool caseSensitive = false)
    {
        ArgumentNullException.ThrowIfNull(value);
        ArgumentNullException.ThrowIfNull(other);

        if (!caseSensitive)
        {
            value = value.ToLowerInvariant();
            other = other.ToLowerInvariant();
        }

        var grams1 = GetNGrams(value, nGramSize);
        var grams2 = GetNGrams(other, nGramSize);

        var intersection = grams1.Intersect(grams2).Count();
        var union = grams1.Union(grams2).Count();

        return union == 0 ? 1.0 : (double)intersection / union;
    }

    /// <summary>
    /// Jaro–Winkler-likhet — optimerad för korta strängar som namn.
    /// Ger högre poäng när strängarna delar ett gemensamt prefix.
    /// </summary>
    /// <example>
    /// "Marcus".JaroWinklerSimilarity("Markus") → ~0.88
    /// </example>
    public static double JaroWinklerSimilarity(this string value, string other, bool caseSensitive = false)
    {
        ArgumentNullException.ThrowIfNull(value);
        ArgumentNullException.ThrowIfNull(other);

        if (!caseSensitive)
        {
            value = value.ToLowerInvariant();
            other = other.ToLowerInvariant();
        }

        if (value == other) return 1.0;
        if (value.Length == 0 || other.Length == 0) return 0.0;

        var matchWindow = Math.Max(0, Math.Max(value.Length, other.Length) / 2 - 1);
        var matched1 = new bool[value.Length];
        var matched2 = new bool[other.Length];
        var common = 0;

        for (int i = 0; i < value.Length; i++)
        {
            var start = Math.Max(0, i - matchWindow);
            var end = Math.Min(other.Length - 1, i + matchWindow);
            for (int j = start; j <= end; j++)
            {
                if (matched2[j] || value[i] != other[j]) continue;
                matched1[i] = true;
                matched2[j] = true;
                common++;
                break;
            }
        }

        if (common == 0) return 0.0;

        var transpositions = 0;
        var k = 0;
        for (int i = 0; i < value.Length; i++)
        {
            if (!matched1[i]) continue;
            while (!matched2[k]) k++;
            if (value[i] != other[k]) transpositions++;
            k++;
        }

        var jaro = ((double)common / value.Length
                  + (double)common / other.Length
                  + ((double)common - transpositions / 2.0) / common) / 3.0;

        var prefix = 0;
        for (int i = 0; i < Math.Min(4, Math.Min(value.Length, other.Length)); i++)
        {
            if (value[i] != other[i]) break;
            prefix++;
        }

        return jaro + prefix * 0.1 * (1.0 - jaro);
    }

    /// <summary>
    /// Längsta gemensamma delsträngen mellan två strängar.
    /// </summary>
    /// <example>
    /// "abcdef".LongestCommonSubstring("zbcdf") → "bcd"
    /// </example>
    public static string LongestCommonSubstring(this string value, string other, bool caseSensitive = false)
    {
        ArgumentNullException.ThrowIfNull(value);
        ArgumentNullException.ThrowIfNull(other);

        if (!caseSensitive)
        {
            value = value.ToLowerInvariant();
            other = other.ToLowerInvariant();
        }

        var n = value.Length;
        var m = other.Length;
        var dp = new int[n + 1, m + 1];
        var maxLen = 0;
        var endPos = 0;

        for (int i = 1; i <= n; i++)
        {
            for (int j = 1; j <= m; j++)
            {
                if (value[i - 1] == other[j - 1])
                {
                    dp[i, j] = dp[i - 1, j - 1] + 1;
                    if (dp[i, j] > maxLen)
                    {
                        maxLen = dp[i, j];
                        endPos = i;
                    }
                }
            }
        }

        return value.Substring(endPos - maxLen, maxLen);
    }

    /// <summary>
    /// Kontrollerar om strängen är ett palindrom (läses likadant baklänges).
    /// Ignorerar skiftläge och mellanslag som standard.
    /// </summary>
    /// <param name="value">Strängen att testa.</param>
    /// <param name="ignoreWhitespace">Om true ignoreras mellanslag.</param>
    /// <example>
    /// "anna".IsPalindrome() → true
    /// "A man a plan a canal panama".IsPalindrome(true) → true
    /// </example>
    public static bool IsPalindrome(this string value, bool ignoreWhitespace = false)
    {
        ArgumentNullException.ThrowIfNull(value);

        var s = ignoreWhitespace ? value.Replace(" ", "") : value;
        s = s.ToLowerInvariant();

        var half = s.Length / 2;
        for (int i = 0; i < half; i++)
        {
            if (s[i] != s[s.Length - 1 - i])
                return false;
        }
        return true;
    }

    /// <summary>
    /// Kontrollerar om strängen är ett anagram av en annan sträng.
    /// </summary>
    /// <example>
    /// "listen".IsAnagramOf("silent") → true
    /// </example>
    public static bool IsAnagramOf(this string value, string other, bool caseSensitive = false)
    {
        ArgumentNullException.ThrowIfNull(value);
        ArgumentNullException.ThrowIfNull(other);

        if (value.Length != other.Length) return false;

        var comparison = caseSensitive ? StringComparison.Ordinal : StringComparison.OrdinalIgnoreCase;
        return string.Concat(value.OrderBy(c => c))
            .Equals(string.Concat(other.OrderBy(c => c)), comparison);
    }

    /// <summary>
    /// Kontrollerar om strängen innehåller alla bokstäver i alfabetet (A–Ö).
    /// </summary>
    /// <param name="value">Strängen att testa.</param>
    /// <param name="includeSwedish">Om false används engelska alfabetet A–Z.</param>
    /// <example>
    /// "The quick brown fox jumps over the lazy dog".IsPangram() → true
    /// </example>
    public static bool IsPangram(this string value, bool includeSwedish = false)
    {
        ArgumentNullException.ThrowIfNull(value);

        var letters = value.Where(char.IsLetter).Select(char.ToLowerInvariant);
        var alphabet = includeSwedish
            ? "abcdefghijklmnopqrstuvwxyzåäö"
            : "abcdefghijklmnopqrstuvwxyz";

        return alphabet.All(c => letters.Contains(c));
    }

    /// <summary>
    /// Genererar en fonetisk nyckel (Soundex-inspirerad) för en sträng.
    /// Strängar som låter lika får samma nyckel.
    /// </summary>
    /// <example>
    /// "Kalle".PhoneticKey() == "Kålle".PhoneticKey() → true
    /// </example>
    public static string PhoneticKey(this string value)
    {
        ArgumentNullException.ThrowIfNull(value);

        if (value.Length == 0) return "";

        var s = value.ToLowerInvariant();
        var key = new System.Text.StringBuilder();
        key.Append(char.ToUpperInvariant(s[0]));

        for (int i = 1; i < s.Length; i++)
        {
            var mapped = s[i] switch
            {
                'b' or 'f' or 'p' or 'v' => '1',
                'c' or 'g' or 'j' or 'k' or 'q' or 's' or 'x' or 'z' => '2',
                'd' or 't' => '3',
                'l' => '4',
                'm' or 'n' => '5',
                'r' => '6',
                'å' or 'ä' or 'a' => '7',
                'ö' or 'o' or 'u' => '8',
                'e' or 'i' or 'y' => '9',
                _ => '0'
            };

            if (mapped != key[key.Length - 1])
                key.Append(mapped);
        }

        while (key.Length < 4) key.Append('0');
        return key.ToString()[..4];
    }

    /// <summary>
    /// Kontrollerar om strängen är en giltig e-postadress.
    /// </summary>
    /// <example>
    /// "hej@example.com".IsEmail() → true
    /// </example>
    public static bool IsEmail(this string value)
    {
        ArgumentNullException.ThrowIfNull(value);
        return EmailRegex().IsMatch(value);
    }

    /// <summary>
    /// Kontrollerar om strängen är en giltig URL (http/https/ftp).
    /// </summary>
    /// <example>
    /// "https://example.com".IsUrl() → true
    /// </example>
    public static bool IsUrl(this string value)
    {
        ArgumentNullException.ThrowIfNull(value);
        return UrlRegex().IsMatch(value);
    }

    /// <summary>
    /// Kontrollerar om strängen är en giltig IP-adress (IPv4 eller IPv6).
    /// </summary>
    /// <example>
    /// "192.168.1.1".IsIpAddress() → true
    /// </example>
    public static bool IsIpAddress(this string value)
    {
        ArgumentNullException.ThrowIfNull(value);
        return IpAddressRegex().IsMatch(value.Trim());
    }

    /// <summary>
    /// Avkänner vilken typ av data strängen innehåller.
    /// </summary>
    /// <param name="value">Strängen att analysera.</param>
    /// <example>
    /// "hej@example.com".DetectStringType() → StringType.Email
    /// "192.168.1.1".DetectStringType() → StringType.IpAddress
    /// </example>
    public static StringType DetectStringType(this string value)
    {
        ArgumentNullException.ThrowIfNull(value);

        var trimmed = value.Trim();
        if (trimmed.Length == 0) return StringType.PlainText;

        // Guid
        if (GuidRegex().IsMatch(trimmed))
            return StringType.Guid;

        // Number
        if (NumberRegex().IsMatch(trimmed))
            return StringType.Number;

        // IP address (check before subnet — every subnet is an IP)
        if (IpAddressRegex().IsMatch(trimmed))
        {
            if (SubnetRegex().IsMatch(trimmed))
                return StringType.SubnetMask;
            return StringType.IpAddress;
        }

        // MAC address
        if (MacAddressRegex().IsMatch(trimmed))
            return StringType.MacAddress;

        // Email
        if (EmailRegex().IsMatch(trimmed))
            return StringType.Email;

        // URL (includes ftp://)
        if (UrlRegex().IsMatch(trimmed))
            return StringType.Url;

        // Windows path (C:\, or UNC \\server\share)
        if (trimmed.Length >= 3 && trimmed[1] == ':'
            && (trimmed[2] == '\\' || trimmed[2] == '/'))
            return StringType.WindowsPath;
        if (trimmed.StartsWith("\\\\"))
            return StringType.WindowsPath;

        // Unix file path
        if (trimmed.StartsWith('/'))
            return StringType.FilePath;

        // XML (starts with <)
        if (trimmed.StartsWith('<') && trimmed.EndsWith('>'))
            return StringType.Xml;

        // JSON (starts with { or [)
        if ((trimmed.StartsWith('{') && trimmed.EndsWith('}'))
            || (trimmed.StartsWith('[') && trimmed.EndsWith(']')))
            return StringType.Json;

        // SQL (common keywords)
        if (SqlRegex().IsMatch(trimmed))
            return StringType.Sql;

        return StringType.PlainText;
    }

    private static HashSet<string> GetNGrams(string s, int n)
    {
        var result = new HashSet<string>();
        if (s.Length < n) { result.Add(s); return result; }
        for (int i = 0; i <= s.Length - n; i++)
            result.Add(s.Substring(i, n));
        return result;
    }

    [GeneratedRegex(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$")]
    private static partial Regex EmailRegex();

    [GeneratedRegex(@"^(https?|ftp)://[^\s/$.?#].[^\s]*$")]
    private static partial Regex UrlRegex();

    [GeneratedRegex(@"^(\d{1,3}\.){3}\d{1,3}$")]
    private static partial Regex IpAddressRegex();

    [GeneratedRegex(@"^(255|254|252|248|240|224|192|128|0)\.(255|254|252|248|240|224|192|128|0)\.(255|254|252|248|240|224|192|128|0)\.(255|254|252|248|240|224|192|128|0)$")]
    private static partial Regex SubnetRegex();

    [GeneratedRegex(@"^([0-9A-Fa-f]{2}[:.-]){5}[0-9A-Fa-f]{2}$")]
    private static partial Regex MacAddressRegex();

    [GeneratedRegex(@"^[+-]?\d+(\.\d+)?$")]
    private static partial Regex NumberRegex();

    [GeneratedRegex(@"^[{(]?[0-9A-Fa-f]{8}[-]?([0-9A-Fa-f]{4}[-]?){3}[0-9A-Fa-f]{12}[)}]?$")]
    private static partial Regex GuidRegex();

    [GeneratedRegex(@"^\s*(SELECT|INSERT|UPDATE|DELETE|CREATE|DROP|ALTER)\s", RegexOptions.IgnoreCase)]
    private static partial Regex SqlRegex();
}
