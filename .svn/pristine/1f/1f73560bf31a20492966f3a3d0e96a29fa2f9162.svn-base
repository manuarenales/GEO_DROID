using SQLite;
using System.Runtime.Serialization;


namespace GeoDroid.Data
{
    [DataContract]
    public class Configuration
    {
        [PrimaryKey, Unique, DataMember, AutoIncrement]
        public int id { get; set; }
        [DataMember]
        public int unitNumber { get; set; }
        [DataMember, MaxLength(20)]
        public string password { get; set; }
        [DataMember, MaxLength(15)]
        public string ip { get; set; }
        [DataMember, MaxLength(5)]
        public int port { get; set; }
        [DataMember, MaxLength(20)]
        public string printerMAC { get; set; }
        [DataMember, MaxLength(1)]
        public string? syncToken { get; set; }
        [DataMember, MaxLength(1)] //Used to store license token until all the process is finished then it copied to syncToken
        public string? tempSyncToken { get; set; }
        [DataMember]
        public bool printSignature { get; set; }
        [DataMember]
        public bool printTwoCopies { get; set; }
        [DataMember]
        public int idPersonal { get; set; }
        [DataMember]
        public string userName { get; set; }
        //Esta fecha se usa para saber si el cliente está dentro 
        [DataMember]
        public DateTime serverDate { get; set; }
        [DataMember]
        public bool syncCompressionEnabled { get; set; }
    }

    [DataContract(Name = "Configuration")]
    public class Configuration_V1
    {
        [PrimaryKey, Unique,DataMember]

        public int id { get; set; }
        [DataMember]
        public int unitNumber { get; set; }
        [DataMember, MaxLength(20)]
        public string password { get; set; }
        [DataMember, MaxLength(15)]
        public string ip { get; set; }
        [DataMember, MaxLength(5)]
        public int port { get; set; }
        [DataMember, MaxLength(512)]
        public string syncToken { get; set; }
        [DataMember]
        public bool printSignature { get; set; }
        [DataMember]
        public bool printTwoCopies { get; set; }
    }

    [DataContract(Name = "Configuration")]
    public class Configuration_V2
    {

        [PrimaryKey, Unique,DataMember]

        public int id { get; set; }
        [DataMember]
        public int unitNumber { get; set; }
        [DataMember, MaxLength(20)]
        public string password { get; set; }
        [DataMember(Name = "ip"), MaxLength(15)]
        public string ip2 { get; set; }
        [DataMember, MaxLength(5)]
        public int port { get; set; }
        [DataMember, MaxLength(512)]
        public string syncToken { get; set; }
        [DataMember]
        public bool printSignature { get; set; }
        [DataMember]
        public bool printTwoCopies { get; set; }
    }

}
