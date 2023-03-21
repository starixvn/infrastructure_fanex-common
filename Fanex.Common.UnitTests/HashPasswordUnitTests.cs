namespace Fanex.Common.UnitTests
{
    public class HashPasswordUnitTests
    {
        [Theory]
        [InlineData("", "A5176855768898427B81003799C881")]
        [InlineData("1234aa", "56E8A2D78170E87C928E480797CC73")]
        [InlineData("123456", "A51743627D9238C3997BABD76D7D75")]
        [InlineData("DoRemon@123*~|}", "40C8462177293754D48085577DD975")]
        [InlineData("TenCharsXY", "5CE7E2D7810B38E76B8A1758976C8F")]
        [InlineData("TwentyChars123456789", "372896207662076C5E857A38A47986")]
        [InlineData("ThirtyChars1234567890123456789", "468843718746882ABD743627E7CC73")]
        [InlineData("FourtyChars12345678901234567890123456789", "27B82C2680424797A389A517488A8A")]
        public void Hash_Password_ReturnCorrectHash(string password, string expected)
        {
            var actual = PasswordHash.Hash(password);

            Assert.Equal(expected, actual);
        }
    }
}