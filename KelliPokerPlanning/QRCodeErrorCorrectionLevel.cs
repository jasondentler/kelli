namespace KelliPokerPlanning
{
    public enum QRCodeErrorCorrectionLevel
    {
        /// <summary>Recovers from up to 7% erroneous data.</summary>
        Low,
        /// <summary>Recovers from up to 15% erroneous data.</summary>
        Medium,
        /// <summary>Recovers from up to 25% erroneous data.</summary>
        QuiteGood,
        /// <summary>Recovers from up to 30% erroneous data.</summary>
        High
    }
}