namespace WKExample.Domain.Consts
{
    public static class GenderEnum
    {
        public const string Male = nameof(Male);
        public const string Female = nameof(Female);

        public static bool IsValid(string gender)
        {
            return gender is Male || gender is Female;
        }
    }
}
