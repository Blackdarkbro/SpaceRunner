public static class ScoreExtension
{
    public static string AddApostrophe(this int score)
    {
        if (score > 999)
        {
            var finalString = "";
            var arr = score.ToString().ToCharArray();

            for (var i = 0; i < arr.Length; i++)
            {
                finalString += arr[i];
                if (arr.Length - 4 == i) finalString += "'";
            }

            return finalString;
        }

        return score.ToString();
    }

    public static string AddApostrophe(this float score)
    {
        if (score > 999)
        {
            var finalString = "";
            var arr = (score / 1000).ToString().ToCharArray();

            for (var i = 0; i < arr.Length; i++)
            {
                finalString += arr[i];
                if (arr.Length - 4 == i) finalString += "'";
            }

            return finalString;
        }

        return score.ToString();
    }

    public static string AddApostrophe(this double score)
    {
        if (score > 999)
        {
            var finalString = "";
            var arr = (score / 1000).ToString().ToCharArray();

            for (var i = 0; i < arr.Length; i++)
            {
                finalString += arr[i];
                if (arr.Length - 4 == i) finalString += "'";
            }

            return finalString;
        }

        return score.ToString();
    }
}