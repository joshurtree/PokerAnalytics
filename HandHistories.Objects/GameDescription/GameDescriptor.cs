using System;
using System.Runtime.Serialization;

namespace HandHistories.Objects.GameDescription
{    
    [Serializable]
    [DataContract()]
    public class GameDescriptor
    {
        public GameDescriptor() : this(PokerFormat.Unknown, 
                                       SiteName.Values.Unknown,
                                       GameType.Unknown,
                                       Limit.AllLimit(),
                                       TableType.FromTableTypeDescriptions(),
                                       SeatType.AllSeatType())         
        {

        }

        public GameDescriptor(SiteName siteName,
                              GameType gameType,
                              Limit limit,
                              TableType tableType,
                              SeatType seatType)
            : this(PokerFormat.CashGame, siteName, gameType, limit, tableType, seatType)         
        {

        }

        public GameDescriptor(SiteName siteName,
                              GameType gameType,
                              string tournamentId,
                              Limit limit,
                              TableType tableType,
                              SeatType seatType)
            : this(PokerFormat.SitAndGo, siteName, gameType, tournamentId, limit, tableType, seatType)
        {
            
        }

        public GameDescriptor(PokerFormat pokerFormat,
                              SiteName siteName,
                              GameType gameType,
                              Limit limit,
                              TableType tableType,
                              SeatType seatType)
        {
            PokerFormat = pokerFormat;
            Site = siteName;
            GameType = gameType;
            Limit = limit;
            TableType = tableType;
            SeatType = seatType;
        }

        public GameDescriptor(PokerFormat pokerFormat,
                              SiteName siteName,
                              GameType gameType,
                              string tournamentId,
                              Limit limit,
                              TableType tableType,
                              SeatType seatType)
        {
            PokerFormat = pokerFormat;
            Site = siteName;
            TournamentId = tournamentId;
            Limit = limit;
            GameType = gameType;
            TableType = tableType;
            SeatType = seatType;
        }

        [DataMember]
        public PokerFormat PokerFormat { get; set; }

        [DataMember]
        public SiteName Site { get; set; }

        [DataMember]        
        public GameType GameType { get; set; }

        [DataMember]
        public Limit Limit { get; set; }
         
        [DataMember]
        public string TournamentId { get; set; }

        [DataMember]   
        public SeatType SeatType { get; set; }

        [DataMember]
        public TableType TableType { get; set; }      
        
        [DataMember]        
        public TournamentSummary TournamentSummary { get; set; }

        public override bool Equals(object obj)
        {
            GameDescriptor descriptor = obj as GameDescriptor;
            if (descriptor == null) return false;
            return (descriptor.ToString().Equals(this.ToString()));
        }

        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }

        public override string ToString()
        {
            return string.Format("{0}.{1}.{2}.{3}.{4}", Site, GameType, Limit, TableType, SeatType);
        }
    }
}
