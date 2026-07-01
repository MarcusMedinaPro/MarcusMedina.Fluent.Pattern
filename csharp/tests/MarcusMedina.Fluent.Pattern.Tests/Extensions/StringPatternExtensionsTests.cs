using MarcusMedina.Fluent.Pattern;

namespace MarcusMedina.Fluent.Pattern.Tests;

public class StringPatternExtensionsTests
{
    #region IsLike

    [Fact]
    public void IsLike_PercentWildcard_MatchesAnySequence()
    {
        "hello world".IsLike("hello%").Should().BeTrue();
    }

    [Fact]
    public void IsLike_PercentWildcard_Suffix()
    {
        "hello world".IsLike("%world").Should().BeTrue();
    }

    [Fact]
    public void IsLike_Underscore_MatchesSingleChar()
    {
        "hello world".IsLike("hello_world").Should().BeTrue();
    }

    [Fact]
    public void IsLike_PercentBothEnds()
    {
        "hello world".IsLike("h%d").Should().BeTrue();
    }

    [Fact]
    public void IsLike_CaseInsensitiveByDefault()
    {
        "hello world".IsLike("HELLO%").Should().BeTrue();
    }

    [Fact]
    public void IsLike_CaseSensitive_FailsOnCaseMismatch()
    {
        "hello world".IsLike("HELLO%", caseSensitive: true).Should().BeFalse();
    }

    [Fact]
    public void IsLike_ExactMatch_ReturnsTrue()
    {
        "hello".IsLike("hello").Should().BeTrue();
    }

    [Fact]
    public void IsLike_NoMatch_ReturnsFalse()
    {
        "hello".IsLike("world").Should().BeFalse();
    }

    [Fact]
    public void IsLike_NullValue_ThrowsArgumentNullException()
    {
        string? value = null;
        var act = () => value!.IsLike("%");
        act.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void IsLike_NullPattern_ThrowsArgumentNullException()
    {
        string? pattern = null;
        var act = () => "hello".IsLike(pattern!);
        act.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void IsLike_EmptyString_MatchesEmpty()
    {
        "".IsLike("").Should().BeTrue();
    }

    [Fact]
    public void IsLike_EmptyPattern_MatchesOnlyEmpty()
    {
        "hello".IsLike("").Should().BeFalse();
        "".IsLike("").Should().BeTrue();
    }

    #endregion


    #region IsNotLike

    [Fact]
    public void IsNotLike_ReturnsOppositeOfIsLike()
    {
        "hello world".IsNotLike("goodbye%").Should().BeTrue();
    }

    [Fact]
    public void IsNotLike_FalseWhenPatternMatches()
    {
        "hello world".IsNotLike("hello%").Should().BeFalse();
    }

    [Fact]
    public void IsNotLike_NullValue_Throws()
    {
        string? v = null;
        var act = () => v!.IsNotLike("%");
        act.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void IsNotLike_NullPattern_Throws()
    {
        string? p = null;
        var act = () => "hello".IsNotLike(p!);
        act.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void IsNotLike_EmptyValue_ReturnsTrue()
    {
        "".IsNotLike("hello").Should().BeTrue();
    }

    [Fact]
    public void IsNotLike_EmptyPattern_ReturnsTrue()
    {
        "hello".IsNotLike("").Should().BeTrue();
    }

    [Fact]
    public void IsNotLike_BothEmpty_ReturnsFalse()
    {
        "".IsNotLike("").Should().BeFalse();
    }

    #endregion


    #region LikeContains

    [Fact]
    public void LikeContains_FindsSubstring_ReturnsTrue()
    {
        "hello world".LikeContains("world").Should().BeTrue();
    }

    [Fact]
    public void LikeContains_CaseInsensitiveByDefault()
    {
        "hello world".LikeContains("WORLD").Should().BeTrue();
    }

    [Fact]
    public void LikeContains_CaseSensitive_FailsOnCaseMismatch()
    {
        "hello world".LikeContains("WORLD", caseSensitive: true).Should().BeFalse();
    }

    [Fact]
    public void LikeContains_NotFound_ReturnsFalse()
    {
        "hello world".LikeContains("xyz").Should().BeFalse();
    }

    [Fact]
    public void LikeContains_NullValue_ThrowsArgumentNullException()
    {
        string? value = null;
        var act = () => value!.LikeContains("test");
        act.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void LikeContains_NullSearchTerm_ThrowsArgumentNullException()
    {
        string? term = null;
        var act = () => "hello".LikeContains(term!);
        act.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void LikeContains_EmptyValue_ReturnsFalse()
    {
        "".LikeContains("hello").Should().BeFalse();
    }

    [Fact]
    public void LikeContains_EmptySearchTerm_ReturnsTrue()
    {
        "hello".LikeContains("").Should().BeTrue();
    }

    [Fact]
    public void LikeContains_BothEmpty_ReturnsTrue()
    {
        "".LikeContains("").Should().BeTrue();
    }

    #endregion

    #region LikeStartsWith

    [Fact]
    public void LikeStartsWith_MatchingPrefix_ReturnsTrue()
    {
        "hello world".LikeStartsWith("hello").Should().BeTrue();
    }

    [Fact]
    public void LikeStartsWith_CaseInsensitiveByDefault()
    {
        "hello world".LikeStartsWith("HELLO").Should().BeTrue();
    }

    [Fact]
    public void LikeStartsWith_CaseSensitive_FailsOnCaseMismatch()
    {
        "hello world".LikeStartsWith("HELLO", caseSensitive: true).Should().BeFalse();
    }

    [Fact]
    public void LikeStartsWith_NoMatch_ReturnsFalse()
    {
        "hello world".LikeStartsWith("world").Should().BeFalse();
    }

    [Fact]
    public void LikeStartsWith_NullValue_ThrowsArgumentNullException()
    {
        string? value = null;
        var act = () => value!.LikeStartsWith("hello");
        act.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void LikeStartsWith_NullPrefix_Throws()
    {
        string? p = null;
        var act = () => "hello".LikeStartsWith(p!);
        act.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void LikeStartsWith_EmptyValue_ReturnsFalse()
    {
        "".LikeStartsWith("hello").Should().BeFalse();
    }

    [Fact]
    public void LikeStartsWith_EmptyPrefix_ReturnsTrue()
    {
        "hello".LikeStartsWith("").Should().BeTrue();
    }

    #endregion

    #region LikeEndsWith

    [Fact]
    public void LikeEndsWith_MatchingSuffix_ReturnsTrue()
    {
        "hello world".LikeEndsWith("world").Should().BeTrue();
    }

    [Fact]
    public void LikeEndsWith_CaseInsensitiveByDefault()
    {
        "hello world".LikeEndsWith("WORLD").Should().BeTrue();
    }

    [Fact]
    public void LikeEndsWith_CaseSensitive_FailsOnCaseMismatch()
    {
        "hello world".LikeEndsWith("WORLD", caseSensitive: true).Should().BeFalse();
    }

    [Fact]
    public void LikeEndsWith_NoMatch_ReturnsFalse()
    {
        "hello world".LikeEndsWith("hello").Should().BeFalse();
    }

    [Fact]
    public void LikeEndsWith_NullValue_ThrowsArgumentNullException()
    {
        string? value = null;
        var act = () => value!.LikeEndsWith("world");
        act.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void LikeEndsWith_NullSuffix_Throws()
    {
        string? s = null;
        var act = () => "hello".LikeEndsWith(s!);
        act.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void LikeEndsWith_EmptyValue_ReturnsFalse()
    {
        "".LikeEndsWith("hello").Should().BeFalse();
    }

    [Fact]
    public void LikeEndsWith_EmptySuffix_ReturnsTrue()
    {
        "hello".LikeEndsWith("").Should().BeTrue();
    }

    #endregion

    #region In

    [Fact]
    public void In_MatchingValue_ReturnsTrue()
    {
        "hello".In(["hello", "world"]).Should().BeTrue();
    }

    [Fact]
    public void In_CaseInsensitiveByDefault()
    {
        "HELLO".In(["hello", "world"]).Should().BeTrue();
    }

    [Fact]
    public void In_CaseSensitive_FailsOnCaseMismatch()
    {
        "HELLO".In(["hello", "world"], caseSensitive: true).Should().BeFalse();
    }

    [Fact]
    public void In_NoMatch_ReturnsFalse()
    {
        "test".In(["hello", "world"]).Should().BeFalse();
    }

    [Fact]
    public void In_EmptyCollection_ReturnsFalse()
    {
        "hello".In([]).Should().BeFalse();
    }

    [Fact]
    public void In_NullValue_ThrowsArgumentNullException()
    {
        string? value = null;
        var act = () => value!.In(["hello"]);
        act.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void In_NullCollection_ThrowsArgumentNullException()
    {
        IEnumerable<string>? values = null;
        var act = () => "hello".In(values!);
        act.Should().Throw<ArgumentNullException>();
    }

    #endregion

    #region Between

    [Fact]
    public void Between_ValueInRange_ReturnsTrue()
    {
        "bob".Between("alice", "charlie").Should().BeTrue();
    }

    [Fact]
    public void Between_ValueAtStart_ReturnsTrue()
    {
        "alice".Between("alice", "charlie").Should().BeTrue();
    }

    [Fact]
    public void Between_ValueAtEnd_ReturnsTrue()
    {
        "charlie".Between("alice", "charlie").Should().BeTrue();
    }

    [Fact]
    public void Between_ValueOutsideRange_ReturnsFalse()
    {
        "dave".Between("alice", "charlie").Should().BeFalse();
    }

    [Fact]
    public void Between_CaseInsensitiveByDefault()
    {
        "BOB".Between("alice", "charlie").Should().BeTrue();
    }

    [Fact]
    public void Between_NullValue_ThrowsArgumentNullException()
    {
        string? value = null;
        var act = () => value!.Between("a", "z");
        act.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void Between_NullStart_ThrowsArgumentNullException()
    {
        string? start = null;
        var act = () => "hello".Between(start!, "z");
        act.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void Between_NullEnd_ThrowsArgumentNullException()
    {
        string? end = null;
        var act = () => "hello".Between("a", end!);
        act.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void Between_ReversedRange_ReturnsFalse()
    {
        "m".Between("z", "a").Should().BeFalse();
    }

    [Fact]
    public void Between_EmptyValue_ReturnsFalseForNonEmptyRange()
    {
        "".Between("a", "z").Should().BeFalse();
    }

    [Fact]
    public void Between_EmptyStartAndEnd_ValueEmpty_ReturnsTrue()
    {
        "".Between("", "").Should().BeTrue();
    }

    #endregion

    #region NotIn

    [Fact]
    public void NotIn_NonMatchingValue_ReturnsTrue()
    {
        "test".NotIn(["hello", "world"]).Should().BeTrue();
    }

    [Fact]
    public void NotIn_MatchingValue_ReturnsFalse()
    {
        "hello".NotIn(["hello", "world"]).Should().BeFalse();
    }

    [Fact]
    public void NotIn_CaseInsensitiveByDefault()
    {
        "HELLO".NotIn(["hello", "world"]).Should().BeFalse();
    }

    [Fact]
    public void NotIn_CaseSensitive_FailsOnCaseMismatch()
    {
        "HELLO".NotIn(["hello", "world"], caseSensitive: true).Should().BeTrue();
    }

    [Fact]
    public void NotIn_EmptyCollection_ReturnsTrue()
    {
        "hello".NotIn([]).Should().BeTrue();
    }

    [Fact]
    public void NotIn_NullValue_ThrowsArgumentNullException()
    {
        string? value = null;
        var act = () => value!.NotIn(["hello"]);
        act.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void NotIn_NullCollection_Throws()
    {
        IEnumerable<string>? values = null;
        var act = () => "hello".NotIn(values!);
        act.Should().Throw<ArgumentNullException>();
    }

    #endregion

    #region LevenshteinDistance

    [Fact]
    public void LevenshteinDistance_Identical_ReturnsZero()
    {
        "hello".LevenshteinDistance("hello").Should().Be(0);
    }

    [Fact]
    public void LevenshteinDistance_OneSubstitution_ReturnsOne()
    {
        "Kalle".LevenshteinDistance("Kålle").Should().Be(1);
    }

    [Fact]
    public void LevenshteinDistance_OneInsertion_ReturnsOne()
    {
        "cat".LevenshteinDistance("cats").Should().Be(1);
    }

    [Fact]
    public void LevenshteinDistance_OneDeletion_ReturnsOne()
    {
        "cats".LevenshteinDistance("cat").Should().Be(1);
    }

    [Fact]
    public void LevenshteinDistance_CompletelyDifferent_ReturnsMaxLen()
    {
        "abc".LevenshteinDistance("xyz").Should().Be(3);
    }

    [Fact]
    public void LevenshteinDistance_CaseInsensitiveByDefault()
    {
        "HELLO".LevenshteinDistance("hello").Should().Be(0);
    }

    [Fact]
    public void LevenshteinDistance_CaseSensitive()
    {
        "HELLO".LevenshteinDistance("hello", caseSensitive: true).Should().Be(5);
    }

    [Fact]
    public void LevenshteinDistance_EmptyVsString_ReturnsLength()
    {
        "".LevenshteinDistance("abc").Should().Be(3);
    }

    [Fact]
    public void LevenshteinDistance_BothEmpty_ReturnsZero()
    {
        "".LevenshteinDistance("").Should().Be(0);
    }

    [Fact]
    public void LevenshteinDistance_NullValue_Throws()
    {
        string? v = null;
        var act = () => v!.LevenshteinDistance("x");
        act.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void LevenshteinDistance_NullOther_Throws()
    {
        string? o = null;
        var act = () => "x".LevenshteinDistance(o!);
        act.Should().Throw<ArgumentNullException>();
    }

    #endregion

    #region SimilarityTo

    [Fact]
    public void SimilarityTo_Identical_ReturnsOne()
    {
        "hello".SimilarityTo("hello").Should().Be(1.0);
    }

    [Fact]
    public void SimilarityTo_OneChange_ReturnsHigh()
    {
        "Kalle".SimilarityTo("Kålle").Should().Be(0.8);
    }

    [Fact]
    public void SimilarityTo_CompletelyDifferent_ReturnsZero()
    {
        "abc".SimilarityTo("xyz").Should().Be(0.0);
    }

    [Fact]
    public void SimilarityTo_BothEmpty_ReturnsOne()
    {
        "".SimilarityTo("").Should().Be(1.0);
    }

    [Fact]
    public void SimilarityTo_CaseInsensitiveByDefault()
    {
        "HELLO".SimilarityTo("hello").Should().Be(1.0);
    }

    [Fact]
    public void SimilarityTo_NullValue_Throws()
    {
        string? v = null;
        var act = () => v!.SimilarityTo("x");
        act.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void SimilarityTo_NullOther_Throws()
    {
        string? o = null;
        var act = () => "x".SimilarityTo(o!);
        act.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void SimilarityTo_EmptyVsString_ReturnsZero()
    {
        "".SimilarityTo("abc").Should().Be(0.0);
    }

    #endregion

    #region IsSimilarTo

    [Fact]
    public void IsSimilarTo_Identical_ReturnsTrue()
    {
        "Kalle".IsSimilarTo("Kalle").Should().BeTrue();
    }

    [Fact]
    public void IsSimilarTo_CloseEnough_ReturnsTrue()
    {
        "Kalle".IsSimilarTo("Kålle").Should().BeTrue();
    }

    [Fact]
    public void IsSimilarTo_TooDifferent_ReturnsFalse()
    {
        "Kalle".IsSimilarTo("Anna").Should().BeFalse();
    }

    [Fact]
    public void IsSimilarTo_CustomThreshold_Respected()
    {
        "abc".IsSimilarTo("abx", threshold: 0.9).Should().BeFalse();
    }

    [Fact]
    public void IsSimilarTo_LowThreshold_Catches()
    {
        "abc".IsSimilarTo("abcX", threshold: 0.2).Should().BeTrue();
    }

    [Fact]
    public void IsSimilarTo_CaseSensitive_Respected()
    {
        "Kalle".IsSimilarTo("kalle", threshold: 1.0, caseSensitive: true).Should().BeFalse();
    }

    [Fact]
    public void IsSimilarTo_NullValue_Throws()
    {
        string? v = null;
        var act = () => v!.IsSimilarTo("x");
        act.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void IsSimilarTo_NullOther_Throws()
    {
        string? o = null;
        var act = () => "x".IsSimilarTo(o!);
        act.Should().Throw<ArgumentNullException>();
    }

    #endregion

    #region DamerauLevenshteinDistance

    [Fact]
    public void DamerauLevenshtein_Identical_ReturnsZero()
    {
        "hello".DamerauLevenshteinDistance("hello").Should().Be(0);
    }

    [Fact]
    public void DamerauLevenshtein_Transposition_ReturnsOne()
    {
        "teh".DamerauLevenshteinDistance("the").Should().Be(1);
    }

    [Fact]
    public void DamerauLevenshtein_Substitution_ReturnsOne()
    {
        "Kalle".DamerauLevenshteinDistance("Kålle").Should().Be(1);
    }

    [Fact]
    public void DamerauLevenshtein_CaseInsensitiveByDefault()
    {
        "HELLO".DamerauLevenshteinDistance("hello").Should().Be(0);
    }

    [Fact]
    public void DamerauLevenshtein_EmptyVsString_ReturnsLength()
    {
        "".DamerauLevenshteinDistance("abc").Should().Be(3);
    }

    [Fact]
    public void DamerauLevenshtein_BothEmpty_ReturnsZero()
    {
        "".DamerauLevenshteinDistance("").Should().Be(0);
    }

    [Fact]
    public void DamerauLevenshtein_NullValue_Throws()
    {
        string? v = null;
        var act = () => v!.DamerauLevenshteinDistance("x");
        act.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void DamerauLevenshtein_NullOther_Throws()
    {
        string? o = null;
        var act = () => "x".DamerauLevenshteinDistance(o!);
        act.Should().Throw<ArgumentNullException>();
    }

    #endregion

    #region HammingDistance

    [Fact]
    public void HammingDistance_Identical_ReturnsZero()
    {
        "karolin".HammingDistance("karolin").Should().Be(0);
    }

    [Fact]
    public void HammingDistance_ThreeDifferences()
    {
        "karolin".HammingDistance("kathrin").Should().Be(3);
    }

    [Fact]
    public void HammingDistance_CaseInsensitiveByDefault()
    {
        "HELLO".HammingDistance("hello").Should().Be(0);
    }

    [Fact]
    public void HammingDistance_DifferentLengths_Throws()
    {
        var act = () => "abc".HammingDistance("abcd");
        act.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void HammingDistance_NullValue_Throws()
    {
        string? v = null;
        var act = () => v!.HammingDistance("x");
        act.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void HammingDistance_NullOther_Throws()
    {
        string? o = null;
        var act = () => "x".HammingDistance(o!);
        act.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void HammingDistance_EmptyStrings_ReturnsZero()
    {
        "".HammingDistance("").Should().Be(0);
    }

    #endregion

    #region JaccardSimilarity

    [Fact]
    public void JaccardSimilarity_Identical_ReturnsOne()
    {
        "hello".JaccardSimilarity("hello").Should().Be(1.0);
    }

    [Fact]
    public void JaccardSimilarity_PartialMatch_ReturnsLessThanOne()
    {
        "hello".JaccardSimilarity("hallo").Should().BeGreaterThan(0);
    }

    [Fact]
    public void JaccardSimilarity_NoMatch_ReturnsZero()
    {
        "abc".JaccardSimilarity("xyz").Should().Be(0.0);
    }

    [Fact]
    public void JaccardSimilarity_CustomNGramSize()
    {
        "hello".JaccardSimilarity("hallo", nGramSize: 3).Should().BeGreaterThan(0);
    }

    [Fact]
    public void JaccardSimilarity_BothEmpty_ReturnsOne()
    {
        "".JaccardSimilarity("").Should().Be(1.0);
    }

    [Fact]
    public void JaccardSimilarity_NullValue_Throws()
    {
        string? v = null;
        var act = () => v!.JaccardSimilarity("x");
        act.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void JaccardSimilarity_NullOther_Throws()
    {
        string? o = null;
        var act = () => "x".JaccardSimilarity(o!);
        act.Should().Throw<ArgumentNullException>();
    }

    #endregion

    #region JaroWinklerSimilarity

    [Fact]
    public void JaroWinkler_Identical_ReturnsOne()
    {
        "Marcus".JaroWinklerSimilarity("Marcus").Should().Be(1.0);
    }

    [Fact]
    public void JaroWinkler_Similar_ReturnsHigh()
    {
        "Marcus".JaroWinklerSimilarity("Markus").Should().BeGreaterThan(0.8);
    }

    [Fact]
    public void JaroWinkler_CompletelyDifferent_ReturnsZero()
    {
        "abc".JaroWinklerSimilarity("xyz").Should().Be(0.0);
    }

    [Fact]
    public void JaroWinkler_OneEmpty_ReturnsZero()
    {
        "abc".JaroWinklerSimilarity("").Should().Be(0.0);
    }

    [Fact]
    public void JaroWinkler_BothEmpty_ReturnsOne()
    {
        "".JaroWinklerSimilarity("").Should().Be(1.0);
    }

    [Fact]
    public void JaroWinkler_NullValue_Throws()
    {
        string? v = null;
        var act = () => v!.JaroWinklerSimilarity("x");
        act.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void JaroWinkler_NullOther_Throws()
    {
        string? o = null;
        var act = () => "x".JaroWinklerSimilarity(o!);
        act.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void JaroWinkler_PrefixBoost_GivesHigherScore()
    {
        var similarPrefix = "Marcus".JaroWinklerSimilarity("Markus");
        var noPrefix = "abcde".JaroWinklerSimilarity("fghij");
        similarPrefix.Should().BeGreaterThan(noPrefix);
    }

    #endregion

    #region LongestCommonSubstring

    [Fact]
    public void LongestCommonSubstring_ReturnsMatch()
    {
        "abcdef".LongestCommonSubstring("zbcdf").Should().Be("bcd");
    }

    [Fact]
    public void LongestCommonSubstring_Identical_ReturnsFullString()
    {
        "hello".LongestCommonSubstring("hello").Should().Be("hello");
    }

    [Fact]
    public void LongestCommonSubstring_NoMatch_ReturnsEmpty()
    {
        "abc".LongestCommonSubstring("xyz").Should().Be("");
    }

    [Fact]
    public void LongestCommonSubstring_CaseInsensitiveByDefault()
    {
        "ABC".LongestCommonSubstring("abc").Should().Be("abc");
    }

    [Fact]
    public void LongestCommonSubstring_OneEmpty_ReturnsEmpty()
    {
        "abc".LongestCommonSubstring("").Should().Be("");
    }

    [Fact]
    public void LongestCommonSubstring_BothEmpty_ReturnsEmpty()
    {
        "".LongestCommonSubstring("").Should().Be("");
    }

    [Fact]
    public void LongestCommonSubstring_NullValue_Throws()
    {
        string? v = null;
        var act = () => v!.LongestCommonSubstring("x");
        act.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void LongestCommonSubstring_NullOther_Throws()
    {
        string? o = null;
        var act = () => "x".LongestCommonSubstring(o!);
        act.Should().Throw<ArgumentNullException>();
    }

    #endregion

    #region IsPalindrome

    [Fact]
    public void IsPalindrome_Simple_ReturnsTrue()
    {
        "anna".IsPalindrome().Should().BeTrue();
    }

    [Fact]
    public void IsPalindrome_NotPalindrome_ReturnsFalse()
    {
        "hello".IsPalindrome().Should().BeFalse();
    }

    [Fact]
    public void IsPalindrome_IgnoresCase()
    {
        "Anna".IsPalindrome().Should().BeTrue();
    }

    [Fact]
    public void IsPalindrome_WithWhitespace_ReturnsTrueWhenIgnored()
    {
        "A man a plan a canal panama".IsPalindrome(ignoreWhitespace: true).Should().BeTrue();
    }

    [Fact]
    public void IsPalindrome_EmptyString_ReturnsTrue()
    {
        "".IsPalindrome().Should().BeTrue();
    }

    [Fact]
    public void IsPalindrome_SingleChar_ReturnsTrue()
    {
        "a".IsPalindrome().Should().BeTrue();
    }

    [Fact]
    public void IsPalindrome_NullValue_Throws()
    {
        string? v = null;
        var act = () => v!.IsPalindrome();
        act.Should().Throw<ArgumentNullException>();
    }

    #endregion

    #region IsAnagramOf

    [Fact]
    public void IsAnagramOf_True()
    {
        "listen".IsAnagramOf("silent").Should().BeTrue();
    }

    [Fact]
    public void IsAnagramOf_False()
    {
        "hello".IsAnagramOf("world").Should().BeFalse();
    }

    [Fact]
    public void IsAnagramOf_DifferentLength_ReturnsFalse()
    {
        "abc".IsAnagramOf("abcd").Should().BeFalse();
    }

    [Fact]
    public void IsAnagramOf_CaseInsensitiveByDefault()
    {
        "LISTEN".IsAnagramOf("silent").Should().BeTrue();
    }

    [Fact]
    public void IsAnagramOf_EmptyStrings_ReturnsTrue()
    {
        "".IsAnagramOf("").Should().BeTrue();
    }

    [Fact]
    public void IsAnagramOf_NullValue_Throws()
    {
        string? v = null;
        var act = () => v!.IsAnagramOf("x");
        act.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void IsAnagramOf_NullOther_Throws()
    {
        string? o = null;
        var act = () => "x".IsAnagramOf(o!);
        act.Should().Throw<ArgumentNullException>();
    }

    #endregion

    #region IsPangram

    [Fact]
    public void IsPangram_English_ReturnsTrue()
    {
        "The quick brown fox jumps over the lazy dog".IsPangram().Should().BeTrue();
    }

    [Fact]
    public void IsPangram_NotPangram_ReturnsFalse()
    {
        "hello world".IsPangram().Should().BeFalse();
    }

    [Fact]
    public void IsPangram_Swedish_IncludesÅÄÖ()
    {
        "abcdefghijklmnopqrstuvwxyzåäö".IsPangram(includeSwedish: true).Should().BeTrue();
    }

    [Fact]
    public void IsPangram_Empty_ReturnsFalse()
    {
        "".IsPangram().Should().BeFalse();
    }

    [Fact]
    public void IsPangram_Null_Throws()
    {
        string? v = null;
        var act = () => v!.IsPangram();
        act.Should().Throw<ArgumentNullException>();
    }

    #endregion

    #region PhoneticKey

    [Fact]
    public void PhoneticKey_Similar_ReturnsSameKey()
    {
        "Kalle".PhoneticKey().Should().Be("Kålle".PhoneticKey());
    }

    [Fact]
    public void PhoneticKey_Different_ReturnsDifferentKey()
    {
        "Kalle".PhoneticKey().Should().NotBe("Anna".PhoneticKey());
    }

    [Fact]
    public void PhoneticKey_Empty_ReturnsEmpty()
    {
        "".PhoneticKey().Should().Be("");
    }

    [Fact]
    public void PhoneticKey_AlwaysFourChars()
    {
        "a".PhoneticKey().Length.Should().Be(4);
        "hello".PhoneticKey().Length.Should().Be(4);
        "abcdefghij".PhoneticKey().Length.Should().Be(4);
    }

    [Fact]
    public void PhoneticKey_Null_Throws()
    {
        string? v = null;
        var act = () => v!.PhoneticKey();
        act.Should().Throw<ArgumentNullException>();
    }

    #endregion

    #region IsEmail

    [Fact]
    public void IsEmail_Valid_ReturnsTrue()
    {
        "hej@example.com".IsEmail().Should().BeTrue();
    }

    [Fact]
    public void IsEmail_NoAtSign_ReturnsFalse()
    {
        "notanemail".IsEmail().Should().BeFalse();
    }

    [Fact]
    public void IsEmail_NoDomain_ReturnsFalse()
    {
        "user@".IsEmail().Should().BeFalse();
    }

    [Fact]
    public void IsEmail_Empty_ReturnsFalse()
    {
        "".IsEmail().Should().BeFalse();
    }

    [Fact]
    public void IsEmail_Null_Throws()
    {
        string? v = null;
        var act = () => v!.IsEmail();
        act.Should().Throw<ArgumentNullException>();
    }

    #endregion

    #region IsUrl

    [Fact]
    public void IsUrl_ValidHttp_ReturnsTrue()
    {
        "https://example.com".IsUrl().Should().BeTrue();
    }

    [Fact]
    public void IsUrl_ValidFtp_ReturnsTrue()
    {
        "ftp://files.example.com".IsUrl().Should().BeTrue();
    }

    [Fact]
    public void IsUrl_NoProtocol_ReturnsFalse()
    {
        "example.com".IsUrl().Should().BeFalse();
    }

    [Fact]
    public void IsUrl_Empty_ReturnsFalse()
    {
        "".IsUrl().Should().BeFalse();
    }

    [Fact]
    public void IsUrl_Null_Throws()
    {
        string? v = null;
        var act = () => v!.IsUrl();
        act.Should().Throw<ArgumentNullException>();
    }

    #endregion

    #region IsIpAddress

    [Fact]
    public void IsIpAddress_ValidV4_ReturnsTrue()
    {
        "192.168.1.1".IsIpAddress().Should().BeTrue();
    }

    [Fact]
    public void IsIpAddress_InvalidV4_ReturnsFalse()
    {
        "not.an.ip".IsIpAddress().Should().BeFalse();
    }

    [Fact]
    public void IsIpAddress_NotIp_ReturnsFalse()
    {
        "hello".IsIpAddress().Should().BeFalse();
    }

    [Fact]
    public void IsIpAddress_Empty_ReturnsFalse()
    {
        "".IsIpAddress().Should().BeFalse();
    }

    [Fact]
    public void IsIpAddress_Null_Throws()
    {
        string? v = null;
        var act = () => v!.IsIpAddress();
        act.Should().Throw<ArgumentNullException>();
    }

    #endregion

    #region DetectStringType

    [Fact]
    public void DetectStringType_Email()
    {
        "hej@example.com".DetectStringType().Should().Be(StringType.Email);
    }

    [Fact]
    public void DetectStringType_Url()
    {
        "https://example.com".DetectStringType().Should().Be(StringType.Url);
    }

    [Fact]
    public void DetectStringType_IpAddress()
    {
        "192.168.1.1".DetectStringType().Should().Be(StringType.IpAddress);
    }

    [Fact]
    public void DetectStringType_Number()
    {
        "42".DetectStringType().Should().Be(StringType.Number);
    }

    [Fact]
    public void DetectStringType_Decimal()
    {
        "3.14".DetectStringType().Should().Be(StringType.Number);
    }

    [Fact]
    public void DetectStringType_Guid()
    {
        "550e8400-e29b-41d4-a716-446655440000".DetectStringType().Should().Be(StringType.Guid);
    }

    [Fact]
    public void DetectStringType_Json()
    {
        "{\"key\":\"value\"}".DetectStringType().Should().Be(StringType.Json);
    }

    [Fact]
    public void DetectStringType_Xml()
    {
        "<root />".DetectStringType().Should().Be(StringType.Xml);
    }

    [Fact]
    public void DetectStringType_FilePath()
    {
        "/home/user/file.txt".DetectStringType().Should().Be(StringType.FilePath);
    }

    [Fact]
    public void DetectStringType_WindowsPath()
    {
        "C:\\Users\\test".DetectStringType().Should().Be(StringType.WindowsPath);
    }

    [Fact]
    public void DetectStringType_UncPath()
    {
        "\\\\server\\share".DetectStringType().Should().Be(StringType.WindowsPath);
    }

    [Fact]
    public void DetectStringType_MacAddress()
    {
        "AA:BB:CC:DD:EE:FF".DetectStringType().Should().Be(StringType.MacAddress);
    }

    [Fact]
    public void DetectStringType_Sql()
    {
        "SELECT * FROM users".DetectStringType().Should().Be(StringType.Sql);
    }

    [Fact]
    public void DetectStringType_SubnetMask()
    {
        "255.255.255.0".DetectStringType().Should().Be(StringType.SubnetMask);
    }

    [Fact]
    public void DetectStringType_PlainText()
    {
        "hello world".DetectStringType().Should().Be(StringType.PlainText);
    }

    [Fact]
    public void DetectStringType_Empty_ReturnsPlainText()
    {
        "".DetectStringType().Should().Be(StringType.PlainText);
    }

    [Fact]
    public void DetectStringType_WhitespaceOnly_ReturnsPlainText()
    {
        "   ".DetectStringType().Should().Be(StringType.PlainText);
    }

    [Fact]
    public void DetectStringType_Null_Throws()
    {
        string? v = null;
        var act = () => v!.DetectStringType();
        act.Should().Throw<ArgumentNullException>();
    }

    #endregion
}
