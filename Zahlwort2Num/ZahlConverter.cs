namespace Zahlwort2Num
{
    public class ZahlConverter
    {
        private static readonly Dictionary<string, int> CONST_NUMS = new Dictionary<string, int>
        {
            {"", 0},
            {"null", 0},
            {"ein", 1},
            {"eins", 1},
            {"eine", 1},
            {"er", 1},
            {"zwei", 2},
            {"zwo", 2},
            {"drei", 3},
            {"drit", 3},
            {"vier", 4},
            {"fünf", 5},
            {"sechs", 6},
            {"sieben", 7},
            {"sieb", 7},
            {"acht", 8},
            {"neun", 9},
            {"zehn", 10},
            {"elf", 11},
            {"zwölf", 12},
            {"dreizehn", 13},
            {"vierzehn", 14},
            {"fünfzehn", 15},
            {"sechzehn", 16},
            {"siebzehn", 17},
            {"achtzehn", 18},
            {"neunzehn", 19},
            {"zwanzig", 20},
            {"dreißig", 30},
            {"dreissig", 30},
            {"vierzig", 40},
            {"fünfzig", 50},
            {"sechzig", 60},
            {"siebzig", 70},
            {"achtzig", 80},
            {"neunzig", 90}
        };

        private static readonly string[] ORD_SUFFICES = { "te", "ter", "tes", "tem", "ten" };
        private static readonly string[] SCALES = { "million", "milliarde", "billion", "billiarde", "trillion", "trilliarde", "quadrillion", "quadrilliarde" };
        private static readonly int MAX_SC = SCALES.Length;

        private string number;
        private Func<string, int> convt2;
        private Func<string, int> convh2;
        private Func<string, int> convu2;

        public ZahlConverter(string number)
        {
            this.number = number.ToLower().Trim();
            this.convt2 = (num) => Mult(num, "tausend", 1000, convh2);
            this.convh2 = (num) => Mult(num, "hundert", 100, convu2);
            this.convu2 = (num) => Mult(num, "und", 1, word => CONST_NUMS[word]);
        }

        public int Convert()
        {
            if (string.IsNullOrEmpty(number))
                throw new ArgumentException("Input cannot be null or empty.");

            if (number.StartsWith("minus"))
            {
                var numWithoutMinus = number.Replace("minus ", "");
                var res = OrdBn(numWithoutMinus);
                return -res;
            }
            return OrdBn(number);
        }

        private int Mult(string number, string splitter, int factor, Func<string, int> func)
        {
            var split = number.Split(new[] { splitter }, StringSplitOptions.None);
            if (split.Length == 2)
            {
                if (splitter != "und" && string.IsNullOrEmpty(split[0]))
                    return factor + func(split[1]);
                return func(split[0]) * factor + func(split[1]);
            }
            else if (split.Length == 1)
            {
                return func(split[0]);
            }
            else
            {
                throw new ArgumentException("Given input cannot be properly parsed. Please double check if it has proper structure.");
            }
        }

        private int ConvOrd(string number)
        {
            if (ORD_SUFFICES.Any(suffix => number.EndsWith(suffix)))
            {
                string validNr;
                if (number.EndsWith("te"))
                {
                    validNr = number.EndsWith("ste") ? number[..^3] : number[..^2];
                }
                else
                {
                    validNr = number.EndsWith("ste") ? number[..^4] : number[..^3];
                }
                return int.Parse(Convt2(validNr).ToString() + '.');
            }
            return Convt2(number);
        }

        private int OrdWithBN(string number, int idx)
        {
            if (number.Split(' ').Length == 1 || idx > MAX_SC - 1)
                return ConvOrd(number);

            var split = number.Split(new[] { SCALES[MAX_SC - idx - 1] }, StringSplitOptions.None);
            if (split.Length > 1)
            {
                var sp0 = split[0].Trim();
                var sp1 = split[1].Trim();

                if (split[1].StartsWith("en"))
                    sp1 = split[1][3..];
                else if (split[1].StartsWith("n"))
                    sp1 = split[1][2..];

                return ConvOrd(sp0) * (int)Math.Pow(10, (MAX_SC - idx + 1) * 3) + OrdWithBN(sp1, idx + 1);
            }
            return OrdWithBN(number, idx + 1);
        }

        private int OrdBn(string number)
        {
            return OrdWithBN(number, 0);
        }

        private int Convt2(string number)
        {
            return Mult(number, "tausend", 1000, Convh2);
        }

        private int Convh2(string number)
        {
            return Mult(number, "hundert", 100, Convu2);
        }

        private int Convu2(string number)
        {
            return Mult(number, "und", 1, word =>
            {
                if (!CONST_NUMS.TryGetValue(word, out int value))
                    throw new ArgumentException($"Invalid word: {word}");
                return value;
            });
        }

        public static int Convert(string number)
        {
            var converter = new ZahlConverter(number);
            return converter.Convert();
        }
    }
}