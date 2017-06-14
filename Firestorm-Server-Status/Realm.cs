using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Firestorm_Server_Status
{
    class Realm
    {
        private RealmStatus _status;
        private string _onlineSince;
        private string _rate;
        private string _name;
        private int _players;        
        private string _imgURL;
        private RealmType _type;

        public string OnlineSince { get => _onlineSince; set => _onlineSince = value; }
        public string Rate { get => _rate; set => _rate = value; }
        public string Name { get => _name; set => _name = value; }
        public int Players { get => _players; set => _players = value; }
        public string ImgURL { get => _imgURL; set => _imgURL = value; }
        public RealmStatus Status { get => _status; set => _status = value; }
        public RealmType Type { get => _type; set => _type = value; }

        public Realm(RealmStatus status, string onlineSince, string rate, string name, int players, string imgURL, RealmType type)
        {
            Status = status;
            OnlineSince = onlineSince;
            Rate = rate;
            Name = name;
            Players = players;
            ImgURL = imgURL;
            Type = type;
        }

        public enum RealmStatus
        {
            Online,
            Offline
        }

        public enum RealmType
        {
            PvE,
            PvP,
            None
        }
    }
}
