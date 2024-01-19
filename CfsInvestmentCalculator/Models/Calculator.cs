namespace CfsInvestmentCalculator.Models
{
    public class Calculator
    {
        public decimal InvestmentPercentage { get; set; }
        public int InvestmentAmount { get; set; }
        public int TotalFee { get; set; }
        public int TotalReturn { get; set; }

        public required List<Calculator> InvestmentOption { get; set; }
        public int TotalInvestment { get; set; }

        public String? InvestmentType { get; set; }

        public int InvestmentPerc { get; set; }
        public decimal RIOAmt { get; internal set; }
        public decimal Fees { get; internal set; }
    }

}
