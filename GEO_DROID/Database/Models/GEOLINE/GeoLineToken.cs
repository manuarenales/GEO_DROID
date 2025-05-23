

namespace GeoDroid.Data.GEOLINE
{
    public class TokenData
    {
        public string RequertAt { get; set; }
        public double ExpiresIn { get; set; }
        public string TokeyType { get; set; }
        public string AccessToken { get; set; }
    }

    public class GeolineToken
    {
        public int State { get; set; }
        public object Msg { get; set; }
        public TokenData Data { get; set; }
    }
}
