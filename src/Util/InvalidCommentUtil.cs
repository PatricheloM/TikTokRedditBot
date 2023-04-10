namespace TiktokBot.Util
{
    static class InvalidCommentUtil
    {
        private const string DELETED_OR_NULL = "[deleted]";

        public static string GetDeletedOrNullConst()
        {
            return DELETED_OR_NULL;
        }
    }
}
