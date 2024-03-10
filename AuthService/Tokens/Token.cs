using System.Runtime.Serialization;

namespace AuthService.Tokens
{
    [DataContract]
    public class Token
    {
        [DataMember]
        public Guid SessionId { get; set; }

        [DataMember]
        public Guid UserId { get; set; }

        [DataMember]
        public long ExpirationTicks { get; private set; }

        [DataMember]
        public string AuthToken { get; set; }
        
        [DataMember]
        public int RoleId { get; set; }

        public void SetExpiration()
        {
            ExpirationTicks = DateTime.UtcNow.AddDays(7).Ticks;
        }

        public bool IsExpired()
        {
            return DateTime.UtcNow.Ticks > ExpirationTicks;
        }
    }
}