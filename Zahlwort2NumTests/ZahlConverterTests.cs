using System;
using Xunit;
using Zahlwort2Num;

namespace Zahlwort2NumTests
{
    public class ZahlConverterTests
    {
        [Theory]
        [InlineData("null", 0)]
        [InlineData("eins", 1)]
        [InlineData("zwei", 2)]
        [InlineData("drei", 3)]
        [InlineData("vier", 4)]
        [InlineData("fünf", 5)]
        [InlineData("sechs", 6)]
        [InlineData("sieben", 7)]
        [InlineData("acht", 8)]
        [InlineData("neun", 9)]
        [InlineData("zehn", 10)]
        [InlineData("elf", 11)]
        [InlineData("zwölf", 12)]
        [InlineData("dreizehn", 13)]
        [InlineData("vierzehn", 14)]
        [InlineData("fünfzehn", 15)]
        [InlineData("sechzehn", 16)]
        [InlineData("siebzehn", 17)]
        [InlineData("achtzehn", 18)]
        [InlineData("neunzehn", 19)]
        [InlineData("zwanzig", 20)]
        [InlineData("einundzwanzig", 21)]
        [InlineData("zweiundzwanzig", 22)]
        [InlineData("dreißig", 30)]
        [InlineData("vierzig", 40)]
        [InlineData("fünfzig", 50)]
        [InlineData("sechzig", 60)]
        [InlineData("siebzig", 70)]
        [InlineData("achtzig", 80)]
        [InlineData("neunzig", 90)]
        [InlineData("hundert", 100)]
        [InlineData("einhundert", 100)]
        [InlineData("zweihundert", 200)]
        [InlineData("tausend", 1000)]
        [InlineData("zweitausend", 2000)]
        [InlineData("zehntausend", 10000)]
        [InlineData("eine million", 1000000)]
        [InlineData("eine milliarde", 1000000000)]
        [InlineData("minus fünf", -5)]
        [InlineData("minus hundert", -100)]
        public void Convert_ValidGermanNumberWords_ReturnsExpectedResults(string number, int expected)
        {
            // Act
            int result = ZahlConverter.Convert(number);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("dreiundzwanzigtausendfünfhundertsechzig", 23560)]
        [InlineData("einhundertachtundzwanzig", 128)]
        [InlineData("neunundneunzigtausendneunhundertneunundneunzig", 99999)]
        public void Convert_ValidComplexGermanNumberWords_ReturnsExpectedResults(string number, int expected)
        {
            // Act
            int result = ZahlConverter.Convert(number);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("invalid")]
        [InlineData("12345")]
        [InlineData("")]
        public void Convert_InvalidGermanNumberWords_ThrowsArgumentException(string number)
        {
            // Act & Assert
            Assert.Throws<ArgumentException>(() => ZahlConverter.Convert(number));
        }
    }

}