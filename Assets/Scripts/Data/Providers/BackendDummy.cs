using System.Collections.Generic;
using Other;

namespace Data.Providers
{
    public class BackendDummy : Singleton<BackendDummy>
    {
        private BackendProvider bp;

        public List<WheelData> wheelData;

        public void SendRequest(BackendProvider bp, string URL)
        {
            this.bp = bp;
            bp.Response(wheelData);
        }
    }
}