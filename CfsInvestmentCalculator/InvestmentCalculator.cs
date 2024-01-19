using System.ComponentModel.DataAnnotations;

namespace CfsInvestmentCalculator
{
    public class Investment
    {
        public required string InvestmentOption { get; set; }
        public decimal InvestmentPercentage { get; set; }
        public decimal Fees { get; set; }
        public decimal RIOAmt { get; set; }


    }
}
